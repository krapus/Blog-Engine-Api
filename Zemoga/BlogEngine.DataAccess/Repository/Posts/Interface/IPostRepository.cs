using BlogEngine.DataAccess.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlogEngine.DataAccess.Repository.Posts.Interface
{
    public interface IPostRepository
    {
        Task<IEnumerable<Post>> GetPosts();
        Task<Post> GetPost(long id);
        Task<IEnumerable<Post>> GetPostsByUserId(long userId);
        Task<IEnumerable<Post>> GetPostsByStatusId(long statusId);
        Task<int> CreatePost(Post post);
        Task<int> UpdatePost(Post post, long idPost);
        Task<int> UpdatePostStatus(long idPost, long statusId);
        Task<int> UpdatePostStatusAndComment(long idPost, long statusId, string statusComment);
    }
}
