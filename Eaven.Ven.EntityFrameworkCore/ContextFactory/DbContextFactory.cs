using Eaven.Ven.EntityFrameworkCore.ReadDbStore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Eaven.Ven.EntityFrameworkCore.ContextFactory
{
    public class DbContextFactory
    {
        //todo:这里可以自己通过注入的方式来实现，就会更加灵活
        private static readonly IReadDbStore readReadDbStore = new RandomReadDbStore();
        public DbContext GetWriteDbContext()
        {
            string key = typeof(DbContextFactory).Name + "WriteDbContext";
            DbContext dbContext = CallContext.GetData(WriteAndRead.Write) as DbContext;
            if (dbContext == null)
            {
                dbContext = readReadDbStore.GetDbContext();
                CallContext.SetData(WriteAndRead.Write, dbContext);
            }
            return dbContext;
        }

        public DbContext GetReadDbContext()
        {
            string key = typeof(DbContextFactory).Name + "ReadDbContext";
            DbContext dbContext = CallContext.GetData(WriteAndRead.Read) as DbContext;
            if (dbContext == null)
            {
                dbContext = readReadDbStore.GetDbContext();
                CallContext.SetData(WriteAndRead.Read, dbContext);
            }
            return dbContext;
        }
    }
}
