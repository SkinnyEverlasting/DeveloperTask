using DeveloperTask.Responses;
using DeveloperTask.Services;
using Microsoft.AspNetCore.Mvc;

namespace DeveloperTask.Controllers
{
    [ApiController]
    public class AuthorController
    {

        private readonly AuthorService authorService;

        public AuthorController(AuthorService authorService)
        {
            this.authorService = authorService;
        }

        [HttpGet("get-all-authors")]
        public GetAllAuthorsResponse GetAllAuthors()
        {
            return authorService.GetAllAuthors();
        }

        [HttpGet("get-authors-by-name")]
        public GetAuthorsByNameResponse GetAuthorsByName(string name)
        {
            return authorService.GetAuthorsByName(name);
        }

    }
}
