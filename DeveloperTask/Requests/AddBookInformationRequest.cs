using DeveloperTask.Models;
using System.Runtime.Serialization;

namespace DeveloperTask.Requests
{
    public class AddBookInformationRequest
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImageBase64 { get; set; }
        public float Rating { get; set; }
        public DateTime ReleaseDate { get; set; }
        public List<AuthorModel> Authors { get; set; }
    }
}
