using Autofac;
using Eaven.Ven.EntityFrameworkCore.Repository;
using Eaven.Ven.EntityFrameworkCore.Uow;
using System;

namespace Eaven.Ven.EntityFrameworkCore.MySQL
{
    public class MySqlEfCoreModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            //注册UOW
            builder.RegisterType(typeof(UnitOfWork<DataDbContext>))
                   .As(typeof(IUnitOfWork))
                   .InstancePerLifetimeScope();

            //注册ef公共Repository
            builder.RegisterGeneric(typeof(BaseRepository<,>))
                .UsingConstructor(typeof(DataDbContext))
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            //注册Repository服务
            builder.RegisterAssemblyTypes(this.ThisAssembly)
                .Where(t => t.IsClosedTypeOf(typeof(IRepository<>)))
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();
        }
    }
}
