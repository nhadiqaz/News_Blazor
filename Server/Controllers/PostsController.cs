using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using Repositories;

namespace Server.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        #region Dependency

        public IPostRepository PostRepository { get; }
        public PostsController(IPostRepository postRepository)
        {
            PostRepository = postRepository;
        }


        #endregion \Dependency


        #region Actions

        #region GetAllPosts

        [HttpGet]
        public async Task<ActionResult<List<Post>>> GetAllPosts()
        {
            var _posts = await PostRepository.GetAllPostsAsync();

            return Ok(_posts);
        }

        #endregion \GetAllPosts

        #endregion \Actions
    }
}
