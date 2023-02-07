using DeveloperTask.Repository;
using DeveloperTask.Requests;
using DeveloperTask.Responses;

namespace DeveloperTask.Services
{
    public class BookServiceImpl : BookService
    {
        private readonly IBookRepository bookRepository;
        private readonly IWebHostEnvironment _hostEnvitonment;

        public BookServiceImpl(IBookRepository bookRepository, IWebHostEnvironment hostEnvitonment)
        {
            this.bookRepository = bookRepository;
            _hostEnvitonment = hostEnvitonment;
        }

        public Response AddBookInformation(AddBookInformationRequest request)
        {
            string imagePath;
            SaveImage(request.Title, request.ImageBase64, out imagePath);

            var success = bookRepository.AddBookInformation(request.Title, request.Description, imagePath, request.Rating,request.ReleaseDate, request.Authors);
            var message = success ? "Book Added Successfully" : "Error Occured";
            var statusCode = success ? 200 : 400;
            return new Response()
            {
                StatusCode = statusCode,
                Message = message
            };
        }

        public Response RequestToTakeBook(int bookId)
        {
            var result = bookRepository.RequestToTakeBook(bookId);
            Response response = new Response();

            switch(result)
            {
                case Models.ResultStatus.ERROR:
                    {
                        response.StatusCode = 404;
                        response.Message = "Book Not Found";
                        break;
                    }
                case Models.ResultStatus.SUCCESS:
                    {
                        response.StatusCode = 200;
                        response.Message = "Request To Take Book Accepted";
                        break;
                    }
                case Models.ResultStatus.FAILED:
                    {
                        response.StatusCode = 400;
                        response.Message = "Requested Book Is Already Taken";
                        break;
                    }
            }
            return response;
            
        }

        public Response ReturnBackBook(int bookId)
        {
            var result = bookRepository.ReturnBackBook(bookId);
            Response response = new Response();

            switch (result)
            {
                case Models.ResultStatus.ERROR:
                    {
                        response.StatusCode = 400;
                        response.Message = "Error Occured";
                        break;
                    }
                case Models.ResultStatus.SUCCESS:
                    {
                        response.StatusCode = 200;
                        response.Message = "Book Was Returned Successfully";
                        break;
                    }
            }
            return response;
        }

        public Response UpdateBookInformation(UpdateBookInformationRequest request)
        {
            var bookExists = bookRepository.CheckIfBookExists(request.BookId);
            if(!bookExists)
            {
                return new Response()
                {
                    StatusCode = 404,
                    Message = "Book Does Not Exist"
                };
            }

            string imagePath;
            SaveImage(request.Title, request.ImageBase64, out imagePath);

            var success = bookRepository.UpdateBookInformation(request.BookId, request.Title, request.Description, imagePath, request.Rating, request.ReleaseDate, request.Authors);
            var message = success ? "Book Updated Successfully" : "Error Occured";
            var statusCode = success ? 200 : 400;
            return new Response()
            {
                StatusCode = statusCode,
                Message = message
            };
        }


        public GetAllAuthorsResponse GetAllAuthors()
        {
            throw new NotImplementedException();
        }

        public GetAllBooksResponse GetAllBooks()
        {
            var booksList = bookRepository.GetAllBooks();
            return new GetAllBooksResponse()
            {
                StatusCode = 200,
                Message = "SUCCESS",
                Books = booksList
            };
        }

        public GetBooksByTitleResponse GetBooksByTitle(string title)
        {
            var booksList = bookRepository.GetBooksByTitle(title);
            return new GetBooksByTitleResponse()
            {
                StatusCode = 200,
                Message = "SUCCESS",
                Books = booksList
            };
        }

        public GetAllBookInformationResponse GetAllBookInformation()
        {
            var result = bookRepository.GetAllBooksInformation();
            return new GetAllBookInformationResponse()
            {
                StatusCode = 200,
                Message = "SUCCESS",
                BooksInformation = result
            };
        }

        public GetBookDetailsResponse GetBookDetails(int bookId)
        {
            var bookExists = bookRepository.CheckIfBookExists(bookId);
            if (!bookExists)
            {
                return new GetBookDetailsResponse()
                {
                    StatusCode = 404,
                    Message = "Book Does Not Exist",
                    BookDetails = null
                };
            }
            var result = bookRepository.GetBookDetails(bookId);
            return new GetBookDetailsResponse()
            {
                StatusCode = 200,
                Message = "SUCCESS",
                BookDetails = result
            };
        }

        private void SaveImage(string bookTitle, string imageBase64, out string imagePath)
        {
            string wwwRootPath = _hostEnvitonment.WebRootPath;

            imagePath = bookTitle + DateTime.Now.ToString("yymmssffff") + ".jpg";
            string path = Path.Combine(wwwRootPath, imagePath);

            byte[] imageBytes = Convert.FromBase64String(imageBase64);

            File.WriteAllBytes(path, imageBytes);
        }
    }
}
