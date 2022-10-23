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
    public class UserController : Controller
    {
        TN408Context db = new TN408Context();
        public IActionResult Index(int pg = 1)
        {
            int session = (int)HttpContext.Session.GetInt32("taikhoan");
            var qt = (from s in db.QuanTris where s.MaTaiKhoan == session select s.Avatar).Single();
            TempData["data"] = qt;

            //List<NguoiDung> nguoiDungs = db.NguoiDungs.ToList();

            var nguoidung = (from s in db.NguoiDungs join b in db.TaiKhoans on s.MaTaiKhoan equals b.MaTaiKhoan where b.MaQuyen == 2 select s).ToList();


            const int pageSize = 4;
            if (pg < 1)
                pg = 1;
            int recsCount = nguoidung.Count();
            var pager = new Pager(recsCount, pg, pageSize);
            int recSkip = (pg - 1) * pageSize;
            var data = nguoidung.Skip(recSkip).Take(pager.PageSize).ToList();
            this.ViewBag.Pager = pager;
            return View(data);
        }
    }
}
