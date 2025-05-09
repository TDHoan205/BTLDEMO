using System;
using System.Windows.Forms;
using prjBTLDemo.NET.ViewModels;

namespace prjBTLDemo.NET.Forms
{
    public partial class FormSanPham : Form
    {
        private readonly SanPhamVM _viewModel;

        public FormSanPham()
        {
            InitializeComponent();
            _viewModel = new SanPhamVM();

            // Bind DataGridView to ViewModel's Products
            dataGridView.DataSource = _viewModel.Products;

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
            _viewModel.AddProduct();
            RefreshDataGrid();
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            if (dataGridView.CurrentRow?.DataBoundItem is Product selectedProduct)
            {
                _viewModel.DeleteProduct(selectedProduct.Id);
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
            var editedProduct = dataGridView.Rows[e.RowIndex].DataBoundItem as Product;
            if (editedProduct != null)
            {
                _viewModel.UpdateProduct(editedProduct);
                RefreshDataGrid();
            }
        }

        private void DataGridView_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            if (e.Row.DataBoundItem is Product selectedProduct)
            {
                _viewModel.DeleteProduct(selectedProduct.Id);
            }
        }

        private void RefreshDataGrid()
        {
            dataGridView.DataSource = null;
            dataGridView.DataSource = _viewModel.Products;
        }

        private void FormSanPham_Load(object sender, EventArgs e)
        {

        }
    }
}