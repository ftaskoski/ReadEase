using Dapper;
using System.Data.SqlClient;

namespace ReadEase_C_.Services
{
    public class PhotoService
    {

        private readonly IConfiguration _configuration;

        public PhotoService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        private SqlConnection GetSqlConnection()
        {
            string connectionString = _configuration.GetConnectionString("DefaultConnection");
            return new SqlConnection(connectionString);
        }


        public byte[] GetPhoto(int UserId)
        {
            var connection = GetSqlConnection();
            string selectQuery = "SELECT photo FROM Users WHERE Id = @UserId";

            return connection.QuerySingleOrDefault<byte[]>(selectQuery, new { UserId });


        }
    }
}
