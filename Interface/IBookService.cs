using ReadEase_C_.Models;
using WebApplication1.Models;

namespace ReadEase_C_.Interface
{
    public interface IBookService
    {
        void DeleteBook(int id);
        IEnumerable<BookModel> GetAllBooksForUser(int userId);
        IEnumerable<BookModel> GetAllBooksFromSearch(int id, string search);
        IEnumerable<BookModel> GetPaginatedBooks(int id, int pageNumber = 1, int pageSize = 10);
        IEnumerable<BookModel> GetPaginatedBooksFromSearch(int id, string search, int pageNumber = 1, int pageSize = 10);
        void InsertBook(BookModel book, int id);
        IEnumerable<BookModel> SearchAndCategory(int UserId, string? search, List<int>? categories = null, int pageNumber = 1, int pageSize = 10);
        IEnumerable<BookModel> SearchAndCategoryAll(int UserId, string? search = null, List<int>? categories = null);
        void UpdateBook(UpdateBook book);
    }
}