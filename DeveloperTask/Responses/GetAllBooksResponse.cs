using DeveloperTask.Models;

namespace DeveloperTask.Responses
{
    public class GetAllBooksResponse : Response
    {
        public IEnumerable<Book> Books { get; set; }
    }
}
