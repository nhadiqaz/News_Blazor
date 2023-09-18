using Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using Repositories;
using Resources;
using Swashbuckle.AspNetCore.Annotations;
using ViewModels;

namespace Server.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        #region Dependency

        public IPostRepository PostRepository { get; }
        public IWebHostEnvironment WebHostEnvironment { get; }
        public ILogger<PostsController> Logger { get; }

        public PostsController(IPostRepository postRepository, IWebHostEnvironment webHostEnvironment, ILogger<PostsController> logger)
        {
            PostRepository = postRepository;
            WebHostEnvironment = webHostEnvironment;
            Logger = logger;
        }


        #endregion \Dependency

        #region EndPoints

        #region GetAllPosts

        [HttpGet]
        public async Task<ActionResult<List<Post>>> GetAllPosts()
        {
            try
            {
                var _posts = await PostRepository.GetAllPostsAsync();

                return _posts;
            }
            catch (Exception ex)
            {
                Logger.LogCritical(ex.Message);
                throw new Exception(ex.Message);
            }
        }

        [HttpGet("{pageId}/{filterPostTite?}")]
        public async Task<ActionResult<ShowPostsViewModel>> GetAllPosts(int pageId,string? filterPostTite)
        {
            try
            {
                ShowPostsViewModel _showPostsViewModel;

                if (string.IsNullOrEmpty(filterPostTite))
                {
                    _showPostsViewModel= await PostRepository.GetAllPostsAsync(pageId);
                }
                else 
                {
                    _showPostsViewModel= await PostRepository.GetAllPostsAsync(pageId, filterPostTite);
                }

                return Ok(_showPostsViewModel);

            }
            catch (Exception ex)
            {
                Logger.LogCritical(ex.Message);
                throw new Exception(ex.Message);
            }
        }

        #endregion \GetAllPosts

        #region GetPost

        [HttpGet("{postId}")]
        public async Task<ActionResult<Post>> GetPost(int postId)
        {
            try
            {
                var _post = await PostRepository.GetPostAsync(postId);

                if (_post == null)
                {
                    return BadRequest();
                }

                return Ok(_post);
            }
            catch (Exception ex)
            {
                Logger.LogCritical(ex.Message);
                throw new Exception(ex.Message);
            }
        }

        #endregion \GetPost

        #region AddPost

        [HttpPost]
        public async Task<ActionResult<Post>> AddPost(AddPostViewModel addPostViewModel)
        {
            try
            {
                if (TryValidateModel(addPostViewModel) == false)
                {
                    return BadRequest();
                }

                if (await PostRepository.IsExistPostAsync(addPostViewModel.Title) == true)
                {
                    var _message = "خبری با این عنوان در سیستم ثبت شده است";

                    return BadRequest(_message);
                }

                var _post = addPostViewModel.ConvertTo_Post();

                _post = await PostRepository.AddPostAsync(_post);

                return CreatedAtAction("GetPost", new { postId = _post.PostId }, _post);
            }
            catch (Exception ex)
            {
                Logger.LogCritical(ex.Message);
                throw new Exception(ex.Message);
            }
        }

        #endregion \AddPost

        #region UpdatePost

        [HttpPatch("{postId}")]
        public async Task<ActionResult<Post>> UpdatePost(int postId, EditPostViewModel editPostViewModel)
        {
            try
            {
                if (await PostRepository.IsExistPostAsync(postId) == false)
                {
                    return NotFound();
                }
                if (TryValidateModel(editPostViewModel) == false)
                {
                    BadRequest();
                }

                var _post = await PostRepository.UpdatePostAsync(postId, editPostViewModel);

                return CreatedAtAction("GetPost", new { postId = _post.PostId }, _post);
            }
            catch (Exception ex)
            {
                Logger.LogCritical(ex.Message);
                throw new Exception(ex.Message);
            }
        }

        #endregion \UpdatePost

        #region DeletePost

        [HttpDelete("{postId}")]
        public async Task<ActionResult> DeletePost(int postId)
        {
            try
            {
                if (await PostRepository.IsExistPostAsync(postId) is false)
                {
                    return NotFound();
                }

                await PostRepository.DeletePostAsync(postId);

                return Ok("Delete is Successfully");
            }
            catch (Exception ex)
            {
                Logger.LogCritical(ex.Message);
                throw new Exception(ex.Message);
            }
        }

        #endregion \DeletePost

        #region GetImage

        [HttpGet("{imagename}")]
        public async Task<IActionResult> GetImage(string imageName)
        {
            try
            {
                var _filePath = Path.Combine(WebHostEnvironment.WebRootPath, "Images/Post", $"{imageName}.jpg");

                byte[] _imageBytes = System.IO.File.ReadAllBytes(_filePath);

                if (_imageBytes is null || _imageBytes.Length == 0)
                {
                    return NotFound();
                }

                var _mimeType = "image/jpg";

                return File(_imageBytes, _mimeType);
            }
            catch (Exception ex)
            {
                Logger.LogCritical(ex.Message);
                throw new Exception(ex.Message);
            }
        }

        #endregion \GetImage

        #region IsExistPost

        [HttpGet("{title}")]
        public async Task<ActionResult<bool>> IsExistPost(string title)
        {
            try
            {
                if (await PostRepository.IsExistPostAsync(title))
                {
                    return Ok(true);
                }
                else
                {
                    return Ok(false);
                }
            }
            catch (Exception ex)
            {
                Logger.LogCritical(ex.Message);
                throw new Exception(ex.Message);
            }
        }

        #endregion \IsExistPost

        #region GetImageName

        [HttpGet("{postId}")]
        public async Task<ActionResult<string>> GetImageName(int postId)
        {
            try
            {
                if (await PostRepository.IsExistPostAsync(postId) is false)
                {
                    return NotFound();
                }

                var _imageName = await PostRepository.GetImageName(postId);

                return Ok(_imageName);
            }
            catch (Exception ex)
            {
                Logger.LogCritical(ex.Message);
                throw new Exception(ex.Message);
            }
        }
        #endregion \GetImageName

        #endregion \EndPoints
    }
}
