using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TN408Project.DataDB;
namespace TN408Project.Models
{
    public class CartContent
    {

        public SanPham sanphamdetail { get; set; }
        public int SL { get; set; }
        public AnhSp anhspdeltail { get; set; }

    }
}
