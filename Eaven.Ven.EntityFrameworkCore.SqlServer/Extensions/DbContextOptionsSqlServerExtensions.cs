using Eaven.Ven.EntityFrameworkCore.Extensions;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eaven.Ven.EntityFrameworkCore.SqlServer.Extensions
{
    public static class DbContextOptionsSqlServerExtensions
    {
        public static void UseSqlServer([NotNull] this DbContextOptions options, [CanBeNull] Action<SqlServerDbContextOptionsBuilder> sqlServerOptionsAction = null)
        {
            options.Configure(context =>
            {
                context.UseSqlServer(sqlServerOptionsAction);
            });
        }

        public static void UseSqlServer<TDbContext>([NotNull] this DbContextOptions options, [CanBeNull] Action<SqlServerDbContextOptionsBuilder> sqlServerOptionsAction = null) where TDbContext : DataDbContext<TDbContext>
        {
            options.Configure<TDbContext>(context =>
            {
                context.UseSqlServer(sqlServerOptionsAction);
            });
        }
    }
}
