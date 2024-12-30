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
    public partial class Form9 : Form
    {
        private taikhoanbus taikhoanbus = new taikhoanbus();
        public Form9()
        {
            InitializeComponent();
            LoadTaiKhoan();
        }

        private void guna2Button7_Click(object sender, EventArgs e)
        {
            TaiKhoanDTO newTaiKhoan = new TaiKhoanDTO
            {
                FULLNAME = txtHoTenNV.Text,
                NGAYSINH = timeNV.Value,
                EMAIL = txtEMAILNV.Text,
                SDT = Convert.ToInt64(txtSDTNV.Text),
                CCCD = Convert.ToInt64(txtCCCDNV.Text),
                DIACHI = txtDCNV.Text,
                //USERNAME = txtHoTenNV.Text,
                //PASSWD = txtPassword.Text,
                //IDQUYEN = Convert.ToInt32(txtIDQuyen.Text)
            };

            taikhoanbus.AddTaiKhoan(newTaiKhoan);
            LoadTaiKhoan(); // Tải lại danh sách tài khoản sau khi thêm mới
            ClearInputFields(); // Xóa nội dung các ô nhập liệu sau khi thêm tài khoản
        }

        // Hàm ClearInputFields để xóa nội dung các ô nhập liệu
        private void ClearInputFields()
        {
            txtHoTenNV.Text = "";
            timeNV.Value = DateTime.Now;
            txtHoTenNV.Text = "";
            txtSDTNV.Text = "";
            txtCCCDNV.Text = "";
            txtDCNV.Text = "";
            //txtUsername.Text = "";
            //txtPassword.Text = "";
            //txtIDQuyen.Text = "";
        }
    

        private void LoadTaiKhoan() 
        { 
            dgvNhanVien.DataSource = taikhoanbus.GetAllTaiKhoan(); 
        }
    }
}
