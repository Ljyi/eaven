using Elasticsearch.Net;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eaven.Ven.Elasticsearch.Context
{
    /// <summary>
    /// ElasticClient 提供者接口
    /// </summary>
    public interface IEsClientProvider
    {
        /// <summary>
        /// 获取ElasticClient
        /// </summary>
        /// <returns></returns>
        ElasticClient GetClient();
        /// <summary>
        /// 指定index获取ElasticClient
        /// </summary>
        /// <param name="indexName"></param>
        /// <returns></returns>
        ElasticClient GetClient(string indexName);

        ElasticLowLevelClient GetElasticLowLevelClient();
    }
}
