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
                MessageBox.Show("Tài khoản, mật khẩu không được để trống", "Thông Báo!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                taikhoanbus taikhoanbus = new taikhoanbus();
                TaiKhoanDTO user = taikhoanbus.Login(txt_User.Text, txt_User.Text);

                if (user != null)
                {
                    MessageBox.Show("Đăng nhập thành công", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Kiểm tra quyền hạn và điều hướng đến form tương ứng
                    int roleValue = user.IDQUYEN; // IDQUYEN đã là int, không cần chuyển đổi
                    if (roleValue == 1)
                    {
                        Form4 form4 = new Form4();
                        form4.Show();
                        this.Hide();
                    }
                    else if (roleValue == 2)
                    {
                        Form5 form5 = new Form5();
                        form5.Show();
                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show("Quyền hạn không hợp lệ", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
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
            return string.IsNullOrEmpty(txt_User.Text) || string.IsNullOrEmpty(txt_Pass.Text);
        }

        private void btn_Exit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            Form3 f = new Form3();
            f.Show();
            Hide();
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
                    // Kiểm tra quyền hạn và điều hướng đến form tương ứng
                    int roleValue = user.IDQUYEN; // IDQUYEN đã là int, không cần chuyển đổi
                    if (roleValue == 1)
                    {
                        Form4 form4 = new Form4();
                        form4.Show();
                        this.Hide();
                    }
                    else if (roleValue == 2)
                    {
                        Form5 form5 = new Form5();
                        form5.Show();
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
            Form3 f = new Form3();
            f.Show();
            Hide();
        }
    }
}
