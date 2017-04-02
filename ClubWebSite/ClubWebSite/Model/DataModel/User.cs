using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClubWebSite.Model.DataModel
{
    /// <summary>
    /// 用户实体类
    /// </summary>
    public class User
    {
        /// <summary>
        /// ID
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int ID
        { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName
        { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string Password
        { get; set; }

        /// <summary>
        /// 用户名称
        /// </summary>
        public string Name
        { get; set; }

        /// <summary>
        /// 活动集合
        /// </summary>

        public List<Active> Actives
        { get; set; }
        
    }
}
