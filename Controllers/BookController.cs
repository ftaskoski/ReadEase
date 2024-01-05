using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Dapper;
using WebApplication1.Models;
using System.Text;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("api")]
    public class BooksController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public BooksController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet("getbooks/{id}")]
        public IActionResult DownloadBooks(int id)
        {
            string connectionString = _configuration.GetConnectionString("DefaultConnection");

            using (var connection = new SqlConnection(connectionString))
            {
                string selectQuery = "SELECT Title, Author FROM Books WHERE UserId=@Id;";

                // Use Query to retrieve multiple rows
                var books = connection.Query<BookModel>(selectQuery, new { Id = id });

                // Check if any books were found
                if (books != null && books.AsList().Count > 0)
                {
                    // Create a StringBuilder to build the plain text content
                    StringBuilder content = new StringBuilder();

                    // Append book titles and authors to the StringBuilder
                    foreach (var book in books)
                    {
                        content.AppendLine($"Title: {book.Title}, Author: {book.Author}");
                    }

                    // Return the plain text content as a FileResult
                    byte[] fileContents = Encoding.UTF8.GetBytes(content.ToString());
                    return File(fileContents, "text/plain", "books.txt");
                }
                else
                {
                    return NotFound(); // 404 Not Found if no books were found
                }
            }
        }


        [HttpPost("insertbook/{id}")]
        public async Task<IActionResult> InsertBook([FromBody] BookModel model,int id)
        {
            string connectionString = _configuration.GetConnectionString("DefaultConnection");

            using (var connection = new SqlConnection(connectionString))
            {
                Console.WriteLine(model.UserId);
                string insertQuery = "INSERT INTO Books (Title, Author, UserId) VALUES (@Title, @Author, @UserId);";
                await connection.ExecuteAsync(insertQuery, new { model.Title, Author = model.Author, UserId = id });

                return Ok("Book has been added");
            }
        }


        [HttpDelete("deletebook/{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {

            string connectionString = _configuration.GetConnectionString("DefaultConnection");
            using (var connection = new SqlConnection(connectionString))
            {
                string deleteQuery = "DELETE FROM Books WHERE bookId=@Id;";
                await connection.ExecuteAsync(deleteQuery, new { id });
                return Ok();

            }

        }

    }







}
