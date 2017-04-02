using ClubWebSite.Model;
using ClubWebSite.Model.DataModel;
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
        ClubWebSiteDbContext _dbContext;
        /// <summary>
        /// 权限仓储类构造
        /// </summary>
        /// <param name="dbContext">startup注入的数据库对象</param>
        public ActiveResitory(ClubWebSiteDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        /// <summary>
        /// 添加实体
        /// </summary>
        /// <param name="active">活动实体</param>
        /// <returns></returns>
        public bool AddActive(Active active)
        {
            _dbContext.Actives.Add(active);
            var result = _dbContext.SaveChanges();
            return result > 0;
        }
        /// <summary>
        /// 获取页数和总页数
        /// </summary>
        /// <param name="pageIndex">页数索引</param>
        /// <param name="countPerPage">每页记录数</param>
        /// <returns></returns>
        public (List<Active> actives, int pageCount) GetActivePage(int pageIndex, int countPerPage)
        {
            var actives = _dbContext.Actives.Skip((pageIndex - 1) * countPerPage).Take(countPerPage).ToList();
            var count = _dbContext.Actives.Count();
            //总行数转成页数
            var pageCount = Convert.ToInt32(Math.Ceiling(count / Convert.ToDouble(countPerPage)));
            return (actives, pageCount);
        }
        /// <summary>
        /// 获取单个活动
        /// </summary>
        /// <param name="id">编号</param>
        /// <returns></returns>
        public Active GetActive(int id)
        {
            return _dbContext.Actives.SingleOrDefault(s => s.ID == id);
        }

    }
}
