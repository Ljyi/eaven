using Eaven.Ven.Domain;
using Eaven.Ven.Domain.Repository;
using Eaven.Ven.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eaven.Ven.EntityFrameworkContext.Repository
{
    public class AppUserAddressRepository : BaseRepository<AppDbContext, AppUserAddress>, IAppUserAddressRepository
    {
        AppDbContext _dbContext;
        public AppUserAddressRepository(AppDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
