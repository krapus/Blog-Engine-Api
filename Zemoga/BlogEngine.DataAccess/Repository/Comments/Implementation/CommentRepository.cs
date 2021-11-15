using BlogEngine.DataAccess.Models;
using BlogEngine.DataAccess.Repository.Comments.Interface;
using System.Threading.Tasks;

namespace BlogEngine.DataAccess.Repository.Comments.Implementation
{
    public class CommentRepository : ICommentRepository
    {

        private readonly ZemogaBlogEngineContext _zemogaBlogEngineContext;
        public CommentRepository(ZemogaBlogEngineContext zemogaBlogEngineContext)
        {
            _zemogaBlogEngineContext = zemogaBlogEngineContext;
        }
        public async Task<int> CreateComment(Comment comment)
        {
            await _zemogaBlogEngineContext.Comment.AddAsync(comment);
            return await _zemogaBlogEngineContext.SaveChangesAsync();
        }
    }
}
