using Eaven.Ven.EntityFrameworkCore.ReadDbStore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Eaven.Ven.EntityFrameworkCore.ReadDbStrategy
{
    /// <summary>
    ///  单一策略
    /// </summary>
    public class SingleReadDbStore : IReadDbStore
    {
        public DbContext GetDbContext()
        {
            return null;
           // return new ReadDbContext();
        }
    }
}
