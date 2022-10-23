using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TN408Project.DataDB;
using TN408Project.Models;
namespace TN408Project.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DonDatController : Controller
    {
        TN408Context db = new TN408Context();
        public IActionResult Index()
        {
            return View();
        }


        private void temp4(object selecttemp4 = null)
        {
            ViewBag.temp4 = new SelectList(db.NguoiDungs.ToList(), "MaNguoiDung", "TenNguoiDung", selecttemp4);
        }
        private void temp5(object selecttemp5 = null)
        {
            ViewBag.Compani = new SelectList(db.TrangThais.ToList(), "MaTrangThai", "TenTrangThai", selecttemp5);
        }

        public IActionResult dondat(int pg = 1)
        {
            int session = (int)HttpContext.Session.GetInt32("taikhoan");
            var qt = (from s in db.QuanTris where s.MaTaiKhoan == session select s.Avatar).Single();
            TempData["data"] = qt;

            temp5();
            temp4();

            List<DonDat> dondats = db.DonDats.ToList();
            List<TrangThai> trangthais = db.TrangThais.ToList();
            List<ChiTietDd> chiTietDds = db.ChiTietDds.ToList();
            List<SanPham> sanPhams = db.SanPhams.ToList();
            if (dondats.Count != 0)
            {
                ViewBag.temp = 1;
                var dondat1 = from t in dondats
                              join a in trangthais on t.MaTrangThai equals a.MaTrangThai into table1
                              from a in table1.DefaultIfEmpty()
                              join c in chiTietDds on t.MaDonDat equals c.MaDonDat into table2
                              from c in table2.DefaultIfEmpty()
                              join s in sanPhams on c.MaSanPham equals s.MaSanPham
                              select new dondatadmin
                              {
                                  dondatdetail = t,
                                  trangthaidetail = a,
                                  sanphamdetail = s,
                                  chitietdetail = c
                              };
                const int pageSize = 2;
                if (pg < 1)
                    pg = 1;
                int recsCount = dondat1.Count();
                var pager = new Pager(recsCount, pg, pageSize);
                int recSkip = (pg - 1) * pageSize;
                var data = dondat1.Skip(recSkip).Take(pager.PageSize).ToList();
                this.ViewBag.Pager = pager;
                return View(data);
            }
            else
            {
                ViewBag.temp = 0;
                return View();
            }
        }


        [HttpPost]
        public async Task<IActionResult> EditDD([FromForm] int madondat, [FromForm] int trangthaiid)
        {
            var DDitem = (from s in db.DonDats where s.MaDonDat == madondat select s).FirstOrDefault();
            if (DDitem != null)
            {
                DDitem.MaTrangThai = trangthaiid;
                db.Update(DDitem);
                await db.SaveChangesAsync();
                if (trangthaiid == 4)
                {
                    HoaDon a = new HoaDon();
                    DateTime now = DateTime.Now;
                    //    //c1 hien khong gio
                    DateTime c = new DateTime(now.Year, now.Month, now.Day);
                    a.NgayGiaoHang = now.Day + "-" + now.Month + "-" + now.Year;
                    //    //c2 hien co gio
                    //a.NgayGiaoHang = now.ToString();    //hien c2 bo hang nay
                    //int b = dondats.MaDonDat;
                    int b = DDitem.MaDonDat;

                    a.MaDonDat = b;
                    db.HoaDons.Add(a);
                    db.SaveChanges();
                }
            }
            else
                return NotFound();
            return RedirectToAction(nameof(dondat));
        }

    }
}
