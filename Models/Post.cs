using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Post
    {
        public Post(string title,string shortDescription,string description,string imageName,string tags)
        {
            Title = title;
            ShortDescription = shortDescription;
            Description = description;
            ImageName = imageName;
            Tags = tags;
            CreateDate = DateTime.Now;
        }
        public int PostId { get; set; }
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
        public string ImageName { get; set; }
        public string Tags { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
