using Eaven.Ven.EntityFrameworkCore.DependencyInjection;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using MySql.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eaven.Ven.EntityFrameworkCore.MySQL.Extensions
{
    public static class DbContextConfigurationContextMySQLExtensions
    {
        public static DbContextOptionsBuilder UseMySQL([NotNull] this DbContextConfigurationContext context, [CanBeNull] Action<MySQLDbContextOptionsBuilder> mySQLOptionsAction = null)
        {
            if (context.ExistingConnection != null)
            {
                return context.DbContextOptions.UseMySQL(context.ExistingConnection, optionsBuilder =>
                {
                    optionsBuilder.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
                    mySQLOptionsAction?.Invoke(optionsBuilder);
                });
            }
            else
            {
                return context.DbContextOptions.UseMySQL(context.ConnectionString, optionsBuilder =>
                {
                    optionsBuilder.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
                    mySQLOptionsAction?.Invoke(optionsBuilder);
                });
            }
        }
    }
}
