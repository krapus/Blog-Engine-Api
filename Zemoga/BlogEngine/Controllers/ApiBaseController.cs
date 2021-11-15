using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BlogEngine.Controllers
{
    public class ApiBaseController : ControllerBase
    {
        protected long GetUserId()
        {
            if (HttpContext.User.Identity is ClaimsIdentity identity)
            {
                return long.Parse(identity.FindFirst("user_id").Value);
            }
            return 0;
        }

        protected string GetUserRol()
        {
            if (HttpContext.User.Identity is ClaimsIdentity identity)
            {
                return identity.FindFirst(ClaimTypes.Role).Value;
            }
            return string.Empty;
        }
    }
}
