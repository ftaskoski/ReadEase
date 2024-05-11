using Books.Services;
using Dapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReadEase_C_.Helpers;
using ReadEase_C_.Services;
using System.Data.SqlClient;
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

        public AdminController(IConfiguration configuration, UserService service, BookService bookService)
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

        [HttpGet("paginatedusers")]
        public IEnumerable<FormModel> GePaginatedtUsers(int pageNumber = 1, int pageSize = 10) {
            string str = _configuration.GetConnectionString("DefaultConnection");
            var connection = new SqlConnection(str);
            int startIndex = (pageNumber - 1) * pageSize;

            string query = "SELECT * FROM USERS WHERE Role = 'User' ORDER BY Id OFFSET @startIndex ROWS FETCH NEXT @pageSize ROWS ONLY";

            return connection.Query<FormModel>(query, new {startIndex,pageSize});
        }

        [HttpDelete("deletecategories")]
        public void DeleteCategory(List<int> categories)
        {
            string str = _configuration.GetConnectionString("DefaultConnection") ?? "";
            var connection = new SqlConnection(str);

            string deleteQuery = "DELETE FROM CATEGORIES WHERE CategoryId IN @categories";

            connection.Execute(deleteQuery,new {categories});
            

        }




    }
}
