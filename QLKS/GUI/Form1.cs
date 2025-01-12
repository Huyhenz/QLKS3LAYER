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
using System.Security.Principal;
namespace GUI
{
    public partial class Form1 : Form
    {
        private bool isExchangeMode = false;
        private ThongTinDP tempBooking;
        private KhachHangDTO tempCustomer;
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

        public void SetExchangeMode(ThongTinDP booking, KhachHangDTO customer)
        {
            isExchangeMode = true;
            tempBooking = booking;
            tempCustomer = customer;

            MessageBox.Show("Chế độ đổi phòng được kích hoạt. Nhấp vào phòng trống để thay đổi.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                        btn.Text = $"{room.IDPHONG} - Đã đặt";
                    }
                    else if(room.TINHTRANG.ToLower() == "nhận phòng")
                    {
                        btn.FillColor = System.Drawing.Color.Yellow;
                        btn.HoverState.FillColor = System.Drawing.Color.Yellow;
                        btn.PressedColor = System.Drawing.Color.Yellow;
                        btn.ForeColor = System.Drawing.Color.Black;
                        btn.Text = $"{room.IDPHONG} - Đã nhận phòng";
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
                if (btn != null)
                {
                    // Lấy phần số từ chuỗi văn bản của button
                    string roomIdString = new string(btn.Text.Where(char.IsDigit).ToArray());

                    if (int.TryParse(roomIdString, out int newRoomId))
                    {
                        if (isExchangeMode)
                        {
                            // Kiểm tra trạng thái phòng mới từ cơ sở dữ liệu
                            var room = phongbus.GetAllRooms().FirstOrDefault(r => r.IDPHONG == newRoomId);
                            if (room != null && room.TINHTRANG.ToLower() == "trống")
                            {
                                // Cập nhật thông tin đặt phòng trong cơ sở dữ liệu
                                phongbus.UpdateRoomBooking(tempBooking.IDPHONG, newRoomId);

                                // Cập nhật trạng thái phòng cũ thành "trống" trong cơ sở dữ liệu
                                phongbus.UpdateTinhTrangPhong(tempBooking.IDPHONG, "trống");

                                // Cập nhật trạng thái phòng mới thành "đã đặt" trong cơ sở dữ liệu
                                phongbus.UpdateTinhTrangPhong(newRoomId, "đã đặt");

                                // Cập nhật lại trạng thái của các nút trong Form1
                                Guna2Button oldButton = this.Controls.Find($"guna2Button{tempBooking.IDPHONG}", true).FirstOrDefault() as Guna2Button;
                                if (oldButton != null)
                                {
                                    oldButton.FillColor = Color.SeaGreen;
                                    oldButton.HoverState.FillColor = Color.SeaGreen;
                                    oldButton.PressedColor = Color.SeaGreen;
                                    oldButton.ForeColor = Color.Black;
                                    oldButton.Text = tempBooking.IDPHONG.ToString();
                                }

                                btn.FillColor = Color.Red;
                                btn.HoverState.FillColor = Color.Red;
                                btn.PressedColor = Color.Red;
                                btn.ForeColor = Color.White;
                                btn.Text = $"{newRoomId} - Đã đặt";

                                // Cập nhật trạng thái đổi phòng
                                isExchangeMode = false;

                                MessageBox.Show($"Đã đổi phòng thành công từ {tempBooking.IDPHONG} sang {newRoomId}!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                MessageBox.Show("Chỉ có thể đổi sang phòng trống.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        else
                        {
                            Form4 form4 = new Form4(newRoomId);
                            form4.Show();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Không thể xác định ID phòng.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
        }





        private void guna2Button37_Click(object sender, EventArgs e)
        {
            Guna2Button btn = sender as Guna2Button;
            if (btn != null)
            {
                // Lấy phần số từ chuỗi văn bản của button
                string roomIdString = new string(btn.Text.Where(char.IsDigit).ToArray());

                if (int.TryParse(roomIdString, out int newRoomId))
                {
                    if (isExchangeMode)
                    {
                        // Kiểm tra trạng thái phòng mới từ cơ sở dữ liệu
                        var room = phongbus.GetAllRooms().FirstOrDefault(r => r.IDPHONG == newRoomId);
                        if (room != null && room.TINHTRANG.ToLower() == "trống")
                        {
                            // Cập nhật thông tin đặt phòng trong cơ sở dữ liệu
                            phongbus.UpdateRoomBooking(tempBooking.IDPHONG, newRoomId);

                            // Cập nhật trạng thái phòng cũ thành "trống" trong cơ sở dữ liệu
                            phongbus.UpdateTinhTrangPhong(tempBooking.IDPHONG, "trống");

                            // Cập nhật trạng thái phòng mới thành "đã đặt" trong cơ sở dữ liệu
                            phongbus.UpdateTinhTrangPhong(newRoomId, "đã đặt");

                            // Cập nhật lại trạng thái của các nút trong Form1
                            Guna2Button oldButton = this.Controls.Find($"guna2Button{tempBooking.IDPHONG}", true).FirstOrDefault() as Guna2Button;
                            if (oldButton != null)
                            {
                                oldButton.FillColor = Color.SeaGreen;
                                oldButton.HoverState.FillColor = Color.SeaGreen;
                                oldButton.PressedColor = Color.SeaGreen;
                                oldButton.ForeColor = Color.Black;
                                oldButton.Text = tempBooking.IDPHONG.ToString();
                            }

                            btn.FillColor = Color.Red;
                            btn.HoverState.FillColor = Color.Red;
                            btn.PressedColor = Color.Red;
                            btn.ForeColor = Color.White;
                            btn.Text = $"{newRoomId} - Đã đặt";

                            // Cập nhật trạng thái đổi phòng
                            isExchangeMode = false;

                            MessageBox.Show($"Đã đổi phòng thành công từ {tempBooking.IDPHONG} sang {newRoomId}!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("Chỉ có thể đổi sang phòng trống.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        Form4 form4 = new Form4(newRoomId);
                        form4.Show();
                    }
                }
                else
                {
                    MessageBox.Show("Không thể xác định ID phòng.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void guna2Button45_Click(object sender, EventArgs e)
        {
            Guna2Button btn = sender as Guna2Button;
            if (btn != null)
            {
                // Lấy phần số từ chuỗi văn bản của button
                string roomIdString = new string(btn.Text.Where(char.IsDigit).ToArray());

                if (int.TryParse(roomIdString, out int newRoomId))
                {
                    if (isExchangeMode)
                    {
                        // Kiểm tra trạng thái phòng mới từ cơ sở dữ liệu
                        var room = phongbus.GetAllRooms().FirstOrDefault(r => r.IDPHONG == newRoomId);
                        if (room != null && room.TINHTRANG.ToLower() == "trống")
                        {
                            // Cập nhật thông tin đặt phòng trong cơ sở dữ liệu
                            phongbus.UpdateRoomBooking(tempBooking.IDPHONG, newRoomId);

                            // Cập nhật trạng thái phòng cũ thành "trống" trong cơ sở dữ liệu
                            phongbus.UpdateTinhTrangPhong(tempBooking.IDPHONG, "trống");

                            // Cập nhật trạng thái phòng mới thành "đã đặt" trong cơ sở dữ liệu
                            phongbus.UpdateTinhTrangPhong(newRoomId, "đã đặt");

                            // Cập nhật lại trạng thái của các nút trong Form1
                            Guna2Button oldButton = this.Controls.Find($"guna2Button{tempBooking.IDPHONG}", true).FirstOrDefault() as Guna2Button;
                            if (oldButton != null)
                            {
                                oldButton.FillColor = Color.SeaGreen;
                                oldButton.HoverState.FillColor = Color.SeaGreen;
                                oldButton.PressedColor = Color.SeaGreen;
                                oldButton.ForeColor = Color.Black;
                                oldButton.Text = tempBooking.IDPHONG.ToString();
                            }

                            btn.FillColor = Color.Red;
                            btn.HoverState.FillColor = Color.Red;
                            btn.PressedColor = Color.Red;
                            btn.ForeColor = Color.White;
                            btn.Text = $"{newRoomId} - Đã đặt";

                            // Cập nhật trạng thái đổi phòng
                            isExchangeMode = false;

                            MessageBox.Show($"Đã đổi phòng thành công từ {tempBooking.IDPHONG} sang {newRoomId}!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("Chỉ có thể đổi sang phòng trống.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        Form4 form4 = new Form4(newRoomId);
                        form4.Show();
                    }
                }
                else
                {
                    MessageBox.Show("Không thể xác định ID phòng.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }



        private void Form1_Load(object sender, EventArgs e)
        {
            if (Session.Login != null)
            {
                txtAccount.Text = Session.Login.FULLNAME;
                //txtFullName.ReadOnly = true; // Đặt TextBox thành không thể chỉnh sửa
            }
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

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            Form4 f = new Form4();
            f.Show();
        }

        private void toolStripSplitButton1_ButtonClick(object sender, EventArgs e)
        {
            Form3 f = new Form3();
            f.Show();
        }

        private void toolStripButton8_Click(object sender, EventArgs e)
        {
            DangNhap f = new DangNhap();
            f.Show();
            Hide();
        }
    }
}
