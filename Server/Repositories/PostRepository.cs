
using Data;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Repositories
{
    public class PostRepository : IPostRepository
    {
        #region Dependency
        public MyApplicationDbContext Context { get; }

        public PostRepository(MyApplicationDbContext context)
        {
            Context = context;
        }

        #endregion \Dependency


        #region Methods

        #region GetAllPostsAsync

        public async Task<List<Post>> GetAllPostsAsync()
        {
            var _posts = await Context.Posts.ToListAsync();

            return _posts;
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
                throw ex;
            }
        }


        #endregion \AddPostAsync

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
                throw ex;
            }
        }
        #endregion IsExistPostAsync

        #endregion \Methods
    }
}
