using BlogEngine.Model.Request;
using BlogEngine.Model.Response;
using BlogEngine.Service.Comments.Interface;
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
    public class CommentsController : ControllerBase
    {
        private readonly ICommentService _commentService;
        public CommentsController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> CreateComment(CommentRequest comment)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var response = await _commentService.CreateComment(comment);
                    if (response.Error == null)
                    {
                        return StatusCode(StatusCodes.Status200OK, response);
                    }
                    return StatusCode(response.Error.Code, response);
                }

                return StatusCode(StatusCodes.Status400BadRequest,
                new CommentCreationResponse()
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
