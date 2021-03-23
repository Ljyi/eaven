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
    public class AppUserRepository : BaseRepository<AppDbContext, AppUser>, IAppUserRepository
    {
        //WriteAndRead AndRead = WriteAndRead.Write;
        //public AppUserRepository(WriteAndRead AndRead) : base(AndRead)
        //{
        //}
        public AppUserRepository()
        {
        }
    }
}
