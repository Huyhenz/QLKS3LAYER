using BLL;
using DTO;
using Guna.UI2.WinForms;
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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            LoadPhongButtons();
        }

        private void LoadPhongButtons()
        {
            Phongbus phongBLL = new Phongbus();
            List<Phong> phongList = phongBLL.GetAllPhong();

            foreach (Phong phong in phongList)
            {
                // Tạo Guna2Button mới
                Guna2Button btn = new Guna2Button();
                btn.Text = phong.IDPHONG.ToString(); // Hiển thị ID phòng trên button

                // Gán sự kiện Click cho button
                btn.Click += new EventHandler(guna2Button2_Click);
                btn.Click += new EventHandler(guna2Button37_Click);
                btn.Click += new EventHandler(guna2Button45_Click);
                // Thêm Guna2Button vào form
                this.Controls.Add(btn);
            }
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            Guna2Button btn = (Guna2Button)sender; // Lấy button đã được nhấn
            int roomId = int.Parse(btn.Text); // Lấy ID phòng từ text của button

            // Mở form đặt phòng với số phòng tương ứng
            Form4 f = new Form4(roomId);
            f.Show();
        }

        private void guna2Button37_Click(object sender, EventArgs e)
        {
            Guna2Button btn = (Guna2Button)sender; // Lấy button đã được nhấn
            int roomId = int.Parse(btn.Text); // Lấy ID phòng từ text của button

            // Mở form đặt phòng với số phòng tương ứng
            Form4 f = new Form4(roomId);
            f.Show();
        }

        private void guna2Button45_Click(object sender, EventArgs e)
        {
            Guna2Button btn = (Guna2Button)sender; // Lấy button đã được nhấn
            int roomId = int.Parse(btn.Text); // Lấy ID phòng từ text của button

            // Mở form đặt phòng với số phòng tương ứng
            Form4 f = new Form4(roomId);
            f.Show();
        }
    }
}
