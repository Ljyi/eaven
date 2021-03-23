using Eaven.Ven.Core;
using Eaven.Ven.EntityFrameworkCore.DependencyInjection;
using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eaven.Ven.EntityFrameworkCore.Extensions
{
    public class DbContextOptions
    {
        internal List<Action<DbContextConfigurationContext>> DefaultPreConfigureActions { get; set; }

        internal Action<DbContextConfigurationContext> DefaultConfigureAction { get; set; }

        internal Dictionary<Type, List<object>> PreConfigureActions { get; set; }

        internal Dictionary<Type, object> ConfigureActions { get; set; }

        public DbContextOptions()
        {
            DefaultPreConfigureActions = new List<Action<DbContextConfigurationContext>>();
            PreConfigureActions = new Dictionary<Type, List<object>>();
            ConfigureActions = new Dictionary<Type, object>();
        }

        public void PreConfigure([NotNull] Action<DbContextConfigurationContext> action)
        {
            Check.NotNull(action, nameof(action));
            DefaultPreConfigureActions.Add(action);
        }

        public void Configure([NotNull] Action<DbContextConfigurationContext> action)
        {
            Check.NotNull(action, nameof(action));
            DefaultConfigureAction = action;
        }
        public void Configure<TDbContext>([NotNull] Action<DbContextConfigurationContext<TDbContext>> action) where TDbContext : DataDbContext<TDbContext>
        {
            Check.NotNull(action, nameof(action));

            ConfigureActions[typeof(TDbContext)] = action;
        }
    }
}
