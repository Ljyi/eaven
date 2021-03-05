using Eaven.Ven.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eaven.Ven.Domain
{
    /// <summary>
    /// AppUser【用户表】
    /// </summary>
    [Table("AppUser")]
    public class AppUser: EntityModel
    {
        /// <summary>
        /// 用户姓名
        /// </summary>
        [Required]
        [MaxLength(50)]
        [Display(Name = "用户姓名")]
        public string UserName { get; set; }
        /// <summary>
        /// 随机数
        /// </summary>
        [Required]
        [MaxLength(10)]
        public string UserUniqueId { get; set; }
        /// <summary>
        /// 昵称
        /// </summary>
        [MaxLength(50)]
        public string Nickname { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        [Required]
        public int Gender { get; set; }
        /// <summary>
        /// 手机号
        /// </summary>
        [Required]
        [MaxLength(50)]
        public string Phone { get; set; }
        /// <summary>
        /// 邮件地址
        /// </summary>
        [Required]
        [MaxLength(100)]
        public string Email { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        [Required]
        [MaxLength(50)]
        public string Password { get; set; }
        /// <summary>
        /// 用户状态
        /// </summary>
        [Required]
        public int Status { get; set; }
        /// <summary>
        /// 国家
        /// </summary>
        [MaxLength(50)]
        public string Country { get; set; }
        /// <summary>
        /// 省份
        /// </summary>
        [MaxLength(50)]
        public string Province { get; set; }
        /// <summary>
        /// 城市
        /// </summary>
        [MaxLength(50)]
        public string City { get; set; }
        /// <summary>
        /// 区域
        /// </summary>
        [MaxLength(50)]
        public string Area { get; set; }
        /// <summary>
        /// 生日
        /// </summary>
        public DateTime Birthday { get; set; }
        /// <summary>
        /// 用户类型
        /// </summary>
        [Required]
        public int AppUserType { get; set; }
        /// <summary>
        /// 会员积分
        /// </summary>
        public int Point { get; set; }
        /// <summary>
        /// 会员等级
        /// </summary>
        public int UserLevel { get; set; }
        /// <summary>
        /// 销售员编号
        /// </summary>
        [MaxLength(20)]
        public string SaleNo { get; set; }
        /// <summary>
        /// Nsap客户端代码
        /// </summary>
        [MaxLength(20)]
        public string CustomerNo { get; set; }
        /// <summary>
        /// 是否禁用
        /// </summary>
        public bool Disable { get; set; }
        /// <summary>
        /// 机构、学院、公司
        /// </summary>
        [MaxLength(100)]
        public string InstitutionName { get; set; }
        /// <summary>
        /// 营业执照
        /// </summary>
        [MaxLength(100)]
        public string BusinessLicenseNO { get; set; }
        /// <summary>
        /// 头像链接
        /// </summary>
        [MaxLength(200)]
        public string HeadImg { get; set; }
        /// <summary>
        /// 用户来源
        /// </summary>
        public int Source { get; set; }
        /// <summary>
        /// 授权Id（第三方登录）
        /// </summary>
        [MaxLength(200)]
        public string WechatUnionId { get; set; }
        /// <summary>
        /// 授权Id（第三方登录）
        /// </summary>
        [MaxLength(200)]
        public string QQUnionId { get; set; }
        /// <summary>
        /// Twitter（第三方登录）
        /// </summary>
        [MaxLength(200)]
        public string TwitterUnionId { get; set; }
        /// <summary>
        /// Facebook（第三方登录）
        /// </summary>
        [MaxLength(200)]
        public string FacebookUnionId { get; set; }
        /// <summary>
        /// 小程序唯一Id
        /// </summary>
        [MaxLength(200)]
        public string WechatAppletOpenId { get; set; }
        /// <summary>
        /// 授权Id（第三方登录）
        /// </summary>
        [MaxLength(200)]
        public string AppStoreOpenId { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        [MaxLength(200)]
        public string Remark { get; set; }
        /// <summary>
        /// 账号Id
        /// </summary>
        public string PassportId { get; set; }
        /// <summary>
        /// 邀请用户Id
        /// </summary>
        public int FromAppUserId { get; set; }
    }
}
