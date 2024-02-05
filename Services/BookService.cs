using System.Collections.Generic;
using System.Data.SqlClient;
using Dapper;
using Microsoft.AspNetCore.Mvc;
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

        public IEnumerable<BookModel> GetPaginatedBooks(int id, int pageNumber = 1, int pageSize = 10)
        {
            int startIndex = (pageNumber - 1) * pageSize;
            string getQuery = "SELECT * FROM BOOKS WHERE UserId=@Id ORDER BY BookId OFFSET @startIndex ROWS FETCH NEXT @pageSize ROWS ONLY;";
            return QueryBooks(getQuery, new { id = id, startIndex = startIndex, pageSize = pageSize });


        }

        public IEnumerable<BookModel> GetAllBooksFromSearch(int id,string search)
        {
            string searchQuery = $"SELECT * FROM BOOKS WHERE UserId=@Id AND Author LIKE @Search";
            return QueryBooks(searchQuery, new { id = id, Search = $"{search}%" });

        }

    }
}
