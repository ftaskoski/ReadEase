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
using ReadEase_C_.Services;
using ReadEase_C_.Interface;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("api")]
    [Authorize]

    public class BooksController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IBookService _bookService;
        private readonly IConnectionService _connectionService;
        public BooksController(IConfiguration configuration, IBookService bookService, IConnectionService connectionService)
        {
            _configuration = configuration;
            _bookService = bookService;
            _connectionService = connectionService;
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

        [HttpGet("downloadbook/{bookId}")]
        public IActionResult DownloadBook(int bookId)
        {

            var books = _bookService.GetSingleBook(bookId);

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
        public IActionResult GetBooks(int pageNumber = 1, int pageSize = 10)
        {
            var books = _bookService.GetPaginatedBooks(UserId, pageNumber, pageSize);
            var connection = _connectionService.GetConnection();
            int offset = (pageNumber - 1) * pageSize;
            string str = "SELECT COUNT(*) FROM BOOKS WHERE UserId=@id"; // Define the SQL query
            int totalCount = connection.QueryFirstOrDefault<int>(str, new { id = UserId }); // Pass parameters separately
            int startBookIndex = offset + 1;
            int endBookIndex = Math.Min(offset + pageSize, totalCount);
            return Ok(new
            {
                Books = books,
                Range = $"Displaying books {startBookIndex}-{endBookIndex} out of {totalCount}"
            });
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
        public IEnumerable<BookModel> GetSearchAndCategoryAll(string? search = null, string? searchTitle = null, [FromQuery] string? categories = null)
        {
            // createing a list for categories because the query needs a list/arr, but we are sending the categorias via query string
            List<int>? categoriesList = null;
            if (!string.IsNullOrEmpty(categories))
            {
                categoriesList = categories.Split(',').Select(Int32.Parse).ToList();
            }
            return _bookService.SearchAndCategoryAll(UserId, search, searchTitle, categoriesList);
        }

        [HttpGet("searchandcategory")]
        public IActionResult GetSearchAndCategory(string? search = null, string? searchTitle = null, [FromQuery] string? categories = null, int pageNumber = 1, int pageSize = 10)
        {
            List<int>? categoriesList = null;
            if (!string.IsNullOrEmpty(categories))
            {
                categoriesList = categories.Split(',').Select(Int32.Parse).ToList();
            }

            var (books, totalCount) = _bookService.SearchAndCategory(UserId, search, searchTitle, categoriesList, pageNumber, pageSize);

            int offset = (pageNumber - 1) * pageSize;
            int startBookIndex = offset + 1;
            int endBookIndex = Math.Min(offset + pageSize, totalCount);

            return Ok(new
            {
                Books = books,
                Range = $"Displaying books {startBookIndex}-{endBookIndex} out of {totalCount}"
            });
        }



        [HttpPost("updatebook")]
        public IActionResult UpdateBook(UpdateBook book)
        {
           _bookService.UpdateBook(book);
            return Ok("Updated");
        }

    }

}


