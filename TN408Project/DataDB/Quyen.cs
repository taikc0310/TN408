using System;
using System.Collections.Generic;

#nullable disable

namespace TN408Project.DataDB
{
    public partial class Quyen
    {
        public Quyen()
        {
            TaiKhoans = new HashSet<TaiKhoan>();
        }

        public int MaQuyen { get; set; }
        public string TenQuyen { get; set; }

        public virtual ICollection<TaiKhoan> TaiKhoans { get; set; }
    }
}
