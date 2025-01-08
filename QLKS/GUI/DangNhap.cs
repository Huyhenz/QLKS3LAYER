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
using System.Web.Security;
namespace GUI
{
    public partial class DangNhap : DevExpress.XtraEditors.XtraForm
    {
        public DangNhap()
        {
            InitializeComponent();
        }
        public bool emptyFields()
        {
            return string.IsNullOrEmpty(txt_User.Text) || string.IsNullOrEmpty(txt_Pass.Text);
        }

        private void btn_Exit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            
        }

        private void txtUsername_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (emptyFields())
            {
                MessageBox.Show("Tài khoản, mật khẩu không được để trống", "Thông Báo!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                taikhoanbus taikhoanbus = new taikhoanbus();
                TaiKhoanDTO user = taikhoanbus.Login(txt_User.Text, txt_Pass.Text);

                if (user != null)
                {
                    labelError.Visible = false;
                    Session.Login = user;
                    // Kiểm tra quyền hạn và điều hướng đến form tương ứng
                    int roleValue = user.IDQUYEN; // IDQUYEN đã là int, không cần chuyển đổi
                    if (roleValue == 1)
                    {
                        Form1 f = new Form1();
                        f.Show();
                        this.Hide();

                    }
                    else if (roleValue == 2)
                    {
                        Form9 form9 = new Form9();
                        form9.Show();
                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show("Quyền hạn không hợp lệ", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    labelError.Visible = true;
                    txt_Pass.Clear();
                }
            }
        }

        private void btnSingup_Click(object sender, EventArgs e)
        {
            Form6 f = new Form6();
            f.Show();
            Hide();
        }

        private void DangNhap_Load(object sender, EventArgs e)
        {

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
