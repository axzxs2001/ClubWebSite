using ClubWebSite.Model;
using ClubWebSite.Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Asp.NetCore_WebPage.Model.Repository
{
    /// <summary>
    /// 活动仓储类
    /// </summary>
    public class ActiveResitory : IActiveResitory
    {
        /// <summary>
        /// 数据库对象
        /// </summary>
        DataHandle _dataHandle;
        /// <summary>
        /// 权限仓储类构造
        /// </summary>
        /// <param name="dbContext">startup注入的数据库对象</param>
        public ActiveResitory(DataHandle dataHandle)
        {
            _dataHandle = dataHandle;
        }
        /// <summary>
        /// 添加实体
        /// </summary>
        /// <param name="active">活动实体</param>
        /// <returns></returns>
        public bool AddActive(Active active)
        {
            return _dataHandle.AddEntity(active);
        }
        /// <summary>
        /// 获取页数和总页数
        /// </summary>
        /// <param name="pageIndex">页数索引</param>
        /// <param name="countPerPage">每页记录数</param>
        /// <returns></returns>
        public (List<Active> actives, int pageCount) GetActivePage(int pageIndex, int countPerPage)
        {
            var result = _dataHandle.GetPageEntities<Active>(pageIndex, countPerPage);
            //总行数转成页数
            var pageCount = Convert.ToInt32(Math.Ceiling(result.count / Convert.ToDouble(countPerPage)));
            return (result.entities, pageCount);
        }
        /// <summary>
        /// 获取单个活动
        /// </summary>
        /// <param name="id">编号</param>
        /// <returns></returns>
        public Active GetActive(string id)
        {
           return  _dataHandle.GetEntity<Active>(id);
        }

    }
}
