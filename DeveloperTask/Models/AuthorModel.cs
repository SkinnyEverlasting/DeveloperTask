using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace DeveloperTask.Models
{
    public class AuthorModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
    }
}
