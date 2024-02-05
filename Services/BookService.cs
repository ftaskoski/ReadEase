using System.Data.SqlClient;
using Dapper;
using WebApplication1.Models;
namespace Books.Services

{
    public class BookService
    {
        private readonly IConfiguration _configuration;

        public BookService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        private SqlConnection GetSqlConnection()
        {
            string connectionString = _configuration.GetConnectionString("DefaultConnection");
            return new SqlConnection(connectionString);
        }

        private IEnumerable<BookModel> QueryBooks(string query, object parameters = null)
        {
            using (var connection = GetSqlConnection())
            {
                return connection.Query<BookModel>(query, parameters);
            }
        }

        private void Execute(string query, object parameters = null)
        {
            using (var connection = GetSqlConnection())
            {
                connection.Execute(query, parameters);
            }
        }

        public IEnumerable<BookModel> GetAllBooksForUser(int userId)
        {
            string getQuery = "SELECT * FROM BOOKS WHERE UserId=@UserId";
             return QueryBooks(getQuery, new {UserId=userId});
        }

        public IEnumerable<BookModel> GetPaginatedBooks(int id, int pageNumber = 1, int pageSize = 10)
        {
            int startIndex = (pageNumber - 1) * pageSize;
            string getQuery = "SELECT * FROM BOOKS WHERE UserId=@Id ORDER BY BookId OFFSET @startIndex ROWS FETCH NEXT @pageSize ROWS ONLY;";
            return QueryBooks(getQuery, new { id = id, startIndex = startIndex, pageSize = pageSize });
        }

        public IEnumerable<BookModel> GetAllBooksFromSearch(int id,string search)
        {
            string searchQuery = $"SELECT * FROM BOOKS WHERE UserId=@Id AND Author LIKE @Search";
            return QueryBooks(searchQuery, new { id = id, Search = $"{search}%" });
        }

        public IEnumerable<BookModel> GetPaginatedBooksFromSearch(int id, string search, int pageNumber = 1, int pageSize = 10)
        {
            int startIndex = (pageNumber - 1) * pageSize;
            string searchQuery = "SELECT * FROM BOOKS WHERE UserId=@Id AND Author LIKE @Search ORDER BY BookId OFFSET @startIndex ROWS FETCH NEXT @pageSize ROWS ONLY;";
            return QueryBooks(searchQuery, new { id = id, Search = $"{search}%", startIndex = startIndex, pageSize = pageSize });      
        }

        public void InsertBook(BookModel book,int id) 
        {
            string insertQuery = "INSERT INTO Books (Title, Author, UserId, CategoryId) VALUES (@Title, @Author, @UserId, @CategoryId);";
            Execute(insertQuery, new { book.Title, Author = book.Author, UserId = id, CategoryId = book.CategoryId });
        }

        public void DeleteBook(int id)
        {
            string deleteQuery = "DELETE FROM Books WHERE bookId=@id";
            Execute(deleteQuery, new { id = id });
        }

    }
}
