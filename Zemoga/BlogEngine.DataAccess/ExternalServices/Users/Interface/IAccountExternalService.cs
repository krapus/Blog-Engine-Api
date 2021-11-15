using BlogEngine.Model.Request;
using BlogEngine.Model.Response;
using System.Threading.Tasks;

namespace BlogEngine.DataAccess.ExternalServices.Users.Interface
{
    public interface IAccountExternalService
    {
        Task<UserSignUpExternalResponse> SignUpUser(UserSignUpExternalRequest user);
        Task<UserSignUpExternalResponse> SignInUser(UserSignUpExternalRequest user);
    }
}
