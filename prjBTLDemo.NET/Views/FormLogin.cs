using System;
using System.Windows.Forms;
using prjBTLDemo.NET.Models;
using prjBTLDemo.NET.ViewModels;

namespace prjBTLDemo.NET.Forms
{
    public partial class FormLogin : Form
    {
        private TaiKhoanVM _viewModel;

        public FormLogin()
        {
            InitializeComponent();
            _viewModel = new TaiKhoanVM();

            // Attach event handler for login button
            btnLogin.Click += BtnLogin_Click;
        }

        private void BtnLogin_Click(object sender, EventArgs e)
        {
            _viewModel.TaiKhoanHienTai = new TaiKhoan
            {
                TenDangNhap = txtUsername.Text,
                MatKhau = txtPassword.Text
            };
            _viewModel.DangNhapCommand.Execute(null);

            if (_viewModel.TaiKhoanHienTai != null)
            {
                lblMessage.ForeColor = System.Drawing.Color.Green;
                lblMessage.Text = "Đăng nhập thành công!";
                this.Hide();
                FormMain mainForm = new FormMain();
                mainForm.ShowDialog();
                this.Close();
            }
            else
            {
                lblMessage.ForeColor = System.Drawing.Color.Red;
                lblMessage.Text = "Tên đăng nhập hoặc mật khẩu không chính xác!";
            }
        }
    }
}