using BLL;
using DTO;
using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Data.SqlClient;
namespace GUI
{
    public partial class Form1 : Form
    {

        private Phongbus phongbus = new Phongbus();
        private List<Phong> rooms;
        public Form1()
        {
            InitializeComponent();
            Guna2Button btn = new Guna2Button();
            btn.Click += new EventHandler(guna2Button2_Click);
            btn.Click += new EventHandler(guna2Button37_Click);
            btn.Click += new EventHandler(guna2Button45_Click);
            LoadRooms();
        }
         
        public void LoadRooms()
        {
            // Lấy danh sách phòng từ BLL
            rooms = phongbus.GetAllRooms();

            // Kiểm tra danh sách phòng
            foreach (var room in rooms)
            {
                Console.WriteLine($"IDPHONG: {room.IDPHONG}, TENPHONG: {room.TENPHONG}, TINHTRANG: {room.TINHTRANG}");

                // Tìm nút tương ứng với ID phòng
                Guna2Button btn = this.Controls.Find($"guna2Button{room.IDPHONG}", true).FirstOrDefault() as Guna2Button;
                if (btn != null)
                {
                    // Cập nhật màu nút theo trạng thái
                    if (room.TINHTRANG.ToLower() == "đã đặt")
                    {
                        btn.FillColor = System.Drawing.Color.Red;       // Màu nền chính
                        btn.HoverState.FillColor = System.Drawing.Color.Red; // Màu khi di chuột
                        btn.PressedColor = System.Drawing.Color.Red;   // Màu khi nhấn
                        btn.ForeColor = System.Drawing.Color.White;
                        btn.Text = room.TINHTRANG;
                    }
                    else if(room.TINHTRANG.ToLower() == "Đang ở")
                    {
                        btn.FillColor = System.Drawing.Color.Yellow;
                        btn.HoverState.FillColor = System.Drawing.Color.Yellow;
                        btn.PressedColor = System.Drawing.Color.Yellow;
                        btn.ForeColor = System.Drawing.Color.Black;
                        btn.Text = room.TINHTRANG;
                    }
                    else
                    {
                        btn.FillColor = System.Drawing.Color.SeaGreen;
                        btn.HoverState.FillColor = System.Drawing.Color.SeaGreen;
                        btn.PressedColor = System.Drawing.Color.SeaGreen;
                        btn.ForeColor = System.Drawing.Color.Black;

                    }

                    // Đặt tên phòng làm Text hoặc Tooltip
                    //btn.Text = room.TINHTRANG;
                    btn.Tag = room.IDPHONG; // Lưu ID phòng vào Tag nếu cần
                }
                else
                {
                    Console.WriteLine($"Không tìm thấy nút với IDPHONG: {room.IDPHONG}");
                }
            }
        }




        private void guna2Button2_Click(object sender, EventArgs e)
        {
            Guna2Button btn = sender as Guna2Button;
            int roomId = int.Parse(btn.Text);
            Form4 form4 = new Form4(roomId);
            form4.Show();
        }




        private void guna2Button37_Click(object sender, EventArgs e)
        {
            Guna2Button btn = sender as Guna2Button;
            int roomId = int.Parse(btn.Text);
            Form4 form4 = new Form4(roomId);
            form4.Show();
        }

        private void guna2Button45_Click(object sender, EventArgs e)
        {
            Guna2Button btn = sender as Guna2Button;
            int roomId = int.Parse(btn.Text);
            Form4 form4 = new Form4(roomId);
            form4.Show();
        }



        private void Form1_Load(object sender, EventArgs e)
        {
            //SetupRoomButtons();
            
        }
    //     private void SetupRoomButtons()
    //{
    //    for (int i = 1; i <= 20; i++)
    //    {
    //        Guna.UI2.WinForms.Guna2Button btn = this.Controls.Find($"guna2Button{i}", true).FirstOrDefault() as Guna.UI2.WinForms.Guna2Button;

    //        if (btn != null)
    //        {
    //            btn.Click += new EventHandler(guna2Button2_Click); // Gắn sự kiện chung
    //        }
    //    }
    //}
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            Form11 f = new Form11();
            f.Show();
        }

      
    }
}
