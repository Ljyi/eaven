using Autofac;
using Eaven.Ven.EntityFrameworkCore.ContextFactory;
using Eaven.Ven.EntityFrameworkCore.Repository;
using Eaven.Ven.EntityFrameworkCore.Uow;
using System;

namespace Eaven.Ven.EntityFrameworkCore.MySQL
{
    public class MySqlEfCoreModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        { 
            // RegisterType 方式指定具体类
            // builder.RegisterType<IDbContextFactory>().As<DbContextFactory>().InstancePerDependency();

            //注册UOW
            //builder.RegisterType(typeof(UnitOfWork<DataDbContext>))
            //       .As(typeof(IUnitOfWork))
            //       .InstancePerLifetimeScope();

            // Register 方式指定具体类
            //  builder.Register(c => new InjectionTestService()).As<IService>().InstancePerDependency();
            // RegisterType 方式指定具体类
            // builder.RegisterType<UnitOfWork<DataDbContext>>().As<IUnitOfWork>().InstancePerDependency();

            ////注册ef公共Repository(构建器方法)
            //builder.RegisterGeneric(typeof(BaseRepository<,>))
            //    .UsingConstructor(typeof(DataDbContext))
            //    .AsImplementedInterfaces()//为接口注入具体类
            //    .InstancePerLifetimeScope();

            //注册ef公共Repository(构建器方法)
            builder.RegisterGeneric(typeof(BaseRepository<,>))
                .As(typeof(IEfRepository<>))
                .AsImplementedInterfaces()//为接口注入具体类
                .InstancePerLifetimeScope();

            //注册Repository服务(程序集注入)
            builder.RegisterAssemblyTypes(this.ThisAssembly)
                .Where(t => t.IsClosedTypeOf(typeof(IRepository<>)))
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();//保证对象生命周期基于请求
        }
    }
}
