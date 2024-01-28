using Dapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebApplication1.Models;

namespace userController.Controllers
{
    [ApiController]
    [Route("api")]
    public class UsersController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public UsersController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet("users")]
        [Authorize]
        public ActionResult getUsers()
        {
            string connectionString = _configuration.GetConnectionString("DefaultConnection");

            using(var connection = new SqlConnection(connectionString))
            {
                string selectQuery = "SELECT * FROM Users";
                var users = connection.Query(selectQuery);
                return Ok(users);
            }

        }


        [HttpPost("register")]
        public async Task<IActionResult> PostUser([FromBody] FormModel model)
        {

            string connectionString = _configuration.GetConnectionString("DefaultConnection");

            using (var connection = new SqlConnection(connectionString))
            {

                // Check if the user with the same username already exists
                string checkUserQuery = "SELECT COUNT(*) FROM Users WHERE Username = @Username";
                int existingUser = await connection.QueryFirstOrDefaultAsync<int>(checkUserQuery, new { Username = model.Username });

                if (existingUser > 0)
                {
                    // User with the same username already exists, handle accordingly
                    return Conflict("Username already exists");
                }

                // Insert the data into the Users table and retrieve the generated Id using Dapper
                string insertQuery = "INSERT INTO Users (Username, Password) OUTPUT INSERTED.Id VALUES (@Username, @Password)";
                int userId = await connection.QueryFirstOrDefaultAsync<int>(insertQuery, new { Username = model.Username, Password = model.Password });

                // Set the generated Id in the FormModel
                model.Id = userId;
                var token = GenerateJwtToken(model);
                return Ok(new { Token = token, User = model });
            }


        }

        [HttpPost("login")]
        public async Task<ActionResult> LoginUser([FromBody] FormModel model)
        {

            string connectionString = _configuration.GetConnectionString("DefaultConnection");

            using (var connection = new SqlConnection(connectionString))
            {
                string selectQuery = "SELECT * FROM Users WHERE Username = @Username AND Password = @Password";
                var user = await connection.QueryFirstOrDefaultAsync<FormModel>(selectQuery, new { Username = model.Username, Password = model.Password });

                if (user != null)
                {
                    var token = GenerateJwtToken(user);

                    return Ok(new { Token = token, User = user });
                }
                else
                {
                    // Return 401 Unauthorized
                    return Unauthorized();
                }
            }

        }

        [HttpPut("update/{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateUser([FromBody] FormModel model, int id)
        {

            string connectionString = _configuration.GetConnectionString("DefaultConnection");

            using (var connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                string checkUserQuery = "SELECT COUNT(*) FROM Users WHERE Id = @Id";
                var userCount = await connection.QueryFirstOrDefaultAsync<int>(checkUserQuery, new { Id = id });
                if (userCount == 0)
                {
                    // User with the given ID does not exist
                    return NotFound();
                }

                string updateQuery = "UPDATE Users SET Username = @Username WHERE Id = @Id";
                await connection.ExecuteAsync(updateQuery, new { Id = id, Username = model.Username });

                return Ok();
            }

        }


        [HttpDelete("delete/{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteUser(int id)
        {

            string connectionString = _configuration.GetConnectionString("DefaultConnection");

            using (var connection = new SqlConnection(connectionString))
            {
                string deleteQuery = "DELETE FROM Users WHERE Id = @Id;";
                await connection.ExecuteAsync(deleteQuery, new { Id = id });

                return Ok($"User with ID {id} has been deleted");
            }
        }

        private string GenerateJwtToken(FormModel user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            // Use a byte array for the key
            var key = Encoding.UTF8.GetBytes("this is my custom Secret key for authentication");

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.Username)
                    // Add additional claims as needed
                }),
                Expires = DateTime.UtcNow.AddHours(1), // Adjust the expiration time as needed
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
