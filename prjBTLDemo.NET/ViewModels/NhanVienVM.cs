using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

namespace prjBTLDemo.NET.ViewModels
{
    public class NhanVienVM : INotifyPropertyChanged
    {
        private ObservableCollection<NhanVien> _allNhanVien; // Stores the original list
        private ObservableCollection<NhanVien> _danhSachNhanVien;
        private string _searchTerm;

        public ObservableCollection<NhanVien> DanhSachNhanVien
        {
            get => _danhSachNhanVien;
            set
            {
                _danhSachNhanVien = value;
                OnPropertyChanged(nameof(DanhSachNhanVien));
            }
        }

        public string SearchTerm
        {
            get => _searchTerm;
            set
            {
                _searchTerm = value;
                FilterNhanVien();
                OnPropertyChanged(nameof(SearchTerm));
            }
        }

        public NhanVien NhanVienHienTai { get; set; }

        public NhanVienVM()
        {
            // Initialize the original list of employees
            _allNhanVien = new ObservableCollection<NhanVien>
            {
                new NhanVien { MaNhanVien = "NV001", TenNhanVien = "Nguyen Van A" },
                new NhanVien { MaNhanVien = "NV002", TenNhanVien = "Tran Van B" },
                new NhanVien { MaNhanVien = "NV003", TenNhanVien = "Le Thi C" }
            };

            _danhSachNhanVien = new ObservableCollection<NhanVien>(_allNhanVien); // Initialize filtered list
        }

        private void FilterNhanVien()
        {
            if (string.IsNullOrWhiteSpace(_searchTerm))
            {
                // If search term is empty, reset to the original list
                DanhSachNhanVien = new ObservableCollection<NhanVien>(_allNhanVien);
            }
            else
            {
                // Filter by MaNhanVien
                var filtered = _allNhanVien
                    .Where(nv => nv.MaNhanVien.ToLower().Contains(_searchTerm.ToLower()))
                    .ToList();
                DanhSachNhanVien = new ObservableCollection<NhanVien>(filtered);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class NhanVien
    {
        public string MaNhanVien { get; set; }
        public string TenNhanVien { get; set; }
    }
}