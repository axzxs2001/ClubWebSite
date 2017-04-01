using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace ClubWebSite.Controllers
{
    [Authorize(Roles = "admin")]
    public class ManageController : Controller
    {
        /// <summary>
        /// 添加活动
        /// </summary>
        /// <returns></returns>
        [HttpGet("add")]
        public IActionResult AddActive()
        {
            return View();
        }
        /// <summary>
        /// 详细信息
        /// </summary>
        /// <returns></returns>
        [HttpPost("add")]
        public IActionResult AddActive(int id)
        {
            ViewData["Message"] = "Your application description page."+id;

            return View();
        }

       
    }
}
