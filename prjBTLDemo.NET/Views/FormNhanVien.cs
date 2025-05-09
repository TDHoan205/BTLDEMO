using System;
using System.Windows.Forms;
using prjBTLDemo.NET.ViewModels;

namespace prjBTLDemo.NET.Forms
{
    public partial class FormNhanVien : Form
    {
        private readonly NhanVienVM _viewModel;

        public FormNhanVien()
        {
            InitializeComponent();
            _viewModel = new NhanVienVM();

            // Bind DataGridView to ViewModel
            dataGridView.DataSource = _viewModel.DanhSachNhanVien;

            // Allow user to edit cells directly
            dataGridView.AutoGenerateColumns = true;
            dataGridView.AllowUserToAddRows = false;

            // Attach event handlers
            btnAdd.Click += BtnAdd_Click;
            btnDelete.Click += BtnDelete_Click;
            txtSearch.TextChanged += TxtSearch_TextChanged;

            // DataGridView events for inline editing and deletion
            dataGridView.CellEndEdit += DataGridView_CellEndEdit;
            dataGridView.UserDeletingRow += DataGridView_UserDeletingRow;
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            _viewModel.DanhSachNhanVien.Add(new NhanVien
            {
                MaNhanVien = $"NV{_viewModel.DanhSachNhanVien.Count + 1:D3}",
                TenNhanVien = "Nhan Vien Moi"
            });
            RefreshDataGrid();
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            if (dataGridView.CurrentRow?.DataBoundItem is NhanVien selectedNhanVien)
            {
                _viewModel.DanhSachNhanVien.Remove(selectedNhanVien);
                RefreshDataGrid();
            }
        }

        private void TxtSearch_TextChanged(object sender, EventArgs e)
        {
            _viewModel.SearchTerm = txtSearch.Text; // Update the search term and filter the list
            RefreshDataGrid();
        }

        private void DataGridView_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            // Update the ViewModel when a cell is edited
            var editedNhanVien = dataGridView.Rows[e.RowIndex].DataBoundItem as NhanVien;
            if (editedNhanVien != null)
            {
                // Notify ViewModel of changes if needed
                RefreshDataGrid();
            }
        }

        private void DataGridView_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            if (e.Row.DataBoundItem is NhanVien selectedNhanVien)
            {
                _viewModel.DanhSachNhanVien.Remove(selectedNhanVien);
            }
        }

        private void RefreshDataGrid()
        {
            dataGridView.DataSource = null;
            dataGridView.DataSource = _viewModel.DanhSachNhanVien;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {

        }
    }
}