using Eaven.Ven.EntityFrameworkCore.Extensions;
using JetBrains.Annotations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eaven.Ven.EntityFrameworkCore.PostgreSql.Extensions
{
    public static class DbContextOptionsPostgreSqlExtensions
    {

        public static void UseNpgsql([NotNull] this DbContextOptions options, [CanBeNull] Action<NpgsqlDbContextOptionsBuilder> postgreSqlOptionsAction = null)
        {
            options.Configure(context =>
            {
                context.UseNpgsql(postgreSqlOptionsAction);
            });
        }

        public static void UseNpgsql<TDbContext>([NotNull] this DbContextOptions options, [CanBeNull] Action<NpgsqlDbContextOptionsBuilder> postgreSqlOptionsAction = null) where TDbContext : DataDbContext<TDbContext>
        {
            options.Configure<TDbContext>(context =>
            {
                context.UseNpgsql(postgreSqlOptionsAction);
            });
        }
    }
}
