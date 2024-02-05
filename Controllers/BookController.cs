using Microsoft.AspNetCore.Mvc;
using Dapper;
using WebApplication1.Models;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Books.Services;
using System.Security.Claims;


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
        [HttpGet("downloadbooks/{id}")]
        public IActionResult DownloadBooks(int id)
        {
            
            var books = _bookService.GetAllBooksForUser(id);

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
            _bookService.InsertBook(model,id);
            return Ok("Book has been added");
        }

        [HttpGet("getallbooks/{id}")]
        public IEnumerable<BookModel> GetAllBooks(int id)
        {
            Console.WriteLine(UserId);
            return _bookService.GetAllBooksForUser(id);

        }

        [HttpDelete("deletebook/{id}")]
        public ActionResult DeleteBook(int id)
        {
            _bookService.DeleteBook(id);
            return Ok();
        }
    }
}
