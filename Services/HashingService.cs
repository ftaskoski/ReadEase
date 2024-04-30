using Dapper;
using ReadEase_C_.Interface;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;
namespace ReadEase_C_.Services
{
    public class HashingService : IHashingService
    {



        public async Task<string> GetSalt(string username, SqlConnection connection)
        {
            string getSaltQuery = "SELECT Salt FROM Users WHERE Username = @Username";
            string salt = await connection.QueryFirstOrDefaultAsync<string>(getSaltQuery, new { Username = username });
            return salt;
        }


        public async Task<string> GenerateSaltedHash(string password, string salt)
        {
            string saltedPassword = password + salt;
            using (var sha512 = SHA512.Create())
            {
                byte[] hashedBytes = sha512.ComputeHash(Encoding.UTF8.GetBytes(saltedPassword));
                string hashedPassword = BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
                return hashedPassword;
            }
        }

        public async Task<string> GenerateSaltAsync()
        {
            var rng = new RNGCryptoServiceProvider();
            byte[] saltBytes = new byte[16];
            rng.GetBytes(saltBytes);
            string salt = Convert.ToBase64String(saltBytes);
            return salt;
        }

        public string GenerateRandomPassword(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#$%^&*()_+";
            var random = new Random();
            return new string(Enumerable.Repeat(chars, length)
            .Select(s => s[random.Next(s.Length)]).ToArray());
        }

    }
}
