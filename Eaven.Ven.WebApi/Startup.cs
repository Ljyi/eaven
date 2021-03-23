using Autofac;
using Eaven.Ven.Application;
using Eaven.Ven.Domain;
using Eaven.Ven.Domain.Repository;
using Eaven.Ven.EntityFrameworkContext;
using Eaven.Ven.EntityFrameworkContext.Repository;
using Eaven.Ven.EntityFrameworkCore.ContextFactory;
using Eaven.Ven.EntityFrameworkCore.MySQL;
using Eaven.Ven.EntityFrameworkCore.Uow;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Eaven.Ven.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            #region 初始化DB
            //
            services.AddDbContext<AppDbContext>(options => options.UseMySQL(Configuration.GetConnectionString("MySql")), ServiceLifetime.Scoped);
            //单个 HTTP 请求中的多个工作单元
          //  services.AddDbContextFactory<AppDbContext>(options => options.UseMySQL(Configuration.GetConnectionString("MySql")));
            #endregion

            services.AddSingleton<IApplicationService, ApplicationService>();
            services.AddSingleton<IAppUserService, AppUserService>();
            services.AddSingleton<IAppUserRepository, AppUserRepository>();
            services.AddSingleton<IAppUserAddressRepository, AppUserAddressRepository>();
            //    services.AddScoped<IUnitOfWorkContext, UnitOfWorkContext>();
            //   services.AddScoped<IDbContextFactory, Eaven.Ven.EntityFrameworkCore.MySQL.DbContextFactory>();


            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Eaven.Ven.WebApi", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Eaven.Ven.WebApi v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
        //注册依赖模块
        public void ConfigureContainer(ContainerBuilder builder)
        {
            //注册依赖模块
            builder.RegisterModule<ApplicationModule>();
        }
    }
}
