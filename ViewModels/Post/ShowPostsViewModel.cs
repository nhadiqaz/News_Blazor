using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels
{
    public class ShowPostsViewModel
    {
        public List<Post> Posts { get; set; }
        public int CurrentPage { get; set; }
        public int PageCount { get; set; }
    }
}
