
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

        #endregion \Methods
    }
}
