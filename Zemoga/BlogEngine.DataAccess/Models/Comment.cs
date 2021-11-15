// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace BlogEngine.DataAccess.Models
{
    public partial class Comment
    {
        public long Id { get; set; }
        public string Description { get; set; }
        public long IdPost { get; set; }

        public virtual Post IdPostNavigation { get; set; }
    }
}
