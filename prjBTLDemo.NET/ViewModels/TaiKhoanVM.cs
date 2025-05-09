using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using prjBTLDemo.NET.Utilities;
using prjBTLDemo.NET.Models;
using System.Windows.Forms;

namespace prjBTLDemo.NET.ViewModels
{
    public class TaiKhoanVM
    {
        public ObservableCollection<TaiKhoan> DanhSachTaiKhoan { get; set; }
        public TaiKhoan TaiKhoanHienTai { get; set; }
        public ICommand DangNhapCommand { get; }

        public TaiKhoanVM()
        {
            DanhSachTaiKhoan = new ObservableCollection<TaiKhoan>();
            DangNhapCommand = new RelayCommand(DangNhap);
        }

        private void DangNhap(object parameter)
        {
            if (TaiKhoanHienTai != null && TaiKhoanHienTai.TenDangNhap == "admin" && TaiKhoanHienTai.MatKhau == "123456")
            {
                MessageBox.Show("Đăng nhập thành công với quyền Admin!");
            }   
            else
            {
                MessageBox.Show("Đăng nhập thất bại. Vui lòng kiểm tra lại tài khoản/mật khẩu.");
            }
        }
    }
}