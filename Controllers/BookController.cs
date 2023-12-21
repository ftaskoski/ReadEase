using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Dapper;
using WebApplication1.Models;

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
        public IActionResult GetBooks(int id)
        {
            try
            {
                string connectionString = _configuration.GetConnectionString("DefaultConnection");

                using (var connection = new SqlConnection(connectionString))
                {
                    string selectQuery = "SELECT * FROM Books WHERE UserId=@Id;";

                    // Use Query to retrieve multiple rows
                    var books = connection.Query<BookModel>(selectQuery, new { Id = id });

                    // Check if any books were found
                    if (books != null && books.AsList().Count > 0)
                    {
                        return Ok(books); // 200 OK with the list of books
                    }
                    else
                    {
                        return NotFound(); // 404 Not Found if no books were found
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
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
