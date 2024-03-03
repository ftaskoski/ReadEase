using Microsoft.AspNetCore.Mvc;
using Dapper;
using WebApplication1.Models;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Books.Services;
using System.Security.Claims;
using System.Data.SqlClient;
using ReadEase_C_.Models;
using System.Data;

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
        private int UserId
        {
            get
            {
                return Int32.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            }
        }
        [HttpGet("downloadbooks")]
        public IActionResult DownloadBooks()
        {
            
            var books = _bookService.GetAllBooksForUser(UserId);

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

        [HttpGet("getbooks")]
        public IActionResult GetBooks( int pageNumber = 1, int pageSize = 10)
        {
           var books =_bookService.GetPaginatedBooks(UserId,pageNumber,pageSize);
           return Ok(books);
        }

        [HttpGet("searchbooksall")]
        public IActionResult SearchBooksAll( string search)
        {
          var searchedBooks = _bookService.GetAllBooksFromSearch(UserId,search);
          return Ok(searchedBooks);
        }

        [HttpGet("searchbooks")]
        public IActionResult SearchBooks( string search,int pageNumber,int pageSize=10)
        {
            var book = _bookService.GetPaginatedBooksFromSearch(UserId,search,pageNumber,pageSize);
            return Ok(book);
        }

        [HttpPost("insertbook")]
        public IActionResult InsertBook([FromBody] BookModel model)
        {
            _bookService.InsertBook(model,UserId);
            return Ok("Book has been added");
        }

        [HttpGet("getallbooks")]
        public IEnumerable<BookModel> GetAllBooks()
        {
            return _bookService.GetAllBooksForUser(UserId);

        }

        [HttpDelete("deletebook/{id}")]
        public ActionResult DeleteBook(int id)
        {
            _bookService.DeleteBook(id);
            return Ok();
        }



        [HttpGet("searchandcategoryall")]
        public IEnumerable<BookModel> GetSearchAndCategoryAll(string? search = null, [FromQuery] string? categories = null)
        {
            List<int>? categoriesList = null;
            if (!string.IsNullOrEmpty(categories))
            {
                categoriesList = categories.Split(',').Select(Int32.Parse).ToList();
            }
            return _bookService.SearchAndCategoryAll(UserId, search, categoriesList);
        }

        [HttpGet("searchandcategory")]
        public IEnumerable<BookModel> GetSearchAndCategory(string? search = null, [FromQuery] string? categories = null, int pageNumber = 1, int pageSize = 10)
        {
            List<int>? categoriesList = null;
            if (!string.IsNullOrEmpty(categories))
            {
                categoriesList = categories.Split(',').Select(Int32.Parse).ToList();
            }
            return _bookService.SearchAndCategory(UserId, search, categoriesList, pageNumber, pageSize);
        }
        private SqlConnection GetSqlConnection()
        {
            string connectionString = _configuration.GetConnectionString("DefaultConnection");
            return new SqlConnection(connectionString);
        }

        [HttpPost("updatebook")]
        public IActionResult UpdateBook(UpdateBook book)
        {
           _bookService.UpdateBook(book);
            return Ok("Updated");
        }

    }

}


