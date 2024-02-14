using Dapper;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebApplication1.Models;
using RestSharp;
using ReadEase_C_.Services;
using static Microsoft.ApplicationInsights.MetricDimensionNames.TelemetryContext;
using System.Security.Cryptography;

namespace userController.Controllers
{
    [ApiController]
    [Route("api")]
    public class UsersController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly UserService _userService;

        public UsersController(IConfiguration configuration,UserService service)
        {
            _configuration = configuration;
            _userService = service;
        }

        [HttpGet("users")]
        [Authorize(Roles ="Admin")]
        public IEnumerable<FormModel> getUsers()
        {
           return _userService.GetAllUsers();
        }

        [HttpPost("logout")]
        public IActionResult Logout()
        {
            Response.Cookies.Delete(".AspNetCore.Cookies", new CookieOptions
            {
                Path = "/",
                Secure = true, 
                HttpOnly = true,
               SameSite = SameSiteMode.None
            });


            return Ok( "Logout successful" );
        }


        [HttpPost("register")]
        public async Task<IActionResult> PostUser([FromBody] FormModel model)
        {
            string connectionString = _configuration.GetConnectionString("DefaultConnection");

            using (var connection = new SqlConnection(connectionString))
            {
                var rng = new RNGCryptoServiceProvider();
                byte[] saltBytes = new byte[16];
                rng.GetBytes(saltBytes);
                string salt = Convert.ToBase64String(saltBytes);
                string saltedPassword = model.Password + salt;

                // Hash the password with salt
                using (var sha512 = SHA512.Create())
                {
                    byte[] hashedBytes = sha512.ComputeHash(Encoding.UTF8.GetBytes(saltedPassword));
                    string hashedPassword = BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();

                    // Check if the user with the same username already exists
                    string checkUserQuery = "SELECT COUNT(*) FROM Users WHERE Username = @Username";
                    int existingUser = await connection.QueryFirstOrDefaultAsync<int>(checkUserQuery, new { Username = model.Username });

                    if (existingUser > 0)
                    {
                        // User with the same username already exists, handle accordingly
                        return Conflict("Username already exists");
                    }

                    // Insert the data into the Users table and retrieve the generated Id using Dapper
                    string insertQuery = "INSERT INTO Users (Username, Password, Salt, Role) OUTPUT INSERTED.Id VALUES (@Username, @Password, @Salt, 'User')";
                    int userId = await connection.QueryFirstOrDefaultAsync<int>(insertQuery, new { Username = model.Username, Password = hashedPassword, Salt = salt });

                    // Set the generated Id in the FormModel
                    model.Id = userId;
                    string role = await _userService.CheckIfUserIsAdminAsync(model.Id);

                    // Create claims for the registered user
                    var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, model.Id.ToString()),
                new Claim(ClaimTypes.Name, model.Username),
                new Claim(ClaimTypes.Role, role ?? "User")
                // Add additional claims as needed
            };
                    var authProperties = new AuthenticationProperties
                    {
                        // Persist the cookie even after the browser is closed
                        IsPersistent = true
                    };
                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    var principal = new ClaimsPrincipal(identity);

                    // Sign in the user after registration
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, authProperties);
                    model.Role = role ?? "User";
                    return Ok(model.Role);
                }
            }
        }

        [HttpPost("login")]
        public async Task<ActionResult> LoginUser([FromBody] FormModel model)
        {
            string connectionString = _configuration.GetConnectionString("DefaultConnection");

            using (var connection = new SqlConnection(connectionString))
            {
                // Retrieve the salt for the user
                string getSaltQuery = "SELECT Salt FROM Users WHERE Username = @Username";
                string salt = await connection.QueryFirstOrDefaultAsync<string>(getSaltQuery, new { Username = model.Username });

                if (salt == null)
                {
                    // User not found, return 401 Unauthorized
                    return Unauthorized();
                }

                // Hash the password with retrieved salt
                string saltedPassword = model.Password + salt;
                using (var sha512 = SHA512.Create())
                {
                    byte[] hashedBytes = sha512.ComputeHash(Encoding.UTF8.GetBytes(saltedPassword));
                    string hashedPassword = BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();

                    string selectQuery = "SELECT * FROM Users WHERE Username = @Username AND Password = @Password";
                    var user = await connection.QueryFirstOrDefaultAsync<FormModel>(selectQuery, new { Username = model.Username, Password = hashedPassword });

                    if (user != null)
                    {
                        // Retrieve the role from the database
                        string role = await _userService.CheckIfUserIsAdminAsync(user.Id);

                        // Create claims for the authenticated user
                        var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Name, user.Username),
                    new Claim(ClaimTypes.Role, role ?? "User") // Default to "User" if role is null
                    // Add additional claims as needed
                };

                        var authProperties = new AuthenticationProperties
                        {
                            // Persist the cookie even after the browser is closed
                            IsPersistent = true
                        };
                        var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                        var principal = new ClaimsPrincipal(identity);

                        // Sign in the user
                        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, authProperties);

                        user.Role = role ?? "User";
                        return Ok(user.Role);
                    }
                    else
                    {
                        // Return 401 Unauthorized
                        return Unauthorized();
                    }
                }
            }
        }

        public int UserId
        {
            get
            {
                return Int32.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            }
        }

        [HttpGet("role")]
        [Authorize]
        public async Task<ActionResult> GetRole()
        {
            string role = await _userService.CheckIfUserIsAdminAsync(UserId);

            return Ok(role);
        }



        [HttpPut("update")]
        [Authorize]
        public async Task<IActionResult> UpdateUser([FromBody]NewUsername user )
        {

           await _userService.UpdateUserAsync(user.UpdatedUsername, UserId);
            return Ok("Updated Username");
        }


        [HttpDelete("delete/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteUser(int id)
        {

            _userService.DeleteUser(id);
            return Ok();    
        }


    }
}
