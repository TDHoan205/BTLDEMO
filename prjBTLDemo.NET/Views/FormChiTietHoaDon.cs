using System;
using System.Linq;
using System.Windows.Forms;
using prjBTLDemo.NET.Models;
using prjBTLDemo.NET.ViewModels;

namespace prjBTLDemo.NET.Forms
{
    public partial class FormChiTietHoaDon : Form
    {
        private readonly ChiTietHoaDonVM _viewModel;
        private ViewModels.HoaDonVM hoaDonHienTai;

        public FormChiTietHoaDon(Models.HoaDon hoaDon)
        {
            InitializeComponent();

            // Xử lý khi tham số hoaDon là null
            if (hoaDon == null)
            {
                hoaDon = new Models.HoaDon
                {
                    MaHoaDon = "HD_Default", // Giá trị mặc định
                    NgayLap = DateTime.Now   // Giá trị mặc định
                };
                MessageBox.Show("Hóa đơn đầu vào là null. Đã khởi tạo hóa đơn mặc định!", "Thông báo");
            }

            // Khởi tạo ViewModel với giá trị hoaDon
            _viewModel = new ChiTietHoaDonVM(hoaDon);

            // Bind DataGridView to ViewModel
            dataGridView.DataSource = _viewModel.DanhSachChiTiet;

            // Allow user to edit cells directly
            dataGridView.AutoGenerateColumns = true;
            dataGridView.AllowUserToAddRows = false;

            // Populate ComboBox với danh sách sản phẩm mẫu
            cmbProducts.Items.AddRange(new string[] { "Product A", "Product B", "Product C" });

            // Attach event handlers
            btnAdd.Click += BtnAdd_Click;
            btnUpdate.Click += BtnUpdate_Click;
            btnDelete.Click += BtnDelete_Click;
            txtSearch.TextChanged += TxtSearch_TextChanged;

            // Ensure selection tracking for the current detail
            dataGridView.SelectionChanged += DataGridView_SelectionChanged;
        }

        public FormChiTietHoaDon(ViewModels.HoaDon hoaDonHienTai)
        {
            this.hoaDonHienTai = hoaDonHienTai;
        }

        public FormChiTietHoaDon()
        {
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            if (cmbProducts.SelectedItem != null && numQuantity.Value > 0)
            {
                var existingDetail = _viewModel.DanhSachChiTiet
                    .FirstOrDefault(detail => detail.MaSanPham.Equals(cmbProducts.SelectedItem?.ToString()));

                if (existingDetail != null)
                {
                    MessageBox.Show("Sản phẩm này đã tồn tại trong danh sách. Vui lòng cập nhật số lượng thay vì thêm mới!", "Thông báo");
                    return;
                }

                _viewModel.SelectedProduct = cmbProducts.SelectedItem.ToString();
                _viewModel.SelectedQuantity = (int)numQuantity.Value;
                _viewModel.ThemChiTietCommand.Execute(null);
                RefreshDataGrid();
                MessageBox.Show("Chi tiết hóa đơn mới đã được thêm!", "Thông báo");
            }
            else
            {
                MessageBox.Show("Vui lòng chọn sản phẩm và nhập số lượng hợp lệ để thêm!", "Thông báo");
            }
        }

        private void BtnUpdate_Click(object sender, EventArgs e)
        {
            if (_viewModel.ChiTietHienTai != null)
            {
                _viewModel.CapNhatChiTietCommand.Execute(null);
                RefreshDataGrid();
                MessageBox.Show("Chi tiết hóa đơn đã được cập nhật!", "Thông báo");
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một chi tiết để cập nhật!", "Thông báo");
            }
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            if (_viewModel.ChiTietHienTai != null)
            {
                _viewModel.XoaChiTietCommand.Execute(null);
                RefreshDataGrid();
                MessageBox.Show("Chi tiết hóa đơn đã được xóa!", "Thông báo");
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một chi tiết để xóa!", "Thông báo");
            }
        }

        private void TxtSearch_TextChanged(object sender, EventArgs e)
        {
            _viewModel.SearchTerm = txtSearch.Text;
            RefreshDataGrid();
        }

        private void DataGridView_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView.CurrentRow?.DataBoundItem is Models.ChiTietHoaDon selectedChiTiet)
            {
                _viewModel.ChiTietHienTai = selectedChiTiet;

                cmbProducts.SelectedItem = selectedChiTiet.MaSanPham; // Ensure proper mapping
                numQuantity.Value = selectedChiTiet.SoLuong;
            }
        }

        private void RefreshDataGrid()
        {
            dataGridView.DataSource = null;
            dataGridView.DataSource = _viewModel.DanhSachChiTiet;
        }
    }
}