using System;
using System.Collections.Generic;

#nullable disable

namespace TN408Project.DataDB
{
    public partial class QuanLyNhapKho
    {
        public int MaNhap { get; set; }
        public int? MaSanPham { get; set; }
        public DateTime? NgayNhap { get; set; }
        public string GiaNhap { get; set; }
        public int? SoLuong { get; set; }

        public virtual SanPham MaSanPhamNavigation { get; set; }
    }
}
