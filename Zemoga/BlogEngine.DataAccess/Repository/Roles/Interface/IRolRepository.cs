using BlogEngine.DataAccess.Models;
using System.Threading.Tasks;

namespace BlogEngine.DataAccess.Repository.Roles.Interface
{
    public interface IRolRepository
    {
        Task<Rol> Get(long id);
    }
}
