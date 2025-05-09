using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using prjBTLDemo.NET.Helpers;
using prjBTLDemo.NET.Models;
using prjBTLDemo.NET.Utilities; // Đảm bảo sử dụng namespace đúng

namespace prjBTLDemo.NET.ViewModels
{
    public class HoaDonVM : BaseViewsModel
    {
        private ObservableCollection<HoaDon> _allHoaDon; // Original list for filtering
        private ObservableCollection<HoaDon> _danhSachHoaDon;
        private HoaDon _hoaDonHienTai;
        private string _searchTerm;

        public ObservableCollection<HoaDon> DanhSachHoaDon
        {
            get => _danhSachHoaDon;
            set
            {
                _danhSachHoaDon = value;
                OnPropertyChanged(nameof(DanhSachHoaDon));
            }
        }

        public HoaDon HoaDonHienTai
        {
            get => _hoaDonHienTai;
            set
            {
                if (_hoaDonHienTai != value)
                {
                    _hoaDonHienTai = value;
                    OnPropertyChanged(nameof(HoaDonHienTai));
                }
            }
        }

        public string SearchTerm
        {
            get => _searchTerm;
            set
            {
                if (_searchTerm != value)
                {
                    _searchTerm = value;
                    FilterHoaDon();
                    OnPropertyChanged(nameof(SearchTerm));
                }
            }
        }

        public ICommand ThemHoaDonCommand { get; }
        public ICommand XemChiTietHoaDonCommand { get; }

        public HoaDonVM()
        {
            // Khởi tạo dữ liệu mẫu
            _allHoaDon = new ObservableCollection<HoaDon>
            {
                new HoaDon { MaHoaDon = "HD001", NgayLap = DateTime.Now.AddDays(-1), TongTien = 200000m },
                new HoaDon { MaHoaDon = "HD002", NgayLap = DateTime.Now, TongTien = 300000m }
            };

            DanhSachHoaDon = new ObservableCollection<HoaDon>(_allHoaDon);

            ThemHoaDonCommand = new RelayCommand(param => ThemHoaDon(), param => true);
            XemChiTietHoaDonCommand = new RelayCommand(param => XemChiTietHoaDon(), param => HoaDonHienTai != null);
        }

        private void ThemHoaDon()
        {
            var hoaDonMoi = new HoaDon
            {
                MaHoaDon = $"HD{_allHoaDon.Count + 1:D3}",
                NgayLap = DateTime.Now,
                TongTien = 150000m
            };

            _allHoaDon.Add(hoaDonMoi);
            FilterHoaDon();
            HoaDonHienTai = hoaDonMoi; // Gán hóa đơn mới là hóa đơn hiện tại
        }

        private void XemChiTietHoaDon()
        {
            if (HoaDonHienTai != null)
            {
                Console.WriteLine($"Xem chi tiết hóa đơn: {HoaDonHienTai.MaHoaDon}");
            }
        }

        private void FilterHoaDon()
        {
            if (string.IsNullOrWhiteSpace(_searchTerm))
            {
                DanhSachHoaDon = new ObservableCollection<HoaDon>(_allHoaDon);
            }
            else
            {
                DanhSachHoaDon = new ObservableCollection<HoaDon>(
                    _allHoaDon.Where(hd => hd.MaHoaDon.ToLower().Contains(_searchTerm.ToLower())));
            }
        }
    }
}