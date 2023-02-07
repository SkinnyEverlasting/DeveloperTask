using DeveloperTask.Models;

namespace DeveloperTask.Responses
{
    public class GetBookDetailsResponse :  Response
    {
        public BookDetails BookDetails { get; set; }
    }
}
