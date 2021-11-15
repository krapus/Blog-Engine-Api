using BlogEngine.DataAccess.Models;
using BlogEngine.DataAccess.Repository.Status.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BlogEngine.DataAccess.Repository.Status.Implementation
{
    public class StatusRepository : IStatusRepository
    {
        private ZemogaBlogEngineContext _zemogaBlogEngineContext;
        public StatusRepository(ZemogaBlogEngineContext zemogaBlogEngineContext)
        {
            _zemogaBlogEngineContext = zemogaBlogEngineContext;
        }

        public async Task<IEnumerable<Models.Status>> GetStatus(Expression<Func<Models.Status, bool>> condition)
        {
            return await _zemogaBlogEngineContext.Status.Where(condition).ToListAsync();
        }
    }
}
