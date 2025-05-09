using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prjBTLDemo.NET.Models
{
    public class HoaDon
    {
        public string MaHoaDon { get; set; } // Mã hóa đơn (unique)
        public DateTime NgayLap { get; set; } // Ngày lập hóa đơn
        public string MaKhachHang { get; set; } // Mã khách hàng
        public string TenKhachHang { get; set; } // Tên khách hàng
        public string MaNhanVien { get; set; } // Mã nhân viên lập hóa đơn
        public decimal TongTien { get; set; } // Tổng tiền của hóa đơn
        public List<ChiTietHoaDon> ChiTietHoaDons { get; set; } = new List<ChiTietHoaDon>(); // Danh sách chi tiết hóa đơn
    }
}
