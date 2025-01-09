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
        public List<KhachHangDTO> GetCustomers()
        {
            List<KhachHangDTO> customers = new List<KhachHangDTO>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT * FROM tb_KhachHang";
                SqlCommand cmd = new SqlCommand(query, conn);   
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    KhachHangDTO customer = new KhachHangDTO
                    {
                        IDKH = Convert.ToInt32(reader["IDKH"]),
                        CCCD = Convert.ToInt64(reader["CCCD"]),
                        HOTEN = reader["HOTEN"].ToString(),
                        NGAYSINH = reader["NGAYSINH"].ToString(),
                        GIOITINH = reader["GIOITINH"].ToString(),
                        DIENTHOAI = Convert.ToInt64(reader["DIENTHOAI"]),
                        EMAIL = reader["EMAIL"].ToString(),
                        LOAIKH = reader["LOAIKH"].ToString(),
                        GHICHU = reader["GHICHU"].ToString()
                    };
                    customers.Add(customer);
                }
            }

            return customers;
        }

        public void UpdateCustomer(KhachHangDTO customer)
        {
            // Thực hiện truy vấn SQL để cập nhật thông tin customer
            string query = "UPDATE tb_KhachHang SET HOTEN = @HOTEN, NGAYSINH = @NGAYSINH, GIOITINH = @GIOITINH, DIENTHOAI = @DIENTHOAI, EMAIL = @EMAIL, LOAIKH = @LOAIKH, GHICHU = @GHICHU WHERE CCCD = @CCCD";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@CCCD", customer.CCCD);
                cmd.Parameters.AddWithValue("@HOTEN", customer.HOTEN);
                cmd.Parameters.AddWithValue("@NGAYSINH", customer.NGAYSINH);
                cmd.Parameters.AddWithValue("@GIOITINH", customer.GIOITINH);
                cmd.Parameters.AddWithValue("@DIENTHOAI", customer.DIENTHOAI);
                cmd.Parameters.AddWithValue("@EMAIL", customer.EMAIL);
                cmd.Parameters.AddWithValue("@LOAIKH", customer.LOAIKH);
                cmd.Parameters.AddWithValue("@GHICHU", customer.GHICHU);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void DeleteCustomer(int idKhachHang)
        {
            // Thực hiện truy vấn SQL để xóa thông tin khách hàng
            string query = "DELETE FROM tb_KhachHang WHERE IDKH = @IDKH";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@IDKH", idKhachHang);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}
