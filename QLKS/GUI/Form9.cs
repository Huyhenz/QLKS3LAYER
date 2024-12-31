using BLL;
using DevExpress.Internal.WinApi.Windows.UI.Notifications;
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
                // Xác định giá trị IDQUYEN từ ComboBox
                int idQuyen = comboCVNV.SelectedItem.ToString() == "User" ? 1 : 2;

                // Tạo đối tượng tài khoản mới và điền dữ liệu từ form
                TaiKhoanDTO newTaiKhoan = new TaiKhoanDTO
                {
                    FULLNAME = txtHoTenNV.Text,
                    NGAYSINH = dateEdit1.DateTime.ToString("yyyy-MM-dd"), // Chuyển đổi DateTime sang string
                    EMAIL = txtEMAILNV.Text,
                    SDT = Convert.ToInt64(txtSDTNV.Text),
                    CCCD = Convert.ToInt64(txtCCCDNV.Text),
                    DIACHI = txtDCNV.Text,
                    USERNAME = txtUSERNV.Text,
                    PASSWD = txtPASSNV.Text,
                    IDQUYEN = idQuyen
                };

                // Gọi phương thức BLL để thêm tài khoản
                taikhoanbus tkBus = new taikhoanbus();
                bool result = tkBus.AddTaiKhoan(newTaiKhoan);

                // Hiển thị thông báo dựa trên kết quả
                if (result)
                {
                    MessageBox.Show("Thêm tài khoản mới thành công!");
                    LoadTaiKhoanToGrid(); // Tải lại danh sách nhân viên sau khi thêm mới
                    ClearInputFields(); // Xóa nội dung các ô nhập liệu sau khi thêm tài khoản
                }
                else
                {
                    MessageBox.Show("Thêm tài khoản mới thất bại!");
                }
        }

        // Hàm ClearInputFields để xóa nội dung các ô nhập liệu
        private void ClearInputFields()
        {
            txtHoTenNV.Text = "";
            //timeNV.Value = DateTime.Now;
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

                    // Chuyển đổi từ string sang DateTime khi hiển thị lên DateEdit
                    DateTime ngaySinh;
                    if (DateTime.TryParse(row.Cells["NGAYSINH"].Value.ToString(), out ngaySinh))
                    {
                        dateEdit1.DateTime = ngaySinh;
                    }
                    else
                    {
                        dateEdit1.DateTime = DateTime.Now;
                    }

                    txtEMAILNV.Text = row.Cells["EMAIL"].Value.ToString();
                    txtSDTNV.Text = row.Cells["SDT"].Value.ToString();
                    txtCCCDNV.Text = row.Cells["CCCD"].Value.ToString();
                    txtDCNV.Text = row.Cells["DIACHI"].Value.ToString();
                    txtUSERNV.Text = row.Cells["USERNAME"].Value.ToString();
                    txtPASSNV.Text = row.Cells["PASSWD"].Value.ToString();

                    // Hiển thị giá trị IDQUYEN trong ComboBox
                    int idQuyen = Convert.ToInt32(row.Cells["IDQUYEN"].Value);
                    if (idQuyen == 1)
                    {
                        comboCVNV.SelectedItem = "User";
                    }
                    else if (idQuyen == 2)
                    {
                    comboCVNV.SelectedItem = "Manager";
                    }
                }
        }
        private void guna2Button8_Click(object sender, EventArgs e)
        {
                // Xác định giá trị IDQUYEN từ ComboBox
                int idQuyen = comboCVNV.SelectedItem.ToString() == "User" ? 1 : 2;

                TaiKhoanDTO taiKhoan = new TaiKhoanDTO
                {
                    UID = int.Parse(txtMANV.Text),
                    FULLNAME = txtHoTenNV.Text,
                    NGAYSINH = dateEdit1.DateTime.ToString("yyyy-MM-dd"), // Chuyển đổi DateTime sang string
                    EMAIL = txtEMAILNV.Text,
                    SDT = long.Parse(txtSDTNV.Text),
                    CCCD = long.Parse(txtCCCDNV.Text),
                    DIACHI = txtDCNV.Text,
                    USERNAME = txtUSERNV.Text,
                    PASSWD = txtPASSNV.Text,
                    IDQUYEN = idQuyen
                };

                taikhoanbus tkBus = new taikhoanbus();
                bool result = tkBus.UpdateTaiKhoan(taiKhoan);

                if (result)
                {
                    MessageBox.Show("Cập nhật thông tin tài khoản thành công!");
                    LoadTaiKhoanToGrid(); // Tải lại danh sách nhân viên sau khi cập nhật
                }
                else
                {
                    MessageBox.Show("Cập nhật thông tin tài khoản thất bại!");
                }
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
            comboCVNV.Items.Add("User");
            comboCVNV.Items.Add("Manager");
            comboCVNV.DropDownStyle = ComboBoxStyle.DropDownList;
            // Đảm bảo DataGridView được gán sự kiện
            dgvNhanVien.CellClick += dgvNhanVien_CellClick;
            dgvNhanVien.MultiSelect = true;
            dgvNhanVien.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            // Tải danh sách tài khoản khi form load
            LoadTaiKhoanToGrid();
        }
    }
}
    

