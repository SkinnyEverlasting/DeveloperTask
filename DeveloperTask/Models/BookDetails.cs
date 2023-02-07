namespace DeveloperTask.Models
{
    public class BookDetails
    {
        public int BookID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }
        public float Rating { get; set; }
        public DateTime ReleaseDate { get; set; }
        public bool TakenAway { get; set; }
        public string Authors { get; set; }
    }
}
