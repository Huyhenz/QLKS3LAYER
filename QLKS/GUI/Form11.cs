using BLL;
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
        private DichVuBus dichvubus;

        public Form11()
        {
            InitializeComponent();
            dichvubus = new DichVuBus();
            LoadPhongData();

        }
        private void LoadPhongData()
        {
            try
            {
                DataTable dt = dichvubus.GetAllPhong();
                dataGridView2.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Đã xảy ra lỗi khi tải dữ liệu phòng: {ex.Message}");
            }


        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            try
            {
                int maPhong;
                if (int.TryParse(guna2TextBox6.Text, out maPhong))
                {
                    DataTable dt = dichvubus.GetPhongById(maPhong);
                    if (dt.Rows.Count > 0)
                    {
                        dataGridView2.DataSource = dt;
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy phòng với mã này hoặc phòng không có trạng thái 'Nhận phòng'.");
                    }
                }
                else
                {
                    MessageBox.Show("Vui lòng nhập mã phòng hợp lệ.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Đã xảy ra lỗi: {ex.Message}");
            }
        }

        private void Form11_Load(object sender, EventArgs e)
        {
            LoadPhongData();

        }

        private void guna2TextBox6_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(guna2TextBox6.Text))
            {
                LoadPhongData();
            }

        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            int tongTien = 0; foreach (DataGridViewRow row in dataGridView1.Rows) { 
                if (row.Cells["Số Lượng"].Value != DBNull.Value && Convert.ToInt32(row.Cells["Số Lượng"].Value) > 0) 
                {
                    int soLuong = Convert.ToInt32(row.Cells["Số Lượng"].Value); 
                    int giaDichVu = Convert.ToInt32(row.Cells["GIADV"].Value); 
                    tongTien += soLuong * giaDichVu; } 
            }
            guna2TextBox4.Text = tongTien.ToString();
        }

        private void guna2CheckBox4_CheckedChanged(object sender, EventArgs e)
        {
            if (guna2CheckBox4.Checked)
            {
                guna2TextBox8.Enabled = true;
                guna2TextBox8.Text = "1";
            }
            else
            {
                guna2TextBox8.Enabled = false;
                guna2TextBox8.Text = "0";
            }
        }

        private void guna2CheckBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (guna2CheckBox2.Checked)
            {
                guna2TextBox8.Enabled = true;
                guna2TextBox8.Text = "1";
            }
            else
            {
                guna2TextBox8.Enabled = false;
                guna2TextBox8.Text = "0";
            }
        }

        private void guna2CheckBox6_CheckedChanged(object sender, EventArgs e)
        {
            if (guna2CheckBox6.Checked)
            {
                guna2TextBox8.Enabled = true;
                guna2TextBox8.Text = "1";
            }
            else
            {
                guna2TextBox8.Enabled = false;
                guna2TextBox8.Text = "0";
            }

       }

        private void guna2CheckBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (guna2CheckBox3.Checked)
            {
                guna2TextBox8.Enabled = true;
                guna2TextBox8.Text = "1";
            }
            else
            {
                guna2TextBox8.Enabled = false;
                guna2TextBox8.Text = "0";
            }
        }

        private void guna2CheckBox7_CheckedChanged(object sender, EventArgs e)
        {
            if (guna2CheckBox7.Checked)
            {
                guna2TextBox8.Enabled = true;
                guna2TextBox8.Text = "1";
            }
            else
            {
                guna2TextBox8.Enabled = false;
                guna2TextBox8.Text = "0";
            }
        }

        private void guna2CheckBox8_CheckedChanged(object sender, EventArgs e)
        {
            if (guna2CheckBox8.Checked)
            {
                guna2TextBox8.Enabled = true;
                guna2TextBox8.Text = "1";
            }
            else
            {
                guna2TextBox8.Enabled = false;
                guna2TextBox8.Text = "0";
            }
        }

        private void guna2CheckBox5_CheckedChanged(object sender, EventArgs e)
        {
            if (guna2CheckBox5.Checked)
            {
                guna2TextBox8.Enabled = true;
                guna2TextBox8.Text = "1";
            }
            else
            {
                guna2TextBox8.Enabled = false;
                guna2TextBox8.Text = "0";
            }
        }

        private void guna2CheckBox10_CheckedChanged(object sender, EventArgs e)
        {
            if (guna2CheckBox10.Checked)
            {
                guna2TextBox8.Enabled = true;
                guna2TextBox8.Text = "1";
            }
            else
            {
                guna2TextBox8.Enabled = false;
                guna2TextBox8.Text = "0";
            }
        }

        private void guna2CheckBox11_CheckedChanged(object sender, EventArgs e)
        {
            if (guna2CheckBox11.Checked)
            {
                guna2TextBox8.Enabled = true;
                guna2TextBox8.Text = "1";
            }
            else
            {
                guna2TextBox8.Enabled = false;
                guna2TextBox8.Text = "0";
            }
        }

        private void guna2CheckBox12_CheckedChanged(object sender, EventArgs e)
        {
            if (guna2CheckBox12.Checked)
            {
                guna2TextBox8.Enabled = true;
                guna2TextBox8.Text = "1";
            }
            else
            {
                guna2TextBox8.Enabled = false;
                guna2TextBox8.Text = "0";
            }
        }

        private void guna2CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (guna2CheckBox1.Checked)
            {
                guna2TextBox8.Enabled = true;
                guna2TextBox8.Text = "1";
            }
            else
            {
                guna2TextBox8.Enabled = false;
                guna2TextBox8.Text = "0";
            }
        }

        private void guna2CheckBox9_CheckedChanged(object sender, EventArgs e)
        {
            if (guna2CheckBox9.Checked)
            {
                guna2TextBox8.Enabled = true;
                guna2TextBox8.Text = "1";
            }
            else
            {
                guna2TextBox8.Enabled = false;
                guna2TextBox8.Text = "0";
            }
        }
    }
}
