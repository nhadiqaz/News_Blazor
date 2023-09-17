
using Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;
using ViewModels;

namespace Repositories
{
    public class PostRepository : IPostRepository
    {
        #region Dependency
        public MyApplicationDbContext Context { get; }
        public ILogger<PostRepository> Logger { get; }

        public PostRepository(MyApplicationDbContext context,ILogger<PostRepository> logger)
        {
            Context = context;
            Logger = logger;
        }

        #endregion \Dependency

        #region Methods

        #region GetAllPostsAsync

        //public async Task<List<Post>> GetAllPostsAsync()
        //{
        //    var _posts = await Context.Posts.ToListAsync();

        //    return _posts;
        //}

        public async Task<ShowPostsViewModel> GetAllPostsAsync(int pageId = 1)
        {
            try
            {
                //var _posts = Context.Posts;
                IQueryable<Post> _posts = Context.Posts;

                var _take = 5;
                var _skip = (pageId - 1) * _take;

                var _showPostsViewModel = new ShowPostsViewModel
                {
                    Posts = await _posts.OrderBy(p => p.CreateDate).Skip(_skip).Take(_take).ToListAsync(),
                    CurrentPage = pageId,
                    PageCount = ((_posts.Count() % _take == 0) ? (_posts.Count() / _take) : (_posts.Count() / _take + 1))
                };

                return _showPostsViewModel;
            }
            catch (Exception ex)
            {
                Logger.LogCritical(ex.Message);
                throw new Exception(ex.Message);
            }
        }

        #endregion \GetAllPostsAsync

        #region GetPostAsync

        public async Task<Post> GetPostAsync(int postId)
        {
            try
            {
                var _post = await Context.Posts
                                         .FindAsync(postId);

                return _post;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion \GetPostAsync

        #region AddPostAsync

        public async Task<Post> AddPostAsync(Post post)
        {
            try
            {
                await Context.Posts.AddAsync(post);
                await Context.SaveChangesAsync();

                return post;
            }
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync(ex.Message);
                throw ex;
            }
        }


        #endregion \AddPostAsync

        #region UpdatePostAsync

        public async Task<Post> UpdatePostAsync(int postId, EditPostViewModel editPostViewModel)
        {
            try
            {
                var _post = await GetPostAsync(postId);

                if (_post is null)
                {
                    return null;
                }

                await Task.Run(() =>
                {
                    _post.Title = editPostViewModel.Title;
                    _post.ShortDescription = editPostViewModel.ShrotDescription;
                    _post.Description = editPostViewModel.Description;
                    _post.ImageName = editPostViewModel.ImageName;
                    _post.Tags = editPostViewModel.Tags;

                    Context.Posts.Update(_post);
                });

                await Context.SaveChangesAsync();

                return _post;
            }
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync(ex.Message);
                throw ex;
            }
        }

        #endregion \UpdatePostAsync

        #region IsExistPostAsync

        public async Task<bool> IsExistPostAsync(string title)
        {
            try
            {
                return await Context.Posts
                              .AnyAsync(p => p.Title == title);
            }
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync(ex.Message);
                throw ex;
            }
        }

        public async Task<bool> IsExistPostAsync(int postId)
        {
            try
            {
                return await Context.Posts
                                    .AnyAsync(p => p.PostId == postId);
            }
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync(ex.Message);
                throw ex;
            }
        }

        #endregion IsExistPostAsync

        #region DeletePostAsync

        public async Task DeletePostAsync(int postId)
        {
            try
            {
                var _post = await GetPostAsync(postId);

                await Task.Run(() =>
                {
                    Context.Posts.Remove(_post);
                });

                await Context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync(ex.Message);
                throw new Exception(ex.Message);
            }
        }

        #endregion \DeletePostAsync

        #region GetImageName

        public async Task<string> GetImageName(int postId)
        {
            var _imageName = Context.Posts
                                    .Where(p => p.PostId == postId)
                                    .Select(p => p.ImageName)
                                    .SingleOrDefault();

            return _imageName;
        }

        #endregion \GetImageName


        #endregion \Methods
    }
}
