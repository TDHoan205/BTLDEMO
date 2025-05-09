using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using prjBTLDemo.NET.Models;
using prjBTLDemo.NET.Helpers;
using System;
using prjBTLDemo.NET.Utilities;



namespace prjBTLDemo.NET.ViewModels
{
    public class ChiTietHoaDon : BaseViewsModel
    {
        private ObservableCollection<ChiTietHoaDon> _danhSachChiTietGoc; // Gốc không bị lọc
        private ObservableCollection<ChiTietHoaDon> _danhSachChiTiet;
        private ChiTietHoaDon _chiTietHienTai;
        private string _searchTerm;
        private Models.HoaDon hoaDon;
        public string SelectedProduct { get; set; }
        public int SelectedQuantity { get; set; }

        public ObservableCollection<ChiTietHoaDon> DanhSachChiTiet
        {
            get => _danhSachChiTiet;
            set
            {
                _danhSachChiTiet = value;
                OnPropertyChanged(nameof(DanhSachChiTiet));
            }
        }

        public ChiTietHoaDon ChiTietHienTai
        {
            get => _chiTietHienTai;
            set
            {
                _chiTietHienTai = value;
                OnPropertyChanged(nameof(ChiTietHienTai));
            }
        }

        public string SearchTerm
        {
            get => _searchTerm;
            set
            {
                _searchTerm = value;
                FilterChiTiet();
                OnPropertyChanged(nameof(SearchTerm));
            }
        }

        public ICommand ThemChiTietCommand { get; }
        public ICommand CapNhatChiTietCommand { get; }
        public ICommand XoaChiTietCommand { get; }

        public ChiTietHoaDon(HoaDon hoaDon)
        {
            // Khởi tạo dữ liệu giả lập
            _danhSachChiTietGoc = new ObservableCollection<ChiTietHoaDon>
            {
                new ChiTietHoaDon { MaSanPham = "Product A", SoLuong = 2, ThanhTien = 400000 },
                new ChiTietHoaDon { MaSanPham = "Product B", SoLuong = 1, ThanhTien = 200000 }
            };

            DanhSachChiTiet = new ObservableCollection<ChiTietHoaDon>(_danhSachChiTietGoc);

            ThemChiTietCommand = new RelayCommand(param => ThemChiTiet((string)param), param => param != null);
            CapNhatChiTietCommand = new RelayCommand(param => CapNhatChiTiet((int)param), param => ChiTietHienTai != null);
            XoaChiTietCommand = new RelayCommand(param => XoaChiTiet(), param => ChiTietHienTai != null);
        }

        public ChiTietHoaDon(Models.HoaDon hoaDon)
        {
            this.hoaDon = hoaDon;
        }

        private void ThemChiTiet(string maSanPham, decimal donGia = 200000)
        {
            // Kiểm tra trùng lặp sản phẩm
            var existingChiTiet = _danhSachChiTietGoc.FirstOrDefault(ct => ct.MaSanPham == maSanPham);
            if (existingChiTiet != null)
            {
                // Nếu sản phẩm đã tồn tại, chỉ tăng số lượng và tính lại tổng tiền
                existingChiTiet.SoLuong += 1;
                existingChiTiet.ThanhTien = existingChiTiet.SoLuong * donGia;
            }
            else
            {
                // Nếu sản phẩm chưa tồn tại, thêm chi tiết mới
                var chiTietMoi = new ChiTietHoaDon
                {
                    MaSanPham = maSanPham,
                    SoLuong = 1,
                    ThanhTien = donGia
                };
                _danhSachChiTietGoc.Add(chiTietMoi);
            }
            FilterChiTiet();
        }

        private void CapNhatChiTiet(int soLuongMoi)
        {
            if (ChiTietHienTai != null)
            {
                ChiTietHienTai.SoLuong = soLuongMoi;
                ChiTietHienTai.ThanhTien = ChiTietHienTai.SoLuong * 200000; // Cập nhật lại tổng tiền
                FilterChiTiet();
            }
        }

        private void XoaChiTiet()
        {
            if (ChiTietHienTai != null)
            {
                _danhSachChiTietGoc.Remove(ChiTietHienTai);
                ChiTietHienTai = null;
                FilterChiTiet();
            }
        }

        private void FilterChiTiet()
        {
            if (string.IsNullOrWhiteSpace(SearchTerm))
            {
                DanhSachChiTiet = new ObservableCollection<ChiTietHoaDon>(_danhSachChiTietGoc);
            }
            else
            {
                var lowerTerm = SearchTerm.ToLower();
                var filtered = _danhSachChiTietGoc
                    .Where(ct => ct.MaSanPham.ToLower().Contains(lowerTerm));
                DanhSachChiTiet = new ObservableCollection<ChiTietHoaDon>(filtered);
            }
        }
    }

    public class ChiTietHoaDon
    {
        public string MaSanPham { get; set; }
        public int SoLuong { get; set; }
        public decimal ThanhTien { get; set; }
        public object SanPham { get; internal set; }

        public static implicit operator ChiTietHoaDon(Models.ChiTietHoaDon v)
        {
            throw new NotImplementedException();
        }
            
    }
}