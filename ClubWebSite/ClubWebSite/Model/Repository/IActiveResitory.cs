using ClubWebSite.Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Asp.NetCore_WebPage.Model.Repository
{
    /// <summary>
    /// 活动管理仓储接口
    /// </summary>
    public interface IActiveResitory
    {
        /// <summary>
        /// 添加实体
        /// </summary>
        /// <param name="active">活动实体</param>
        /// <returns></returns>
        bool AddActive(Active active);
        /// <summary>
        /// 获取页数和总页数
        /// </summary>
        /// <param name="pageIndex">页数索引</param>
        /// <param name="countPerPage">每页记录数</param>
        /// <returns></returns>
        (List<Active> actives, int pageCount) GetActivePage(int pageIndex, int countPerPage);
        /// <summary>
        /// 获取单个活动
        /// </summary>
        /// <param name="id">编号</param>
        /// <returns></returns>
        Active GetActive(string id);
    }
}
