using Eaven.Ven.EntityFrameworkCore.DependencyInjection;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Npgsql.EntityFrameworkCore.PostgreSQL.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eaven.Ven.EntityFrameworkCore.PostgreSql.Extensions
{
    public static class DbContextConfigurationContextPostgreSqlExtensions
    {
        [Obsolete("Use 'UseNpgsql(...)' method instead. This will be removed in future versions.")]
        public static DbContextOptionsBuilder UsePostgreSql([NotNull] this DbContextConfigurationContext context, [CanBeNull] Action<NpgsqlDbContextOptionsBuilder> postgreSqlOptionsAction = null)
        {
            return context.UseNpgsql(postgreSqlOptionsAction);
        }

        public static DbContextOptionsBuilder UseNpgsql([NotNull] this DbContextConfigurationContext context, [CanBeNull] Action<NpgsqlDbContextOptionsBuilder> postgreSqlOptionsAction = null)
        {
            if (context.ExistingConnection != null)
            {
                return context.DbContextOptions.UseNpgsql(context.ExistingConnection, optionsBuilder =>
                {
                    optionsBuilder.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
                    postgreSqlOptionsAction?.Invoke(optionsBuilder);
                });
            }
            else
            {
                return context.DbContextOptions.UseNpgsql(context.ConnectionString, optionsBuilder =>
                {
                    optionsBuilder.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
                    postgreSqlOptionsAction?.Invoke(optionsBuilder);
                });
            }
        }
    }
}
