using Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels
{
    public class LogInUserViewModel
    {
        #region Email

        [Required
            (ErrorMessageResourceType =typeof(ErrorMessage),
            ErrorMessageResourceName =nameof(ErrorMessage.Required))]

        [MaxLength
            (length:30,
            ErrorMessageResourceType =typeof(ErrorMessage),
            ErrorMessageResourceName =nameof(ErrorMessage.Required))]
        
        public string Email { get; set; }

        #endregion \Email

        #region Password

        [Required
            (ErrorMessageResourceType =typeof(ErrorMessage),
            ErrorMessageResourceName =nameof(ErrorMessage))]

        [MaxLength
            (length:30,
            ErrorMessageResourceType =typeof(ErrorMessage),
            ErrorMessageResourceName =nameof(ErrorMessage.MaxLength))]

        [MinLength
            (length:6,
            ErrorMessageResourceType =typeof(ErrorMessage),
            ErrorMessageResourceName =nameof(ErrorMessage.MinLength))]
        public string Password { get; set; }

        #endregion \Password
    }
}
