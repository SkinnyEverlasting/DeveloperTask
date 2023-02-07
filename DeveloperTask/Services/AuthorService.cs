using DeveloperTask.Responses;

namespace DeveloperTask.Services
{
    public interface AuthorService
    {
        public GetAllAuthorsResponse GetAllAuthors();
        public GetAuthorsByNameResponse GetAuthorsByName(string name);
    }
}
