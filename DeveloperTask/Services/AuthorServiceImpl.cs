using DeveloperTask.Repository;
using DeveloperTask.Responses;

namespace DeveloperTask.Services
{
    public class AuthorServiceImpl : AuthorService
    {
        private readonly IAuthorRepository authorRepository;

        public AuthorServiceImpl(IAuthorRepository authorRepository)
        {
            this.authorRepository = authorRepository;
        }

        public GetAllAuthorsResponse GetAllAuthors()
        {
            var authorsList = authorRepository.GetAllAuthors();
            return new GetAllAuthorsResponse()
            {
                StatusCode = 200,
                Message = "SUCCESS",
                Authors = authorsList
            };
        }

        public GetAuthorsByNameResponse GetAuthorsByName(string name)
        {
            var authorsList = authorRepository.GetAuthorsByName(name);
            return new GetAuthorsByNameResponse()
            {
                StatusCode = 200,
                Message = "SUCCESS",
                Authors = authorsList
            };
        }
    }
}
