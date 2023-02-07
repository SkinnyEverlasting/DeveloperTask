using DeveloperTask.Requests;
using DeveloperTask.Responses;
using DeveloperTask.Services;
using Microsoft.AspNetCore.Mvc;

namespace DeveloperTask.Controllers
{
    [ApiController]
    public class BookController
    {
        private readonly BookService bookService;

        public BookController(BookService bookService)
        {
            this.bookService = bookService;
        }

        [HttpPost("add-book-information")]
        public Response AddBookInformation(AddBookInformationRequest request)
        {
            return bookService.AddBookInformation(request);
        }


        [HttpPost("request-to-take-book")]
        public Response RequestToTakeBook(int bookId)
        {
            return bookService.RequestToTakeBook(bookId);
        }

        [HttpPost("return-back-book")]
        public Response ReturnBackBook(int bookId)
        {
            return bookService.ReturnBackBook(bookId);
        }


        [HttpPost("update-book-information")]
        public Response UpdateBookInformation(UpdateBookInformationRequest request)
        {
            return bookService.UpdateBookInformation(request);
        }

        [HttpGet("get-all-books")]
        public GetAllBooksResponse GetAllBooks()
        {
            return bookService.GetAllBooks();
        }

        [HttpGet("get-books-by-title")]
        public GetBooksByTitleResponse GetBooksByTitle(string title)
        {
            return bookService.GetBooksByTitle(title);
        }

        [HttpGet("get-all-books-information")]
        public GetAllBookInformationResponse GetAllBooksInformation()
        {
            return bookService.GetAllBookInformation();
        }


        [HttpGet("get-book-details")]
        public GetBookDetailsResponse GetBookDetails(int bookId)
        {
            return bookService.GetBookDetails(bookId);
        }
    }
}
