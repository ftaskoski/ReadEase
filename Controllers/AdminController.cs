using Books.Services;
using Dapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReadEase_C_.Helpers;
using ReadEase_C_.Models;
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
        private readonly ConnectionService _connectionService;

        public AdminController(IConfiguration configuration, UserService service, BookService bookService, ConnectionService connectionService)
        {
            _configuration = configuration;
            _userService = service;
            _bookService = bookService;
            _connectionService = connectionService;
        }

        [HttpGet("users")]
        public IEnumerable<UserModel> getUsers()
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
        public IEnumerable<UserModel> GePaginatedtUsers(int pageNumber = 1, int pageSize = 10) {
            var connection = _connectionService.GetConnection();
            int startIndex = (pageNumber - 1) * pageSize;

            string query = "SELECT * FROM USERS WHERE Role = 'User' ORDER BY Id OFFSET @startIndex ROWS FETCH NEXT @pageSize ROWS ONLY";

            return connection.Query<UserModel>(query, new {startIndex,pageSize});
        }

        [HttpDelete("deletecategories")]
        public void DeleteCategory(List<int> categories)
        {
            var connection = _connectionService.GetConnection();

            string deleteQuery = "DELETE FROM CATEGORIES WHERE CategoryId IN @categories";

            connection.Execute(deleteQuery,new {categories});
            

        }

        [HttpPost("insertcategory")]
        public void insertCategories(CategoriesModel category)
        {
            using var connection = _connectionService.GetConnection();
            string insertQuery = "INSERT INTO Categories (CategoryName) VALUES (@CategoryName)";
            connection.Execute(insertQuery, new { CategoryName = category.CategoryName });
        }


        [HttpGet("searchusers")]
        public IEnumerable<UserModel> SearchedUsers(string search, int pageNumber = 1, int pageSize = 10)
        {
            var connection = _connectionService.GetConnection();
            int startIndex = (pageNumber - 1) * pageSize;
            string searchQuery = "SELECT * FROM USERS WHERE Username LIKE @search AND Role='User' ORDER BY Id OFFSET @startIndex ROWS FETCH NEXT @pageSize ROWS ONLY";
           return connection.Query<UserModel>(searchQuery, new { search=$"{search}%",startIndex,pageSize });

        }


        [HttpGet("searchusersall")]
        public IEnumerable<UserModel> SearchedUsersall(string search)
        {
            var connection = _connectionService.GetConnection();
            string searchQuery = "SELECT * FROM USERS WHERE Username LIKE @search AND Role='User'";
            return connection.Query<UserModel>(searchQuery, new { search = $"{search}%",});

        }



    }
}
