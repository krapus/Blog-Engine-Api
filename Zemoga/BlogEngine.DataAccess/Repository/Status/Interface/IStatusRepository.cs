using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BlogEngine.DataAccess.Repository.Status.Interface
{
    public interface IStatusRepository
    {
        Task<IEnumerable<Models.Status>> GetStatus(Expression<Func<Models.Status, bool>> condition);
    }
}
