using System;
using System.Windows.Forms;

namespace prjBTLDemo.NET.Forms
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();

            // Attach event handlers to each menu item
            menuSanPham.Click += MenuSanPham_Click;
            menuKhachHang.Click += MenuKhachHang_Click;
            menuNhanVien.Click += MenuNhanVien_Click;
            menuHoaDon.Click += MenuHoaDon_Click;
            menuThongKe.Click += MenuThongKe_Click;
            menuDangXuat.Click += MenuDangXuat_Click;
            menuThoat.Click += MenuThoat_Click;
        }

        private void MenuSanPham_Click(object sender, EventArgs e)
        {
            FormSanPham childForm = new FormSanPham();
            childForm.MdiParent = this; // Gán form cha
            childForm.Show();

        }

        private void MenuKhachHang_Click(object sender, EventArgs e)
        {
            FormKhachHang formKhachHang = new FormKhachHang { MdiParent = this };
            formKhachHang.Show();
        }

        private void MenuNhanVien_Click(object sender, EventArgs e)
        {
            FormNhanVien formNhanVien = new FormNhanVien { MdiParent = this };
            formNhanVien.Show();
        }

        private void MenuHoaDon_Click(object sender, EventArgs e)
        {
            
        }

        private void MenuThongKe_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Chức năng thống kê đang được phát triển!", "Thông báo");
        }

        private void MenuDangXuat_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Đăng xuất thành công!", "Thông báo");
            this.Close(); // Close the main form (simulate logout)
        }

        private void MenuThoat_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Bạn có chắc chắn muốn thoát không?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        
    }
}