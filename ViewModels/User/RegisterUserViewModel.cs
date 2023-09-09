using Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels
{
    public class RegisterUserViewModel
    {
        #region Email
        
        [Required
            (AllowEmptyStrings =false,
            ErrorMessageResourceType =typeof(ErrorMessage),
            ErrorMessageResourceName =nameof(ErrorMessage.Required))]
        [MaxLength
            (length:30,
            ErrorMessageResourceType =typeof(ErrorMessage),
            ErrorMessageResourceName =nameof(ErrorMessage))]

        [EmailAddress
            (ErrorMessageResourceType =typeof(ErrorMessage),
            ErrorMessageResourceName =nameof(ErrorMessage.Email))]
        public string Email { get; set; }

        #endregion \Email

        #region Password

        [Required
            (AllowEmptyStrings =false,
            ErrorMessageResourceType =typeof(ErrorMessage),
            ErrorMessageResourceName =nameof(ErrorMessage.Required))]

        [MaxLength
            (length:30,
            ErrorMessageResourceType =typeof(ErrorMessage),
            ErrorMessageResourceName =nameof(ErrorMessage.MaxLength))]

        [MinLength
            (length:6,
            ErrorMessageResourceType =typeof(ErrorMessage),
            ErrorMessageResourceName =nameof(ErrorMessage.MinLength))]

        public string Password { get; set; }

        #endregion \Passworrd

        #region ConfirmPassworrd

        [Required
            (AllowEmptyStrings =false,
            ErrorMessageResourceType =typeof(ErrorMessage),
            ErrorMessageResourceName =nameof(ErrorMessage.Required))]

        [MaxLength
            (length:30,
            ErrorMessageResourceType =typeof(ErrorMessage),
            ErrorMessageResourceName =nameof(ErrorMessage.MaxLength)
            )]

        [MinLength
            (length:6,
            ErrorMessageResourceType =typeof(ErrorMessage),
            ErrorMessageResourceName =nameof(ErrorMessage.MinLength))]

        [Compare
            (otherProperty:"Password",
            ErrorMessageResourceType =typeof(ErrorMessage),
            ErrorMessageResourceName =nameof(ErrorMessage.ConfrimPassword))]

        public string ConfirmPassworrd { get; set; }

        #endregion \ConfirmPassworrd

        #region Firstname

        [MaxLength
            (length:30,
            ErrorMessageResourceType =typeof(ErrorMessage),
            ErrorMessageResourceName =nameof(ErrorMessage.MaxLength))]
        public string Firstname { get; set; }

        #endregion \Firstname

        #region Lastname

        [MaxLength
            (length:30,
            ErrorMessageResourceType =typeof(ErrorMessage),
            ErrorMessageResourceName =nameof(ErrorMessage.MaxLength))]
        public string Lastname { get; set; }

        #endregion \Lastname
    }
}
