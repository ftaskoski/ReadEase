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
        public (IEnumerable<BookModel>, int) SearchAndCategory(int UserId, string? search, string? searchTitle = null, List<int>? categories = null, int pageNumber = 1, int pageSize = 10);
        public IEnumerable<BookModel> SearchAndCategoryAll(int UserId, string? search = null, string? searchTitle = null, List<int>? categories = null);
        void UpdateBook(UpdateBook book);
        void DeleteBooksByUser(int userId);
        public IEnumerable<BookModel> GetSingleBook(int bookId);


    }
}