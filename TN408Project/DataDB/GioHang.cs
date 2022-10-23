using System;
using System.Collections.Generic;

#nullable disable

namespace TN408Project.DataDB
{
    public partial class GioHang
    {
        public int MaTaiKhoan { get; set; }
        public int MaSanPham { get; set; }
        public int? SoLuong { get; set; }

        public virtual SanPham MaSanPhamNavigation { get; set; }
        public virtual TaiKhoan MaTaiKhoanNavigation { get; set; }
    }
}
