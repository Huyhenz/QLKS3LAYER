using BLL;
using DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TheArtOfDevHtmlRenderer.Adapters;

namespace GUI
{
    public partial class Form9 : Form
    {
        private taikhoanbus taikhoanbus = new taikhoanbus();
        public Form9()
        {
            InitializeComponent();

        }

        private void guna2Button7_Click(object sender, EventArgs e)
        {
            TaiKhoanDTO newTaiKhoan = new TaiKhoanDTO
            {
                FULLNAME = txtHoTenNV.Text,
                NGAYSINH = timeNV.Value,
                EMAIL = txtEMAILNV.Text,
                SDT = Convert.ToInt64(txtSDTNV.Text),
                CCCD = Convert.ToInt64(txtCCCDNV.Text),
                DIACHI = txtDCNV.Text,
                //USERNAME = txtHoTenNV.Text,
                //PASSWD = txtPassword.Text,
                //IDQUYEN = Convert.ToInt32(txtIDQuyen.Text)
            };

            taikhoanbus.AddTaiKhoan(newTaiKhoan);
            // Tải lại danh sách tài khoản sau khi thêm mới
            ClearInputFields(); // Xóa nội dung các ô nhập liệu sau khi thêm tài khoản
        }

        // Hàm ClearInputFields để xóa nội dung các ô nhập liệu
        private void ClearInputFields()
        {
            txtHoTenNV.Text = "";
            timeNV.Value = DateTime.Now;
            txtHoTenNV.Text = "";
            txtSDTNV.Text = "";
            txtCCCDNV.Text = "";
            txtDCNV.Text = "";
            //txtUsername.Text = "";
            //txtPassword.Text = "";
            //txtIDQuyen.Text = "";
        }


        private void LoadTaiKhoanToGrid()
        {
            taikhoanbus tkBus = new taikhoanbus();
            List<TaiKhoanDTO> taiKhoanList = tkBus.GetAllTaiKhoan();
            dgvNhanVien.DataSource = taiKhoanList;
        }




        private void dgvNhanVien_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvNhanVien.Rows[e.RowIndex];

                txtMANV.Text = row.Cells["UID"].Value.ToString();
                txtHoTenNV.Text = row.Cells["FULLNAME"].Value.ToString();
                //timeNV.Value = Convert.ToDateTime(row.Cells["NGAYSINH"].Value);
                txtEMAILNV.Text = row.Cells["EMAIL"].Value.ToString();
                txtSDTNV.Text = row.Cells["SDT"].Value.ToString();
                txtCCCDNV.Text = row.Cells["CCCD"].Value.ToString();
                txtDCNV.Text = row.Cells["DIACHI"].Value.ToString();
                //txtuse.Text = row.Cells["USERNAME"].Value.ToString();
                //txtMatKhau.Text = row.Cells["PASSWD"].Value.ToString();
                // Add more fields as necessary
            }
        }



        private void guna2Button8_Click(object sender, EventArgs e)
        {
            // Tạo đối tượng tài khoản mới và điền dữ liệu từ form
            TaiKhoanDTO taiKhoan = new TaiKhoanDTO();
            taiKhoan.UID = int.Parse(txtMANV.Text);
            taiKhoan.FULLNAME = txtHoTenNV.Text;
            //taiKhoan.NGAYSINH = timeNV.Value;
            taiKhoan.EMAIL = txtEMAILNV.Text;
            taiKhoan.SDT = long.Parse(txtSDTNV.Text);
            taiKhoan.CCCD = long.Parse(txtCCCDNV.Text);
            taiKhoan.DIACHI = txtDCNV.Text;
            //taiKhoan.USERNAME = txt.Text;
            //taiKhoan.PASSWD = txtMatKhau.Text;
            // Add more fields as necessary

            // Gọi phương thức BLL để cập nhật thông tin tài khoản
            taikhoanbus tkBus = new taikhoanbus();
            bool result = tkBus.UpdateTaiKhoan(taiKhoan);

            // Hiển thị thông báo dựa trên kết quả
            if (result)
            {
                MessageBox.Show("Cập nhật thông tin tài khoản thành công!");
            }
            else
            {
                MessageBox.Show("Cập nhật thông tin tài khoản thất bại!");
            }

            // Tải lại danh sách tài khoản sau khi cập nhật
            LoadTaiKhoanToGrid();
        }




        private void guna2Button8_Click_1(object sender, EventArgs e)
        {
                // Xác nhận người dùng muốn xóa các nhân viên đã chọn
                DialogResult dialogResult = MessageBox.Show("Bạn có chắc chắn muốn xóa các nhân viên đã chọn không?", "Xác nhận xóa", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    List<int> maNhanVienList = new List<int>();

                    // Lấy danh sách các ID nhân viên được chọn trong DataGridView
                    foreach (DataGridViewRow row in dgvNhanVien.SelectedRows)
                    {
                        int maNhanVien = int.Parse(row.Cells["UID"].Value.ToString());
                        maNhanVienList.Add(maNhanVien);
                    }

                    // Gọi phương thức BLL để xóa thông tin nhân viên
                    taikhoanbus tkBus = new taikhoanbus();
                    bool result = true;
                    foreach (int maNhanVien in maNhanVienList)
                    {
                        result = result && tkBus.DeleteTaiKhoan(maNhanVien);
                    }

                    // Hiển thị thông báo dựa trên kết quả
                    if (result)
                    {
                        MessageBox.Show("Xóa các nhân viên thành công!");
                    // Tải lại danh sách nhân viên sau khi xóa
                    LoadTaiKhoanToGrid();
                    }
                    else
                    {
                        MessageBox.Show("Xóa một hoặc nhiều nhân viên thất bại!");
                    }
                }
            }



        

        private void Form9_Load(object sender, EventArgs e)
        {
            // Đảm bảo DataGridView được gán sự kiện
            dgvNhanVien.CellClick += dgvNhanVien_CellClick;
            dgvNhanVien.MultiSelect = true;
            dgvNhanVien.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            // Tải danh sách tài khoản khi form load
            LoadTaiKhoanToGrid();
        }
    }
}
    

