using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eaven.Ven.Core.Enums
{
    /// <summary>
    /// 枚举公用返回状态信息
    /// </summary>
    public enum ResultCode
    {
        /// <summary>
        /// 请求成功（所有接口请求成功时默认值）
        /// </summary>
        [Description("请求成功")]
        OK = 200,
        /// <summary>
        /// 未授权 (Unauthorized)
        /// </summary> 
        [Description("未授权 (Unauthorized)")]
        Unauthorized = 401,
        /// <summary>
        /// 访问权限等级不够
        /// </summary>
        [Description("访问权限等级不够")]
        Forbidden = 403,
        /// <summary>
        /// 请求失败
        /// </summary>
        [Description("请求失败")]
        NotFound = 404,
        /// <summary>
        /// 服务器内部错误
        /// </summary>
        [Description("服务器内部错误")]
        ServerError = 500,
        /// <summary>
        /// 请求失败（逻辑处理过程中需返回信息时使用）
        /// </summary>
        [Description("请求失败")]
        Fail = -1001,
        /// <summary>
        /// 身份验证失败（token失效或未登录）
        /// </summary>
        [Description("身份验证失败")]
        ValidateFail = -1002,
        /// <summary>
        /// 数据为空（查询数据库找不到数据时使用）
        /// </summary>
        [Description("数据为空")]
        NoRecord = -1003,
        /// <summary>
        /// 提交数据检测未通过（验证输入参数时使用）
        /// </summary>
        [Description("提交数据检测未通过")]
        RequestValidateFail = -1004,
        /// <summary>
        /// 服务数据异常（异常捕捉时使用）
        /// </summary>
        [Description("服务数据异常,抛出异常")]
        HandleError = -1005,
        /// <summary>
        /// 未授权访问系统（自身验证不通过时使用）
        /// </summary>
        [Description("未授权访问系统")]
        Deny = -1006,
        /// <summary>
        /// IP验证有误（逻辑处理过程中需返回信息时使用）
        /// </summary>
        [Description("IP验证不通过")]
        IPFail = -1008,
        /// <summary>
        /// 请求密码不正确（逻辑处理过程中需返回信息时使用）
        /// </summary>
        [Description("请求密码不正确")]
        PasswordFail = -1009,
        /// <summary>
        /// 传入参数有误或者为空（逻辑处理过程中需返回信息时使用）
        /// </summary>
        [Description("传入参数有误或者为空")]
        ParameterFail = -1010,
        /// <summary>
        /// 不允许操作
        /// </summary>
        [Description("不允许操作")]
        NotAllow = -1011,
        /// <summary>
        /// 调起支付异常
        /// </summary>
        [Description("调起支付异常")]
        PayFail = -1012,
        /// <summary>
        /// 已存在相同数据
        /// </summary>
        [Description("已存在相同数据")]
        AlreadyExists = -1013,
        /// <summary>
        /// 请求类型错误
        /// </summary>
        [Description("请求类型错误")]
        RequestMethod = -1014,
        /// <summary>
        /// 用户不存在角色信息
        /// </summary>
        [Description("用户不存在角色信息")]
        UserRoleNUll = -1015,
    }
}
