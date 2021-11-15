using BlogEngine.DataAccess.Models;
using BlogEngine.DataAccess.Repository.Users.Interface;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace BlogEngine.DataAccess.Repository.Users.Implementation
{
    public class UserRepository : IUserRepository
    {
        private ZemogaBlogEngineContext _zemogaBlogEngineContext;
        public UserRepository(ZemogaBlogEngineContext zemogaBlogEngineContext)
        {
            _zemogaBlogEngineContext = zemogaBlogEngineContext;
        }
        public async Task<int> CreateUser(User user)
        {
            await _zemogaBlogEngineContext.User.AddAsync(user);
            return await _zemogaBlogEngineContext.SaveChangesAsync();
        }

        public async Task<User> GetUserByEmail(string email)
        {
            return await _zemogaBlogEngineContext.User.FirstOrDefaultAsync(u => u.Email == email);
        }
    }
}
