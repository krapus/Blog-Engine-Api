using BlogEngine.DataAccess.Models;
using BlogEngine.DataAccess.Repository.Posts.Interface;
using BlogEngine.Model.Constants;
using BlogEngine.Model.Request;
using BlogEngine.Model.Response;
using BlogEngine.Model.Settings;
using BlogEngine.Service.Posts.Interface;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogEngine.Service.Posts.Implementation
{
    public class PostService : IPostService
    {
        private readonly IPostRepository _postRepository;
        private readonly IOptions<AppSettings> _settings;

        public PostService(IPostRepository postRepository, IOptions<AppSettings> settings)
        {
            _postRepository = postRepository;
            _settings = settings;
        }

        public async Task<PostCreationResponse> CreatePost(PostRequest postRequest, long userId)
        {
            var resposeRepository = await _postRepository.CreatePost(new Post()
            {
                Title = postRequest.Title,
                Content = postRequest.Content,
                Published = DateTime.Now,
                IdUser = userId,
                IdStatus = Model.Constants.Status.PendingApproval
            });
            return resposeRepository == 0 ?
            new PostCreationResponse()
            {
                Error = new Error()
                {
                    Code = 500,
                    Message = _settings.Value.Messages.PostCreationFailed
                }
            } :
            new PostCreationResponse()
            {
                Message = _settings.Value.Messages.PostCreationSucessfull
            };
        }

        public async Task<PostEditionResponse> EditPost(PostRequest postRequest, long postId)
        {
            var invalidStatus = new List<long>()
            {
                Model.Constants.Status.Published,
                Model.Constants.Status.PendingApproval
            };

            var postEntity = await _postRepository.GetPost(postId);
            if (postEntity == null)
            {
                return new PostEditionResponse()
                {
                    Error = new Error()
                    {
                        Code = 404,
                        Message = _settings.Value.Messages.PostEditionNotFound
                    }
                };
            }

            if (invalidStatus.Contains(postEntity.IdStatus))
            {
                return new PostEditionResponse()
                {
                    Error = new Error()
                    {
                        Code = 400,
                        Message = _settings.Value.Messages.PostEditionBadRequest
                    }
                };
            }

            var resposePostEditionRepository = await _postRepository
                .UpdatePost(new Post()
                {
                    Content = postRequest.Content,
                    Title = postRequest.Title
                }, postId);


            return resposePostEditionRepository == 0 ?
            new PostEditionResponse()
            {
                Error = new Error()
                {
                    Code = 500,
                    Message = _settings.Value.Messages.PostEditionFailed
                }
            } :
            new PostEditionResponse()
            {
                Message = _settings.Value.Messages.PostEditionSucessfull
            };
        }


        public async Task<PostEditionResponse> EditPost(long postId, long statusId)
        {
            var postEntity = await _postRepository.GetPost(postId);
            if (postEntity == null)
            {
                return new PostEditionResponse()
                {
                    Error = new Error()
                    {
                        Code = 404,
                        Message = _settings.Value.Messages.PostEditionNotFound
                    }
                };
            }

            var resposePostEditionRepository = await _postRepository
                .UpdatePostStatus(postId, statusId);


            return resposePostEditionRepository == 0 ?
            new PostEditionResponse()
            {
                Error = new Error()
                {
                    Code = 500,
                    Message = _settings.Value.Messages.PostEditionFailed
                }
            } :
            new PostEditionResponse()
            {
                Message = _settings.Value.Messages.PostEditionSucessfull
            };
        }

        public async Task<PostEditionResponse> EditPost(PostRejectRequest postRejectRequest, long postId)
        {
            var postEntity = await _postRepository.GetPost(postId);
            if (postEntity == null)
            {
                return new PostEditionResponse()
                {
                    Error = new Error()
                    {
                        Code = 404,
                        Message = _settings.Value.Messages.PostEditionNotFound
                    }
                };
            }

            var resposePostEditionRepository = await _postRepository
                .UpdatePostStatusAndComment(postId, Model.Constants.Status.Rejected, postRejectRequest.StatusComment);

            return resposePostEditionRepository == 0 ?
            new PostEditionResponse()
            {
                Error = new Error()
                {
                    Code = 500,
                    Message = _settings.Value.Messages.PostEditionFailed
                }
            } :
            new PostEditionResponse()
            {
                Message = _settings.Value.Messages.PostEditionSucessfull
            };
        }

        public async Task<List<PostResponse>> GetAllPost(long userId, string rol)
        {
            var response = new List<PostResponse>();
            if (rol == Role.Writer && userId > 0)
            {
                var postsRepository = await _postRepository.GetPostsByUserId(userId);
                response = postsRepository.Select(p =>
                new PostResponse()
                {
                    Id = p.Id,
                    Content = p.Content,
                    Title = p.Title,
                    Published = p.Published,
                    StatusComment = p.StatusComment,
                    Comments = p.Comment
                    .Select(c => new PostComment()
                    {
                        Id = c.Id,
                        Description = c.Description
                    })
                    .ToList()
                }).ToList();
            }
            else if (rol == Role.Editor && userId > 0)
            {
                var postsRepository = await _postRepository.GetPostsByStatusId(Model.Constants.Status.PendingApproval);
                response = postsRepository.Select(p =>
                new PostResponse()
                {
                    Id = p.Id,
                    Content = p.Content,
                    Title = p.Title,
                    Published = p.Published,
                    Comments = p.Comment
                    .Select(c => new PostComment()
                    {
                        Id = c.Id,
                        Description = c.Description
                    })
                    .ToList()
                }).ToList();
            }
            return response;
        }

        public async Task<List<PostResponse>> GetAllPostPublished()
        {
            var allPostPublishedEntity = await _postRepository.GetPostsByStatusId(Model.Constants.Status.Published);
            return allPostPublishedEntity.Select(p =>
            new PostResponse()
            {
                Id = p.Id,
                Content = p.Content,
                Title = p.Title,
                Published = p.Published,
                Comments = p.Comment
                .Select(c => new PostComment()
                {
                    Id = c.Id,
                    Description = c.Description
                }).ToList()
            }).ToList();
        }

    }
}

