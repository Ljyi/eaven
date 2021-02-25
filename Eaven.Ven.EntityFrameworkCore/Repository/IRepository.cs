using System;
using System.Collections.Generic;
using System.Text;

namespace Eaven.Ven.EntityFrameworkCore.Repository
{
    /// <summary>
    /// Represents an entity repository
    /// </summary>
    /// <typeparam name="TEntity">Entity type</>
    public interface IRepository<TEntity> where TEntity : EntityModel
    {
    }
}
