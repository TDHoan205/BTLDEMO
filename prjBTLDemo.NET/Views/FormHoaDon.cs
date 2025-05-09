using System;
using System.Windows.Forms;
using prjBTLDemo.NET.ViewModels;

namespace prjBTLDemo.NET.Views
{
    public partial class FormHoaDon : Form
    {
        private readonly HoaDonVM _hoaDonVM;
        private readonly BindingSource _bindingSource;

        public FormHoaDon()
        {
            InitializeComponent();
            _hoaDonVM = new HoaDonVM();
            _bindingSource = new BindingSource();
            BindData();
        }

       public void BindData()
        {
            // Đảm bảo BindingSource
            _bindingSource.DataSource = _hoaDonVM.DanhSachHoaDon;
            dgvHoaDon.DataSource = _bindingSource;

            // Định nghĩa cột thủ công để đảm bảo định dạng chính xác
            dgvHoaDon.AutoGenerateColumns = false;
            dgvHoaDon.Columns.Clear();

            // Cột Mã Hóa Đơn
            var maHoaDonColumn = new DataGridViewTextBoxColumn
            {
                DataPropertyName = "MaHoaDon",
                HeaderText = "Mã Hóa Đơn",
                Name = "MaHoaDonColumn"
            };
            dgvHoaDon.Columns.Add(maHoaDonColumn);

            // Cột Ngày Lập
            var ngayLapColumn = new DataGridViewTextBoxColumn
            {
                DataPropertyName = "NgayLap",
                HeaderText = "Ngày Lập",
                Name = "NgayLapColumn",
                DefaultCellStyle = new DataGridViewCellStyle { Format = "dd/MM/yyyy" } // Định dạng ngày tháng
            };
            dgvHoaDon.Columns.Add(ngayLapColumn);

            // Cột Tổng Tiền
            var tongTienColumn = new DataGridViewTextBoxColumn
            {
                DataPropertyName = "TongTien",
                HeaderText = "Tổng Tiền",
                Name = "TongTienColumn",
                DefaultCellStyle = new DataGridViewCellStyle { Format = "N0" } // Định dạng số
            };
            dgvHoaDon.Columns.Add(tongTienColumn);

            // Ràng buộc dữ liệu cho TextBox
            txtSearch.DataBindings.Add("Text", _hoaDonVM, nameof(_hoaDonVM.SearchTerm), true, DataSourceUpdateMode.OnPropertyChanged);

            // Sự kiện xử lý lỗi
            dgvHoaDon.DataError += DgvHoaDon_DataError;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            // Thu thập thông tin từ giao diện
            string maHoaDon = $"HD{_hoaDonVM.DanhSachHoaDon.Count + 1:D3}"; // Tự động tạo mã hóa đơn
            DateTime ngayLap = dtpNgayLap.Value; // Lấy giá trị từ DateTimePicker
            decimal tongTien = numTongTien.Value; // Lấy giá trị từ NumericUpDown

            // Gọi phương thức ThemHoaDon trong ViewModel
            _hoaDonVM.ThemHoaDonCommand.Execute(new { MaHoaDon = maHoaDon, NgayLap = ngayLap, TongTien = tongTien });

            // Cập nhật BindingSource
            _bindingSource.ResetBindings(false); // Reset bindings để cập nhật DataGridView
        }

        private void btnDetails_Click(object sender, EventArgs e)
        {
            _hoaDonVM.XemChiTietHoaDonCommand.Execute(null);
        }

        private void DgvHoaDon_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            MessageBox.Show($"Lỗi dữ liệu tại dòng {e.RowIndex}, cột {e.ColumnIndex}: {e.Exception.Message}");
        }
    }
}