using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eaven.Ven.EntityFrameworkCore.Extensions
{

    //public static class DbContextConfigurationContextSqlServerExtensions
    //{
    //    public static DbContextOptionsBuilder UseSqlServer( [NotNull] this AbpDbContextConfigurationContext context, [CanBeNull] Action<SqlServerDbContextOptionsBuilder> sqlServerOptionsAction = null)
    //    {
    //        if (context.ExistingConnection != null)
    //        {
    //            return context.DbContextOptions.UseSqlServer(context.ExistingConnection, optionsBuilder =>
    //            {
    //                optionsBuilder.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
    //                sqlServerOptionsAction?.Invoke(optionsBuilder);
    //            });
    //        }
    //        else
    //        {
    //            return context.DbContextOptions.UseSqlServer(context.ConnectionString, optionsBuilder =>
    //            {
    //                optionsBuilder.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
    //                sqlServerOptionsAction?.Invoke(optionsBuilder);
    //            });
    //        }
    //    }
    //}
}
