using System.Data.SqlClient;
using ReadEase_C_.Interface;

namespace ReadEase_C_.Services
{
    public class ConnectionService : IConnectionService
    {

        private readonly IConfiguration _configuration;

        public ConnectionService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public SqlConnection GetConnection()
        {
            string connectionString = _configuration.GetConnectionString("DefaultConnection");
            return new SqlConnection(connectionString);

        }

    }
}
