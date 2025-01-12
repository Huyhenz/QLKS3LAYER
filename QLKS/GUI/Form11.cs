using BLL;
using DTO;
using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUI
{
    public partial class Form11 : Form
    {
        private DichVuBus dichvubus;
        private int selectedRoomID;

        public Form11()
        {
            InitializeComponent();
            dichvubus = new DichVuBus();
            LoadPhongData();
            serviceDictionary = dichvubus.GetAllServices().ToDictionary(s => s.IDDV, s => s.GIADV);

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
            if (Session.Login != null)
            {
                txtAC.Text = Session.Login.FULLNAME;
                //txtFullName.ReadOnly = true; // Đặt TextBox thành không thể chỉnh sửa
            }
        }

        private void dataGridView2_SelectionChanged(object sender, EventArgs e)
        {

            //if (dataGridView2.SelectedRows.Count > 0)
            //{
            //    this.selectedRoomID = (int)dataGridView2.SelectedRows[0].Cells["IDPHONG"].Value;

            //    // Thêm một hàng mới vào dataGridView1 nếu chưa tồn tại
            //    if (!IsRoomIdAlreadyInGrid(selectedRoomID))
            //    {
            //        AddNewRoomRowToGrid(selectedRoomID);
            //    }

            //    // Reset trạng thái tất cả checkbox
            //    ResetAllCheckboxes();
            //    UpdateDataGridView1();
            //}
            if (dataGridView2.SelectedRows.Count > 0)
            {
                // Lấy IDPHONG từ hàng được chọn trong dataGridView2
                selectedRoomID = (int)dataGridView2.SelectedRows[0].Cells["IDPHONG"].Value;
                ResetAllCheckboxes();

                // Hiển thị thông tin trong dataGridView1
                DisplayRoomInGrid1(selectedRoomID);
            }
        }

        private void DisplayRoomInGrid1(int roomId)
        {
            // Tạo mới DataTable nếu dataGridView1 chưa có DataSource
            DataTable table = dataGridView1.DataSource as DataTable;
            if (table == null)
            {
                table = new DataTable();
                table.Columns.Add("IDPHONG", typeof(int));
                table.Columns.Add("TONGSODVDASUDUNG", typeof(int));
                table.Columns.Add("TONGSOTIENDV", typeof(int));
                dataGridView1.DataSource = table;
            }

            // Xóa tất cả các hàng hiện tại
            table.Rows.Clear();

            // Thêm hàng mới với dữ liệu của roomId
            DataRow row = table.NewRow();
            row["IDPHONG"] = roomId;
            row["TONGSODVDASUDUNG"] = 0; // Giá trị mặc định hoặc có thể thay đổi theo logic của bạn
            row["TONGSOTIENDV"] = 0; // Giá trị mặc định hoặc có thể thay đổi theo logic của bạn
            table.Rows.Add(row);
        }


        private bool IsRoomIdAlreadyInGrid(int roomId)
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Cells["IDPHONG"].Value != null && (int)row.Cells["IDPHONG"].Value == roomId)
                {
                    return true;
                }
            }
            return false;
        }

        private void AddNewRoomRowToGrid(int roomId)
        {
            DataTable table = dataGridView1.DataSource as DataTable;

            // Nếu dataGridView2 chưa có DataSource, tạo mới DataTable
            if (table == null)
            {
                table = new DataTable();
                table.Columns.Add("IDPHONG", typeof(int));
                table.Columns.Add("TONGSODVDASUDUNG", typeof(int));
                table.Columns.Add("TONGSOTIENDV", typeof(int));
                dataGridView1.DataSource = table;
            }
            else
            {
                // Kiểm tra và thêm cột nếu chưa tồn tại
                if (!table.Columns.Contains("IDPHONG"))
                {
                    table.Columns.Add("IDPHONG", typeof(int));
                }
                if (!table.Columns.Contains("TONGSODVDASUDUNG"))
                {
                    table.Columns.Add("TONGSODVDASUDUNG", typeof(int));
                }
                if (!table.Columns.Contains("TONGSOTIENDV"))
                {
                    table.Columns.Add("TONGSOTIENDV", typeof(int));
                }
            }

            // Thêm hàng mới
            table.Rows.Add(roomId, 0, 0);
        }


        private void ResetAllCheckboxes()
        {
            for (int i = 1; i <= 12; i++)
            {
                var checkBox = this.Controls.Find($"guna2CheckBox{i}", true).FirstOrDefault() as Guna2CheckBox;
                if (checkBox != null)
                {
                    checkBox.Checked = false;
                }
            }
        }

        private void UpdateDataGridView1()
        {
            //DataTable table = dataGridView1.DataSource as DataTable;

            //// Nếu dataGridView2 chưa có DataSource, tạo mới DataTable
            //if (table == null)
            //{
            //    table = new DataTable();
            //    table.Columns.Add("IDPHONG", typeof(int));
            //    table.Columns.Add("TONGSODVDASUDUNG", typeof(int));
            //    table.Columns.Add("TONGSOTIENDV", typeof(int));
            //    dataGridView1.DataSource = table;
            //}
            //else
            //{
            //    // Kiểm tra và thêm cột nếu chưa tồn tại
            //    if (!table.Columns.Contains("IDPHONG"))
            //    {
            //        table.Columns.Add("IDPHONG", typeof(int));
            //    }
            //    if (!table.Columns.Contains("TONGSODVDASUDUNG"))
            //    {
            //        table.Columns.Add("TONGSODVDASUDUNG", typeof(int));
            //    }
            //    if (!table.Columns.Contains("TONGSOTIENDV"))
            //    {
            //        table.Columns.Add("TONGSOTIENDV", typeof(int));
            //    }
            //}

            //// Tìm hàng tương ứng với selectedRoomID
            //DataRow row = table.Rows.Cast<DataRow>().FirstOrDefault(r => (int)r["IDPHONG"] == selectedRoomID);

            //if (row != null)
            //{
            //    // Cập nhật hàng hiện có
            //    row["TONGSODVDASUDUNG"] = GetSelectedServicesCount();
            //    row["TONGSOTIENDV"] = GetTotalCostOfSelectedServices();
            //}
            DataTable table = dataGridView1.DataSource as DataTable;

            if (table != null && table.Rows.Count > 0)
            {
                DataRow row = table.Rows[0]; // Luôn chỉ có 1 hàng
                row["TONGSODVDASUDUNG"] = GetSelectedServicesCount();
                row["TONGSOTIENDV"] = GetTotalCostOfSelectedServices();
            }
        }

        private void guna2TextBox6_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(guna2TextBox6.Text))
            {
                LoadPhongData();
            }
        }

        private void guna2Button3_Click_1(object sender, EventArgs e)
        {
            //int tongTien = GetTotalCostOfSelectedServices();
            //guna2TextBox4.Text = tongTien.ToString();
        }

        private int GetSelectedServicesCount()
        {
            int count = 0;
            if (guna2CheckBox1.Checked) count++;
            if (guna2CheckBox2.Checked) count++;
            if (guna2CheckBox3.Checked) count++;
            if (guna2CheckBox4.Checked) count++;
            if (guna2CheckBox5.Checked) count++;
            if (guna2CheckBox6.Checked) count++;
            if (guna2CheckBox7.Checked) count++;
            if (guna2CheckBox8.Checked) count++;
            if (guna2CheckBox9.Checked) count++;
            if (guna2CheckBox10.Checked) count++;
            if (guna2CheckBox11.Checked) count++;
            if (guna2CheckBox12.Checked) count++;
            return count;
        }
        private Dictionary<int, int> serviceDictionary;
        private int GetTotalCostOfSelectedServices()
        {
            int totalCost = 0;

            for (int i = 1; i <= 12; i++)
            {
                var checkBox = this.Controls.Find($"guna2CheckBox{i}", true).FirstOrDefault() as Guna2CheckBox;
                if (checkBox != null && checkBox.Checked && serviceDictionary.ContainsKey(i))
                {
                    totalCost += serviceDictionary[i];
                }
            }

            return totalCost;
        }

        private void guna2CheckBox4_CheckedChanged(object sender, EventArgs e)
        {
            UpdateDataGridView1();
        }

        private void guna2CheckBox2_CheckedChanged(object sender, EventArgs e)
        {
            UpdateDataGridView1();
        }

        private void guna2CheckBox6_CheckedChanged(object sender, EventArgs e)
        {
            UpdateDataGridView1();
        }

        private void guna2CheckBox3_CheckedChanged(object sender, EventArgs e)
        {
            UpdateDataGridView1();
        }

        private void guna2CheckBox7_CheckedChanged(object sender, EventArgs e)
        {
            UpdateDataGridView1();
        }

        private void guna2CheckBox8_CheckedChanged(object sender, EventArgs e)
        {
            UpdateDataGridView1();
        }

        private void guna2CheckBox5_CheckedChanged(object sender, EventArgs e)
        {
            UpdateDataGridView1();
        }

        private void guna2CheckBox10_CheckedChanged(object sender, EventArgs e)
        {
            UpdateDataGridView1();
        }

        private void guna2CheckBox11_CheckedChanged(object sender, EventArgs e)
        {
            UpdateDataGridView1();
        }

        private void guna2CheckBox12_CheckedChanged(object sender, EventArgs e)
        {
            UpdateDataGridView1();
        }

        private void guna2CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            UpdateDataGridView1();
        }

        private void guna2CheckBox9_CheckedChanged(object sender, EventArgs e)
        {
            UpdateDataGridView1();
        }

        private void guna2Button5_Click(object sender, EventArgs e)
        {
            SaveDataGridView1ToDatabase();
        }

        private void SaveDataGridView1ToDatabase()
        {
            try
            {
                // Kiểm tra DataSource của dataGridView1
                DataTable table = dataGridView1.DataSource as DataTable;

                if (table == null || table.Rows.Count == 0)
                {
                    MessageBox.Show("Không có dữ liệu để lưu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Danh sách các IDPHONG đã tồn tại trong cơ sở dữ liệu
                List<int> existingRoomIDs = dichvubus.GetExistingRoomIDs();

                // Lưu các hàng mới (chưa tồn tại trong SQL)
                List<int> duplicateIDs = new List<int>();
                List<int> savedIDs = new List<int>();

                foreach (DataRow row in table.Rows)
                {
                    int idPhong = Convert.ToInt32(row["IDPHONG"]);
                    int tongSoDVDaSuDung = Convert.ToInt32(row["TONGSODVDASUDUNG"]);
                    int tongSoTienDV = Convert.ToInt32(row["TONGSOTIENDV"]);

                    // Kiểm tra xem IDPHONG đã tồn tại trong cơ sở dữ liệu hay chưa
                    if (existingRoomIDs.Contains(idPhong))
                    {
                        duplicateIDs.Add(idPhong);
                        continue; // Bỏ qua hàng này
                    }

                    // Gửi dữ liệu đến cơ sở dữ liệu (thông qua BLL)
                    dichvubus.SaveRoomServiceData(idPhong, tongSoDVDaSuDung, tongSoTienDV);
                    savedIDs.Add(idPhong);
                }

                // Thông báo kết quả
                if (savedIDs.Count > 0)
                {
                    MessageBox.Show($"Đã lưu thành công các phòng: {string.Join(", ", savedIDs)}",
                                    "Thông báo",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information);
                }

                if (duplicateIDs.Count > 0)
                {
                    MessageBox.Show($"Không thể lưu vì các phòng đã tồn tại: {string.Join(", ", duplicateIDs)}",
                                    "Thông báo",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Đã xảy ra lỗi khi lưu dữ liệu: {ex.Message}",
                                "Lỗi",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
