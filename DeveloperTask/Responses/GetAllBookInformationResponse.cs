using DeveloperTask.Models;

namespace DeveloperTask.Responses
{
    public class GetAllBookInformationResponse: Response
    {
        public IEnumerable<BookModel> BooksInformation{ get; set; }
    }
}
