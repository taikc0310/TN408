using System;
using System.Collections.Generic;

#nullable disable

namespace TN408Project.DataDB
{
    public partial class SanPham
    {
        public SanPham()
        {
            AnhSps = new HashSet<AnhSp>();
            ChiTietDds = new HashSet<ChiTietDd>();
            GioHangs = new HashSet<GioHang>();
            QuanLyNhapKhos = new HashSet<QuanLyNhapKho>();
        }

        public int MaSanPham { get; set; }
        public int MaLoaiSanPham { get; set; }
        public int MaNhaSanXuat { get; set; }
        public string TenSanPham { get; set; }
        public string Gia { get; set; }
        public string MoTa { get; set; }
        public string Mau { get; set; }
        public int? SoLuong { get; set; }
        public int? TrangThaiTt { get; set; }

        public virtual LoaiSanPham MaLoaiSanPhamNavigation { get; set; }
        public virtual NhaSanXuat MaNhaSanXuatNavigation { get; set; }
        public virtual ICollection<AnhSp> AnhSps { get; set; }
        public virtual ICollection<ChiTietDd> ChiTietDds { get; set; }
        public virtual ICollection<GioHang> GioHangs { get; set; }
        public virtual ICollection<QuanLyNhapKho> QuanLyNhapKhos { get; set; }
    }
}
