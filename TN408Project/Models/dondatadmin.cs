using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TN408Project.DataDB;

namespace TN408Project.Models
{
    public class dondatadmin
    {
        public DonDat dondatdetail { get; set; }
        public TrangThai trangthaidetail { get; set; }
        public ChiTietDd chitietdetail { get; set; }
        public SanPham sanphamdetail { get; set; }
    }
}
