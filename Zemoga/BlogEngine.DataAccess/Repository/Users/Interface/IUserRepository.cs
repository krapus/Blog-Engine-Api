using BlogEngine.DataAccess.Models;
using System.Threading.Tasks;

namespace BlogEngine.DataAccess.Repository.Users.Interface
{
    public interface IUserRepository
    {
        Task<int> CreateUser(User user);
        Task<User> GetUserByEmail(string email);
    }
}
