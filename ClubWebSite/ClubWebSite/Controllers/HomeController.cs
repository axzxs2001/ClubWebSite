using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Asp.NetCore_WebPage.Model.Repository;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using ClubWebSite.Model.DataModel;
using Microsoft.AspNetCore.Authentication;

namespace ClubWebSite.Controllers
{

    /// <summary>
    /// home控制类
    /// </summary>
    //[Authorize(Roles = "admin")]
    public class HomeController : Controller
    {
        /// <summary>
        /// 活动仓储对象
        /// </summary>
        IActiveResitory _acctiveResitory;
        /// <summary>
        /// 用户仓储对象
        /// </summary>
        IUserResitory _userResitory;
        /// <summary>
        /// home构造
        /// </summary>
        /// <param name="acctiveResitory"></param>
        public HomeController(IActiveResitory acctiveResitory,IUserResitory userResitory)
        {
            _acctiveResitory = acctiveResitory;
            _userResitory = userResitory;
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
             var result = _acctiveResitory.GetActivePage(pageindex, 10);

            return new JsonResult(result, new Newtonsoft.Json.JsonSerializerSettings()
            {
                Formatting = Newtonsoft.Json.Formatting.None
            });
        }
        /// <summary>
        /// 获取未举办的活动
        /// </summary>
        /// <returns></returns>
        [HttpGet("validactives")]
        public JsonResult GetValidActives()
        {
            var result = _acctiveResitory.GetValidityActives();
            return new JsonResult(result, new Newtonsoft.Json.JsonSerializerSettings()
            {
                DateFormatString ="yyyy年MM月dd日HH时mm分"
            });
        }
        /// <summary>
        /// 获取历史举办的活动
        /// </summary>
        /// <returns></returns>
        [HttpGet("hostoryactives")]
        public JsonResult GetHostoryActives()
        {
            var result = _acctiveResitory.GetNoValidityActives();
            return new JsonResult(result, new Newtonsoft.Json.JsonSerializerSettings()
            {
                DateFormatString = "yyyy年MM月dd日HH时mm分"
            });
        }
        /// <summary>
        /// 活动报名页
        /// </summary>
        /// <returns></returns>
        [HttpGet("active/{id}")]
        public IActionResult Active(int? id)
        {
            ViewData["id"] = id;
            return View();
        }
        /// <summary>
        /// 历史活动页
        /// </summary>
        /// <returns></returns>
        [HttpGet("hisactive/{id}")]
        public IActionResult HisActive(int? id)
        {
            ViewData["id"] = id;
            return View();
        }

        /// <summary>
        /// 按照ID获取活动信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("getactive")]
        public JsonResult GetActive(int id)
        {
            var result=_acctiveResitory.GetActive(id);
            var count = _acctiveResitory.GetEnrollCountByActiveID(id);
            return new JsonResult(new { Active = result, EnrollCount = count }, new Newtonsoft.Json.JsonSerializerSettings()
            {
                DateFormatString = "yyyy年MM月dd日 HH:mm:00"
            });
        }

        /// <summary>
        /// 添加报名信息
        /// </summary>
        /// <param name="enroll">报名信息</param>
        /// <returns></returns>
        [HttpPost("enroll")]
        public JsonResult SavaEnroll(Enroll enroll)
        {
            var result = _acctiveResitory.AddEnroll(enroll);
            return new JsonResult(new { BackResult = result.BackResult, Message = result.Message }, new Newtonsoft.Json.JsonSerializerSettings()
            {
            });
        }

        /// <summary>
        /// 错误
        /// </summary>
        /// <returns></returns>
        public IActionResult Error()
        {
            return View();
        }


        #region 登录页
        /// <summary>
        /// 登录页
        /// </summary>
        /// <param name="returnUrl"></param>
        /// <returns></returns> 
        [AllowAnonymous]
        [Route("login")]
        public IActionResult Login(string returnUrl)
        {
            //判断是否验证
            if (!HttpContext.User.Identity.IsAuthenticated)
            {
                //把返回地址保存在前台的hide表单中
                ViewBag.returnUrl = returnUrl;
            }
            ViewBag.error = null;
            HttpContext.SignOutAsync("loginvalidate");
            return View();
        }
        /// <summary>
        /// 实现登录
        /// </summary>
        /// <param name="fname"></param>
        /// <param name="password"></param>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("login")]
        public IActionResult Login(string userName, string password, string returnUrl)
        {
            //查询users
            var user = _userResitory.Login(userName, password);
            if (user!=null)
            {
                var claims = new Claim[]
                {
                    new Claim(ClaimTypes.UserData,user.UserName),
                    new Claim(ClaimTypes.Role,"admin"),
                    new Claim(ClaimTypes.Name,user.Name),
                    new Claim(ClaimTypes.Sid,user.ID.ToString())
                 };
                HttpContext.SignOutAsync("loginvalidate");
                HttpContext.SignInAsync("loginvalidate", new ClaimsPrincipal(new ClaimsIdentity(claims, "Cookie")));
                HttpContext.User = new ClaimsPrincipal(new ClaimsIdentity(claims));
                return new RedirectResult(returnUrl == null ? "/myactives" : returnUrl);
            }
            else
            {
                ViewBag.error = "用户名或密码错误！";
                return View();
            }
        }

        #endregion
    }
}
