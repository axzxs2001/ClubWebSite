using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClubWebSite.Model.Entity
{
    /// <summary>
    /// 用户实体类
    /// </summary>
    public class User:EntityObject
    {
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
        
    }
}
