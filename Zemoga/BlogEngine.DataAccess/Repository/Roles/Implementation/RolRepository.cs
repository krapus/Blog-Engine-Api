using BlogEngine.DataAccess.Models;
using BlogEngine.DataAccess.Repository.Roles.Interface;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace BlogEngine.DataAccess.Repository.Roles.Implementation
{
    public class RolRepository : IRolRepository
    {

        private ZemogaBlogEngineContext _zemogaBlogEngineContext;
        public RolRepository(ZemogaBlogEngineContext zemogaBlogEngineContext)
        {
            _zemogaBlogEngineContext = zemogaBlogEngineContext;
        }
        public Task<Rol> Get(long id)
        {
            return _zemogaBlogEngineContext.Rol.FirstOrDefaultAsync(r => r.Id == id);
        }
    }
}
