using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Asp.NetCore_WebPage.Model.Repository;
using ClubWebSite.Model.DataModel;

namespace ClubWebSite.Controllers
{
    // [Authorize(Roles = "admin")]
    public class ManageController : Controller
    {
        /// <summary>
        /// 活动仓储对象
        /// </summary>
        IActiveResitory _acctiveResitory;
        /// <summary>
        /// home构造
        /// </summary>
        /// <param name="acctiveResitory"></param>
        public ManageController(IActiveResitory acctiveResitory)
        {
            _acctiveResitory = acctiveResitory;
        }
        /// <summary>
        /// 添加活动
        /// </summary>
        /// <returns></returns>
        [HttpGet("addactive")]
        public IActionResult AddActive()
        {
            return View();
        }

        /// <summary>
        /// 添加活动
        /// </summary>
        /// <param name="activename"></param>
        /// <param name="activeaddress"></param>
        /// <param name="begindate"></param>
        /// <param name="begintime"></param>
        /// <param name="enddate"></param>
        /// <param name="endtime"></param>
        /// <param name="content"></param>
        /// <param name="isEnroll"></param>
        /// <param name="logoPath"></param>
        /// <param name="peopleNumber"></param>
        /// <returns></returns>

        [HttpPost("addactive")]

        public bool SavaActive(string activename, string activeaddress, string begindate, string begintime, string enddate, string endtime, string content, bool isEnroll, string logoPath, int peopleNumber)
        {
            return true;
            //return _acctiveResitory.AddActive(new Active()
            //{
            //    Address = activeaddress,
            //    Name = activename,
            //    BeginTime = Convert.ToDateTime($"{begindate} {begintime}"),
            //    EndTime = Convert.ToDateTime($"{enddate} {endtime}"),
            //    Content = content,
            //    IsEnroll = isEnroll,
            //    Logo = logoPath,
            //    PeopleNumber = peopleNumber,
            //    CreateTime = DateTime.Now,
            //    ID =1
            //});
        }

    }
}
