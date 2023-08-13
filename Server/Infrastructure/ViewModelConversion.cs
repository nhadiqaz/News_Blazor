using Models;
using ViewModels;

namespace Infrastructure
{
    public static class ViewModelConversion
    {
        #region Post

        #region AddPostViewModel_ConvertTo_Post
        public static Post AddPostViewModel_ConvertTo_Post(this AddPostViewModel addPostViewModel)
        {
            var _post = new Post(
                                addPostViewModel.Title,
                                addPostViewModel.ShrotDescription,
                                addPostViewModel.Description,
                                addPostViewModel.ImageName,
                                addPostViewModel.Tags);
            return _post;
            
        }
        #endregion \AddPostViewModel_ConvertTo_Post

        #endregion \Post
    }
}
