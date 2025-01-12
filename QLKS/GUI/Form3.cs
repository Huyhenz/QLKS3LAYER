using System;
using System.Collections.Generic;
using System.Windows.Forms;
using BLL;
using DTO;

namespace GUI
{
    public partial class Form3 : Form
    {
        private TTvaHDbus dpBUS = new TTvaHDbus();

        public Form3()
        {
            InitializeComponent();
            LoadDanhSachDatPhong();
        }

        // Tải danh sách đặt phòng lên DataGridView
        private void LoadDanhSachDatPhong()
        {
            List<ThongTinDP> danhSachDP = dpBUS.GetDanhSachDatPhong();
            dataGridView1.DataSource = danhSachDP;
        }

        // Xử lý sự kiện tìm kiếm
        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            if (int.TryParse(txtFullname.Text, out int soPhong))
            {
                List<ThongTinDP> ketQuaTimKiem = dpBUS.TimKiemDatPhongTheoSoPhong(soPhong);
                dataGridView1.DataSource = ketQuaTimKiem;
            }
            else
            {
                MessageBox.Show("Vui lòng nhập số phòng hợp lệ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
