using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using prjBTLDemo.NET.Utilities; // Ensure the correct namespace for RelayCommand

namespace prjBTLDemo.NET.ViewModels
{
    public class KhachHangVM : INotifyPropertyChanged
    {
        private ObservableCollection<KhachHang> _allKhachHang; // Original list of customers
        private ObservableCollection<KhachHang> _danhSachKhachHang;
        private string _searchTerm;

        public ObservableCollection<KhachHang> DanhSachKhachHang
        {
            get => _danhSachKhachHang;
            set
            {
                _danhSachKhachHang = value;
                OnPropertyChanged(nameof(DanhSachKhachHang));
            }
        }

        public string SearchTerm
        {
            get => _searchTerm;
            set
            {
                _searchTerm = value;
                FilterKhachHang();
                OnPropertyChanged(nameof(SearchTerm));
            }
        }

        public KhachHang KhachHangHienTai { get; set; }
        public ICommand ThemKhachHangCommand { get; }
        public ICommand CapNhatKhachHangCommand { get; }
        public ICommand XoaKhachHangCommand { get; }

        public KhachHangVM()
        {
            _allKhachHang = new ObservableCollection<KhachHang>
            {
                new KhachHang { MaKhachHang = "KH001", TenKhachHang = "Nguyen Van A", SoDienThoai = "0123456789" },
                new KhachHang { MaKhachHang = "KH002", TenKhachHang = "Tran Van B", SoDienThoai = "0987654321" }
            };

            _danhSachKhachHang = new ObservableCollection<KhachHang>(_allKhachHang);

            ThemKhachHangCommand = new RelayCommand(ThemKhachHang);
            CapNhatKhachHangCommand = new RelayCommand(CapNhatKhachHang);
            XoaKhachHangCommand = new RelayCommand(XoaKhachHang);
        }

        private void ThemKhachHang(object parameter)
        {
            var newKhachHang = new KhachHang
            {
                MaKhachHang = $"KH{_allKhachHang.Count + 1:D3}",
                TenKhachHang = "Khach Hang Moi",
                SoDienThoai = "0000000000"
            };
            _allKhachHang.Add(newKhachHang);
            FilterKhachHang();
        }

        private void CapNhatKhachHang(object parameter)
        {
            if (parameter is KhachHang updatedKhachHang && KhachHangHienTai != null)
            {
                var existingKhachHang = _allKhachHang.FirstOrDefault(kh => kh.MaKhachHang == updatedKhachHang.MaKhachHang);
                if (existingKhachHang != null)
                {
                    existingKhachHang.TenKhachHang = updatedKhachHang.TenKhachHang;
                    existingKhachHang.SoDienThoai = updatedKhachHang.SoDienThoai;
                }
                FilterKhachHang();
            }
        }

        private void XoaKhachHang(object parameter)
        {
            if (parameter is KhachHang khachHang)
            {
                _allKhachHang.Remove(khachHang);
                FilterKhachHang();
            }
        }

        private void FilterKhachHang()
        {
            if (string.IsNullOrWhiteSpace(_searchTerm))
            {
                // Reset to the original list if the search term is empty
                DanhSachKhachHang = new ObservableCollection<KhachHang>(_allKhachHang);
            }
            else
            {
                // Filter customers by MaKhachHang or TenKhachHang
                var filtered = _allKhachHang
                    .Where(kh =>
                        kh.MaKhachHang.ToLower().Contains(_searchTerm.ToLower()) ||
                        kh.TenKhachHang.ToLower().Contains(_searchTerm.ToLower()))
                    .ToList();
                DanhSachKhachHang = new ObservableCollection<KhachHang>(filtered);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class KhachHang
    {
        public string MaKhachHang { get; set; }
        public string TenKhachHang { get; set; }
        public string SoDienThoai { get; set; }
    }
}