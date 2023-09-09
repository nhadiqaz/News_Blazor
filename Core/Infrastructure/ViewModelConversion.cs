using Generator;
using Generator;
using Models;
using System.Net.Http.Headers;
using ViewModels;

namespace Infrastructure
{
    public static class ViewModelConversion
    {
        #region Post

        #region AddPostViewModel ConvertTo Post
        public static Post ConvertTo_Post(this AddPostViewModel addPostViewModel)
        {
            var _post = new Post(
                                addPostViewModel.Title,
                                addPostViewModel.ShrotDescription,
                                addPostViewModel.Description,
                                addPostViewModel.ImageName,
                                addPostViewModel.Tags);
            return _post;
            
        }
        #endregion \AddPostViewModel ConvertTo Post

        #region Post Convert EditPostViewModel

        public static EditPostViewModel ConvertTo_EditPostViewModel(this Post post)
        {
            var _editPostViewModel = new EditPostViewModel
            {
                PostId = post.PostId,
                Title = post.Title,
                ShrotDescription = post.ShortDescription,
                Description = post.Description,
                Tags = post.Tags,
                ImageName = post.ImageName,
            };

            return _editPostViewModel;
        }

        #endregion \Post ConvertTo EditPostViewModel

        #endregion \Post

        #region User

        #region RegisterUserViewModel ConvertTo User

        public static User ConvertTo_User(this RegisterUserViewModel registerUserViewModel)
        {
            HashGenerator.CreatePasswordHash(registerUserViewModel.Password, out var passwordHash, out var passwordSalt);

            var _user = new User(registerUserViewModel.Email, passwordHash, passwordSalt)
            {
                FirstName = registerUserViewModel.Firstname,
                LastName = registerUserViewModel.Lastname
            };

            return _user;
        }
        #endregion \RegisterUserViewModel ConvertTo User

        #endregion \User
    }
}
