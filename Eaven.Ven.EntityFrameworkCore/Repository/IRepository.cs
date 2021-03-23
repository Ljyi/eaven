using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Eaven.Ven.EntityFrameworkCore.Repository
{
    /// <summary>
    /// Represents an entity repository
    /// </summary>
    /// <typeparam name="TEntity">Entity type</>
    public interface IRepository<TEntity> where TEntity : EntityModel
    {
        // Task<DbContext> GetDbContextAsync();
    }
}
