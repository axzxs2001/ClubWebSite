using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClubWebSite.Model.DataModel
{
    /// <summary>
    /// 活动实体
    /// </summary>
    public class Active 
    {
        /// <summary>
        /// ID
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int ID
        { get; set; }
        /// <summary>
        /// 活动名称
        /// </summary>
        public string Name
        {
            get; set;
        }
        /// <summary>
        /// 活动开始时间
        /// </summary>
        public DateTime BeginTime
        {
            get; set;
        }
        /// <summary>
        /// 活动结束时间
        /// </summary>
        public DateTime EndTime
        {
            get; set;
        }
        /// <summary>
        /// 活动地址
        /// </summary>
        public string Address
        {
            get; set;
        }
        /// <summary>
        /// 活动海报
        /// </summary>
        public string Logo
        {
            get; set;
        }
        /// <summary>
        /// 活动人数
        /// </summary>
        public int PeopleNumber
        { get; set; }
        /// <summary>
        /// 数活内容
        /// </summary>
        public string Content
        {
            get; set;
        }
        /// <summary>
        /// 活动时间
        /// </summary>
        public DateTime CreateTime
        {
            get; set;
        }
        /// <summary>
        /// 用户ID
        /// </summary>
        public int UserID
        { get; set; }
        /// <summary>
        /// 创建活动人员
        /// </summary>
        public User CreateUser
        {
            get; set;
        }
        /// <summary>
        /// 是否开放报名
        /// </summary>
        public bool IsEnroll
        {
            get;set;
        }
        /// <summary>
        /// 报名列表
        /// </summary>
        public List<Enroll> Enrolls
        { get; set; }



    }
}
