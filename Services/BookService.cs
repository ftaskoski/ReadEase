using System.Collections.Generic;
using System.Data.SqlClient;
using Dapper;
using Microsoft.Extensions.Configuration;
using ReadEase_C_.Models;
using WebApplication1.Models;

namespace Books.Services
{

    public class BookService
    {
        private readonly IConfiguration _configuration;

        public BookService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        private SqlConnection GetSqlConnection()
        {
            string connectionString = _configuration.GetConnectionString("DefaultConnection");
            return new SqlConnection(connectionString);
        }

        private IEnumerable<BookModel> QueryBooks(string query, object parameters = null)
        {
            using (var connection = GetSqlConnection())
            {
                return connection.Query<BookModel>(query, parameters);
            }
        }

        public IEnumerable<BookModel> GetAllBooksForUser(int userId)
        {
            string getQuery = "SELECT * FROM BOOKS WHERE UserId=@UserId";
             return QueryBooks(getQuery, new {UserId=userId});
        }


    }
}
