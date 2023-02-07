using DeveloperTask.Models;
using DeveloperTask.Requests;

namespace DeveloperTask.Repository
{
    public interface IBookRepository
    {
        public bool AddBookInformation(string title, string description,string imagePath, float rating, DateTime releaseDate, List<AuthorModel> authors);
        public ResultStatus RequestToTakeBook(int bookId);
        public ResultStatus ReturnBackBook(int bookId);
        public bool UpdateBookInformation(int bookId, string title, string description, string imagePath, float rating, DateTime releaseDate, List<AuthorModel> authors);
        public bool CheckIfBookExists(int bookId);
        public IEnumerable<Book> GetAllBooks();
        public IEnumerable<Book> GetBooksByTitle(string title);
        public IEnumerable<BookModel> GetAllBooksInformation();
        public BookDetails GetBookDetails(int bookId);
    }
}
