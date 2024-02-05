using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Dapper;
using WebApplication1.Models;
using System.Data.SqlClient;
using System.Text;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using ReadEase_C_.Models;
using Books.Services;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("api")]
    [Authorize]

    public class BooksController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly BookService _bookService;

        public BooksController(IConfiguration configuration, BookService bookService)
        {
            _configuration = configuration;
            _bookService = bookService;
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
           var books =_bookService.GetPaginatedBooks(id,pageNumber);
            return Ok(books);
        }

        [HttpGet("searchbooksall/{id}")]
        public IActionResult SearchBooksAll(int id, string search)
        {
          var searchedBooks = _bookService.GetAllBooksFromSearch(id,search);
            return Ok(searchedBooks);
        }

        [HttpGet("searchbooks/{id}")]
        public IActionResult SearchBooks(int id, string search,int pageNumber)
        {
         
            var book = _bookService.GetPaginatedBooksFromSearch(id,search,pageNumber);
            return Ok(book);
        }

        [HttpPost("insertbook/{id}")]
        public IActionResult InsertBook([FromBody] BookModel model, int id)
        {


            string insertQuery = "INSERT INTO Books (Title, Author, UserId, CategoryId) VALUES (@Title, @Author, @UserId, @CategoryId);";
            Execute(insertQuery, new { model.Title, Author = model.Author, UserId = id, CategoryId = model.CategoryId });

            return Ok("Book has been added");
        }

        [HttpGet("getallbooks/{id}")]
        public IEnumerable<BookModel> GetAllBooks(int id)
        {
            return _bookService.GetAllBooksForUser(id);

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
