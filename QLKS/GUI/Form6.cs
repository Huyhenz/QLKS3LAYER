using BLL;
using DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUI
{
    public partial class Form6 : Form
    {
        private taikhoanbus taiKhoanBus;
        public Form6()
        {
            taiKhoanBus = new taikhoanbus();
            InitializeComponent();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            string fullname = txtFullname.Text;
            string username = txtUser.Text;
            string password = txtPass.Text;
            string confirmPassword = txtComfirmPass.Text;
            string email = txtEmail.Text;
            string address = txtDC.Text;
            string phoneNumber = txtSDT.Text;
            string cccd = txtCCCD.Text;
            string role = comboQH.SelectedItem?.ToString();

            if (string.IsNullOrEmpty(fullname) || string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password) ||
                string.IsNullOrEmpty(confirmPassword) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(address) ||
                string.IsNullOrEmpty(phoneNumber) || string.IsNullOrEmpty(cccd) || string.IsNullOrEmpty(role))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!");
                return;
            }

            if (password != confirmPassword)
            {
                MessageBox.Show("Mật khẩu xác nhận không khớp!");
                return;
            }

            int roleValue = role == "User" ? 1 : 2;

            TaiKhoanDTO newTaiKhoan = new TaiKhoanDTO
            {
                FULLNAME = fullname,
                USERNAME = username,
                PASSWD = password,
                EMAIL = email,
                DIACHI = address,
                SDT = long.Parse(phoneNumber),
                CCCD = long.Parse(cccd),
                IDQUYEN = roleValue // Gán giá trị số cho IDQUYEN
            };

            bool result = taiKhoanBus.AddTaiKhoan(newTaiKhoan);

            if (result)
            {
                MessageBox.Show("Đăng ký thành công!");
                this.Hide();
            }
            else
            {
                MessageBox.Show("Đăng ký thất bại!");
            }
        }

        private void Form6_Load(object sender, EventArgs e)
        {
            comboQH.Items.Add("User");
            comboQH.Items.Add("Manager");
        }
    }
}
