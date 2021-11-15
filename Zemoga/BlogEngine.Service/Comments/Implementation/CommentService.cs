using BlogEngine.DataAccess.Repository.Comments.Interface;
using BlogEngine.DataAccess.Repository.Posts.Interface;
using BlogEngine.Model.Constants;
using BlogEngine.Model.Request;
using BlogEngine.Model.Response;
using BlogEngine.Model.Settings;
using BlogEngine.Service.Comments.Interface;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;

namespace BlogEngine.Service.Comments.Implementation
{
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IPostRepository _postRepository;
        private readonly IOptions<AppSettings> _settings;
        public CommentService(ICommentRepository commentRepository, IPostRepository postRepository, IOptions<AppSettings> settings)
        {
            _commentRepository = commentRepository;
            _postRepository = postRepository;
            _settings = settings;
        }

        public async Task<CommentCreationResponse> CreateComment(CommentRequest comment)
        {
            var postEntity = await _postRepository.GetPost(comment.PostId);
            if (postEntity.IdStatus != Status.Published)
            {
                return new CommentCreationResponse()
                {
                    Error = new Error()
                    {
                        Code = 400,
                        Message = _settings.Value.Messages.CommentCreationFailed
                    }
                };
            }

            var commentRepository = await _commentRepository
                .CreateComment(new DataAccess.Models.Comment()
                {
                    IdPost = comment.PostId,
                    Description = comment.Description
                });

            return commentRepository == 0 ?
            new CommentCreationResponse()
            {
                Error = new Error()
                {
                    Code = 500,
                    Message = _settings.Value.Messages.CommentCreationFailed
                }
            } :
            new CommentCreationResponse()
            {
                Message = _settings.Value.Messages.CommentCreationSucessfull
            };
        }
    }
}
