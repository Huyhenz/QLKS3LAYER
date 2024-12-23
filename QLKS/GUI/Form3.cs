using BLL;
using DevExpress.Data.Mask.Internal;
using DevExpress.XtraLayout.Customization.UserCustomization;
using DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Security;
using System.Windows.Forms;

namespace GUI
{
    public partial class Form3 : Form
    {
        private taikhoanbus taiKhoanBus;
        public Form3()
        {
            taiKhoanBus = new taikhoanbus();
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string fullname = txt_fullname.Text;
            string username = txt_User.Text;
            string password = txt_Pass.Text;
            string confirmPassword = txt_Passx2.Text;
            string email = txt_Email.Text;
            string address = txt_DIaChi.Text;
            string phoneNumber = txt_SDT.Text;
            string cccd = txt_CCCD.Text;
            string role = cmb_QH.SelectedItem?.ToString();

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

        private void button2_Click(object sender, EventArgs e)
        {
            DangNhap f1 = new DangNhap();
            f1.Show();
            Hide();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            cmb_QH.Items.Add("User");
            cmb_QH.Items.Add("Manager");
        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }
    }
}
