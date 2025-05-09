using System;
using System.Windows.Forms;
using prjBTLDemo.NET.ViewModels;

namespace prjBTLDemo.NET.Forms
{
    public partial class FormKhachHang : Form
    {
        private readonly KhachHangVM _viewModel;

        public FormKhachHang()
        {
            InitializeComponent();
            _viewModel = new KhachHangVM();

            // Bind DataGridView to ViewModel
            dataGridView.DataSource = _viewModel.DanhSachKhachHang;

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
            _viewModel.ThemKhachHangCommand.Execute(null);
            RefreshDataGrid();
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            if (dataGridView.CurrentRow?.DataBoundItem is KhachHang selectedKhachHang)
            {
                _viewModel.XoaKhachHangCommand.Execute(selectedKhachHang);
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
            var editedKhachHang = dataGridView.Rows[e.RowIndex].DataBoundItem as KhachHang;
            if (editedKhachHang != null)
            {
                _viewModel.CapNhatKhachHangCommand.Execute(editedKhachHang);
                RefreshDataGrid();
            }
        }

        private void DataGridView_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            if (e.Row.DataBoundItem is KhachHang selectedKhachHang)
            {
                _viewModel.XoaKhachHangCommand.Execute(selectedKhachHang);
            }
        }

        private void RefreshDataGrid()
        {
            dataGridView.DataSource = null;
            dataGridView.DataSource = _viewModel.DanhSachKhachHang;
        }
    }
}