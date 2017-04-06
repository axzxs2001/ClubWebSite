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
        /// <summary>
        /// 按用户ID获取活动
        /// </summary>
        /// <param name="userID">用户ID</param>
        /// <returns></returns>
        public List<Active> GetActivesByUserID(int userID)
        {
            return _dbContext.Actives.Where(w => w.UserID == userID).OrderByDescending(o => o.CreateTime).ToList();
        }

        /// <summary>
        /// 按照活动ID查询报名信息
        /// </summary>
        /// <param name="activeID"></param>
        /// <returns></returns>
        public List<Enroll> GetEnrollsByActiveID(int activeID)
        {
            return _dbContext.Enrolls.Where(w => w.ActiveID == activeID).ToList();
        }
        /// <summary>
        /// 查询没有活动完的活动
        /// </summary>
        /// <returns></returns>
        public List<Active> GetValidityActives()
        {
            return _dbContext.Actives.Where(w => w.EndTime > DateTime.Now).ToList();
        }
        /// <summary>
        /// 查询参加完活动的人
        /// </summary>
        /// <returns></returns>
        public List<Active> GetNoValidityActives()
        {
            return _dbContext.Actives.Where(w => w.EndTime< DateTime.Now).Take(60).ToList();
        }

        /// <summary>
        /// 添加报名信息
        /// </summary>
        /// <param name="enroll">报名信息</param>
        /// <returns></returns>
        public (bool BackResult, string Message) AddEnroll(Enroll enroll)
        {
            var count = _dbContext.Enrolls.Where(s => s.Contact == enroll.Contact && s.ActiveID == enroll.ActiveID).Count();
            var backResult = false;
            var message = "";
            if (count == 0)
            {
                enroll.CreateTime = DateTime.Now;
                _dbContext.Enrolls.Add(enroll);
                var result = _dbContext.SaveChanges();
                backResult = result > 0 ? true : false;
            }
            else
            {
                backResult = false;
                message = "该手机号已报过名，不能重复报名！";
            }
            return (backResult, message);

        }

        /// <summary>
        /// 按照活动ID查询报表的总数
        /// </summary>
        /// <param name="activeID">活动ID</param>
        /// <returns></returns>
        public int GetEnrollCountByActiveID(int activeID)
        {
            return _dbContext.Enrolls.Where(w => w.ActiveID == activeID).Count();
        }

        /// <summary>
        /// 修改活动
        /// </summary>
        /// <param name="newActive">新活动</param>
        /// <returns></returns>
        public bool ModifyActive(Active newActive)
        {
            var oldActive = _dbContext.Actives.SingleOrDefault(s => s.ID == newActive.ID);
            if (oldActive == null)
            {
                return false;
            }
            else
            {
                oldActive.Address = newActive.Address;
                oldActive.BeginTime = newActive.BeginTime;
                oldActive.EndTime = newActive.EndTime;
                oldActive.Content = newActive.Content;
                oldActive.IsEnroll = newActive.IsEnroll;
                if (!string.IsNullOrEmpty(newActive.Logo))
                {
                    oldActive.Logo = newActive.Logo;
                }
                oldActive.Name = newActive.Name;
                oldActive.PeopleNumber = newActive.PeopleNumber;
                oldActive.UserID = newActive.UserID;
                var result = _dbContext.SaveChanges();
                return result > 0;
            }


        }
    }
}
