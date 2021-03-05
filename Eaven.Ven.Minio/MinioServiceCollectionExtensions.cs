using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eaven.Ven.Minio
{
    /// <summary>
    /// 扩展
    /// </summary>
    public static class MinioServiceCollectionExtensions
    {
        public static IServiceCollection AddMinioOption(this IServiceCollection services, Action<MinioOption> setupAction)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }
            if (setupAction == null)
            {
                throw new ArgumentNullException(nameof(setupAction));
            }
            services.AddOptions();
            AddMinioService(services);
            services.Configure(setupAction);
            return services;
        }
        /// <summary>
        /// 服务自注册，实现自管理
        /// </summary>
        /// <param name="services">The services.</param>
        /// <returns></returns>
        public static void AddMinioService(this IServiceCollection services)
        {
            services.Add(ServiceDescriptor.Singleton<IMinioService, MinioService>());
        }
    }
}
