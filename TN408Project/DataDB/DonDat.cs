using System;
using System.Collections.Generic;

#nullable disable

namespace TN408Project.DataDB
{
    public partial class DonDat
    {
        public DonDat()
        {
            ChiTietDds = new HashSet<ChiTietDd>();
            HoaDons = new HashSet<HoaDon>();
        }

        public int MaDonDat { get; set; }
        public int? MaTrangThai { get; set; }
        public int MaNguoiDung { get; set; }
        public string NgayDatHang { get; set; }

        public virtual NguoiDung MaNguoiDungNavigation { get; set; }
        public virtual TrangThai MaTrangThaiNavigation { get; set; }
        public virtual ICollection<ChiTietDd> ChiTietDds { get; set; }
        public virtual ICollection<HoaDon> HoaDons { get; set; }
    }
}
