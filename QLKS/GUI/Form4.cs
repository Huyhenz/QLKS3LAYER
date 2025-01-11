using BLL;
using DevExpress.Utils.CodedUISupport;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DTO;
using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using System.Drawing;
using DevExpress.XtraGrid.Views.BandedGrid.Handler;

namespace GUI
{
    public partial class Form4 : Form
    {

        private Phongbus phongbus = new Phongbus();
        private khachhangbus khachhangbus = new khachhangbus();
        private Form1 form1;
        public Form4(int roomId)
        {
            
            InitializeComponent();
            setRoomNumber(roomId);
            LoadDataToGridControl("trống");
            var gridView = gcDanhSach.MainView as DevExpress.XtraGrid.Views.Grid.GridView;
            gridView.FocusedRowChanged += GridView_FocusedRowChanged;
            gridView.OptionsSelection.MultiSelect = false;
            gridView.OptionsSelection.EnableAppearanceFocusedCell = false;
            gridView.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.GridView_FocusedRowChanged);
            gcDanhSach.Click += new EventHandler(this.gcDanhSach_Click);


        }


        private void GridView_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
                var gridView = sender as DevExpress.XtraGrid.Views.Grid.GridView;
                int selectedRowHandle = e.FocusedRowHandle;

                if (selectedRowHandle >= 0)
                {
                    // Lấy dữ liệu từ hàng được chọn
                    object idPhongValue = gridView.GetRowCellValue(selectedRowHandle, "IDPHONG");
                    object ngayDatValue = gridView.GetRowCellValue(selectedRowHandle, "NGAYDAT");
                    object ngayTraValue = gridView.GetRowCellValue(selectedRowHandle, "NGAYTRA");
                    object soNgayOValue = gridView.GetRowCellValue(selectedRowHandle, "SONGAYO");
                    object cccdValue = gridView.GetRowCellValue(selectedRowHandle, "CCCD");
                    object tenKhachHangValue = gridView.GetRowCellValue(selectedRowHandle, "TENKH");
                    object ngaySinhValue = gridView.GetRowCellValue(selectedRowHandle, "NGAYSINH");
                    object dienThoaiValue = gridView.GetRowCellValue(selectedRowHandle, "DIENTHOAI");
                    object emailValue = gridView.GetRowCellValue(selectedRowHandle, "EMAIL");
                    object loaiKHValue = gridView.GetRowCellValue(selectedRowHandle, "LOAIKH");
                    object ghiChuValue = gridView.GetRowCellValue(selectedRowHandle, "GHICHU");

                    if (idPhongValue != DBNull.Value)
                    {
                        int idPhong = Convert.ToInt32(idPhongValue);
                        DateTime ngayDat = ngayDatValue != DBNull.Value ? Convert.ToDateTime(ngayDatValue) : DateTime.MinValue;
                        DateTime ngayTra = ngayTraValue != DBNull.Value ? Convert.ToDateTime(ngayTraValue) : DateTime.MinValue;
                        string soNgayO = soNgayOValue != DBNull.Value ? soNgayOValue.ToString() : string.Empty;
                        string cccd = cccdValue != DBNull.Value ? cccdValue.ToString() : string.Empty;
                        string tenKhachHang = tenKhachHangValue != DBNull.Value ? tenKhachHangValue.ToString() : string.Empty;
                        DateTime ngaySinh = ngaySinhValue != DBNull.Value ? Convert.ToDateTime(ngaySinhValue) : DateTime.MinValue;
                        string dienThoai = dienThoaiValue != DBNull.Value ? dienThoaiValue.ToString() : string.Empty;
                        string email = emailValue != DBNull.Value ? emailValue.ToString() : string.Empty;
                        string loaiKH = loaiKHValue != DBNull.Value ? loaiKHValue.ToString() : string.Empty;
                        string ghiChu = ghiChuValue != DBNull.Value ? ghiChuValue.ToString() : string.Empty;

                        // Lấy dữ liệu từ cơ sở dữ liệu với IDPHONG tương ứng
                        var roomDetails = phongbus.SetRoomIDD(idPhong);

                        // Gán giá trị cho các điều khiển tương ứng
                        if (roomDetails != null)
                        {
                            txtSP.Text = roomDetails.IDPHONG.ToString();
                            txtPhong.Text = roomDetails.TENPHONG;
                            txtLP.Text = roomDetails.LoaiPhong.TENLOAIPHONG;
                            txtGIA.Text = roomDetails.LoaiPhong.DONGIA.ToString();
                            txtSoGIUONG.Text = roomDetails.LoaiPhong.SOGIUONG.ToString();
                            datetime1.Value = ngayDat != DateTime.MinValue ? ngayDat : DateTime.Now;
                            datetime2.Value = ngayTra != DateTime.MinValue ? ngayTra : DateTime.Now;
                            txt_TSNO.Text = soNgayO;

                            txtCCCD.Text = cccd;
                            txtTEN.Text = tenKhachHang;
                            datetime3.Value = ngaySinh != DateTime.MinValue ? ngaySinh : DateTime.Now;
                            txtSDT.Text = dienThoai;
                            txtEMAIL.Text = email;
                            txtLKH.Text = loaiKH;
                            txtGHICHU.Text = ghiChu;
                        }
                        else
                        {
                            MessageBox.Show("Không thể lấy thông tin phòng.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Không thể xác định ID phòng.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
        }


        private void LoadDataToGridControl(string status)
        {
            var bookings = phongbus.GetBookings();
            var customers = khachhangbus.GetCustomers();
            var table = new DataTable();
            table.Columns.Add("IDKH");
            table.Columns.Add("IDPHONG");
            table.Columns.Add("NGAYDAT");
            table.Columns.Add("NGAYTRA");
            table.Columns.Add("SONGAYO");
            table.Columns.Add("SOGIUONG");
            table.Columns.Add("CCCD");
            table.Columns.Add("TENKH");
            table.Columns.Add("NGAYSINH");
            table.Columns.Add("DIENTHOAI");
            table.Columns.Add("EMAIL");
            table.Columns.Add("LOAIKH");
            table.Columns.Add("GHICHU");

            foreach (var booking in bookings)
            {
                var room = phongbus.GetAllRooms().FirstOrDefault(r => r.IDPHONG == booking.IDPHONG);
                if (room != null && room.TINHTRANG.ToLower() == status.ToLower())
                {
                    var row = table.NewRow();
                    row["IDKH"] = booking.IDKH;
                    row["IDPHONG"] = booking.IDPHONG;
                    row["NGAYDAT"] = booking.NGAYDAT;
                    row["NGAYTRA"] = booking.NGAYTRA;
                    row["SONGAYO"] = booking.SONGAYO;
                    var customer = customers.FirstOrDefault(c => c.IDKH == booking.IDKH);
                    if (customer != null)
                    {
                        row["CCCD"] = customer.CCCD;
                        row["TENKH"] = customer.HOTEN;
                        row["NGAYSINH"] = customer.NGAYSINH;
                        row["DIENTHOAI"] = customer.DIENTHOAI;
                        row["EMAIL"] = customer.EMAIL;
                        row["LOAIKH"] = customer.LOAIKH;
                        row["GHICHU"] = customer.GHICHU;
                    }
                    var roomDetails = phongbus.SetRoomIDD(booking.IDPHONG);
                    if (roomDetails != null)
                    {
                        row["SOGIUONG"] = roomDetails.LoaiPhong.SOGIUONG;
                    }
                    table.Rows.Add(row);
                }
            }
            gcDanhSach.DataSource = table;
            var gridView = gcDanhSach.MainView as DevExpress.XtraGrid.Views.Grid.GridView;
            foreach (DevExpress.XtraGrid.Columns.GridColumn column in gridView.Columns)
            {
                column.OptionsColumn.AllowEdit = false;
            }
            gridView.Columns["NGAYDAT"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            gridView.Columns["NGAYDAT"].DisplayFormat.FormatString = "dd/MM/yyyy";
            gridView.Columns["NGAYTRA"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            gridView.Columns["NGAYTRA"].DisplayFormat.FormatString = "dd/MM/yyyy";
            gridView.Columns["NGAYSINH"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            gridView.Columns["NGAYSINH"].DisplayFormat.FormatString = "dd/MM/yyyy";
        }



        private void guna2GroupBox1_Click(object sender, EventArgs e)
        {

        }

        private void guna2GroupBox3_Click(object sender, EventArgs e)
        {

        }

        private void Form4_Load(object sender, EventArgs e)
        {

        }

        private void setRoomNumber(int roomId)
        {
            var room = phongbus.SetRoomIDD(roomId);
            if (room != null)
            {
                txtSP.Text = room.IDPHONG.ToString();
                txtPhong.Text = room.TENPHONG;
                txtLP.Text = room.LoaiPhong.TENLOAIPHONG;
                txtGIA.Text = room.LoaiPhong.DONGIA.ToString();
                txtSoGIUONG.Text = room.LoaiPhong.SOGIUONG.ToString();
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
                DateTime ngayDat = datetime1.Value;
                DateTime ngayTra = datetime2.Value;
                int soNgayO = (ngayTra - ngayDat).Days;

                txt_TSNO.Text = soNgayO.ToString();

                ThongTinDP booking = new ThongTinDP
                {
                    //IDKH = int.Parse(txtIDKH.Text),
                    IDPHONG = int.Parse(txtSP.Text),
                    NGAYDAT = ngayDat.ToString("yyyy-MM-dd"),
                    NGAYTRA = ngayTra.ToString("yyyy-MM-dd"),
                    SONGAYO = soNgayO
                };

                KhachHangDTO customer = new KhachHangDTO
                {
                    CCCD = long.Parse(txtCCCD.Text),
                    HOTEN = txtTEN.Text,
                    NGAYSINH = datetime3.Value.ToString("yyyy-MM-dd"),
                    DIENTHOAI = long.Parse(txtSDT.Text),
                    EMAIL = txtEMAIL.Text,
                    LOAIKH = txtLKH.Text,
                    GHICHU = txtGHICHU.Text
                };

                // Kiểm tra nếu IDPHONG đã tồn tại trong GridControl
                var gridView = gcDanhSach.MainView as DevExpress.XtraGrid.Views.Grid.GridView;
                bool roomExists = false;

                for (int i = 0; i < gridView.RowCount; i++)
                {
                    if (gridView.GetRowCellValue(i, "IDPHONG").ToString() == booking.IDPHONG.ToString())
                    {
                        roomExists = true;
                        break;
                    }
                }

                if (roomExists)
                {
                    MessageBox.Show("Phòng đã được thêm trước đó!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Thêm thông tin đặt phòng vào GridControl
                var table = gcDanhSach.DataSource as DataTable;
                if (table == null)
                {
                    table = new DataTable();
                    table.Columns.Add("IDKH");
                    table.Columns.Add("IDPHONG");
                    table.Columns.Add("NGAYDAT");
                    table.Columns.Add("NGAYTRA");
                    table.Columns.Add("SONGAYO");
                    table.Columns.Add("CCCD");
                    table.Columns.Add("TENKH");
                    table.Columns.Add("NGAYSINH");
                    table.Columns.Add("GIOITINH");
                    table.Columns.Add("DIENTHOAI");
                    table.Columns.Add("EMAIL");
                    table.Columns.Add("LOAIKH");
                    table.Columns.Add("GHICHU");
                    gcDanhSach.DataSource = table;
                }

                var row = table.NewRow();
                row["IDKH"] = booking.IDKH;
                row["IDPHONG"] = booking.IDPHONG;
                row["NGAYDAT"] = booking.NGAYDAT;
                row["NGAYTRA"] = booking.NGAYTRA;
                row["SONGAYO"] = booking.SONGAYO;
                row["CCCD"] = customer.CCCD;
                row["TENKH"] = customer.HOTEN;
                row["NGAYSINH"] = customer.NGAYSINH;
                row["DIENTHOAI"] = customer.DIENTHOAI;
                row["EMAIL"] = customer.EMAIL;
                row["LOAIKH"] = customer.LOAIKH;
                row["GHICHU"] = customer.GHICHU;
                table.Rows.Add(row);

                // Thêm thông tin đặt phòng và khách hàng vào cơ sở dữ liệu
                phongbus.AddBooking(booking);
                khachhangbus.AddCustomer(customer);

                // Tải lại dữ liệu lên GridControl để đảm bảo dữ liệu mới thêm được hiển thị
                LoadDataToGridControl("trống");
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            // Cập nhật dữ liệu từ TextBox vào biến đối tượng
            DateTime ngayDat = datetime1.Value;
            DateTime ngayTra = datetime2.Value;
            int soNgayO = (ngayTra - ngayDat).Days;

            ThongTinDP booking = new ThongTinDP
            {
                IDPHONG = int.Parse(txtSP.Text),
                NGAYDAT = ngayDat.ToString("yyyy-MM-dd"),
                NGAYTRA = ngayTra.ToString("yyyy-MM-dd"),
                SONGAYO = soNgayO
            };

            KhachHangDTO customer = new KhachHangDTO
            {
                CCCD = long.Parse(txtCCCD.Text),
                HOTEN = txtTEN.Text,
                NGAYSINH = datetime3.Value.ToString("yyyy-MM-dd"),
                //GIOITINH = txtGender.Text,
                DIENTHOAI = long.Parse(txtSDT.Text),
                EMAIL = txtEMAIL.Text,
                LOAIKH = txtLKH.Text,
                GHICHU = txtGHICHU.Text
            };

            // Cập nhật thông tin trong GridControl
            var gridView = gcDanhSach.MainView as DevExpress.XtraGrid.Views.Grid.GridView;
            int selectedRowHandle = gridView.FocusedRowHandle;
            if (selectedRowHandle >= 0)
            {
                gridView.SetRowCellValue(selectedRowHandle, "IDPHONG", booking.IDPHONG);
                gridView.SetRowCellValue(selectedRowHandle, "NGAYDAT", booking.NGAYDAT);
                gridView.SetRowCellValue(selectedRowHandle, "NGAYTRA", booking.NGAYTRA);
                gridView.SetRowCellValue(selectedRowHandle, "SONGAYO", booking.SONGAYO);
                gridView.SetRowCellValue(selectedRowHandle, "CCCD", customer.CCCD);
                gridView.SetRowCellValue(selectedRowHandle, "TENKH", customer.HOTEN);
                gridView.SetRowCellValue(selectedRowHandle, "NGAYSINH", customer.NGAYSINH);
                gridView.SetRowCellValue(selectedRowHandle, "DIENTHOAI", customer.DIENTHOAI);
                gridView.SetRowCellValue(selectedRowHandle, "EMAIL", customer.EMAIL);
                gridView.SetRowCellValue(selectedRowHandle, "LOAIKH", customer.LOAIKH);
                gridView.SetRowCellValue(selectedRowHandle, "GHICHU", customer.GHICHU);
            }

            // Cập nhật thông tin đặt phòng và khách hàng vào cơ sở dữ liệu
            phongbus.UpdateBooking(booking);
            khachhangbus.UpdateCustomer(customer);

            // Tải lại dữ liệu lên GridControl để đảm bảo dữ liệu đã cập nhật được hiển thị
            LoadDataToGridControl("trống");
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            var gridView = gcDanhSach.MainView as DevExpress.XtraGrid.Views.Grid.GridView;
            int selectedRowHandle = gridView.FocusedRowHandle;

            if (selectedRowHandle >= 0)
            {
                int idPhong = int.Parse(gridView.GetRowCellValue(selectedRowHandle, "IDPHONG").ToString());
                int idKhachHang = int.Parse(gridView.GetRowCellValue(selectedRowHandle, "IDKH").ToString());

                // Kiểm tra trạng thái phòng từ cơ sở dữ liệu
                var room = phongbus.GetAllRooms().FirstOrDefault(r => r.IDPHONG == idPhong);

                if (room != null)
                {
                    if (room.TINHTRANG.ToLower() == "đã đặt")
                    {
                        // Xóa đặt phòng và khách hàng
                        phongbus.DeleteBooking(idPhong, idKhachHang);
                        khachhangbus.DeleteCustomer(idKhachHang);

                        // Cập nhật trạng thái phòng thành "trống"
                        phongbus.UpdateTinhTrangPhong(idPhong, "trống");

                        // Xóa hàng từ GridView và tải lại dữ liệu
                        gridView.DeleteRow(selectedRowHandle);
                        LoadDataToGridControl("trống");

                        MessageBox.Show("Phòng đã đặt đã được xóa và trạng thái phòng được cập nhật thành 'trống'.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Form1 formHeThong = (Form1)Application.OpenForms["Form1"];
                        if (formHeThong != null)
                        {
                            Guna2Button btn = formHeThong.Controls.Find($"guna2Button{idPhong}", true).FirstOrDefault() as Guna2Button;
                            if(btn!= null)
                            {
                                btn.FillColor = Color.SeaGreen;
                                btn.HoverState.FillColor = Color.SeaGreen;
                                btn.PressedColor = Color.SeaGreen;
                                btn.ForeColor = Color.Black;
                                btn.Text = idPhong.ToString();
                            }
                            formHeThong.LoadRooms();
                        }
                        this.Close();
                    }
                    else if (room.TINHTRANG.ToLower() == "nhận phòng")
                    {
                        MessageBox.Show("Không thể xóa phòng đang ở trạng thái 'nhận phòng'.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    
                }
            }
        }


        private void guna2Button1_Click(object sender, EventArgs e)
        {
            var gridView = gcDanhSach.MainView as DevExpress.XtraGrid.Views.Grid.GridView;
            int selectedRowHandle = gridView.FocusedRowHandle;
            if (selectedRowHandle >= 0)
            {
                int idPhong = int.Parse(gridView.GetRowCellValue(selectedRowHandle, "IDPHONG").ToString());

                // Cập nhật trạng thái phòng trong cơ sở dữ liệu
                phongbus.UpdateTinhTrangPhong(idPhong, "Đã đặt");

                // Thông báo đặt phòng thành công
                MessageBox.Show("Đặt phòng thành công!");

                Form1 formHeThong = (Form1)Application.OpenForms["Form1"];
                if(formHeThong != null)
                {
                    formHeThong.LoadRooms();
                }
                this.Close();
            }
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            var gridView = gcDanhSach.MainView as DevExpress.XtraGrid.Views.Grid.GridView;
            int selectedRowHandle = gridView.FocusedRowHandle;
            if (selectedRowHandle >= 0)
            {
                int idPhong = int.Parse(gridView.GetRowCellValue(selectedRowHandle, "IDPHONG").ToString());

                // Kiểm tra trạng thái phòng từ cơ sở dữ liệu
                var room = phongbus.GetAllRooms().FirstOrDefault(r => r.IDPHONG == idPhong);
                if (room != null && room.TINHTRANG.ToLower() == "đã đặt")
                {
                    // Gọi hàm từ BLL để cập nhật trạng thái phòng
                    phongbus.UpdateTinhTrangPhong(idPhong, "nhận phòng");

                    // Thông báo nhận phòng thành công
                    MessageBox.Show("Phòng đã được chuyển thành trạng thái nhận phòng.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Cập nhật trạng thái trong GridView
                    gridView.SetRowCellValue(selectedRowHandle, "TINHTRANG", "nhận phòng");

                    // Cập nhật màu của button trên FormHeThong
                    Form1 formHeThong = (Form1)Application.OpenForms["Form1"];
                    if (formHeThong != null)
                    {
                        formHeThong.LoadRooms();
                    }
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Phòng chưa được đặt hoặc trạng thái không hợp lệ.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void phòngĐãĐặtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadDataToGridControl("đã đặt");
        }

        private void phòngTrốngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadDataToGridControl("trống");
        }

        private void phòngĐãNhậnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadDataToGridControl("nhận phòng");
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            var gridView = gcDanhSach.MainView as DevExpress.XtraGrid.Views.Grid.GridView;
            int selectedRowHandle = gridView.FocusedRowHandle;

            if (selectedRowHandle >= 0)
            {
                int idPhong = int.Parse(gridView.GetRowCellValue(selectedRowHandle, "IDPHONG").ToString());
                int idKhachHang = int.Parse(gridView.GetRowCellValue(selectedRowHandle, "IDKH").ToString());

                // Kiểm tra trạng thái phòng từ cơ sở dữ liệu
                var room = phongbus.GetAllRooms().FirstOrDefault(r => r.IDPHONG == idPhong);

                if (room != null)
                {
                    if (room.TINHTRANG.ToLower() == "đã đặt")
                    {
                        // Xóa đặt phòng và khách hàng
                        phongbus.DeleteBooking(idPhong, idKhachHang);
                        khachhangbus.DeleteCustomer(idKhachHang);

                        // Cập nhật trạng thái phòng thành "trống"
                        phongbus.UpdateTinhTrangPhong(idPhong, "trống");

                        // Xóa hàng từ GridView và tải lại dữ liệu
                        gridView.DeleteRow(selectedRowHandle);
                        LoadDataToGridControl("trống");

                        MessageBox.Show("Phòng đã đặt đã được xóa và trạng thái phòng được cập nhật thành 'trống'.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Form1 formHeThong = (Form1)Application.OpenForms["Form1"];
                        if (formHeThong != null)
                        {
                            Guna2Button btn = formHeThong.Controls.Find($"guna2Button{idPhong}", true).FirstOrDefault() as Guna2Button;
                            if (btn != null)
                            {
                                btn.FillColor = Color.SeaGreen;
                                btn.HoverState.FillColor = Color.SeaGreen;
                                btn.PressedColor = Color.SeaGreen;
                                btn.ForeColor = Color.Black;
                                btn.Text = idPhong.ToString();
                            }
                            formHeThong.LoadRooms();
                        }
                        this.Close();
                    }
                    else if (room.TINHTRANG.ToLower() == "nhận phòng")
                    {
                        MessageBox.Show("Không thể xóa phòng đang ở trạng thái 'nhận phòng'.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                }
            }
        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            // Lưu thông tin hiện tại vào biến tạm
            DateTime ngayDat = datetime1.Value;
            DateTime ngayTra = datetime2.Value;
            int soNgayO = (ngayTra - ngayDat).Days;

            ThongTinDP booking = new ThongTinDP
            {
                IDPHONG = int.Parse(txtSP.Text),
                NGAYDAT = ngayDat.ToString("yyyy-MM-dd"),
                NGAYTRA = ngayTra.ToString("yyyy-MM-dd"),
                SONGAYO = soNgayO
            };

            KhachHangDTO customer = new KhachHangDTO
            {
                CCCD = long.Parse(txtCCCD.Text),
                HOTEN = txtTEN.Text,
                NGAYSINH = datetime3.Value.ToString("yyyy-MM-dd"),
                DIENTHOAI = long.Parse(txtSDT.Text),
                EMAIL = txtEMAIL.Text,
                LOAIKH = txtLKH.Text,
                GHICHU = txtGHICHU.Text
            };

            // Truyền thông tin sang Form1
            Form1 formHeThong = (Form1)Application.OpenForms["Form1"];
            if (formHeThong != null)
            {
                formHeThong.SetExchangeMode(booking, customer);
                this.Close();
            }
        }

        private void gcDanhSach_Click(object sender, EventArgs e)
        {
            GridView_FocusedRowChanged(sender, new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs(gridView1.FocusedRowHandle, gridView1.FocusedRowHandle));
        }
    }
    }

