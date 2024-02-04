using Dapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReadEase_C_.Models;
using System.Data.SqlClient;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("api")]
    [Authorize]
    public class Categories : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public Categories(IConfiguration configuration) 
        { 
        _configuration = configuration;
        
        }

        private SqlConnection GetSqlConnection()
        {
            string connectionString = _configuration.GetConnectionString("DefaultConnection");
            return new SqlConnection(connectionString);
        }

        private IEnumerable<CategoriesModel> QueryCategories(string query, object parameters = null)
        {
            using (var connection = GetSqlConnection())
            {
                return connection.Query<CategoriesModel>(query, parameters);
            }
        }

        [HttpGet("categories")]
        public IEnumerable<CategoriesModel> getCategories()
        {
            string getQuery = "SELECT * FROM CATEGORIES";
            return QueryCategories(getQuery);
        }


        [Authorize(Roles = "Admin")]
        [HttpPost("insertcategory")]
        public void insertCategories(CategoriesModel category)
        {
            using(var connection = GetSqlConnection())
            {
                string insertQuery = "INSERT INTO Categories (CategoryName) VALUES (@CategoryName)";
                connection.Execute(insertQuery, new { CategoryName=category.CategoryName });
            }
        }


        [HttpGet("checked/{id}")]
        public IEnumerable<BookModel> getChecked(int id, [FromQuery] string categories)
        {
            var categoriesList = categories.Split(',').Select(Int32.Parse).ToList();
            var connection = GetSqlConnection();
            var getQuery = "SELECT * FROM Books WHERE UserId=@Id AND CategoryId IN @categories";
            return connection.Query<BookModel>(getQuery, new { id, categories = categoriesList });
        }

    }
}
