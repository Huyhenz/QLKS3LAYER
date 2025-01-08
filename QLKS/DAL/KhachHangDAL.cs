using DTO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class KhachHangDAL
    {
        private string connectionString = "Data Source=HUYCATMOI;Initial Catalog=QLKS;Integrated Security=True;Encrypt=True;TrustServerCertificate=True;";
        public void AddCustomer(KhachHangDTO customer)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "INSERT INTO tb_KhachHang (CCCD, HOTEN, NGAYSINH, GIOITINH, DIENTHOAI, EMAIL, LOAIKH, GHICHU) VALUES (@CCCD, @HOTEN, @NGAYSINH, @GIOITINH, @DIENTHOAI, @EMAIL, @LOAIKH, @GHICHU)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@CCCD", customer.CCCD);
                cmd.Parameters.AddWithValue("@HOTEN", customer.HOTEN);
                cmd.Parameters.AddWithValue("@NGAYSINH", customer.NGAYSINH);
                cmd.Parameters.AddWithValue("@GIOITINH", customer.GIOITINH);
                cmd.Parameters.AddWithValue("@DIENTHOAI", customer.DIENTHOAI);
                cmd.Parameters.AddWithValue("@EMAIL", customer.EMAIL);
                cmd.Parameters.AddWithValue("@LOAIKH", customer.LOAIKH);
                cmd.Parameters.AddWithValue("@GHICHU", customer.GHICHU);
                cmd.ExecuteNonQuery();
            }
        }
    }
}
