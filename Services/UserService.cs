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

        private IEnumerable<FormModel> QueryUsers(string query, object parameters = null)
        {
            using (var connection = GetSqlConnection())
            {
                return connection.Query<FormModel>(query, parameters);
            }
        }

        public IEnumerable<FormModel> GetAllUsers()
        {
            string selectQuery = "SELECT * FROM Users";
            return QueryUsers(selectQuery);

        }

    }
}
