using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using BLL;
using DTO;
using System.Linq;
using System.Data.SqlClient;
using System.Security.Principal;

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
            if (Session.Login != null)
            {
                txtAC.Text = Session.Login.FULLNAME;
                //txtFullName.ReadOnly = true; // Đặt TextBox thành không thể chỉnh sửa
            }
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

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            int roomId;
            if (int.TryParse(txtFullname.Text, out roomId))
            {
                List<ThongTinDP> datPhongList = dpBUS.GetDanhSachDatPhong().Where(dp => dp.IDPHONG == roomId).ToList();
                List<CTDVDTO> dichVuList = dichVuBUS.GetAllDichVu().Where(dv => dv.IDPHONG == roomId).ToList();
                dataGridView2.DataSource = datPhongList;
                dataGridView1.DataSource = dichVuList;

                if (datPhongList.Count == 0)
                {
                    MessageBox.Show("Không tìm thấy phòng với ID: " + roomId);
                }
            }
            //else if(int.TryParse(txtIDPHONG.Text, out roomId))
            //{
            //    List<CTDVDTO> dichVuList = dichVuBUS.GetAllDichVu().Where(dv => dv.IDPHONG == roomId).ToList();
            //    dataGridView1.DataSource = dichVuList;
            //    if (dichVuList.Count == 0)
            //    {
            //        MessageBox.Show("Không tìm thấy phòng với ID: " + roomId);
            //    }
            //}
            else
            {
                MessageBox.Show("Vui lòng nhập ID phòng hợp lệ.");
            }
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Kiểm tra xem dòng được chọn có hợp lệ không
            if (e.RowIndex >= 0)
            {
                // Lấy dòng được chọn
                DataGridViewRow selectedRow = dataGridView2.Rows[e.RowIndex];

                // Hiển thị dữ liệu từ các ô trong dòng lên các TextBox
                txtFullname.Text = selectedRow.Cells["IDPHONG"].Value.ToString();
                guna2TextBox2.Text = selectedRow.Cells["IDKH"].Value.ToString();
                guna2TextBox5.Text = selectedRow.Cells["NGAYDAT"].Value.ToString();
                guna2TextBox6.Text = selectedRow.Cells["NGAYTRA"].Value.ToString();
                guna2TextBox3.Text = selectedRow.Cells["SONGAYO"].Value.ToString();
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                // Lấy dòng được chọn
                DataGridViewRow selectedRow = dataGridView1.Rows[e.RowIndex];

                // Hiển thị dữ liệu từ các ô trong dòng lên các TextBox
                txtIDPHONG.Text = selectedRow.Cells["IDPHONG"].Value.ToString();
                txtSDV.Text = selectedRow.Cells["TONGSODVDASUDUNG"].Value.ToString();
                txtTongTIen.Text = selectedRow.Cells["TONGSOTIENDV"].Value.ToString();
            }
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                DataGridViewRow row = dataGridView1.SelectedRows[0];
                txtIDPHONG.Text = row.Cells["IDPHONG"].Value.ToString();
                txtSDV.Text = row.Cells["TONGSODVDASUDUNG"].Value.ToString();
                txtTongTIen.Text = row.Cells["TONGSOTIENDV"].Value.ToString();
            }
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            int roomId;
            if (int.TryParse(txtFullname.Text, out roomId))
            {
                try
                {
                    // Thiết lập chuỗi kết nối và khởi tạo SqlConnection
                    string connectionString = "Data Source=HUYCATMOI;Initial Catalog=QLKS;Integrated Security=True;Encrypt=True;TrustServerCertificate=True;";
                    using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                    {
                        // Mở kết nối
                        sqlConnection.Open();

                        // Xóa các bản ghi trong bảng tb_CTDV
                        string queryCTDV = "DELETE FROM tb_CTDV WHERE IDPHONG = @IDPHONG";
                        using (SqlCommand cmd = new SqlCommand(queryCTDV, sqlConnection))
                        {
                            cmd.Parameters.AddWithValue("@IDPHONG", roomId);
                            cmd.ExecuteNonQuery();
                        }

                        // Xóa các bản ghi trong bảng tb_DatPhong và lưu lại IDKH
                        int idKhachHang;
                        string queryDatPhong = "DELETE FROM tb_DatPhong OUTPUT DELETED.IDKH WHERE IDPHONG = @IDPHONG";
                        using (SqlCommand cmd = new SqlCommand(queryDatPhong, sqlConnection))
                        {
                            cmd.Parameters.AddWithValue("@IDPHONG", roomId);
                            idKhachHang = (int)cmd.ExecuteScalar();
                        }

                        // Cập nhật cột TINHTRANG trong bảng tb_Phong
                        string queryUpdatePhong = "UPDATE tb_Phong SET TINHTRANG = N'Trống' WHERE IDPHONG = @IDPHONG";
                        using (SqlCommand cmd = new SqlCommand(queryUpdatePhong, sqlConnection))
                        {
                            cmd.Parameters.AddWithValue("@IDPHONG", roomId);
                            cmd.ExecuteNonQuery();
                        }

                        // Xóa các bản ghi trong bảng tb_KhachHang nếu không còn phòng nào khác của khách hàng này
                        string queryCheckKhachHang = "SELECT COUNT(*) FROM tb_DatPhong WHERE IDKH = @IDKH";
                        using (SqlCommand cmd = new SqlCommand(queryCheckKhachHang, sqlConnection))
                        {
                            cmd.Parameters.AddWithValue("@IDKH", idKhachHang);
                            int count = (int)cmd.ExecuteScalar();
                            if (count == 0)
                            {
                                string queryKhachHang = "DELETE FROM tb_KhachHang WHERE IDKH = @IDKH";
                                using (SqlCommand cmdDeleteKH = new SqlCommand(queryKhachHang, sqlConnection))
                                {
                                    cmdDeleteKH.Parameters.AddWithValue("@IDKH", idKhachHang);
                                    cmdDeleteKH.ExecuteNonQuery();
                                }
                            }
                        }

                        // Đóng kết nối
                        sqlConnection.Close();
                    }
                    Form1 form1 = (Form1)Application.OpenForms["Form1"];
                    if (form1 != null)
                    {
                        form1 = new Form1();
                        form1.Show();
                    }
                    else
                    {
                        form1.BringToFront();
                        form1.UpdateButtonColor(roomId);
                    }
                    this.Close();

                    MessageBox.Show("Thanh toán thành công và dữ liệu đã bị xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Cập nhật lại dữ liệu trong DataGridView
                    LoadDanhSachDatPhong();
                    LoadDichVuData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi trong quá trình thanh toán: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn phòng hợp lệ để thanh toán.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
    
        
