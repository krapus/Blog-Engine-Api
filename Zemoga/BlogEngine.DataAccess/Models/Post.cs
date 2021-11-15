using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace BlogEngine.DataAccess.Models
{
    public partial class Post
    {
        public Post()
        {
            Comment = new HashSet<Comment>();
        }

        public long Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime Published { get; set; }
        public long IdUser { get; set; }
        public long IdStatus { get; set; }
        public string StatusComment { get; set; }

        public virtual Status IdStatusNavigation { get; set; }
        public virtual User IdUserNavigation { get; set; }
        public virtual ICollection<Comment> Comment { get; set; }
    }
}
