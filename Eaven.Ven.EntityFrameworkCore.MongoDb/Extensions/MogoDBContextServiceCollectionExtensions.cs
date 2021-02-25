using Eaven.Ven.Core;
using Eaven.Ven.Core.Dependency;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace Eaven.Ven.EntityFrameworkCore.MongoDb.Extensions
{
    public static class MogoDBContextServiceCollectionExtensions
    {
        /// <summary>
        /// 要扫描的程序集名称
        /// 默认为[]多个使用|分隔
        /// </summary>
        public static string MatchAssemblies = "Xw.MongoDbCore|MogoDb.Service";
        public static IServiceCollection AddMogoDBContext<T>(this IServiceCollection services, Action<MogoDBContextOptions> setupAction) where T : class, IMogoDBContext
        {
            if (null == services)
            {
                throw new ArgumentNullException(nameof(services));
            }
            if (null == setupAction)
            {
                throw new ArgumentNullException(nameof(setupAction));
            }
            services.AddOptions();
            services.Configure(setupAction);
            services.AddScoped<IMogoDBContext, T>();
            services.AddMogoService();
            //   services.AddMogoAppServices();
            return services;
        }

        /// <summary>
        /// 服务自注册，实现自管理
        /// </summary>
        /// <param name="services">The services.</param>
        /// <returns></returns>
        public static IServiceCollection AddMogoService(this IServiceCollection services)
        {
            #region 依赖注入        
            var baseType = typeof(IDependency);
            var path = AppDomain.CurrentDomain.RelativeSearchPath ?? AppDomain.CurrentDomain.BaseDirectory;
            var getFiles = Directory.GetFiles(path, "*.dll").Where(Match);
            var referencedAssemblies = getFiles.Select(Assembly.LoadFrom).ToList();
            var ss = referencedAssemblies.SelectMany(o => o.GetTypes());
            var types = referencedAssemblies
                .SelectMany(a => a.DefinedTypes)
                .Select(type => type.AsType())
                .Where(x => x != baseType && baseType.IsAssignableFrom(x)).ToList();
            var implementTypes = types.Where(x => x.IsClass).ToList();
            var interfaceTypes = types.Where(x => x.IsInterface).ToList();
            foreach (var implementType in implementTypes)
            {
                if (typeof(IScopeDependency).IsAssignableFrom(implementType))
                {
                    //var interfaceType = interfaceTypes.FirstOrDefault(x => x.IsAssignableFrom(implementType));
                    //if (interfaceType != null)
                    //    services.AddScoped(interfaceType, implementType);
                    services.AddScoped(implementType);
                }
                else if (typeof(ISingletonDependency).IsAssignableFrom(implementType))
                {
                    //var interfaceType = interfaceTypes.FirstOrDefault(x => x.IsAssignableFrom(implementType));
                    //if (interfaceType != null)
                    //    services.AddSingleton(interfaceType, implementType);
                    services.AddSingleton(implementType);
                }
                else
                {
                    var interfaceType = interfaceTypes.FirstOrDefault(x => x.IsAssignableFrom(implementType));
                    if (interfaceType != null)
                        services.AddTransient(interfaceType, implementType);
                }
            }
            #endregion
            return services;
        }

        /// <summary>
        /// 注册应用程序域中所有有MogoAppService特性的服务
        /// </summary>
        /// <param name="services"></param>
        public static IServiceCollection AddMogoAppServices(this IServiceCollection services)
        {
            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                foreach (var type in assembly.GetTypes())
                {
                    var serviceAttribute = type.GetCustomAttribute<ServiceAttribute>();
                    if (serviceAttribute != null)
                    {
                        var serviceType = serviceAttribute.ServiceType;
                        if (serviceType == null && serviceAttribute.InterfaceServiceType)
                        {
                            serviceType = type.GetInterfaces().FirstOrDefault();
                        }
                        if (serviceType == null)
                        {
                            serviceType = type;
                        }
                        switch (serviceAttribute.Lifetime)
                        {
                            case ServiceLifetime.Singleton:
                                services.AddSingleton(serviceType, type);
                                break;
                            case ServiceLifetime.Scoped:
                                services.AddScoped(serviceType, type);
                                break;
                            case ServiceLifetime.Transient:
                                services.AddTransient(serviceType, type);
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
            return services;
        }
        /// <summary>
        /// 程序集是否匹配
        /// </summary>
        public static bool Match(string assemblyName)
        {
            assemblyName = Path.GetFileName(assemblyName);
            return Regex.IsMatch(assemblyName, MatchAssemblies, RegexOptions.IgnoreCase | RegexOptions.Compiled);
        }

    }
}
