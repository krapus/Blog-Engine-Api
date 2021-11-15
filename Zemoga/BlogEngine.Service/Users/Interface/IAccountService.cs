using BlogEngine.Model.Request;
using BlogEngine.Model.Response;
using System.Threading.Tasks;

namespace BlogEngine.Service.Users.Interface
{
    public interface IAccountService
    {
        Task<UserSignUpExternalResponse> SignUpUser(UserSignUpRequest user);
        Task<UserSignUpExternalResponse> SignInUser(UserSignUpRequest user);
    }
}