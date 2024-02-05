using Dapper;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using WebApplication1.Models;

namespace ReadEase_C_.Services
{
    public class UserService
    {
        private readonly IConfiguration _configuration;
        public UserService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        private SqlConnection GetSqlConnection()
        {
            string connectionString = _configuration.GetConnectionString("DefaultConnection");
            return new SqlConnection(connectionString);
        }

        public IEnumerable<FormModel> GetAllUsers()
        {
            var connection = GetSqlConnection();
            string selectQuery = "SELECT * FROM Users";
            return connection.Query<FormModel>(selectQuery);

        }

    }
}
