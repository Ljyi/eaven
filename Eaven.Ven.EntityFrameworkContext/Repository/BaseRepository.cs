using Eaven.Ven.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eaven.Ven.EntityFrameworkContext.Repository
{
    public class BaseRepository<TEntity> : EFCoreRepository<AppDbContext, TEntity> where TEntity : EntityModel
    {
        AppDbContext _dbContext;
        public BaseRepository(AppDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
