using DeveloperTask.Models;

namespace DeveloperTask.Requests
{
    public class UpdateBookInformationRequest
    {
        public int BookId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImageBase64 { get; set; }
        public float Rating { get; set; }
        public DateTime ReleaseDate { get; set; }
        public List<AuthorModel> Authors { get; set; }
    }
}
