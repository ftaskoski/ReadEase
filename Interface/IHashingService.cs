using System.Data.SqlClient;

namespace ReadEase_C_.Interface
{
    public interface IHashingService
    {
        Task<string> GenerateSaltAsync();
        Task<string> GenerateSaltedHash(string password, string salt);
        Task<string> GetSalt(string username, SqlConnection connection);
        public string GenerateRandomPassword(int length);

    }
}