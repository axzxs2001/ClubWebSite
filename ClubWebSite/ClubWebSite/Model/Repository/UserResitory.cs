using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClubWebSite.Model.Entity;
using ClubWebSite.Model;

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
        DataHandle _dataHandle;
        /// <summary>
        /// 权限仓储类构造
        /// </summary>
        /// <param name="dbContext">startup注入的数据库对象</param>
        public UserResitory(DataHandle dataHandle)
        {
            _dataHandle = dataHandle;
        }
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="password">密码</param>
        /// <returns></returns>
        public User Login(string userName, string password)
        {
            var users = _dataHandle.GetAll(typeof(User));
            return users.SingleOrDefault(s => (s as User).UserName == userName && (s as User).Password == password) as User;
        }
    }
}
