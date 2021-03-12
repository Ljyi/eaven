using Autofac;
using Eaven.Ven.EntityFrameworkCore;
using Eaven.Ven.EntityFrameworkCore.MySQL;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eaven.Ven.Application
{
    public class ApplicationModule : Module
    {
        public ApplicationModule()
        {
        }
        public ApplicationModule(IServiceCollection services, IConfiguration configuration) : base()
        {
        }
        protected override void Load(ContainerBuilder builder)
        {
            //注册依赖模块
            this.LoadDepends(builder);
            //注册服务
            builder.RegisterAssemblyTypes(this.ThisAssembly)
                .Where(t => t.IsAssignableTo<IApplicationService>())
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();
        }
        private void LoadDepends(ContainerBuilder builder)
        {
             builder.RegisterModule<MySqlEfCoreModule>();
        }
    }
}