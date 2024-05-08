using Books.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReadEase_C_.Helpers;
using ReadEase_C_.Services;
using WebApplication1.Models;

namespace ReadEase_C_.Controllers
{
    [ApiController]
    [Route("api")]
    [Authorize(Roles = "Admin")]
    public class AdminController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly UserService _userService;

        private readonly BookService _bookService;

        public AdminController(IConfiguration configuration, UserService service, HashingService hashingService, PhotoService photoService, Mail mail, BookService bookService)
        {
            _configuration = configuration;
            _userService = service;
            _bookService = bookService;
        }

        [HttpGet("users")]
        public IEnumerable<FormModel> getUsers()
        {
            return _userService.GetAllUsers();
        }


        [HttpDelete("delete/{id}")]
        public IActionResult DeleteUser(int id)
        {
            _bookService.DeleteBooksByUser(id);
            _userService.DeleteUser(id);
            return Ok();
        }
    }
}
