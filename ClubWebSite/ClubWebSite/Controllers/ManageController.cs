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

            return _acctiveResitory.AddActive(new Active()
            {
                Address = activeaddress,
                Name = activename,
                BeginTime = Convert.ToDateTime($"{begindate} {begintime}"),
                EndTime = Convert.ToDateTime($"{enddate} {endtime}"),
                Content = content,
                IsEnroll = isEnroll,
                Logo = logoPath,
                PeopleNumber = peopleNumber,
                CreateTime = DateTime.Now,
                UserID = 1,
                ID = 1
            });
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
                imagePath = @"\upload\myimage\" + DateTime.Now.ToString("yyyyMMddhhmmssfff") + (i++).ToString() + ".jpg";
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
        public JsonResult GetMyActives()
        {
            var userIDChars = User.Claims.SingleOrDefault(s => s.Type == ClaimTypes.Sid)?.Value;
            var actives = new List<Active>();
            if (!string.IsNullOrEmpty(userIDChars))
            {
                var userID = Convert.ToInt32(userIDChars);
                actives = _acctiveResitory.GetActivesByUserID(userID);
            }
            return new JsonResult(actives, new Newtonsoft.Json.JsonSerializerSettings(){
                
            });
        }
    }
}
