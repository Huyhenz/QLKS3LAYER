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
            string gender = guna2RadioButton1.Checked ? "Nam" : "Nữ"; // Lấy giới tính từ RadioButton
            string startDate = dateEdit2.DateTime.ToString("yyyy-MM-dd"); // Lấy ngày vào làm từ DateEdit

            if (string.IsNullOrEmpty(fullname) || string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password) ||
                string.IsNullOrEmpty(confirmPassword) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(address) ||
                string.IsNullOrEmpty(phoneNumber) || string.IsNullOrEmpty(cccd) || string.IsNullOrEmpty(role) ||
                string.IsNullOrEmpty(gender) || string.IsNullOrEmpty(startDate))
            {
                labelFullinfo.Visible = true;
                return;
            }

            if (password != confirmPassword)
            {
                labelFullinfo.Visible = false;
                labelX2.Visible = true;
                txtPass.Clear();
                txtComfirmPass.Clear();
                return;
            }

            int roleValue = role == "User" ? 1 : 2;

            // Lấy giá trị ngày sinh từ DateEdit
            string ngaySinh = dateEdit1.DateTime.ToString("yyyy-MM-dd");

            TaiKhoanDTO newTaiKhoan = new TaiKhoanDTO
            {
                FULLNAME = fullname,
                USERNAME = username,
                PASSWD = password,
                EMAIL = email,
                DIACHI = address,
                SDT = long.Parse(phoneNumber),
                CCCD = long.Parse(cccd),
                NGAYSINH = ngaySinh,
                IDQUYEN = roleValue,
                GIOITINH = gender, // Gán giới tính
                NGAYVAOLAM = startDate // Gán ngày vào làm
            };

            bool result = taiKhoanBus.AddTaiKhoan(newTaiKhoan);

            if (result)
            {
                labelX2.Visible = false;
                labelSuccess.Visible = true;
                Task.Delay(2000).ContinueWith(t =>
                {
                    this.Invoke((MethodInvoker)delegate
                    {
                        DangNhap dangNhap = new DangNhap();
                        dangNhap.Show();
                        this.Hide();
                    });
                });
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

        private void guna2Button2_Click(object sender, EventArgs e)
        {   
            DangNhap f = new DangNhap();
            f.Show();
            this.Hide();
            
        }
    }
}
