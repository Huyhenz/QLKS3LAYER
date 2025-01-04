using BLL;
using DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using TheArtOfDevHtmlRenderer.Adapters;

namespace GUI
{
    public partial class Form9 : Form
    {
        private string connectionString = "Data Source=HUYCATMOI;Initial Catalog=QLKS;Integrated Security=True;Encrypt=True;TrustServerCertificate=True";

        private taikhoanbus taikhoanbus = new taikhoanbus();
        public Form9()
        {
            InitializeComponent();
        }

        private void guna2Button7_Click(object sender, EventArgs e)
        {
            // Bạn có thể thêm code cho button này nếu cần thiết
        }

        private void ClearInputFields()
        {
            txtHoTenNV.Text = "";
            txtSDTNV.Text = "";
            txtCCCDNV.Text = "";
            txtDCNV.Text = "";
            txtUSERNV.Text = "";
            txtPASSNV.Text = "";
        }

        private void LoadTaiKhoanToGrid()
        {
            List<TaiKhoanDTO> taiKhoanList = taikhoanbus.GetAllTaiKhoan();
            dgvNhanVien.DataSource = taiKhoanList;

            // Ẩn cột PHOTO
            if (dgvNhanVien.Columns["PHOTO"] != null)
            {
                dgvNhanVien.Columns["PHOTO"].Visible = false;
            }
        }


        private void dgvNhanVien_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvNhanVien.Rows[e.RowIndex];
                int uid = Convert.ToInt32(row.Cells["UID"].Value);

                TaiKhoanDTO taiKhoan = taikhoanbus.GetTaiKhoanById(uid);
                if (taiKhoan != null)
                {
                    txtMANV.Text = taiKhoan.UID.ToString();
                    txtHoTenNV.Text = taiKhoan.FULLNAME;
                    txtUSERNV.Text = taiKhoan.USERNAME;
                    dateEdit1.DateTime = DateTime.TryParse(taiKhoan.NGAYSINH, out var ngaySinh) ? ngaySinh : DateTime.Now;
                    txtEMAILNV.Text = taiKhoan.EMAIL;
                    txtSDTNV.Text = taiKhoan.SDT.ToString();
                    txtCCCDNV.Text = taiKhoan.CCCD.ToString();
                    txtDCNV.Text = taiKhoan.DIACHI;
                    txtPASSNV.Text = taiKhoan.PASSWD;
                    comboCVNV.SelectedItem = taiKhoan.IDQUYEN == 1 ? "User" : "Manager";
                    guna2RadioButton1.Checked = taiKhoan.GIOITINH == "Nam";
                    guna2RadioButton2.Checked = taiKhoan.GIOITINH == "Nữ";
                    dateEdit2.DateTime = DateTime.TryParse(taiKhoan.NGAYVAOLAM, out var ngayVaoLam) ? ngayVaoLam : DateTime.Now;

                    if (!string.IsNullOrEmpty(taiKhoan.PHOTO) && System.IO.File.Exists(taiKhoan.PHOTO))
                    {
                        guna2PictureBox1.Image = Image.FromFile(taiKhoan.PHOTO);
                    }
                    else
                    {
                        guna2PictureBox1.Image = null;
                    }
                }
            }
        }


        private void guna2Button8_Click(object sender, EventArgs e)
        {
            // Bạn có thể thêm code cho button này nếu cần thiết
        }

        private void guna2Button8_Click_1(object sender, EventArgs e)
        {
            // Bạn có thể thêm code cho button này nếu cần thiết
        }

        private void Form9_Load(object sender, EventArgs e)
        {
            comboCVNV.Items.Add("User");
            comboCVNV.Items.Add("Manager");
            comboCVNV.DropDownStyle = ComboBoxStyle.DropDownList;
            dgvNhanVien.CellClick += dgvNhanVien_CellClick;
            dgvNhanVien.MultiSelect = true;
            dgvNhanVien.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            LoadTaiKhoanToGrid();
            dateEdit1.Properties.ReadOnly = true;
            dateEdit2.Properties.ReadOnly = true;

            if (dgvNhanVien.Columns["PHOTO"] != null)
            {
                dgvNhanVien.Columns["PHOTO"].Visible = false;
            }
        }

        private void guna2CirclePictureBox1_Click(object sender, EventArgs e)
        {
            // Bạn có thể thêm code cho sự kiện này nếu cần thiết
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            // Bạn có thể thêm code cho button này nếu cần thiết
        }

        private void dgvNhanVien_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Bạn có thể thêm code cho sự kiện này nếu cần thiết
        }

        private void toolStripButton9_Click(object sender, EventArgs e)
        {
            int idQuyen = comboCVNV.SelectedItem.ToString() == "User" ? 1 : 2;
            string gioiTinh = guna2RadioButton1.Checked ? "Nam" : "Nữ";

            TaiKhoanDTO newTaiKhoan = new TaiKhoanDTO
            {
                FULLNAME = txtHoTenNV.Text,
                NGAYSINH = dateEdit1.DateTime.ToString("yyyy-MM-dd"),
                EMAIL = txtEMAILNV.Text,
                SDT = Convert.ToInt64(txtSDTNV.Text),
                CCCD = Convert.ToInt64(txtCCCDNV.Text),
                DIACHI = txtDCNV.Text,
                USERNAME = txtUSERNV.Text,
                PASSWD = txtPASSNV.Text,
                IDQUYEN = idQuyen,
                GIOITINH = gioiTinh,
                NGAYVAOLAM = dateEdit2.DateTime.ToString("yyyy-MM-dd")
            };

            bool result = taikhoanbus.AddTaiKhoan(newTaiKhoan);

            if (result)
            {
                MessageBox.Show("Thêm tài khoản mới thành công!");
                LoadTaiKhoanToGrid();
                ClearInputFields();
            }
            else
            {
                MessageBox.Show("Thêm tài khoản mới thất bại!");
            }
        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Bạn có chắc chắn muốn xóa các nhân viên đã chọn không?", "Xác nhận xóa", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                List<int> maNhanVienList = new List<int>();
                foreach (DataGridViewRow row in dgvNhanVien.SelectedRows)
                {
                    int maNhanVien = int.Parse(row.Cells["UID"].Value.ToString());
                    maNhanVienList.Add(maNhanVien);
                }

                bool result = true;
                foreach (int maNhanVien in maNhanVienList)
                {
                    result = result && taikhoanbus.DeleteTaiKhoan(maNhanVien);
                }

                if (result)
                {
                    MessageBox.Show("Xóa các nhân viên thành công!");
                    LoadTaiKhoanToGrid();
                }
                else
                {
                    MessageBox.Show("Xóa một hoặc nhiều nhân viên thất bại!");
                }
            }
        }

        private void toolStripButton10_Click(object sender, EventArgs e)
        {
            int idQuyen = comboCVNV.SelectedItem.ToString() == "User" ? 1 : 2;
            string gioiTinh = guna2RadioButton1.Checked ? "Nam" : "Nữ";

            TaiKhoanDTO taiKhoan = new TaiKhoanDTO
            {
                UID = int.Parse(txtMANV.Text),
                FULLNAME = txtHoTenNV.Text,
                NGAYSINH = dateEdit1.DateTime.ToString("yyyy-MM-dd"),
                EMAIL = txtEMAILNV.Text,
                SDT = long.Parse(txtSDTNV.Text),
                CCCD = long.Parse(txtCCCDNV.Text),
                DIACHI = txtDCNV.Text,
                USERNAME = txtUSERNV.Text,
                PASSWD = txtPASSNV.Text,
                IDQUYEN = idQuyen,
                GIOITINH = gioiTinh,
                NGAYVAOLAM = dateEdit2.DateTime.ToString("yyyy-MM-dd")
            };

            bool result = taikhoanbus.UpdateTaiKhoan(taiKhoan);

            if (result)
            {
                MessageBox.Show("Cập nhật thông tin tài khoản thành công!");
                LoadTaiKhoanToGrid();
            }
            else
            {
                MessageBox.Show("Cập nhật thông tin tài khoản thất bại!");
            }
        }

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            // Bạn có thể thêm code cho sự kiện này nếu cần thiết
        }

        private void guna2Button5_Click(object sender, EventArgs e)
        {
            if (dgvNhanVien.SelectedRows.Count > 0)
            {
                int selectedEmployeeID = Convert.ToInt32(dgvNhanVien.SelectedRows[0].Cells["UID"].Value);

                // Sử dụng OpenFileDialog để chọn hình ảnh mới
                OpenFileDialog openFileDialog = new OpenFileDialog
                {
                    Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.bmp",
                    Title = "Chọn hình ảnh nhân viên"
                };

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string newImagePath = openFileDialog.FileName;

                    // Cập nhật đường dẫn hình ảnh trong cơ sở dữ liệu
                    UpdateEmployeeImagePath(selectedEmployeeID, newImagePath);

                    // Hiển thị hình ảnh mới
                    guna2PictureBox1.Image = Image.FromFile(newImagePath);
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một nhân viên từ danh sách.");
            }
        }

        // Phương thức cập nhật đường dẫn hình ảnh trong cơ sở dữ liệu
        private void UpdateEmployeeImagePath(int employeeID, string imagePath)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "UPDATE tb_User SET PHOTO = @PHOTO WHERE UID = @UID";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@PHOTO", imagePath);
                cmd.Parameters.AddWithValue("@UID", employeeID);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        private void guna2Button10_Click(object sender, EventArgs e)
        {
                string searchCCCD = txt_TIMCCCD.Text.Trim();
                bool found = false;

                foreach (DataGridViewRow row in dgvNhanVien.Rows)
                {
                    if (row.Cells["CCCD"].Value != null && row.Cells["CCCD"].Value.ToString().Equals(searchCCCD))
                    {
                        // Hiển thị thông tin nhân viên
                        txtMANV.Text = row.Cells["UID"].Value.ToString();
                        txtHoTenNV.Text = row.Cells["FULLNAME"].Value.ToString();
                        dateEdit1.EditValue = Convert.ToDateTime(row.Cells["NGAYSINH"].Value);
                        comboCVNV.SelectedItem = row.Cells["IDQUYEN"].Value.ToString();
                        if (row.Cells["GIOITINH"].Value.ToString() == "Nam")
                        {
                            guna2RadioButton1.Checked = true;
                        }
                        else
                        {
                        guna2RadioButton2.Checked = true;
                        }
                        dateEdit2.EditValue = Convert.ToDateTime(row.Cells["NGAYVAOLAM"].Value);
                        txtDCNV.Text = row.Cells["DIACHI"].Value.ToString();
                        txtSDTNV.Text = row.Cells["SDT"].Value.ToString();
                        txtEMAILNV.Text = row.Cells["EMAIL"].Value.ToString();
                        txtUSERNV.Text = row.Cells["USERNAME"].Value.ToString();
                        txtPASSNV.Text = row.Cells["PASSWD"].Value.ToString();
                        // Giả sử ảnh được lưu dưới dạng đường dẫn tệp
                        guna2PictureBox1.Image = Image.FromFile(row.Cells["PHOTO"].Value.ToString());

                        found = true;
                        break;
                    }
                }

                if (!found)
                {
                    MessageBox.Show("Không tìm thấy nhân viên", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
        }

        private void txt_TIMCCCD_TextChanged(object sender, EventArgs e)
        {

        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }



        // Phương thức cập nhật đường dẫn hình ảnh trong cơ sở dữ liệu


        // Ví dụ về phương thức lấy đường dẫn hình ảnh dựa trên ID nhân viê
    }
}

