using BlogEngine.Model.Request;
using BlogEngine.Model.Response;
using System.Threading.Tasks;

namespace BlogEngine.Service.Comments.Interface
{
    public interface ICommentService
    {
        Task<CommentCreationResponse> CreateComment(CommentRequest comment);
    }
}
