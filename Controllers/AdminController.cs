using Dapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReadEase_C_.Interface;
using ReadEase_C_.Models;
using WebApplication1.Models;

namespace ReadEase_C_.Controllers
{
    [ApiController]
    [Route("api")]
    [Authorize(Roles = "Admin")]
    public class AdminController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IBookService _bookService;
        private readonly IConnectionService _connectionService;

        public AdminController(IUserService service, IBookService bookService, IConnectionService connectionService)
        {
            _userService = service;
            _bookService = bookService;
            _connectionService = connectionService;
        }

        [HttpGet("users")]
        public IEnumerable<protectedUserModel> getUsers()
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
        public IEnumerable<protectedUserModel> GePaginatedtUsers(int pageNumber = 1, int pageSize = 10) {
            var connection = _connectionService.GetConnection();
            int startIndex = (pageNumber - 1) * pageSize;

            string query = "SELECT * FROM USERS WHERE Role = 'User' ORDER BY Id OFFSET @startIndex ROWS FETCH NEXT @pageSize ROWS ONLY";

            return connection.Query<protectedUserModel>(query, new {startIndex,pageSize});
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
        public IEnumerable<protectedUserModel> SearchedUsers(string search, int pageNumber = 1, int pageSize = 10)
        {
            var connection = _connectionService.GetConnection();
            int startIndex = (pageNumber - 1) * pageSize;
            string searchQuery = "SELECT * FROM USERS WHERE Username LIKE @search AND Role='User' ORDER BY Id OFFSET @startIndex ROWS FETCH NEXT @pageSize ROWS ONLY";
           return connection.Query<protectedUserModel>(searchQuery, new { search=$"{search}%",startIndex,pageSize });

        }


        [HttpGet("searchusersall")]
        public IEnumerable<protectedUserModel> SearchedUsersall(string search)
        {
            var connection = _connectionService.GetConnection();
            string searchQuery = "SELECT * FROM USERS WHERE Username LIKE @search AND Role='User'";
            return connection.Query<protectedUserModel>(searchQuery, new { search = $"{search}%",});

        }



    }
}
