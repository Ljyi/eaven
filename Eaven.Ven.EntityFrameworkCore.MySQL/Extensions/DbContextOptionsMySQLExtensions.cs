using Eaven.Ven.EntityFrameworkCore.Extensions;
using JetBrains.Annotations;
using MySql.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eaven.Ven.EntityFrameworkCore.MySQL.Extensions
{
    public static class DbContextOptionsMySQLExtensions
    {
        public static void UseMySQL([NotNull] this DbContextOptions options, [CanBeNull] Action<MySQLDbContextOptionsBuilder> mySQLOptionsAction = null)
        {
            options.Configure(context =>
            {
                context.UseMySQL(mySQLOptionsAction);
            });
        }

        public static void UseMySQL<TDbContext>([NotNull] this DbContextOptions options,[CanBeNull] Action<MySQLDbContextOptionsBuilder> mySQLOptionsAction = null) where TDbContext : DataDbContext<TDbContext>
        {
            options.Configure<TDbContext>(context =>
            {
                context.UseMySQL(mySQLOptionsAction);
            });
        }
    }
}
