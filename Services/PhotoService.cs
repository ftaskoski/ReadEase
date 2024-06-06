using Dapper;
using ReadEase_C_.Interface;

namespace ReadEase_C_.Services
{
    public class PhotoService : IPhotoService
    {

        private readonly IConnectionService _connectionService;

        public PhotoService(IConnectionService connectionService)
        {
            _connectionService = connectionService;
        }




        public byte[] GetPhoto(int UserId)
        {
            var connection = _connectionService.GetConnection();
            string selectQuery = "SELECT photo FROM Users WHERE Id = @UserId";

            return connection.QuerySingleOrDefault<byte[]>(selectQuery, new { UserId });


        }

        public void InsertPhoto(int UserId, byte[] photo)
        {
            var connection = _connectionService.GetConnection();
            string insertQuery = "UPDATE Users SET photo = @Photo WHERE Id = @UserId"; // Update column name to ProfilePicture

            connection.Execute(insertQuery, new { Photo = photo, UserId });
        }
    }
}
