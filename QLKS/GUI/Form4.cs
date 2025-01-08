using BLL;
using DevExpress.XtraGrid;
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
using System.Windows.Forms;

namespace GUI
{
    public partial class Form4 : Form
    {

        private Phongbus phongbus = new Phongbus();
        private khachhangbus khachhangbus = new khachhangbus();
        public Form4(int roomId)
        {
            InitializeComponent();
            setRoomNumber(roomId);
            LoadDataToGridControl();
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

                table.Rows.Add(row);
            }

            gcDanhSach.DataSource = table;
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
    }
}
