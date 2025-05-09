using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prjBTLDemo.NET.Models
{
    public class ChiTietHoaDon
    {
        public string MaHoaDon { get; set; } // Mã hóa đơn liên kết
        public string MaSanPham { get; set; } // Mã sản phẩm
        public string TenSanPham { get; set; } // Tên sản phẩm
        public int SoLuong { get; set; } // Số lượng sản phẩm
        public decimal DonGia { get; set; } // Đơn giá sản phẩm
        public int GiamGia { get; set; } // Phần trăm giảm giá
        public decimal ThanhTien => SoLuong * DonGia * (1 - GiamGia / 100m); // Thành tiền

        public object SanPham { get; internal set; }
    }
}
