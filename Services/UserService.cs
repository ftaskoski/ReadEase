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

        private void Execute (string query, object parametars = null)
        {
           var connection = GetSqlConnection();
            connection.Execute(query, parametars);
        }

        public IEnumerable<FormModel> GetAllUsers()
        {
            string selectQuery = "SELECT * FROM Users";
            return QueryUsers(selectQuery);

        }

        public void DeleteUser(int id)
        {
            string deleteQuery = "DELETE FROM Users WHERE Id = @Id;";
            Execute(deleteQuery, new {Id=id});
        }

        public async Task<string> CheckIfUserIsAdminAsync(int userId)
        {
           var connection = GetSqlConnection();
           string checkAdminQuery = "SELECT Role FROM Users WHERE Id = @Id";
           string role = await connection.QueryFirstOrDefaultAsync<string>(checkAdminQuery, new { Id = userId });

           return role ?? string.Empty;
        }

    }
}
