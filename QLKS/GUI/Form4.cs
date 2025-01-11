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
            LoadDataToGridControl();
            var gridView = gcDanhSach.MainView as DevExpress.XtraGrid.Views.Grid.GridView;
            gridView.FocusedRowChanged += GridView_FocusedRowChanged;
            

        }
      

        private void GridView_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            var gridView = sender as DevExpress.XtraGrid.Views.Grid.GridView;
            int selectedRowHandle = e.FocusedRowHandle;


            if (selectedRowHandle >= 0)
            {
                // Lấy dữ liệu từ hàng được chọn
                int idPhong = int.Parse(gridView.GetRowCellValue(selectedRowHandle, "IDPHONG").ToString());
                DateTime ngayDat = Convert.ToDateTime(gridView.GetRowCellValue(selectedRowHandle, "NGAYDAT"));
                DateTime ngayTra = Convert.ToDateTime(gridView.GetRowCellValue(selectedRowHandle, "NGAYTRA"));
                string soNgayO = gridView.GetRowCellValue(selectedRowHandle, "SONGAYO").ToString();

                string cccd = gridView.GetRowCellValue(selectedRowHandle, "CCCD").ToString();
                string tenKhachHang = gridView.GetRowCellValue(selectedRowHandle, "TENKH").ToString();
                DateTime ngaySinh = Convert.ToDateTime(gridView.GetRowCellValue(selectedRowHandle, "NGAYSINH"));
                string gioiTinh = gridView.GetRowCellValue(selectedRowHandle, "GIOITINH").ToString();
                string dienThoai = gridView.GetRowCellValue(selectedRowHandle, "DIENTHOAI").ToString();
                string email = gridView.GetRowCellValue(selectedRowHandle, "EMAIL").ToString();
                string loaiKH = gridView.GetRowCellValue(selectedRowHandle, "LOAIKH").ToString();
                string ghiChu = gridView.GetRowCellValue(selectedRowHandle, "GHICHU").ToString();

                // Lấy dữ liệu từ cơ sở dữ liệu với IDPHONG tương ứng
                var roomDetails = phongbus.SetRoomIDD(idPhong);

                // Gán giá trị cho các điều khiển tương ứng
                txtSP.Text = roomDetails.IDPHONG.ToString();
                txtPhong.Text = roomDetails.TENPHONG;
                txtLP.Text = roomDetails.LoaiPhong.TENLOAIPHONG;
                txtGIA.Text = roomDetails.LoaiPhong.DONGIA.ToString();
                txtSoGIUONG.Text = roomDetails.LoaiPhong.SOGIUONG.ToString();
                datetime1.Value = ngayDat;
                datetime2.Value = ngayTra;
                txt_TSNO.Text = soNgayO;

                txtCCCD.Text = cccd;
                txtTEN.Text = tenKhachHang;
                datetime3.Value = ngaySinh;
                txtGender.Text = gioiTinh;
                txtSDT.Text = dienThoai;
                txtEMAIL.Text = email;
                txtLKH.Text = loaiKH;
                txtGHICHU.Text = ghiChu;
            }
        }

        private void LoadDataToGridControl()
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
            table.Columns.Add("GIOITINH");
            table.Columns.Add("DIENTHOAI");
            table.Columns.Add("EMAIL");
            table.Columns.Add("LOAIKH");
            table.Columns.Add("GHICHU");

            foreach (var booking in bookings)
            {
                var row = table.NewRow();
                row["IDKH"] = booking.IDKH;
                row["IDPHONG"] = booking.IDPHONG;
                row["NGAYDAT"] = booking.NGAYDAT;
                row["NGAYTRA"] = booking.NGAYTRA;
                row["SONGAYO"] = booking.SONGAYO;

                var customer = customers.Find(c => c.IDKH == booking.IDKH);
                if (customer != null)
                {
                    row["CCCD"] = customer.CCCD;
                    row["TENKH"] = customer.HOTEN;
                    row["NGAYSINH"] = customer.NGAYSINH;
                    row["GIOITINH"] = customer.GIOITINH;
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
                GIOITINH = txtGender.Text,
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
            row["GIOITINH"] = customer.GIOITINH;
            row["DIENTHOAI"] = customer.DIENTHOAI;
            row["EMAIL"] = customer.EMAIL;
            row["LOAIKH"] = customer.LOAIKH;
            row["GHICHU"] = customer.GHICHU;
            table.Rows.Add(row);

            // Thêm thông tin đặt phòng và khách hàng vào cơ sở dữ liệu
            phongbus.AddBooking(booking);
            khachhangbus.AddCustomer(customer);

            // Tải lại dữ liệu lên GridControl để đảm bảo dữ liệu mới thêm được hiển thị
            LoadDataToGridControl();
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
                GIOITINH = txtGender.Text,
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
                gridView.SetRowCellValue(selectedRowHandle, "GIOITINH", customer.GIOITINH);
                gridView.SetRowCellValue(selectedRowHandle, "DIENTHOAI", customer.DIENTHOAI);
                gridView.SetRowCellValue(selectedRowHandle, "EMAIL", customer.EMAIL);
                gridView.SetRowCellValue(selectedRowHandle, "LOAIKH", customer.LOAIKH);
                gridView.SetRowCellValue(selectedRowHandle, "GHICHU", customer.GHICHU);
            }

            // Cập nhật thông tin đặt phòng và khách hàng vào cơ sở dữ liệu
            phongbus.UpdateBooking(booking);
            khachhangbus.UpdateCustomer(customer);

            // Tải lại dữ liệu lên GridControl để đảm bảo dữ liệu đã cập nhật được hiển thị
            LoadDataToGridControl();
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

                phongbus.DeleteBooking(idPhong, idKhachHang);
                khachhangbus.DeleteCustomer(idKhachHang);

                gridView.DeleteRow(selectedRowHandle);
                LoadDataToGridControl();
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
        
    }
}

