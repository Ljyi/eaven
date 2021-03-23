using Eaven.Ven.EntityFrameworkCore.ContextFactory;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Eaven.Ven.EntityFrameworkCore
{
    public class DbContextFactory<TDbContext> : IDbContextFactory where TDbContext : DbContext
    {
        private static IConfiguration _configuration;
        public DbContextFactory(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        /// <summary>
        /// 获取DbContext的Options
        /// </summary>
        /// <param name="writeRead"></param>
        /// <returns></returns>
        //public static DbContextOptions<DataDbContext<TDbContext>> GetOptions(WriteAndRead writeRead)
        //{
        //    string masterConnectionString = _configuration.GetConnectionString("MySql:Write");
        //    //随机选择读数据库节点
        //    var optionsBuilder = new DbContextOptionsBuilder<DataDbContext>();
        //    if (writeRead == WriteAndRead.Read)
        //    {
        //        string slaveConnectionString = _configuration.GetConnectionString("MySql:Read");
        //        optionsBuilder.UseMySQL(slaveConnectionString);
        //        // optionsBuilder.UseLazyLoadingProxies();//启用延迟加载
        //    }
        //    else
        //    {
        //        optionsBuilder.UseMySQL(masterConnectionString);
        //    }
        //    return optionsBuilder.Options;
        //}
        /// <summary>
        /// 创建ReadDbContext实例
        /// </summary>
        /// <returns></returns>
        public static DbContext CreateReadDbContext()
        {
            try
            {
                DbContextOptions<DbContext> dbContextOption = new DbContextOptions<DbContext>();
                DbContextOptionsBuilder<DbContext> dbContextOptionBuilder = new DbContextOptionsBuilder<DbContext>(dbContextOption);
                //先从线程获取实例，保证线程安全
                DataDbContext<DbContext> dbContext = (DataDbContext<DbContext>)CallContext.GetData(WriteAndRead.Read);
                //if (dbContext == null)
                //{
                //    string slaveConnectionString = "server=192.168.99.66;database=XwShop;userid=root;pwd=XinWei123;port=3306;sslmode=none;allowPublicKeyRetrieval=true;CharSet=utf8;";//_configuration.GetConnectionString("MySql");
                //    dbContext = new DataDbContext<DbContext>(dbContextOptionBuilder.UseMySQL(slaveConnectionString).Options);
                //    CallContext.SetData(WriteAndRead.Read, dbContext);
                //}
                return dbContext;
            }
            catch (Exception ex)
            {
                string errorMsg = "数据库链接异常";
                throw ex;
            }
        }
        /// <summary>
        /// 创建WriteDbContext实例
        /// </summary>
        /// <returns></returns>
        public static DataDbContext<DbContext> CreateWriteDbContext()
        {
            DbContextOptions<DbContext> dbContextOption = new DbContextOptions<DbContext>();
            DbContextOptionsBuilder<DbContext> dbContextOptionBuilder = new DbContextOptionsBuilder<DbContext>(dbContextOption);
            //先从线程获取实例，保证线程安全
            DataDbContext<DbContext> dbContext = (DataDbContext<DbContext>)CallContext.GetData(WriteAndRead.Read);
            //if (dbContext == null)
            //{
            //    string slaveConnectionString = "server=192.168.99.66;database=XwShop;userid=root;pwd=XinWei123;port=3306;sslmode=none;allowPublicKeyRetrieval=true;CharSet=utf8;";//_configuration.GetConnectionString("MySql");
            //    DbContextOptions<DbContext> options = dbContextOptionBuilder.UseMySQL(slaveConnectionString).Options;
            //    dbContext = new DataDbContext<DbContext>(options);
            //    CallContext.SetData(WriteAndRead.Write, dbContext);
            //}
            return dbContext;
        }
        /// <summary>
        /// 创建WriteDbContext实例
        /// </summary>
        /// <returns></returns>
        //public static DataDbContext<TDbContext> CreateWriteDbContext()
        //{
        //    //先从线程获取实例，保证线程安全
        //    DataDbContext<TDbContext> dbContext = (DataDbContext<TDbContext>)CallContext.GetData(WriteAndRead.Write);
        //    if (dbContext == null)
        //    {
        //        dbContext = new DataDbContext<TDbContext>(WriteAndRead.Write);
        //        CallContext.SetData(WriteAndRead.Write, dbContext);
        //    }
        //    return dbContext;
        //}
        /// <summary>
        /// 随机策略
        /// </summary>
        /// <returns></returns>
        private string GetReadConnectionString()
        {
            /*
             * 随机策略
             * 权重策略
             * 轮询策略
             */
            //随机策略
            string connectionString = "";//ReadConnectionStrings[new Random().Next(0, ReadConnectionStrings.Length)];
            return connectionString;
        }
    }
}
