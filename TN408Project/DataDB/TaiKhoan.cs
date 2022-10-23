using System;
using System.Collections.Generic;

#nullable disable

namespace TN408Project.DataDB
{
    public partial class TaiKhoan
    {
        public TaiKhoan()
        {
            GioHangs = new HashSet<GioHang>();
            NguoiDungs = new HashSet<NguoiDung>();
            QuanTris = new HashSet<QuanTri>();
        }

        public int MaTaiKhoan { get; set; }
        public string TenDangNhap { get; set; }
        public string MatKhau { get; set; }
        public string Email { get; set; }
        public int MaQuyen { get; set; }

        public virtual Quyen MaQuyenNavigation { get; set; }
        public virtual ICollection<GioHang> GioHangs { get; set; }
        public virtual ICollection<NguoiDung> NguoiDungs { get; set; }
        public virtual ICollection<QuanTri> QuanTris { get; set; }
    }
}
