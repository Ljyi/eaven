using Eaven.Ven.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eaven.Ven.Domain
{
    /// <summary>
    /// AppUserAddress【用户收货地址表】
    /// </summary>
    [Table("AppUserAddress")]
    public class AppUserAddress : EntityModel
    {
        /// <summary>
        /// 收件人
        /// </summary>
        [Required]
        [MaxLength(50)]
        public string ReceiverName { get; set; }
        /// <summary>
        /// 联系电话
        /// </summary>
        [Required]
        [MaxLength(20)]
        public string Phone { get; set; }
        /// <summary>
        /// 省份
        /// </summary>
        [Required]
        [MaxLength(50)]
        public string Province { get; set; }
        /// <summary>
        /// 城市
        /// </summary>
        [Required]
        [MaxLength(50)]
        public string City { get; set; }
        /// <summary>
        /// 区
        /// </summary>
        [Required]
        [MaxLength(50)]
        public string Area { get; set; }
        /// <summary>
        /// 街道
        /// </summary> 
        [Required]
        [MaxLength(200)]
        public string Street { get; set; }
        /// <summary>
        /// 详细地址
        /// </summary>  
        [Required]
        [MaxLength(500)]
        public string DetailedAddress { get; set; }
        /// <summary>
        /// 0：家
        /// 1：公司
        /// 2：学习
        /// </summary>
        public int Type { get; set; }
        /// <summary>
        /// 默认收货地址
        /// </summary>
        public bool Default { get; set; }
        /// <summary>
        /// 用户Id
        /// </summary>
        [Required]
        public int AppUserId { get; set; }
    }
}
