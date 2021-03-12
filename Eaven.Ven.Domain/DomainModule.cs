﻿using Autofac;
using Eaven.Ven.EntityFrameworkCore.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eaven.Ven.Domain
{
    public class DomainModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            //注册其他Repository服务
            builder.RegisterAssemblyTypes(this.ThisAssembly)
                .Where(t => t.IsClosedTypeOf(typeof(IRepository<>)))
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();
        }
        private void LoadEntityFramwork(ContainerBuilder builder)
        {

            //var mysqlConfig = ConfigurationHelper.Current.GetSection("Mysql").Get<MysqlConfig>();
            //if (mysqlConfig?.WriteDb == null || string.IsNullOrWhiteSpace(mysqlConfig.WriteDb.ConnectionString))
            //    throw new ArgumentException("没有配置写服务器");
            //if (mysqlConfig?.ReadDbs == null || string.IsNullOrWhiteSpace(mysqlConfig.ReadDbs[0].ConnectionString))
            //    throw new ArgumentException("没有配置读服务器");

            //var writeDbConfig = mysqlConfig.WriteDb;
            //var readDbConfig = mysqlConfig.ReadDbs[0];

            //foreach (var redadDb in mysqlConfig.ReadDbs)
            //{
            //    //todo 根据访问者算法，计算需要读取那台服务器
            //    if (redadDb.Area == "a")
            //    {
            //        readDbConfig = redadDb;
            //        break;
            //    }
            //}
            ////首先注册 options，供 DbContext 服务初始化使用
            //builder.Register(c =>
            //{
            //    var optionsBuilder = new DbContextOptionsBuilder<BookListDbContext>();
            //    optionsBuilder.UseMySql(connectionString, b => b
            //        .MigrationsAssembly("BookList.Domain"));
            //    return optionsBuilder.Options;
            //}).InstancePerLifetimeScope();

            ////https://www.cnblogs.com/dudu/p/10398225.html 连接池问题
            //builder.Register(c =>
            //{
            //    var optionsBuilder = new DbContextOptionsBuilder<SystemManageDbContext>();
            //    optionsBuilder.UseMySql(writeDbConfig.ConnectionString
            //        , mySqlOptions => mySqlOptions.ServerVersion(new ServerVersion(new Version(8, 0, 18), ServerType.MySql)));
            //    return optionsBuilder.Options;
            //})
            //.InstancePerLifetimeScope();

            ////注册 DbContext
            //builder.RegisterType<SystemManageDbContext>()
            //    .AsSelf()
            //    .InstancePerLifetimeScope();
        }

        //private void LoadMongo(ContainerBuilder builder)
        //{
        //    var mongoDbConfig = ConfigurationHelper.Current.GetSection("MongoDb").Get<MongoConfig>();

        //    if (mongoDbConfig == null || string.IsNullOrWhiteSpace(mongoDbConfig.ConnectionStrings))
        //        return;

        //    var options = Options.Create(new MongoRepositoryOptions
        //    {
        //        ConnectionString = mongoDbConfig.ConnectionStrings
        //        ,
        //        CollectionNamingConvention = (NamingConvention)mongoDbConfig.CollectionNamingConvention
        //        ,
        //        PluralizeCollectionNames = mongoDbConfig.PluralizeCollectionNames
        //    });


        //    builder.RegisterInstance(options).SingleInstance();
        //    builder.RegisterType<MongoContext>().As<IMongoContext>().SingleInstance();
        //    builder.RegisterGeneric(typeof(MongoRepository<>)).As(typeof(IMongoRepository<>)).InstancePerDependency();
        //    builder.RegisterGeneric(typeof(SoftDeletableMongoRepository<>)).As(typeof(ISoftDeletableMongoRepository<>)).InstancePerDependency();


        //    builder.RegisterAssemblyTypes(this.ThisAssembly)
        //        .Where(t => t.IsClosedTypeOf(typeof(IMongoRepository<>)))
        //        .AsImplementedInterfaces()
        //        .InstancePerLifetimeScope();

        //    builder.RegisterAssemblyTypes(this.ThisAssembly)
        //        .Where(t => t.IsClosedTypeOf(typeof(IMongoEntityConfiguration<>)))
        //        .AsImplementedInterfaces()
        //        .InstancePerLifetimeScope();
        //}
    }
}
