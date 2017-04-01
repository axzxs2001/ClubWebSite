using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClubWebSite.Model.Entity
{
    /// <summary>
    /// 报名实体
    /// </summary>
    public class Enroll:EntityObject
    {
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
    }
}
