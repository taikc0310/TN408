using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TN408Project.DataDB;
namespace TN408Project.Models
{
    public class SanPhamContent
    {
        public SanPham sanphamdetail { get; set; }
        public AnhSp anhdetail { get; set; }
        public LoaiSanPham loaisanphamdetail { get; set; }
        public NhaSanXuat nhasanxuatdetail { get; set; }
    }
}
