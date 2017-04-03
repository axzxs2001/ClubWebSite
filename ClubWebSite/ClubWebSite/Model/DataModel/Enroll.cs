using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClubWebSite.Model.DataModel
{
    /// <summary>
    /// 报名实体
    /// </summary>
    public class Enroll
    {
        /// <summary>
        /// ID
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int ID
        { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name
        { get; set; }
        /// <summary>
        /// 联系方式
        /// </summary>
        public string Contact
        { get; set; }
        /// <summary>
        /// 邮箱
        /// </summary>
        public string Email
        { get; set; }
        /// <summary>
        /// 报名时间
        /// </summary>
        public DateTime CreateTime
        {
            get;set;
        }

        /// <summary>
        /// 活动ID
        /// </summary>
        public int ActiveID
        { get; set; }
        /// <summary>
        /// 活动
        /// </summary>
        public Active Active
        { get; set; }
    }
}
