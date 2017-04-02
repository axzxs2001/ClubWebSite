using ClubWebSite.Model.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Asp.NetCore_WebPage.Model.Repository
{
    /// <summary>
    /// 用户管理接口
    /// </summary>
    public interface IUserResitory
    {
        User Login(string userName, string password);
    }
}
