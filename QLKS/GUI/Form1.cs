using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DTO;
using BLL;
namespace GUI
{
    public partial class Form1 : DevExpress.XtraEditors.XtraForm
    {
        public Form1()
        {
            InitializeComponent();
        }
        private void btn_Login_Click(object sender, EventArgs e)
        {
            if (emptyFields())
            {
                MessageBox.Show("Tài khoản , mật khẩu không được để trống ", "Thông Báo !", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                taikhoanbus taikhoanbus = new taikhoanbus();
                bool isValid = taikhoanbus.Login(txt_User.Text, txt_Pass.Text);

                if (isValid)
                {
                    MessageBox.Show("Đăng nhập thành công", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    Form3 f = new Form3();
                    f.Show();

                    // Ẩn form đăng nhập (Form1)
                    this.Hide();

                }
                else
                {
                    MessageBox.Show("Tài khoản hoặc mật khẩu không chính xác", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        public bool emptyFields()
        {
            if (txt_User.Text == "" || txt_Pass.Text == "")
            {
                return true;
            }
            return false;
        }

        private void btn_Exit_Click(object sender, EventArgs e)
        {

        }
    }
}
