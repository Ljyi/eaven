using Eaven.Ven.Core;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Data.Common;

namespace Eaven.Ven.EntityFrameworkCore.DependencyInjection
{
    public class DbContextConfigurationContext : IServiceProviderAccessor
    {
        public IServiceProvider ServiceProvider { get; }

        public string ConnectionString { get; }

        public string ConnectionStringName { get; }

        public DbConnection ExistingConnection { get; }

        public DbContextOptionsBuilder DbContextOptions { get;  set; }

        public DbContextConfigurationContext([NotNull] string connectionString, [NotNull] IServiceProvider serviceProvider, [CanBeNull] string connectionStringName, [CanBeNull] DbConnection existingConnection)
        {
            ConnectionString = connectionString;
            ServiceProvider = serviceProvider;
            ConnectionStringName = connectionStringName;
            ExistingConnection = existingConnection;
            DbContextOptions = new DbContextOptionsBuilder().UseLoggerFactory(serviceProvider.GetRequiredService<ILoggerFactory>());
        }
    }
    public class DbContextConfigurationContext<TDbContext> : DbContextConfigurationContext where TDbContext : DataDbContext<TDbContext>
    {
        public new DbContextOptionsBuilder<TDbContext> DbContextOptions => (DbContextOptionsBuilder<TDbContext>)base.DbContextOptions;

        public DbContextConfigurationContext(string connectionString, [NotNull] IServiceProvider serviceProvider, [CanBeNull] string connectionStringName, [CanBeNull] DbConnection existingConnection) : base(
                  connectionString,
                  serviceProvider,
                  connectionStringName,
                  existingConnection)
        {
            base.DbContextOptions = new DbContextOptionsBuilder<TDbContext>().UseLoggerFactory(serviceProvider.GetRequiredService<ILoggerFactory>());
        }
    }
}
