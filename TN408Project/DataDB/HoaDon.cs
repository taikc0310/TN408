using System;
using System.Collections.Generic;

#nullable disable

namespace TN408Project.DataDB
{
    public partial class HoaDon
    {
        public int MaHoaDon { get; set; }
        public int MaDonDat { get; set; }
        public string NgayGiaoHang { get; set; }

        public virtual DonDat MaDonDatNavigation { get; set; }
    }
}
