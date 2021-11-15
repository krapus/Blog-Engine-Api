using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace BlogEngine.DataAccess.Models
{
    public partial class User
    {
        public User()
        {
            Post = new HashSet<Post>();
        }

        public long Id { get; set; }
        public string Uid { get; set; }
        public string Email { get; set; }
        public long IdRol { get; set; }

        public virtual Rol IdRolNavigation { get; set; }
        public virtual ICollection<Post> Post { get; set; }
    }
}
