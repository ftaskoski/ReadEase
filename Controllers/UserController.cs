using Dapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReadEase_C_.Services;
using RestSharp;
using System.Data.SqlClient;
using System.Net.Mail;
using System.Net;
using System.Security.Claims;
using WebApplication1.Models;

namespace userController.Controllers
{
    [ApiController]
    [Route("api")]
    public class UsersController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly UserService _userService;
        private readonly HashingService _hashingService;

        public UsersController(IConfiguration configuration,UserService service, HashingService hashingService)
        {
            _configuration = configuration;
            _userService = service;
            _hashingService = hashingService;
        }

        [HttpGet("users")]
        [Authorize(Roles ="Admin")]
        public IEnumerable<FormModel> getUsers()
        {
           return _userService.GetAllUsers();
        }

        [HttpGet("lookup")]
        [Authorize]
        public IActionResult Lookup()
        {
            var isAuthenticated = User?.Identity?.IsAuthenticated ?? false;
            var roleClaim = User?.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role);
            var role = roleClaim?.Value;

            var response = new
            {
                IsAuthenticated = isAuthenticated,
                Role = role
            };

            return Ok(response);
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
                if (model.Password.Length < 3)
                {
                    return Conflict("Password is too short!");
                }
                // Retrieve salt asynchronously
                string salt = await _hashingService.GenerateSaltAsync();

                // Hash the password with retrieved salt
                string hashedPassword = await _hashingService.GenerateSaltedHash(model.Password, salt);

                // Check if the user with the same username already exists
                string checkUserQuery = "SELECT COUNT(*) FROM Users WHERE Username = @Username";
                int existingUser = await connection.QueryFirstOrDefaultAsync<int>(checkUserQuery, new { Username = model.Username });

                if (existingUser > 0)
                {
                    // User with the same username already exists, handle accordingly
                    return Conflict("Username already exists!");
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

                // Sign in the user after successful registration
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, authProperties);
                model.Role = role ?? "User";

                // Send the registration success email
                try
                {
                    string senderEmail = "filip.taskoski69@gmail.com"; // Your Gmail address
                    string senderPassword = "vyut wntk rmfu wazg\r\n"; // Your Gmail password
                    string recipientEmail = "filip.taskoski77@gmail.com"; // Recipient's email

                    MailMessage mail = new MailMessage(senderEmail, recipientEmail);
                    SmtpClient client = new SmtpClient();

                    // Check if the sender email is a Gmail address
                    if (senderEmail.EndsWith("@gmail.com"))
                    {
                        client.Host = "smtp.gmail.com";
                        client.Port = 587;
                        client.EnableSsl = true;
                        client.UseDefaultCredentials = false;
                        client.Credentials = new NetworkCredential(senderEmail, senderPassword);
                    }
                    // Check if the sender email is a Hotmail/Outlook address
                    else if (senderEmail.EndsWith("@hotmail.com") || senderEmail.EndsWith("@outlook.com"))
                    {
                        client.Host = "smtp-mail.outlook.com";
                        client.Port = 587; // or 465 if using SSL
                        client.EnableSsl = true;
                        client.UseDefaultCredentials = false;
                        client.Credentials = new NetworkCredential(senderEmail, senderPassword);
                    }
                    else
                    {
                        // Handle unsupported email provider
                        return BadRequest("Unsupported email provider.");
                    }

                    mail.Subject = "Registration Successful";
                    mail.Body = "Congratulations! You have successfully registered.";

                    client.Send(mail);
                }
                catch (Exception ex)
                {
                    // Handle exception, log error, etc.
                    return StatusCode(500, "Failed to send email: " + ex.Message);
                }

                // Return the user's role after successful registration
                return Ok(model.Role);
            }
        }


        [HttpPost("login")]
        public async Task<ActionResult> LoginUser([FromBody] FormModel model)
        {
            string connectionString = _configuration.GetConnectionString("DefaultConnection");

            using (var connection = new SqlConnection(connectionString))
            {
                // Retrieve the salt for the user
                string salt = await _hashingService.GetSalt(model.Username, connection);

                if (salt == null)
                {
                    // User not found, return 401 Unauthorized
                    return Unauthorized();
                }

                // Hash the password with retrieved salt
                string hashedPassword = await _hashingService.GenerateSaltedHash(model.Password, salt);

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

        private SqlConnection GetSqlConnection()
        {
            string connectionString = _configuration.GetConnectionString("DefaultConnection");
            return new SqlConnection(connectionString);
        }

        [HttpPost("photo")]
        [Authorize]
        public IActionResult Photo(IFormFile file)
        {
            var connection = GetSqlConnection();

            if (file == null || file.Length == 0)
                return BadRequest("No file is uploaded.");

            using (var memoryStream = new MemoryStream())
            {
                file.CopyTo(memoryStream);
                var imageBytes = memoryStream.ToArray();

                string insertQuery = "UPDATE Users SET photo = @Photo WHERE Id = @UserId";

                connection.Execute(insertQuery, new { Photo = imageBytes, UserId = UserId });
            }

            return Ok("Photo uploaded successfully.");
        }

        [HttpGet("getphoto")]
        [Authorize]
        public IActionResult GetPhoto()
        {
            var connection = GetSqlConnection();

            string selectQuery = "SELECT photo FROM Users WHERE Id = @UserId";

            byte[] photoBytes = connection.QuerySingleOrDefault<byte[]>(selectQuery, new { UserId = UserId });

            if (photoBytes == null)
                return NotFound("No photo found for this user.");

            return File(photoBytes, "image/jpeg"); // adjust the content type based on your image format
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
