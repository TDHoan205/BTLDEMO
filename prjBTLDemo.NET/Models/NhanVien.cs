using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prjBTLDemo.NET.Models
{
    public class NhanVien
    {
        public string MaNhanVien { get; set; } // Mã nhân viên (unique)
        public string TenNhanVien { get; set; } // Tên nhân viên
        public string ChucVu { get; set; } // Chức vụ
        public DateTime NgayVaoLam { get; set; } // Ngày vào làm
        public string SoDienThoai { get; set; } // Số điện thoại nhân viên
    }
}
