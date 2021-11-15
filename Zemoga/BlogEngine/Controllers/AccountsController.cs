using BlogEngine.Model.Request;
using BlogEngine.Model.Response;
using BlogEngine.Service.Users.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net.Mime;
using System.Threading.Tasks;

namespace BlogEngine.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces(MediaTypeNames.Application.Json)]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountsController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [Route("signUp")]
        [HttpPost]
        public async Task<IActionResult> SignUpUser(UserSignUpRequest user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var respose = await _accountService.SignUpUser(user);
                    if (respose.Error == null)
                    {
                        return StatusCode(StatusCodes.Status200OK, respose);
                    }
                    return StatusCode(respose.Error.Code, respose);
                }
                return StatusCode(StatusCodes.Status400BadRequest,
                    new UserSignUpExternalResponse()
                    {
                        Error = new Error()
                        {
                            Message = ModelState.Values.ToString()
                        }
                    });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [Route("signin")]
        [HttpPost]
        public async Task<IActionResult> SignInUser(UserSignUpRequest user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var respose = await _accountService.SignInUser(user);
                    if (respose.Error == null)
                    {
                        return StatusCode(StatusCodes.Status200OK, respose);
                    }
                    return StatusCode(respose.Error.Code, respose);
                }

                return StatusCode(StatusCodes.Status400BadRequest,
                new UserSignUpExternalResponse()
                {
                    Error = new Error()
                    {
                        Message = ModelState.Values.ToString()
                    }
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
