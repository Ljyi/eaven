using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;

namespace Eaven.Ven.Elasticsearch
{
    /// <summary>
    /// es配置文件
    /// </summary>
    public class EsConfig : IOptions<EsConfig>
    {
        public List<string> Urls { get; set; }

        public EsConfig Value => this;
    }
}
