﻿using Infrastructure;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using Repositories;
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

        public PostsController(IPostRepository postRepository, IWebHostEnvironment webHostEnvironment)
        {
            PostRepository = postRepository;
            WebHostEnvironment = webHostEnvironment;
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

        #region GetPost

        [HttpGet("{postId}")]
        public async Task<ActionResult<Post>> GetPost(int postId)
        {
            var _post = await PostRepository.GetPostAsync(postId);

            if (_post == null)
            {
                return BadRequest();
            }

            return Ok(_post);
        }

        #endregion \GetPost

        #region AddPost

        [HttpPost]
        public async Task<ActionResult<Post>> AddPost(AddPostViewModel addPostViewModel)
        {
            if (TryValidateModel(addPostViewModel) == false)
            {
                return BadRequest();
            }

            if (await PostRepository.IsExistPostAsync(addPostViewModel.Title) == true)
            {
                var _errorMessage = new ErrroMessageViewModel
                {
                    Message = "This Post is exist"
                };

                return BadRequest(_errorMessage);
            }

            var _post = addPostViewModel.AddPostViewModel_ConvertTo_Post();

            _post = await PostRepository.AddPostAsync(_post);

            return CreatedAtAction("GetPost", new { postId = _post.PostId }, _post);
        }

        #endregion \AddPost

        #region GetImage

        [HttpGet("{imagename}")]
        public async Task<IActionResult> GetImage(string imageName)
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

        #endregion \GetImage

        #region IsExistPost

        [HttpGet("{title}")]
        public async Task<ActionResult<bool>> IsExistPost(string title)
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

        #endregion

        #endregion \Actions
    }
}