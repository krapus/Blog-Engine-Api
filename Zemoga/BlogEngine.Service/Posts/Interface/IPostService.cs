using BlogEngine.Model.Request;
using BlogEngine.Model.Response;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlogEngine.Service.Posts.Interface
{
    public interface IPostService
    {
        Task<PostCreationResponse> CreatePost(PostRequest postRequest, long userId);
        Task<List<PostResponse>> GetAllPostPublished();
        Task<List<PostResponse>> GetAllPost(long userId, string rol);
        Task<PostEditionResponse> EditPost(PostRequest postRequest, long postId);
        Task<PostEditionResponse> EditPost(PostRejectRequest postRejectRequest, long postId);
        Task<PostEditionResponse> EditPost(long postId, long statusId);
    }
}
