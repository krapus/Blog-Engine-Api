using BlogEngine.DataAccess.Models;
using System.Threading.Tasks;

namespace BlogEngine.DataAccess.Repository.Comments.Interface
{
    public interface ICommentRepository
    {
        Task<int> CreateComment(Comment comment);
    }
}
