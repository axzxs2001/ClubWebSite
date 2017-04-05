using ClubWebSite.Model.DataModel;
using System.Collections.Generic;

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
        Active GetActive(int id);

        /// <summary>
        /// 按用户ID获取活动
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        List<Active> GetActivesByUserID(int userID);

        /// <summary>
        /// 按照活动ID查询报名信息
        /// </summary>
        /// <param name="activeID"></param>
        /// <returns></returns>
        List<Enroll> GetEnrollsByActiveID(int activeID);

        /// <summary>
        /// 查询没有活动完的活动
        /// </summary>
        /// <returns></returns>
        List<Active> GetValidityActives();
        /// <summary>
        /// 查询参加完活动的人
        /// </summary>
        /// <returns></returns>
        List<Active> GetNoValidityActives();

        /// <summary>
        /// 添加报名信息
        /// </summary>
        /// <param name="enroll">报名信息</param>
        /// <returns></returns>
        (bool BackResult, string Message) AddEnroll(Enroll enroll);

        /// <summary>
        /// 按照活动ID查询报表的总数
        /// </summary>
        /// <param name="activeID">活动ID</param>
        /// <returns></returns>
        int GetEnrollCountByActiveID(int activeID);
        /// <summary>
        /// 修改活动
        /// </summary>
        /// <param name="newActive">新活动</param>
        /// <returns></returns>
        bool ModifyActive(Active newActive);
    }
}
