using DeveloperTask.Models;

namespace DeveloperTask.Responses
{
    public class GetAuthorsByNameResponse: Response
    {
        public IEnumerable<Author> Authors{ get; set; }
    }
}
