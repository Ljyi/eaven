using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Eaven.Ven.Core
{
    /// <summary>
    /// 标记服务
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public class ServiceAttribute : Attribute
    {
        /// <summary>
        /// 生命周期
        /// </summary>
        public ServiceLifetime Lifetime { get; set; } = ServiceLifetime.Singleton;
        /// <summary>
        /// 指定服务类型
        /// </summary>
        public Type ServiceType { get; set; }
        /// <summary>
        /// 是否可以从第一个接口获取服务类型
        /// </summary>
        public bool InterfaceServiceType { get; set; } = true;
    }
}
