using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClubWebSite.Model;
using ClubWebSite.Model.DataModel;

namespace Asp.NetCore_WebPage.Model.Repository
{
    /// <summary>
    /// 用户管理接口
    /// </summary>
    public class UserResitory : IUserResitory
    {
        /// <summary>
        /// 数据库对象
        /// </summary>
        ClubWebSiteDbContext _dbContext;
        public UserResitory(ClubWebSiteDbContext dbContext)
        {
           _dbContext=dbContext;
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="password">密码</param>
        /// <returns></returns>
        public User Login(string userName, string password)
        {
            return  _dbContext.Users.SingleOrDefault(s=>s.UserName==userName&&s.Password==password);
        }
    }
}
