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
        TaiKhoan tk = new TaiKhoan();
        TKBLL tkbll = new TKBLL();
        public Form1()
        {
            InitializeComponent();
        }
        private void btn_Login_Click(object sender, EventArgs e)
        {
            tk.USERNAME = txt_User.Text;
            tk.PASSWD = txt_Pass.Text;

            string getuser = tkbll.CheckLogin(tk);
            //TRả lại kết quả nếu BLL ko đúng
            switch (getuser)
            {
                case "Username required":
                    MessageBox.Show("Username không để trống");
                    return;

                case "Password required":
                    MessageBox.Show("Password không để trống");
                    return;

                case "Tài khoản hoặc mật khẩu không chính xác":
                    MessageBox.Show("Tài khoản hoặc mật khẩu không chính xác");
                    return;
            }

            MessageBox.Show("Login Successfull");
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btn_Exit_Click(object sender, EventArgs e)
        {

        }
    }
}
