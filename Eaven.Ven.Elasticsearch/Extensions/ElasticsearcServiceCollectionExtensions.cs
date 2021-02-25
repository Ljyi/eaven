using Eaven.Ven.Elasticsearch.Context;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Eaven.Ven.Elasticsearch.Extensions
{

    /// <summary>
    /// 扩展类
    /// </summary>
    public static class ElasticsearcServiceCollectionExtensions
    {
        public static IServiceCollection ElasticsearchExtensionsServcie(this IServiceCollection services, IConfiguration Configuration)
        {
            services.Configure<EsConfig>(options =>
            {
                options.Urls = Configuration.GetSection("EsConfig:ConnectionStrings").GetChildren().ToList().Select(p => p.Value).ToList();
            });
            services.AddSingleton<IEsClientProvider, EsClientProvider>();
            var types = Assembly.Load("Xw.Application").GetTypes().Where(p => !p.IsAbstract && (p.GetInterfaces().Any(i => i == typeof(IBaseEsContext)))).ToList();
            types.ForEach(p => services.AddTransient(p));
            return services;
        }
    }
}
