using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Asp.NetCore_WebPage.Model.Repository;
using ClubWebSite.Model.DataModel;
using Microsoft.AspNetCore.Hosting;

using System.IO;
using System.Security.Claims;

namespace ClubWebSite.Controllers
{
    /// <summary>
    /// 管理控制器
    /// </summary>
    [Authorize(Roles = "admin")]
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
        /// <param name="activeName">活动名称</param>
        /// <param name="activeAddress">活动地址</param>
        /// <param name="beginDate">开始日期</param>
        /// <param name="beginTime">开始时间</param>
        /// <param name="endDate">结束日期</param>
        /// <param name="endTime">结束时间</param>
        /// <param name="content">内容</param>
        /// <param name="isEnroll">是否报名</param>
        /// <param name="logoPath">海报</param>
        /// <param name="peopleNumber">活动计划人数</param>
        /// <returns></returns>

        [HttpPost("addactive")]

        public bool AddActive(string activeName, string activeAddress, string beginDate, string beginTime, string endDate, string endTime, string content, bool isEnroll, string logoPath, int peopleNumber)
        {
            var userIDChars = User.Claims.SingleOrDefault(s => s.Type == ClaimTypes.Sid)?.Value;
            if (!string.IsNullOrEmpty(userIDChars))
            {
                var userID = Convert.ToInt32(userIDChars);
                return _acctiveResitory.AddActive(new Active()
                {
                    Address = activeAddress,
                    Name = activeName,
                    BeginTime = Convert.ToDateTime($"{beginDate} {beginTime}"),
                    EndTime = Convert.ToDateTime($"{endDate} {endTime}"),
                    Content = content,
                    IsEnroll = isEnroll,
                    Logo = logoPath,
                    PeopleNumber = peopleNumber,
                    CreateTime = DateTime.Now,
                    UserID = userID

                });
            }else
            {
                return false;
            }
        }
        /// <summary>
        /// 上传图片
        /// </summary>
        /// <param name="env"></param>
        /// <returns></returns>
        [HttpPost("uploadimage")]
        public string UploadImg([FromServices]IHostingEnvironment env)
        {
 
            var files = HttpContext.Request.Form.Files;
            int i = 0;
            var imagePath = "";
     
            if (files.Count > 0)
            {
               
                var file = files[0];
                imagePath = @"/upload/myimage/" + DateTime.Now.ToString("yyyyMMddhhmmssfff") + (i++).ToString() + ".jpg";
                var stream = file.OpenReadStream();
                var bytes = new byte[stream.Length];
                stream.Read(bytes, 0, bytes.Length);
            
                var filestream = new FileStream(env.WebRootPath + imagePath, FileMode.CreateNew, FileAccess.ReadWrite);
                filestream.Write(bytes, 0, bytes.Length);
                filestream.Flush();
                filestream.Dispose();
            }
            return imagePath;
        }


        /// <summary>
        /// 获取我的活动
        /// </summary>
        /// <returns></returns>
        [HttpGet("myactives")]
        public IActionResult MyActives()
        {
            return View();
        }
        /// <summary>
        /// 获取我的活动
        /// </summary>
        /// <returns></returns>
        [HttpGet("getmyactives")]
        public JsonResult GetMyActives()
        {
            var userIDChars = User.Claims.SingleOrDefault(s => s.Type == ClaimTypes.Sid)?.Value;
            var actives = new List<Active>();
            if (!string.IsNullOrEmpty(userIDChars))
            {
                var userID = Convert.ToInt32(userIDChars);
                actives = _acctiveResitory.GetActivesByUserID(userID);
            }
            return new JsonResult(actives, new Newtonsoft.Json.JsonSerializerSettings()
            {
              DateFormatString="yyyy年MM月dd日 HH:mm:ss"
            });
        }
        /// <summary>
        /// 按活动ID查询报名信息
        /// </summary>
        /// <param name="activeID">活动ID</param>
        /// <returns></returns>

        [HttpGet("getactiveenrolls")]
        public JsonResult GetActiveEnrolls(int activeID)
        {
            var enrolls = _acctiveResitory.GetEnrollsByActiveID(activeID);
            return new JsonResult(enrolls, new Newtonsoft.Json.JsonSerializerSettings()
            {
                DateFormatString="yyyy年MM月dd日"
            });
        }
        /// <summary>
        /// 修改活动
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("/modifyactive/{id}")]
        public IActionResult ModiyActive(int? id)
        {
            ViewData["activeid"] = id;
            return View();
        }


        /// <summary>
        /// 修改活动
        /// </summary>
        /// <param name="activeID">活动ID</param>
        /// <param name="activeName">活动名称</param>
        /// <param name="activeAddress">活动地址</param>
        /// <param name="beginDate">开始日期</param>
        /// <param name="beginTime">开始时间</param>
        /// <param name="endDate">结束日期</param>
        /// <param name="endTime">结束时间</param>
        /// <param name="content">内容</param>
        /// <param name="isEnroll">是否报名</param>
        /// <param name="logoPath">海报</param>
        /// <param name="peopleNumber">活动计划人数</param>
        /// <returns></returns>
        [HttpPost("modifyactive")]
        public bool ModifyActive(int activeID,string activeName, string activeAddress, string beginDate, string beginTime, string endDate, string endTime, string content, bool isEnroll, string logoPath, int peopleNumber)
        {
            var userIDChars = User.Claims.SingleOrDefault(s => s.Type == ClaimTypes.Sid)?.Value;
            if (!string.IsNullOrEmpty(userIDChars))
            {
                var userID = Convert.ToInt32(userIDChars);
                return _acctiveResitory.ModifyActive(new Active()
                {
                    ID=activeID,
                    Address = activeAddress,
                    Name = activeName,
                    BeginTime = Convert.ToDateTime($"{beginDate} {beginTime}"),
                    EndTime = Convert.ToDateTime($"{endDate} {endTime}"),
                    Content = content,
                    IsEnroll = isEnroll,
                    Logo = logoPath,
                    PeopleNumber = peopleNumber,
                    CreateTime = DateTime.Now,
                    UserID = userID

                });
            }
            else
            {
                return false;
            }
        }
    }
}
