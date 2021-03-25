using Eaven.Ven.Domain;
using Eaven.Ven.EntityFrameworkCore;
using Eaven.Ven.EntityFrameworkCore.MySQL;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eaven.Ven.EntityFrameworkContext.Repository
{
    public class AppUserRepository : BaseRepository<AppUser>, IAppUserRepository
    {
        AppDbContext _dbContext;
        public AppUserRepository(AppDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
