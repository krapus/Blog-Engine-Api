using BlogEngine.CustomAttributes;
using BlogEngine.Model.Constants;
using BlogEngine.Model.Request;
using BlogEngine.Model.Response;
using BlogEngine.Service.Posts.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using System.Threading.Tasks;

namespace BlogEngine.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces(MediaTypeNames.Application.Json)]
    public class PostsController : ApiBaseController
    {
        private readonly IPostService _postService;
        public PostsController(IPostService postService)
        {
            _postService = postService;
        }

        [Authorize(Roles = Role.Writer)]
        [HttpPost]
        public async Task<IActionResult> CreatePost(PostRequest postRequest)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var response = await _postService.CreatePost(postRequest, GetUserId());
                    if (response.Error == null)
                    {
                        return StatusCode(StatusCodes.Status200OK, response);
                    }
                    return StatusCode(response.Error.Code, response);
                }
                return StatusCode(StatusCodes.Status400BadRequest,
                new PostCreationResponse()
                {
                    Error = new Error()
                    {
                        Message = ModelState.Values.ToString()
                    }
                });

            }
            catch (System.Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        [AuthorizeRoles(Role.Writer, Role.Editor)]
        [HttpGet]
        public async Task<IActionResult> GetAllPost()
        {
            try
            {
                var response = await _postService.GetAllPost(GetUserId(), GetUserRol());
                return StatusCode(StatusCodes.Status200OK, response);
            }
            catch (System.Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("published")]
        public async Task<IActionResult> GetAllPostPublished()
        {
            try
            {
                var response = await _postService.GetAllPostPublished();
                return StatusCode(StatusCodes.Status200OK, response);
            }
            catch (System.Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [Authorize(Roles = Role.Editor)]
        [HttpPut]
        [Route("approved/{idPost}")]
        public async Task<IActionResult> ApprovePost(long idPost)
        {
            try
            {
                var response = await _postService.EditPost(idPost, Status.Published);
                if (response.Error == null)
                {
                    return StatusCode(StatusCodes.Status200OK, response);
                }
                return StatusCode(response.Error.Code, response);
            }
            catch (System.Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [Authorize(Roles = Role.Editor)]
        [HttpPut]
        [Route("rejected/{idPost}")]
        public async Task<IActionResult> RejectPost(PostRejectRequest postRejectRequest, long idPost)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var response = await _postService.EditPost(postRejectRequest, idPost);
                    if (response.Error == null)
                    {
                        return StatusCode(StatusCodes.Status200OK, response);
                    }
                    return StatusCode(response.Error.Code, response);
                }
                return StatusCode(StatusCodes.Status400BadRequest,
                new PostEditionResponse()
                {
                    Error = new Error()
                    {
                        Message = ModelState.Values.ToString()
                    }
                });
            }
            catch (System.Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [Authorize(Roles = Role.Writer)]
        [HttpPut]
        [Route("{idPost}")]
        public async Task<IActionResult> UpdatePost(PostRequest postRequest, long idPost)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var response = await _postService.EditPost(postRequest, idPost);
                    if (response.Error == null)
                    {
                        return StatusCode(StatusCodes.Status200OK, response);
                    }
                    return StatusCode(response.Error.Code, response);
                }

                return StatusCode(StatusCodes.Status400BadRequest,
                new PostEditionResponse()
                {
                    Error = new Error()
                    {
                        Message = ModelState.Values.ToString()
                    }
                });
            }
            catch (System.Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
