using Eaven.Ven.Core.Enums;
using Eaven.Ven.Core.Extension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eaven.Ven.Core
{
    /// <summary>
    /// API返回信息
    /// </summary>
    public class ResultJsons<T>
    {
        /// <summary>
        /// 返回类型
        /// </summary>
        public ResultJsons()
        {
            this.api_version = "v1";
            code = "200";
            this.success = true;
        }
        /// <summary>
        /// 版本号
        /// </summary>
        public string api_version { get; set; }
        /// <summary>
        /// 是否成功
        /// </summary>
        public bool success { get; set; }
        /// <summary>
        /// code
        /// </summary>
        public string code { get; set; }
        /// <summary>
        /// 返回信息
        /// </summary>
        public string message { get; set; }
        /// <summary>
        /// 返回数据集合
        /// </summary>
        public List<T> data { get; set; }
    }
    /// <summary>
    /// API返回信息
    /// </summary>
    public class ResultJson
    {
        /// <summary>
        /// 版本号
        /// </summary>
        public string api_version { get; set; }
        /// <summary>
        /// 是否成功
        /// </summary>
        public bool success { get; set; }
        /// <summary>
        /// code
        /// </summary>
        public string code { get; set; }
        /// <summary>
        /// 返回信息
        /// </summary>
        public string message { get; set; }
        /// <summary>
        /// 返回类型
        /// </summary>
        public ResultJson()
        {
            this.api_version = "v1";
            code = "200";
            this.success = true;
        }
        public ResultJson(ResultCode code, string message = null)
        {
            this.api_version = "v1";
            this.code = EnumExtension.GetEnumValue(typeof(ResultCode), code.ToString());
            this.success = true;
            if (string.IsNullOrEmpty(message))
            {
                this.message = EnumExtension.GetEnumDesc(typeof(ResultCode), code.ToString());
            }
            if (code != ResultCode.OK)
            {
                this.success = false;
            }
        }
        /// <summary>
        /// 返回指定 Code
        /// </summary>
        public static ResultJson FromCode(ResultCode code, string message = null)
        {
            return new ResultJson(code, message);
        }
        /// <summary>
        /// 返回异常信息
        /// </summary>
        public static ResultJson FromError(string message, ResultCode code = ResultCode.Fail)
        {
            return FromCode(ResultCode.OK, message);
        }
        /// <summary>
        /// 返回成功
        /// </summary>
        public static ResultJson Ok(string message = null)
        {
            return FromCode(ResultCode.OK, message);
        }
    }
    /// <summary>
    /// API返回信息
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ResultJson<T>
    {
        /// <summary>
        /// 返回类型
        /// </summary>
        public ResultJson()
        {
            this.api_version = "v1";
            code = "200";
            this.success = true;
        }

        /// <summary>
        /// 版本号
        /// </summary>
        public string api_version { get; set; }
        /// <summary>
        /// 是否成功
        /// </summary>
        public bool success { get; set; }
        /// <summary>
        /// code
        /// </summary>
        public string code { get; set; }
        /// <summary>
        /// 返回信息
        /// </summary>
        public string message { get; set; }
        /// <summary>
        /// 返回数据集合
        /// </summary>
        public T data { get; set; }
    }

    /// <summary>
    /// API返回信息
    /// </summary>
    public class ResultModels<T>
    {
        /// <summary>
        /// 返回类型
        /// </summary>
        public ResultModels()
        {
            this.api_version = "v1";
            code = "200";
            this.success = true;
        }
        /// <summary>
        /// 版本号
        /// </summary>
        public string api_version { get; set; }
        /// <summary>
        /// 是否成功
        /// </summary>
        public bool success { get; set; }
        /// <summary>
        /// code
        /// </summary>
        public string code { get; set; }
        /// <summary>
        /// 返回信息
        /// </summary>
        public string message { get; set; }
        /// <summary>
        /// 返回数据集合
        /// </summary>
        public List<T> data { get; set; }
    }
    /// <summary>
    /// API返回信息
    /// </summary>
    public class ResultModel
    {
        /// <summary>
        /// 版本号
        /// </summary>
        public string api_version { get; set; }
        /// <summary>
        /// 是否成功
        /// </summary>
        public bool success { get; set; }
        /// <summary>
        /// code
        /// </summary>
        public string code { get; set; }
        /// <summary>
        /// 返回信息
        /// </summary>
        public string message { get; set; }
        /// <summary>
        /// 返回类型
        /// </summary>
        public ResultModel()
        {
            this.api_version = "v1";
            code = "200";
            this.success = true;
        }
        public ResultModel(ResultCode code, string message = null)
        {
            this.api_version = "v1";
            this.code = code.ToString();
            this.success = true;
        }
        /// <summary>
        /// 返回指定 Code
        /// </summary>
        public static ResultModel FromCode(ResultCode code, string message = null)
        {
            return new ResultModel(code, message);
        }
        /// <summary>
        /// 返回异常信息
        /// </summary>
        public static ResultModel FromError(string message, ResultCode code = ResultCode.Fail)
        {
            return FromCode(ResultCode.OK, message);
        }
        /// <summary>
        /// 返回成功
        /// </summary>
        public static ResultModel Ok(string message = null)
        {
            return FromCode(ResultCode.OK, message);
        }
    }
    /// <summary>
    /// API返回信息
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ResultModel<T>
    {
        /// <summary>
        /// 返回类型
        /// </summary>
        public ResultModel()
        {
            this.api_version = "v1";
            code = "200";
            this.success = true;
        }

        /// <summary>
        /// 版本号
        /// </summary>
        public string api_version { get; set; }
        /// <summary>
        /// 是否成功
        /// </summary>
        public bool success { get; set; }
        /// <summary>
        /// code
        /// </summary>
        public string code { get; set; }
        /// <summary>
        /// 返回信息
        /// </summary>
        public string message { get; set; }
        /// <summary>
        /// 返回数据集合
        /// </summary>
        public T data { get; set; }
    }
    public class ResultPageModel<T>
    {
        /// <summary>
        /// 返回类型
        /// </summary>
        public ResultPageModel()
        {
            this.api_version = "v1";
            code = "200";
            this.success = true;
        }
        /// <summary>
        /// 版本号
        /// </summary>
        public string api_version { get; set; }
        /// <summary>
        /// 是否成功
        /// </summary>
        public bool success { get; set; }
        /// <summary>
        /// code
        /// </summary>
        public string code { get; set; }
        /// <summary>
        /// 返回信息
        /// </summary>
        public string message { get; set; }
        /// <summary>
        /// 分页数据
        /// </summary>
        public PageJson<T> data { get; set; }
    }

    public class PageJson<T>
    {
        /// <summary>
        /// 总条数
        /// </summary>
        public int TotalCount { get; set; }
        /// <summary>
        /// 数据集合
        /// </summary>
        public List<T> Items { get; set; }
    }
}
