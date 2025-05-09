using System;
using System.Windows.Forms;
using prjBTLDemo.NET.ViewModels;

namespace prjBTLDemo.NET.Views
{
    public partial class FormHoaDon2 : Form
    {
        private readonly HoaDonVM _hoaDonVM;

        public FormHoaDon2()
        {
            InitializeComponent();
            _hoaDonVM = new HoaDonVM();
            BindData();
        }

        private void BindData()
        {
            dgvHoaDon.DataSource = _hoaDonVM.DanhSachHoaDon;

            txtSearch.DataBindings.Add("Text", _hoaDonVM, nameof(_hoaDonVM.SearchTerm), true, DataSourceUpdateMode.OnPropertyChanged);
            btnAdd.DataBindings.Add("Enabled", _hoaDonVM, nameof(_hoaDonVM.ThemHoaDonCommand));
            btnDetails.DataBindings.Add("Enabled", _hoaDonVM, nameof(_hoaDonVM.XemChiTietHoaDonCommand));
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            _hoaDonVM.ThemHoaDonCommand.Execute(null);
        }

        private void btnDetails_Click(object sender, EventArgs e)
        {
            _hoaDonVM.XemChiTietHoaDonCommand.Execute(null);
        }
    }
}