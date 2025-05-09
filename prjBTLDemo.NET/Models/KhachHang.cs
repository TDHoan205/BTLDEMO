using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prjBTLDemo.NET.Models
{
    public class KhachHang
    {
        public string MaKhachHang { get; set; } // Mã khách hàng (unique)
        public string TenKhachHang { get; set; } // Tên khách hàng
        public string SoDienThoai { get; set; } // Số điện thoại
        public string DiaChi { get; set; } // Địa chỉ khách hàng
        public string Email { get; set; } // Email khách hàng
    }
}