using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Eaven.Ven.EntityFrameworkCore.DbContextProvider
{
    public interface IEfCoreDbContext : IDisposable, IInfrastructure<IServiceProvider>
    {
        DatabaseFacade Database { get; }

        ChangeTracker ChangeTracker { get; }
    }
}
