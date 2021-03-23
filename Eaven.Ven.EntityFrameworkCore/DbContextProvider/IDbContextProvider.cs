using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eaven.Ven.EntityFrameworkCore.DbContextProvider
{
    public interface IDbContextProvider<TDbContext> where TDbContext : IEfCoreDbContext
    {
        TDbContext GetDbContext();

        Task<TDbContext> GetDbContextAsync();
    }
}
