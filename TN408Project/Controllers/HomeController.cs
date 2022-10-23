using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using TN408Project.Models;
using TN408Project.DataDB;
using Microsoft.AspNetCore.Http;

namespace TN408Project.Controllers
{
    public class HomeController : Controller
    {
        TN408Context db = new TN408Context();
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index(int pg = 1)
        {
            //var list = from s in db.SanPhams select s;
            //list = list.Take(16);
            //return View(list);
            int i = 1;
            List<SanPham> sanPhams = db.SanPhams.ToList();
            List<LoaiSanPham> loaiSanPhams = db.LoaiSanPhams.ToList();
            List<NhaSanXuat> nhaSanXuats = db.NhaSanXuats.ToList();
            List<AnhSp> anhSps = db.AnhSps.ToList();
            var index = from s in sanPhams
                        join a in anhSps on s.MaSanPham equals a.MaSanPham into table1
                        from a in table1.DefaultIfEmpty()
                        join l in loaiSanPhams on s.MaLoaiSanPham equals l.MaLoaiSanPham into table2
                        from l in table2.DefaultIfEmpty()
                        join n in nhaSanXuats on s.MaNhaSanXuat equals n.MaNhaSanXuat
                        where s.TrangThaiTt == i
                        select new SanPhamContent
                        {
                            sanphamdetail = s,
                            loaisanphamdetail = l,
                            anhdetail = a,
                            nhasanxuatdetail = n
                        };
            index = index.Take(16);
            return View(index);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [Route("register")]
        [HttpGet]
        public IActionResult Register()
        {
            TaiKhoan model = new TaiKhoan();
            return View(model);
        }

        [Route("register")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register([Bind("TenDangNhap, Email, MatKhau")] TaiKhoan tk)
        {
            var existAccount = db.TaiKhoans.FirstOrDefault(p => p.TenDangNhap == tk.TenDangNhap);
            if (existAccount != null)
            {
                ViewBag.error = "Tên đăng nhập đã tồn tại! ";
                return View();
            }
            tk.MaQuyen = 2;
            if (ModelState.IsValid)
            {
                db.Add(tk);
                await db.SaveChangesAsync();

                NguoiDung nd = new NguoiDung();
                nd.MaTaiKhoan = tk.MaTaiKhoan;
                nd.TenNguoiDung = tk.TenDangNhap;
                nd.Sdt = "Chưa nhập";
                nd.DiaChi = "Chưa nhập";
                db.Add(nd);
                await db.SaveChangesAsync();

                return RedirectPermanentPreserveMethod("/Login");
            }
            return View();
        }

        [Route("login")]
        [HttpGet]
        public IActionResult Login()
        {
            if (HttpContext.Session.GetString("remember") != null)
            {
                return RedirectPermanentPreserveMethod("/");
            }
            LoginModel model = new LoginModel();
            return View(model);
        }
        [Route("login")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.TenDangNhap == null)
                {
                    ModelState.AddModelError("", "Vui lòng nhập tên đăng nhập! ");
                    return View();
                }
                if (model.MatKhau == null)
                {
                    ModelState.AddModelError("", "Vui lòng nhập mật khẩu! ");
                    return View();
                }

                TaiKhoan tk = db.TaiKhoans.Where(s => s.TenDangNhap == model.TenDangNhap && s.MatKhau == model.MatKhau).SingleOrDefault();
                if (tk != null)
                {
                    HttpContext.Session.SetInt32("Lock", 2);
                    if (model.Remember)
                    {
                        HttpContext.Session.SetString("remember", model.TenDangNhap);
                    }
                    if (tk.MaQuyen == 1)
                    {
                        HttpContext.Session.SetInt32("Lock", 1);
                        var maqt = db.QuanTris.FirstOrDefault(u => u.MaTaiKhoan == tk.MaTaiKhoan).MaQuanTri;
                        var avatar = db.QuanTris.FirstOrDefault(u => u.MaTaiKhoan == tk.MaTaiKhoan).Avatar;
                        HttpContext.Session.SetString("username", model.TenDangNhap);
                        HttpContext.Session.SetString("avatar", avatar);
                        HttpContext.Session.SetInt32("maqt", maqt);
                        HttpContext.Session.SetInt32("taikhoan", tk.MaTaiKhoan);
                        return RedirectPermanentPreserveMethod("/Admin");
                    }
                    HttpContext.Session.SetInt32("matk", tk.MaTaiKhoan);
                    HttpContext.Session.SetString("username", model.TenDangNhap);
                    //  var manguoidung = db.NguoiDungs.FirstOrDefault(u => u.MaTaiKhoan == tk.MaTaiKhoan).MaNguoiDung;
                    // HttpContext.Session.SetInt32("manguoidung", manguoidung);
                    int count = db.GioHangs.Where(s => s.MaTaiKhoan == tk.MaTaiKhoan).Count();
                    HttpContext.Session.SetInt32("countCart", count);
                    return RedirectPermanentPreserveMethod("/");
                }
                else
                {
                    ModelState.AddModelError("", "Thông tin đăng nhập không đúng! ");
                }
            }
            else
            {
                ModelState.AddModelError("", "Thông tin đăng nhập nhập rỗng! ");
            }
            return View();
        }

        [Route("Logout")]
        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("remember");
            HttpContext.Session.Remove("username");
            HttpContext.Session.Clear();
            Response.Cookies.Delete(".AspNetCore.Session");

            return RedirectToAction("login");
        }

     
        public const string CARTKEY = "cart";

        [Route("addcart/{productid:int}", Name = "addcart")]
        public async Task<IActionResult> AddToCart([FromRoute] int productid)
        {
            if (HttpContext.Session.GetString("username") == null)
            {
                return RedirectToAction("login");
            }
            GioHang cartItem = (from s in db.GioHangs where s.MaSanPham == productid select s).FirstOrDefault();
            if (cartItem != null)
            {
                cartItem.SoLuong = cartItem.SoLuong + 1;
                cartItem.MaSanPham = productid;
                cartItem.MaTaiKhoan = (int)HttpContext.Session.GetInt32("matk").Value;
                if (ModelState.IsValid)
                {
                    db.Update(cartItem);
                    await db.SaveChangesAsync();
                }
            }
            else
            {
                cartItem = new GioHang();
                cartItem.SoLuong = 1;
                cartItem.MaSanPham = productid;
                cartItem.MaTaiKhoan = (int)HttpContext.Session.GetInt32("matk").Value;
                if (ModelState.IsValid)
                {
                    db.Add(cartItem);
                    await db.SaveChangesAsync();
                }
                HttpContext.Session.Remove("countCart");
                int count = db.GioHangs.Where(s => s.MaTaiKhoan == cartItem.MaTaiKhoan).Count();
                HttpContext.Session.SetInt32("countCart", count);
            }
            return RedirectToAction("Index");
        }

        //View giỏ hàng
        [Route("/cart", Name = "cart")]
        public IActionResult Cart()
        {
            if (HttpContext.Session.GetString("username") == null)
            {
                return RedirectToAction("login");
            }
            List<SanPham> sanPhams = db.SanPhams.ToList();
            List<AnhSp> anhs = db.AnhSps.ToList();
            List<GioHang> giohangs = (from s in db.GioHangs where s.MaTaiKhoan == (int)HttpContext.Session.GetInt32("matk").Value select s).ToList();
            var cart = from g in giohangs
                       join n in sanPhams on g.MaSanPham equals n.MaSanPham into table1
                       from n in table1.DefaultIfEmpty()
                       join a in anhs on n.MaSanPham equals a.MaSanPham into table2
                       from a in table2.DefaultIfEmpty()
                       select new CartContent
                       {
                           sanphamdetail = n,
                           anhspdeltail = a,
                           SL = (int)g.SoLuong

                       };
            return View(cart);
        }


        [Route("/updatecart", Name = "updatecart")]
        [HttpPost]
        public async Task<IActionResult> UpdateCart([FromForm] int productid, [FromForm] int quantity)
        {
            var cartItem = (from s in db.GioHangs where s.MaSanPham == productid select s).FirstOrDefault();

            if (cartItem != null)
            {
                if (quantity == 0)
                {
                    db.Remove(cartItem);
                    await db.SaveChangesAsync();
                    HttpContext.Session.Remove("countCart");
                    int count = db.GioHangs.Where(s => s.MaTaiKhoan == cartItem.MaTaiKhoan).Count();
                    HttpContext.Session.SetInt32("countCart", count);
                }
                else
                {
                    cartItem.SoLuong = quantity;
                    db.Update(cartItem);
                    await db.SaveChangesAsync();
                }
            }
            else
                return NotFound();
            return RedirectToAction(nameof(Cart));
        }


        [Route("/addDonHang", Name = "addDonHang")]
        [HttpPost]
        public async Task<IActionResult> AddDonHang()
        {
            DonDat donhang = new DonDat();
            donhang.MaNguoiDung = (from s in db.NguoiDungs where s.MaTaiKhoan == (int)HttpContext.Session.GetInt32("matk").Value select s.MaNguoiDung).FirstOrDefault();
            donhang.MaTrangThai = 1;
            donhang.NgayDatHang = DateTime.Now.ToString();
            db.Add(donhang);
            await db.SaveChangesAsync();

            var madondat = (from s in db.DonDats.OrderByDescending(x => x.MaDonDat) where s.MaNguoiDung == donhang.MaNguoiDung select s.MaDonDat).FirstOrDefault();
            List<GioHang> giohangs = (from s in db.GioHangs where s.MaTaiKhoan == (int)HttpContext.Session.GetInt32("matk").Value select s).ToList();
            foreach (var element in giohangs)
            {
                ChiTietDd item = new ChiTietDd();
                item.MaDonDat = madondat;
                item.MaSanPham = element.MaSanPham;
                item.SoLuong = element.SoLuong;
                db.Add(item);
                await db.SaveChangesAsync();
                db.Remove(element);
                await db.SaveChangesAsync();
            }
            int count = db.GioHangs.Where(s => s.MaTaiKhoan == (int)HttpContext.Session.GetInt32("matk")).Count();
            HttpContext.Session.SetInt32("countCart", count);
            await db.SaveChangesAsync();
            return Redirect("/");
        }




    }
}
