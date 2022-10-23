using System;
using System.Collections.Generic;

#nullable disable

namespace TN408Project.DataDB
{
    public partial class NhaSanXuat
    {
        public NhaSanXuat()
        {
            SanPhams = new HashSet<SanPham>();
        }

        public int MaNhaSanXuat { get; set; }
        public string TenNhaSanXuat { get; set; }

        public virtual ICollection<SanPham> SanPhams { get; set; }
    }
}
