using System;
using System.Collections.Generic;

#nullable disable

namespace TN408Project.DataDB
{
    public partial class AnhSp
    {
        public int MaAnh { get; set; }
        public string LinkAnh { get; set; }
        public string MoTa { get; set; }
        public int MaSanPham { get; set; }

        public virtual SanPham MaSanPhamNavigation { get; set; }
    }
}
