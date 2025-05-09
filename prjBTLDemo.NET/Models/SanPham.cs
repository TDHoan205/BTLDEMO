using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prjBTLDemo.NET.Models
{
    public class SanPham
    {
        public string MaSanPham { get; set; } // Mã sản phẩm (unique)
        public string TenSanPham { get; set; } // Tên sản phẩm
        public string NhaCungCap { get; set; } // Nhà cung cấp
        public string DonVi { get; set; } // Đơn vị (ví dụ: chiếc, hộp)
        public decimal DonGia { get; set; } // Đơn giá sản phẩm
        public int SoLuongTon { get; set; } // Số lượng tồn kho
    }
}