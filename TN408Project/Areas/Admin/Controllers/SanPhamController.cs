using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TN408Project.DataDB;
using AspNetCoreHero.ToastNotification.Abstractions;
using TN408Project.Models;
namespace TN408Project.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SanPhamController : Controller
    {

        TN408Context db = new TN408Context();
        
        /// <summary>
        /// //
        /// </summary>
        public INotyfService thongbao { get; }
        public SanPhamController(INotyfService notyfService)
        {
            thongbao = notyfService;
        }


      /// <summary>
      /// 
      /// </summary>
      /// <returns></returns>

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult loaisanpham()
        {

            int session = (int)HttpContext.Session.GetInt32("taikhoan");
            var qt = (from s in db.QuanTris where s.MaTaiKhoan == session select s.Avatar).Single();
            TempData["data"] = qt;

            List<LoaiSanPham> loaiSanPhams = db.LoaiSanPhams.ToList();
            return View(loaiSanPhams);
        }


        public ActionResult Create()
        {            
            return View();
        }


        [HttpPost, ActionName("Create")]
        [ValidateAntiForgeryToken]
        public ActionResult Create(LoaiSanPham loaisanpham1)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.LoaiSanPhams.Add(loaisanpham1);
                    db.SaveChanges();
                    // return RedirectToAction("loainongsan");
                    thongbao.Success("Thêm loại sản phẩm thành công");
                    return RedirectToAction(nameof(loaisanpham));
                }
                catch (Exception)
                {
                    thongbao.Warning("Thêm loại sản phẩm thất bại");
                    ModelState.AddModelError("", "Loi");
                    return View(loaisanpham1);
                }
            }
            return View(loaisanpham1);
        }


        public IActionResult sanpham()
        {
            int session = (int)HttpContext.Session.GetInt32("taikhoan");
            var qt = (from s in db.QuanTris where s.MaTaiKhoan == session select s.Avatar).Single();
            TempData["data"] = qt;

            List<SanPham> sanPhams = (db.SanPhams.Where(s => s.TrangThaiTt == 1)).ToList();
            return View(sanPhams);

        }


        public ActionResult Createsp()
        {
            temp1();
            temp2();
            return View();
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateUploadFile(CreateSPIMG sanpham1, IFormFile myfile)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    //Lay ten luu vao bien fii
                    var fii = Path.GetFileName(myfile.FileName);
                    //Chi dinh duong dan se luu
                    string fullPAth = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "MyFiles", myfile.FileName);

                    //copy file vao thu muc chi dinh
                    using (var file = new FileStream(fullPAth, FileMode.Create))
                    {
                        myfile.CopyTo(file);
                    }
                    int d = 1;
                    SanPham a = new SanPham();
                    a.MaLoaiSanPham = sanpham1.sanphamdetail.MaLoaiSanPham;
                    a.MaNhaSanXuat = sanpham1.sanphamdetail.MaNhaSanXuat;
                    a.TenSanPham = sanpham1.sanphamdetail.TenSanPham;
                    a.Gia = sanpham1.sanphamdetail.Gia;
                    a.MoTa = sanpham1.sanphamdetail.MoTa;
                    a.SoLuong = sanpham1.sanphamdetail.SoLuong;
                    a.TrangThaiTt = d;
                    
                    db.SanPhams.Add(a);
                    //               await db.SaveChangesAsync();
                    await db.SaveChangesAsync();
                    AnhSp b = new AnhSp();
                    b.MaSanPham = a.MaSanPham;
                    b.LinkAnh = fii;
                    b.MoTa = "Chưa cập nhật";
                    //  nongSan.Link_Anh = fii;
                    //  db.AnhNs.Add(b);
                    db.AnhSps.Add(b);
                    await db.SaveChangesAsync();
                    thongbao.Success("Thêm sản phẩm thành công");
                    return RedirectToAction(nameof(sanpham));
                }
                catch (Exception)
                {
                    thongbao.Warning("Vui lòng chọn ảnh");
                    ModelState.AddModelError("", "Vui long chon anh");
                    return View(sanpham1);
                }
            }
            return View(sanpham1);
        }

        private void temp1(object selecttemp1 = null)
        {
            ViewBag.temp1 = new SelectList(db.NhaSanXuats.ToList(), "MaNhaSanXuat", "TenNhaSanXuat", selecttemp1);
        }
        private void temp2(object selecttemp2 = null)
        {
            ViewBag.temp2 = new SelectList(db.LoaiSanPhams.ToList(), "MaLoaiSanPham", "TenLoaiSanPham", selecttemp2);
        }


        [HttpGet]
        public IActionResult Delete(int? id)
        {

            var sanpham1 = db.SanPhams.Find(id);
            if (sanpham1 == null)
            {
                return NotFound();
            }

            return View(sanpham1);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var sp = db.SanPhams.Find(id);
            if (sp == null)
            {
                return RedirectToAction(nameof(Index));
            }

            try
            {
                sp.TrangThaiTt = 2;
                db.SanPhams.Update(sp);
                db.SaveChanges();
                return RedirectToAction(nameof(sanpham));
            }
            catch (DbUpdateException)
            {
                return RedirectToAction(nameof(Delete), new { id = id, saveChangesError = true });
            }
        }

        [HttpGet]
        public IActionResult DeleteLSP(int? id)
        {
            var lsp = db.LoaiSanPhams.Find(id);
            if (lsp == null)
            {
                return NotFound();
            }

            return View(lsp);
        }

        [HttpPost, ActionName("DeleteLSP")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteLSP(int id)
        {
            var loaisp = db.LoaiSanPhams.Find(id);
            if (loaisp == null)
            {
                return RedirectToAction(nameof(loaisanpham));
            }
            try
            {
                db.LoaiSanPhams.Remove(loaisp);
                db.SaveChanges();
                return RedirectToAction(nameof(loaisanpham));
            }
            catch (DbUpdateException)
            {
                return RedirectToAction(nameof(DeleteLSP), new { id = id, saveChangesError = true });
            }
        }

        [HttpGet]
        public IActionResult UpdateLSP(int? id)
        {
            var lsp = db.LoaiSanPhams.Find(id);
            if (lsp == null)
            {
                return NotFound();
            }

            return View(lsp);
        }

        [HttpPost, ActionName("UpdateLSP")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateLSP(int id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var lsp = db.LoaiSanPhams.FirstOrDefault(s => s.MaLoaiSanPham == id);
            if (await TryUpdateModelAsync<LoaiSanPham>(lsp, "", s => s.TenLoaiSanPham))
            {
                try
                {
                    db.SaveChanges();
                    return RedirectToAction(nameof(loaisanpham));
                }
                catch (DbUpdateException)
                {
                    ModelState.AddModelError("", "Unable to save changes. " +
                        "Try again, and if the problem persists, " +
                        "see your system administrator.");
                }
            }
            return View(lsp);
        }

        [HttpGet]
        public IActionResult UpdateSP(int? id)
        {
           
            var sp = db.SanPhams.Find(id);
            if (sp == null)
            {
                return NotFound();
            }
            temp1();
            temp2();
            return View(sp);
        }


        [HttpPost, ActionName("UpdateSP1")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateSP1(SanPham sp)
        {
            if (sp.MaSanPham == null)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {              
                db.SanPhams.Update(sp);
                await db.SaveChangesAsync();
                return RedirectToAction(nameof(sanpham));
            }
            return View(sp);
        }

    }
}
