using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eaven.Ven.Qiniu
{
    /// <summary>
    /// 扩展
    /// </summary>
    public static class QiniuServiceCollectionExtensions
    { /// <summary>
      /// Adds the register  sub.
      /// </summary>
      /// <param name="services">The services.</param>
      /// <param name="option">The option.</param>
      /// <returns></returns>
        public static IServiceCollection AddEMessageOption(this IServiceCollection services, Action<QiniuOption> setupAction)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }
            if (setupAction == null)
            {
                throw new ArgumentNullException(nameof(setupAction));
            }
           // services.AddOptions();
            AddEMessageService(services);
         //   services.Configure(setupAction);
            return services;
        }
        /// <summary>
        /// 服务自注册，实现自管理
        /// </summary>
        /// <param name="services">The services.</param>
        /// <returns></returns>
        public static void AddEMessageService(this IServiceCollection services)
        {
           // services.Add(ServiceDescriptor.Singleton<IEasyMessageService, EasyMessageService>());
        }
    }
}
