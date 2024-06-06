using Dapper;
using Microsoft.AspNetCore.Mvc;
using ReadEase_C_.Interface;
using ReadEase_C_.Models;

namespace ReadEase_C_.Services
{
    public class UserService : IUserService
    {
        private readonly IConnectionService _connectionService;
        public UserService(IConnectionService connService)
        {
            _connectionService = connService;
        }



        private IEnumerable<protectedUserModel> QueryUsers(string query, object parameters = null)
        {
            using var connection = _connectionService.GetConnection();
            return connection.Query<protectedUserModel>(query, parameters);
        }

        private void Execute(string query, object parametars = null)
        {
            var connection = _connectionService.GetConnection();
            connection.Execute(query, parametars);
        }

        public IEnumerable<protectedUserModel> GetAllUsers()
        {
            string selectQuery = "SELECT * FROM Users";
            return QueryUsers(selectQuery);

        }

        public void DeleteUser(int id)
        {
            string deleteQuery = "DELETE FROM Users WHERE Id = @Id;";
            Execute(deleteQuery, new { Id = id });
        }

        public async Task<string> CheckIfUserIsAdminAsync(int userId)
        {
            var connection = _connectionService.GetConnection();
            string checkAdminQuery = "SELECT Role FROM Users WHERE Id = @Id";
            string role = await connection.QueryFirstOrDefaultAsync<string>(checkAdminQuery, new { Id = userId });

            return role ?? string.Empty;
        }

        public async Task<IActionResult> UpdateUserAsync(string newUsername, string newPassword, int id)
        {
            var connection = _connectionService.GetConnection();

            string checkUserQuery = "SELECT COUNT(*) FROM Users WHERE Id = @Id";
            var userCount = await connection.QueryFirstOrDefaultAsync<int>(checkUserQuery, new { Id = id });
            if (userCount == 0)
            {
                // User with the given ID does not exist
                return new NotFoundResult();
            }

            // Construct the update query based on the provided parameters
            string updateQuery = "UPDATE Users SET ";
            List<string> updateParams = [];
            if (!string.IsNullOrEmpty(newUsername))
            {
                updateParams.Add("Username = @Username");
            }
            if (!string.IsNullOrEmpty(newPassword))
            {
                updateParams.Add("Password = @Password");
            }

            // Check if at least one field is being updated
            if (updateParams.Count == 0)
            {
                // Nothing to update
                return new BadRequestResult();
            }

            updateQuery += string.Join(", ", updateParams);
            updateQuery += " WHERE Id = @Id";

            // Create a DynamicParameters object
            var parameters = new DynamicParameters();
            parameters.Add("Id", id);
            parameters.Add("Username", newUsername);

            // Only add the Password parameter if newPassword is not null or empty
            if (!string.IsNullOrEmpty(newPassword))
            {
                parameters.Add("Password", newPassword);
            }

            // Execute the update query
            await connection.ExecuteAsync(updateQuery, parameters);

            return new OkResult();
        }

        public string getUsername(int id)
        {
            var connection = _connectionService.GetConnection();
            string emailQuery = "SELECT Username FROM USERS WHERE Id=@id";

            return connection.QueryFirst<string>(emailQuery, new { Id = id });
        }


    }
}
