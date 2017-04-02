using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Asp.NetCore_WebPage.Model.Repository;

namespace ClubWebSite.Controllers
{
    /// <summary>
    /// home控制类
    /// </summary>
    public class HomeController : Controller
    {
        /// <summary>
        /// 活动仓储对象
        /// </summary>
        IActiveResitory _acctiveResitory;
        /// <summary>
        /// home构造
        /// </summary>
        /// <param name="acctiveResitory"></param>
        public HomeController(IActiveResitory acctiveResitory)
        {
            _acctiveResitory = acctiveResitory;
        }
        /// <summary>
        /// 列表
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 获取分页活动
        /// </summary>
        /// <param name="intexPage">页数</param>
        /// <returns></returns>
        [HttpGet("actives/{pageindex}")]
        public JsonResult GetActives(int pageindex)
        {
            // var result = _acctiveResitory.GetActivePage(pageindex, 10);

            //return new JsonResult(result, new Newtonsoft.Json.JsonSerializerSettings()
            //{
            //    Formatting = Newtonsoft.Json.Formatting.None
            //});
            return null;
        }
        /// <summary>
        /// 详细信息
        /// </summary>
        /// <returns></returns>
        [HttpGet("item/{id}")]
        public IActionResult Details(int id)
        {
            ViewData["Message"] = "Your application description page." + id;

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
