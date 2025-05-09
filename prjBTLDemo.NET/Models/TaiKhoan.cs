using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prjBTLDemo.NET.Models
{
    public class TaiKhoan
    {
        public string TenDangNhap { get; set; } // Tên đăng nhập của tài khoản
        public string MatKhau { get; set; } // Mật khẩu của tài khoản (đã mã hóa)
        public string MaNhanVien { get; set; } // Mã nhân viên liên kết với tài khoản
        public bool QuyenAdmin { get; set; } // Quyền Admin (true nếu là Admin)
    }
}