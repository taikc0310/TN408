using System;
using System.Collections.Generic;

#nullable disable

namespace TN408Project.DataDB
{
    public partial class QuanTri
    {
        public int MaQuanTri { get; set; }
        public int MaTaiKhoan { get; set; }
        public string TenQuanTri { get; set; }
        public string DiaChi { get; set; }
        public string Sdt { get; set; }
        public string Avatar { get; set; }

        public virtual TaiKhoan MaTaiKhoanNavigation { get; set; }
    }
}
