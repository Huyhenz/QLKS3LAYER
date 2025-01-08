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
    public partial class Form11 : Form
    {
        public Form11()
        {
            InitializeComponent();
        }

        decimal tongTien = 0;

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void guna2GroupBox3_Click(object sender, EventArgs e)
        {

        }

        private void guna2GroupBox1_Click(object sender, EventArgs e)
        {

        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void label20_Click(object sender, EventArgs e)
        {

        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {
        }

        private void Form11_Load(object sender, EventArgs e)
        {
            guna2DataGridView1.Columns.Add("MaPhong", "Mã Phòng");
            guna2DataGridView1.Columns.Add("TenPhong", "Tên Phòng");
            guna2DataGridView1.Columns.Add("Gia", "Giá");
            guna2DataGridView1.Columns.Add("TrangThai", "Trạng Thái");

            // Sự kiện khi click vào dòng của DataGridView
            guna2DataGridView1.CellClick += guna2DataGridView1_CellClick;

            guna2DataGridView1.Columns.Add("TenDichVu", "Tên Dịch Vụ");
            guna2DataGridView1.Columns.Add("Gia", "Giá Dịch Vụ");
            guna2DataGridView1.Columns.Add("SoLuong", "Số Lượng");
            guna2DataGridView1.Columns.Add("ThanhTien", "Thành Tiền");

            guna2CheckBox4.CheckedChanged += guna2CheckBox4_CheckedChanged;
            guna2CheckBox8.CheckedChanged += guna2CheckBox4_CheckedChanged;
            guna2CheckBox5.CheckedChanged += guna2CheckBox4_CheckedChanged;
            guna2CheckBox6.CheckedChanged += guna2CheckBox4_CheckedChanged;
            guna2CheckBox10.CheckedChanged += guna2CheckBox4_CheckedChanged;
            guna2CheckBox3.CheckedChanged += guna2CheckBox4_CheckedChanged;
            guna2CheckBox7.CheckedChanged += guna2CheckBox4_CheckedChanged;
            guna2CheckBox11.CheckedChanged += guna2CheckBox4_CheckedChanged;
            guna2CheckBox12.CheckedChanged += guna2CheckBox4_CheckedChanged;
            guna2CheckBox2.CheckedChanged += guna2CheckBox4_CheckedChanged;
            guna2CheckBox9.CheckedChanged += guna2CheckBox4_CheckedChanged;
        }

        private void guna2GroupBox1_Click_1(object sender, EventArgs e)
        {

        }

        private void guna2GroupBox7_Click(object sender, EventArgs e)
        {

        }

        private void guna2GroupBox2_Click(object sender, EventArgs e)
        {

        }

        private void guna2CheckBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void guna2CheckBox4_CheckedChanged(object sender, EventArgs e)
        {

            CheckBox checkbox = sender as CheckBox;

            if (checkbox.Checked)
            {
                switch (checkbox.Name)
                {
                    case "GiatUi":
                        tongTien += 100000;
                        break;
                    case "Spa":
                        tongTien += 500000;
                        break;
                    case "XeDuaDon":
                        tongTien += 200000;
                        break;
                    case "ChoThueXe":
                        tongTien += 300000;
                        break;
                    case "PhongHop":
                        tongTien += 1000000;
                        break;
                    case "HoBoi":
                        tongTien += 100000;
                        break;
                    case "Karaoke":
                        tongTien += 200000;
                        break;
                    case "TrongTre":
                        tongTien += 150000;
                        break;
                    case "FitnessCenter":
                        tongTien += 100000;
                        break;
                    case "PhongHopSangTrong":
                        tongTien += 3000000;
                        break;
                    case "SpaCaoCap":
                        tongTien += 1500000;
                        break;
                    case "Phucvu24h":
                        tongTien += 1500000;
                        break;
                    case "Minibar":
                        tongTien += 1500000;
                        break;
                }
            }
            else
            {
                switch (checkbox.Name)
                {
                    case "kGiatUi":
                        tongTien -= 100000;
                        break;
                    case "kSpa":
                        tongTien -= 500000;
                        break;
                    case "kXeDuaDon":
                        tongTien -= 200000;
                        break;
                    case "ChoThueXe":
                        tongTien -= 300000;
                        break;
                    case "PhongHop":
                        tongTien -= 1000000;
                        break;
                    case "HoBoi":
                        tongTien -= 100000;
                        break;
                    case "Karaoke":
                        tongTien -= 200000;
                        break;
                    case "TrongTre":
                        tongTien -= 150000;
                        break;
                    case "FitnessCenter":
                        tongTien -= 100000;
                        break;
                    case "PhongHopSangTrong":
                        tongTien -= 3000000;
                        break;
                    case "SpaCaoCap":
                        tongTien -= 1500000;
                        break;
                    case "Phucvu24h":
                        tongTien -= 1500000;
                        break;
                    case "Minibar":
                        tongTien -= 1500000;
                        break;
                }
            }

            guna2TextBox4.Text = tongTien.ToString("N0") + " VND";

        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(guna2TextBox8.Text, out int soLuong) || soLuong <= 0)
            {
                MessageBox.Show("Vui lòng nhập số lượng hợp lệ!");
                return;
            }

            foreach (Control control in this.Controls)
            {
                if (control is CheckBox checkbox && checkbox.Checked)
                {
                    // Lấy tên dịch vụ và giá
                    string tenDichVu = checkbox.Text;
                    decimal giaDichVu = 0;

                    switch (checkbox.Name)
                    {
                        case "guna2CheckBox4":
                            giaDichVu = 100000; // Giá giặt ủi
                            break;
                        case "guna2CheckBox8":
                            giaDichVu = 500000; // Giá Spa
                            break;
                        case "guna2CheckBox5":
                            giaDichVu = 200000; // Giá xe đưa đón
                            break;
                        case "guna2CheckBox6":
                            giaDichVu = 300000; // Giá cho thuê xe
                            break;
                        case "guna2CheckBox10":
                            giaDichVu = 1000000; // Giá phòng họp
                            break;
                        case "guna2CheckBox3":
                            giaDichVu = 100000; // Giá hồ bơi
                            break;
                        case "guna2CheckBox7":
                            giaDichVu = 200000; // Giá karaoke
                            break;
                        case "guna2CheckBox11":
                            giaDichVu = 150000; // Giá trông trẻ
                            break;
                        case "guna2CheckBox12":
                            giaDichVu = 100000; // Giá Fitness Center
                            break;
                        case "guna2CheckBox13":
                            giaDichVu = 3000000; // Giá phòng họp sang trọng
                            break;
                        case "guna2CheckBox14":
                            giaDichVu = 1500000; // Giá Spa cao cấp
                            break;
                        case "guna2CheckBox2":
                            giaDichVu = 1500000; // Giá phục vụ 24h
                            break;
                        case "guna2CheckBox9":
                            giaDichVu = 1500000; // Giá minibar
                            break;
                    }

                    decimal thanhTien = giaDichVu * soLuong;

                    guna2DataGridView1.Rows.Add(tenDichVu, giaDichVu.ToString("N0") + " VND", soLuong, thanhTien.ToString("N0") + " VND");

                    checkbox.Checked = false;
                }
            }

            guna2TextBox4.Text = tongTien.ToString("N0") + " VND";
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            string danhSachHoaDon = "Dịch vụ đã thêm:\n";
            foreach (Control control in this.Controls)
            {
                if (control is CheckBox checkbox && checkbox.Checked)
                {
                    danhSachHoaDon += "- " + checkbox.Text + "\n";
                }
            }

            MessageBox.Show(danhSachHoaDon, "Hóa đơn");
        }

        private void guna2DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < guna2DataGridView1.Rows.Count)
            {
                DataGridViewRow row = guna2DataGridView1.Rows[e.RowIndex];

                guna2TextBox1.Text = row.Cells["MaPhong"].Value.ToString();
                guna2TextBox1.Text = row.Cells["soPhong"].Value.ToString();
                guna2TextBox3.Text = row.Cells["TenKH"].Value.ToString();
                guna2TextBox2.Text = row.Cells["MaKH"].Value.ToString();
                guna2ComboBox1.Text = row.Cells["LoaiPhong"].Value.ToString();

            }
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            
        }
    }
}
