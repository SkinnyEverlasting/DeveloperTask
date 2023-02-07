using DeveloperTask.Models;

namespace DeveloperTask.Responses
{
    public class GetAllAuthorsResponse : Response
    {
        public IEnumerable<Author> Authors { get; set; }
    }
}
