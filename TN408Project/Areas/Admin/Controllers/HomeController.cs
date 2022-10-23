using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TN408Project.DataDB;
namespace TN408Project.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        TN408Context db = new TN408Context();
        public IActionResult Index()
        {

            int session = (int)HttpContext.Session.GetInt32("taikhoan");
            var qt = (from s in db.QuanTris where s.MaTaiKhoan == session select s.Avatar).Single();
            TempData["data"] = qt;

            ViewBag.countND = (from s in db.TaiKhoans where s.MaQuyen == 2 select s).ToList().Count();
            ViewBag.countloaisp = (from d in db.SanPhams where d.TrangThaiTt == 1 select d).ToList().Count();
          
            return View();
        }

        [Route("Logout")]
        [HttpPost]
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("remember");
            HttpContext.Session.Remove("username");
            HttpContext.Session.Clear();

  
            Response.Cookies.Delete(".AspNetCore.Session");

            return RedirectToAction("login");
        }
    }
}
