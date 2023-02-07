using DeveloperTask.Models;

namespace DeveloperTask.Responses
{
    public class GetBooksByTitleResponse : Response
    {
        public IEnumerable<Book> Books { get; set; }
    }
}
