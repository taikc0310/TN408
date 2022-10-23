using System;
using System.Collections.Generic;

#nullable disable

namespace TN408Project.DataDB
{
    public partial class ChiTietDd
    {
        public int MaSanPham { get; set; }
        public int MaDonDat { get; set; }
        public int? SoLuong { get; set; }

        public virtual DonDat MaDonDatNavigation { get; set; }
        public virtual SanPham MaSanPhamNavigation { get; set; }
    }
}
