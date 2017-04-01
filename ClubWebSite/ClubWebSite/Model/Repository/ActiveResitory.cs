using ClubWebSite.Model;
using ClubWebSite.Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Asp.NetCore_WebPage.Model.Repository
{
    /// <summary>
    /// 权限仓储类
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
        /// <param name="active"></param>
        /// <returns></returns>
        //public bool AddActive(Active active)
        //{
        //   return  _dataHandle.AddEntity<Active>(active);
        //}
        
    }
}
