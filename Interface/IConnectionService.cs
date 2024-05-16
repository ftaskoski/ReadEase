using System.Data.SqlClient;

namespace ReadEase_C_.Interface
{
    public interface IConnectionService
    {
        SqlConnection GetConnection();
    }
}