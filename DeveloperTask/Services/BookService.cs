using DeveloperTask.Requests;
using DeveloperTask.Responses;

namespace DeveloperTask.Services
{
    public interface BookService
    {
        public Response AddBookInformation(AddBookInformationRequest request);
        public Response RequestToTakeBook(int bookId);
        public Response ReturnBackBook(int bookId);
        public Response UpdateBookInformation(UpdateBookInformationRequest request);
        public GetAllBooksResponse GetAllBooks();
        public GetAllAuthorsResponse GetAllAuthors();
        public GetBooksByTitleResponse GetBooksByTitle(string title);
        public GetAllBookInformationResponse GetAllBookInformation();
        public GetBookDetailsResponse GetBookDetails(int bookId);
    }
}
