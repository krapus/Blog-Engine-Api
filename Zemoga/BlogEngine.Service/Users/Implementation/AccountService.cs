using BlogEngine.DataAccess.ExternalServices.Users.Interface;
using BlogEngine.DataAccess.Repository.Roles.Interface;
using BlogEngine.DataAccess.Repository.Users.Interface;
using BlogEngine.Model.Request;
using BlogEngine.Model.Response;
using BlogEngine.Model.Settings;
using BlogEngine.Service.Users.Interface;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BlogEngine.Service.Users.Implementation
{
    public class AccountService : IAccountService
    {
        private IAccountExternalService _accountExternalService;
        private IUserRepository _userRepository;
        private IRolRepository _rolRepository;
        private readonly IOptions<AppSettings> _settings;

        public AccountService(IAccountExternalService accountExternalService,
            IUserRepository userRepository,
            IRolRepository rolRepository,
            IOptions<AppSettings> settings)
        {
            _accountExternalService = accountExternalService;
            _userRepository = userRepository;
            _rolRepository = rolRepository;
            _settings = settings;
        }

        public async Task<UserSignUpExternalResponse> SignInUser(UserSignUpRequest user)
        {
            var userSignIn = new UserSignUpExternalRequest() { Email = user.Email, Password = user.Password, ReturnSecureToken = true };
            var respose = await _accountExternalService.SignInUser(userSignIn);
            if (respose.Error == null)
            {
                var userRepository = await _userRepository.GetUserByEmail(respose.Email);
                var rolUser = await _rolRepository.Get(userRepository.IdRol);
                respose.Token = GenerateJwtToken(respose.Email, rolUser.Name, userRepository.Id);
            }
            return respose;
        }

        public async Task<UserSignUpExternalResponse> SignUpUser(UserSignUpRequest user)
        {
            var userSignUp = new UserSignUpExternalRequest()
            {
                Email = user.Email,
                Password = user.Password,
                ReturnSecureToken = true
            };
            var respose = await _accountExternalService.SignUpUser(userSignUp);
            if (respose.Error == null)
            {
                var resposeUserRepository = await _userRepository.CreateUser(
                    new DataAccess.Models.User()
                    {
                        Uid = respose.LocalId,
                        Email = respose.Email,
                        IdRol = user.Rol
                    }
                    );
                if (resposeUserRepository == 0)
                {
                    respose.Error = new Error() { Code = 500, Message = _settings.Value.Messages.UserCreationFailed };
                }
            }
            return respose;
        }

        private string GenerateJwtToken(string email, string rol, long userId)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenSecret = Encoding.ASCII.GetBytes(Environment.GetEnvironmentVariable("TOKEN_SECRET"));
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                        new Claim(ClaimTypes.Name, email),
                        new Claim(ClaimTypes.Role, rol),
                        new Claim("user_id", userId.ToString())
                }),
                Expires = DateTime.UtcNow.AddHours(_settings.Value.TokenExpirationTime),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenSecret), SecurityAlgorithms.HmacSha256)
            };

            return tokenHandler.WriteToken(tokenHandler.CreateToken(tokenDescriptor));
        }
    }
}
