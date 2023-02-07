using DeveloperTask.Models;

namespace DeveloperTask.Repository
{
    public interface IAuthorRepository
    {
        public IEnumerable<Author> GetAllAuthors();
        public IEnumerable<Author> GetAuthorsByName(string name);
    }
}
