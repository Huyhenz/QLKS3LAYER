using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using BLL;
using DTO;

namespace GUI
{
    public partial class Form3 : Form
    {
        private TTvaHDbus dpBUS = new TTvaHDbus();
        private TTvaHDbus dichVuBUS = new TTvaHDbus();

        public Form3()
        {
            InitializeComponent();
            LoadDanhSachDatPhong();
            LoadDichVuData();
        }

        // Tải danh sách đặt phòng lên DataGridView
        private void LoadDanhSachDatPhong()
        {
            List<ThongTinDP> danhSachDP = dpBUS.GetDanhSachDatPhong();
            dataGridView2.DataSource = danhSachDP;
        }

        // Xử lý sự kiện tìm kiếm
        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            if (int.TryParse(txtFullname.Text, out int soPhong))
            {
                List<ThongTinDP> ketQuaTimKiem = dpBUS.TimKiemDatPhongTheoSoPhong(soPhong);
                dataGridView2.DataSource = ketQuaTimKiem;
            }
            else
            {
                MessageBox.Show("Vui lòng nhập số phòng hợp lệ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void LoadDichVuData()
        {
            try
            {
                List<CTDVDTO> dichVuList = dichVuBUS.GetAllDichVu();
                dataGridView1.DataSource = dichVuList;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu dịch vụ: " + ex.Message);
            }
        }
        private void Form3_Load(object sender, EventArgs e)
        {
            dataGridView2.Columns[0].DefaultCellStyle.ForeColor = Color.Red;
            dataGridView2.Columns[1].DefaultCellStyle.ForeColor = Color.Red;
            dataGridView2.Columns[2].DefaultCellStyle.ForeColor = Color.Red;
            dataGridView2.Columns[3].DefaultCellStyle.ForeColor = Color.Red;
            dataGridView2.Columns[4].DefaultCellStyle.ForeColor = Color.Red;
            dataGridView2.Columns[5].DefaultCellStyle.ForeColor = Color.Red;

            dataGridView1.Columns[1].Visible = false;
            dataGridView1.Columns[5].Visible = false;
            //dataGridView1.Columns[5].Visible = false;
            dataGridView1.Columns[6].Visible = false;
            dataGridView1.Columns[0].DefaultCellStyle.ForeColor = Color.Red;
            dataGridView1.Columns[1].DefaultCellStyle.ForeColor = Color.Red;
            dataGridView1.Columns[2].DefaultCellStyle.ForeColor = Color.Red;
            dataGridView1.Columns[3].DefaultCellStyle.ForeColor = Color.Red;
            dataGridView1.Columns[4].DefaultCellStyle.ForeColor = Color.Red;
            dataGridView1.Columns[5].DefaultCellStyle.ForeColor = Color.Red;
        }
    }
}
