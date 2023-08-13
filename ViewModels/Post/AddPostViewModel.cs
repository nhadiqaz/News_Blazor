﻿using Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels
{
    public class AddPostViewModel
    {
        #region Title
        [Required
            (ErrorMessageResourceType = typeof(ErrorMessage),
            ErrorMessageResourceName = nameof(ErrorMessage.Required))]
        [MaxLength
            (100,
            ErrorMessageResourceType = typeof(ErrorMessage),
            ErrorMessageResourceName = nameof(ErrorMessage.MaxLength))]
        public string Title { get; set; }

        #endregion \Title

        #region ShrotDescription

        [Required
                    (ErrorMessageResourceType = typeof(ErrorMessage),
                    ErrorMessageResourceName = nameof(ErrorMessage.Required))]
        [MaxLength
                    (150,
                    ErrorMessageResourceType = typeof(ErrorMessage),
                    ErrorMessageResourceName = nameof(ErrorMessage.MaxLength))]
        public string ShrotDescription { get; set; }

        #endregion \ShrotDescription

        #region Description
        [Required
                    (ErrorMessageResourceType = typeof(ErrorMessage),
                    ErrorMessageResourceName = nameof(ErrorMessage.Required))]
        public string Description { get; set; }

        #endregion \Description

        #region ImageName
        [Required
                    (ErrorMessageResourceType = typeof(ErrorMessage),
                    ErrorMessageResourceName = nameof(ErrorMessage.Required))]
        [MaxLength
                    (100,
                    ErrorMessageResourceType = typeof(ErrorMessage),
                    ErrorMessageResourceName = nameof(ErrorMessage.MaxLength))]
        public string ImageName { get; set; }

        #endregion \ImageName

        #region Tags
        [Required
        (ErrorMessageResourceType = typeof(ErrorMessage),
            ErrorMessageResourceName = nameof(ErrorMessage.Required))]
        [MaxLength
            (100,
            ErrorMessageResourceType = typeof(ErrorMessage),
            ErrorMessageResourceName = nameof(ErrorMessage.MaxLength))]
        public string Tags { get; set; }

        #endregion \Tags

    }
}
