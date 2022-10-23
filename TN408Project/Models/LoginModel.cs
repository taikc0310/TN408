using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TN408Project.DataDB;
namespace TN408Project.Models
{
    public class LoginModel
    {
        public int MaTaiKhoan { set; get; }
        public string TenDangNhap { set; get; }
        public string MatKhau { set; get; }
        public bool Remember { set; get; }
    }
}
