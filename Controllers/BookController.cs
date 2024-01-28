using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Dapper;
using WebApplication1.Models;
using System.Data.SqlClient;
using System.Text;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("api")]
    [Authorize]

    public class BooksController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public BooksController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        private SqlConnection GetSqlConnection()
        {
            string connectionString = _configuration.GetConnectionString("DefaultConnection");
            return new SqlConnection(connectionString);
        }

        private IEnumerable<BookModel> QueryBooks(string query, object parameters = null)
        {
            using (var connection = GetSqlConnection())
            {
                return connection.Query<BookModel>(query, parameters);
            }
        }

        private void Execute(string query, object parameters = null)
        {
            using (var connection = GetSqlConnection())
            {
                connection.Execute(query, parameters);
            }
        }

        [HttpGet("downloadbooks/{id}")]
        public IActionResult DownloadBooks(int id)
        {
            string selectQuery = "SELECT Title, Author FROM Books WHERE UserId=@Id;";
            var books = QueryBooks(selectQuery, new { Id = id });

            if (books?.AsList().Count > 0)
            {
                StringBuilder content = new StringBuilder();
                foreach (var book in books)
                {
                    content.AppendLine($"Title: {book.Title}, Author: {book.Author}");
                }

                byte[] fileContents = Encoding.UTF8.GetBytes(content.ToString());
                return File(fileContents, "text/plain", "books.txt");
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet("getbooks/{id}")]
        public IActionResult GetBooks(int id, int pageNumber = 1, int pageSize = 10)
        {
            int startIndex = (pageNumber - 1) * pageSize;
            string getQuery = "SELECT * FROM BOOKS WHERE UserId=@Id ORDER BY BookId OFFSET @startIndex ROWS FETCH NEXT @pageSize ROWS ONLY;";
            var books = QueryBooks(getQuery, new { id = id, startIndex = startIndex, pageSize = pageSize });
            return Ok(books);
        }

        [HttpGet("searchbooksall/{id}")]
        public IActionResult SearchBooksAll(int id, string search)
        {
            string searchQuery = $"SELECT * FROM BOOKS WHERE UserId=@Id AND Author LIKE @Search";
            var book = QueryBooks(searchQuery, new { id = id, Search = $"{search}%" });
            return Ok(book);
        }

        [HttpGet("searchbooks/{id}")]
        public IActionResult SearchBooks(int id, string search, int pageNumber = 1, int pageSize = 10)
        {
            int startIndex = (pageNumber - 1) * pageSize;
            string searchQuery = "SELECT * FROM BOOKS WHERE UserId=@Id AND Author LIKE @Search ORDER BY BookId OFFSET @startIndex ROWS FETCH NEXT @pageSize ROWS ONLY;";
            var book = QueryBooks(searchQuery, new { id = id, Search = $"{search}%", startIndex = startIndex, pageSize = pageSize });
            return Ok(book);
        }

        [HttpPost("insertbook/{id}")]
        public IActionResult InsertBook([FromBody] BookModel model, int id)
        {
            string insertQuery = "INSERT INTO Books (Title, Author, UserId) VALUES (@Title, @Author, @UserId);";
            Execute(insertQuery, new { model.Title, Author = model.Author, UserId = id });

            return Ok("Book has been added");
        }

        [HttpGet("getallbooks/{id}")]
        public IEnumerable<BookModel> GetAllBooks(int id)
        {
            string getQuery = "SELECT * FROM BOOKS WHERE UserId=@Id";
            return QueryBooks(getQuery, new { Id = id });
        }

        [HttpDelete("deletebook/{id}")]
        public ActionResult DeleteBook(int id)
        {
            string deleteQuery = "DELETE FROM Books WHERE bookId=@id";
            Execute(deleteQuery, new { id = id });
            return Ok();
        }
    }
}
