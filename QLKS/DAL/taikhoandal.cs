using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;
using System.Data.SqlClient;
using System.Data;
namespace DAL
{
    public class taikhoandal
    {
        private ConnectionDB conn;

        public taikhoandal()
        {
            conn = new ConnectionDB();
        }

        // Lấy danh sách tất cả tài khoản
        public List<TaiKhoanDTO> GetAllTaiKhoan()
        {
            List<TaiKhoanDTO> taiKhoanList = new List<TaiKhoanDTO>();
            try
            {
                conn.OpenConnection();
                string query = "SELECT * FROM tb_User";
                SqlCommand command = new SqlCommand(query, conn.GetConnection());
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    TaiKhoanDTO taiKhoan = new TaiKhoanDTO(
                        Convert.ToInt32(reader["UID"]),
                        reader["FULLNAME"].ToString(),
                        reader["USERNAME"].ToString(),
                        reader["PASSWD"].ToString(),
                        reader["IDQUYEN"].ToString()
                    );
                    taiKhoanList.Add(taiKhoan);
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi lấy danh sách tài khoản: " + ex.Message);
            }
            finally
            {
                conn.CloseConnection();
            }
            return taiKhoanList;
        }

        public bool AddTaiKhoan(TaiKhoanDTO taiKhoan)
        {
            try
            {
                // Mở kết nối
                conn.OpenConnection();  // Nếu conn là đối tượng kết nối của bạn
                string query = "INSERT INTO tb_User (FULLNAME, USERNAME, PASSWD, IDQUYEN) " +
                               "VALUES ( @FULLNAME, @USERNAME, @PASSWD, @IDQUYEN)";

                // Tạo SqlCommand và truyền query vào
                SqlCommand command = new SqlCommand(query, conn.GetConnection());

                // Thêm tham số vào command với kiểu dữ liệu rõ ràng
                command.Parameters.Add("@FULLNAME", SqlDbType.NVarChar).Value = taiKhoan.FULLNAME;
                command.Parameters.Add("@USERNAME", SqlDbType.NVarChar).Value = taiKhoan.USERNAME;
                command.Parameters.Add("@PASSWD", SqlDbType.NVarChar).Value = taiKhoan.PASSWD;
                command.Parameters.Add("@IDQUYEN", SqlDbType.NVarChar).Value = taiKhoan.IDQUYEN;


                // Thực thi câu lệnh INSERT và kiểm tra xem có bản ghi nào được thêm không
                int result = command.ExecuteNonQuery();

                // Nếu thêm thành công, trả về true, nếu không thì false
                return result > 0;
            }
            catch (Exception ex)
            {
                // In chi tiết lỗi nếu có
                Console.WriteLine(ex.Message);
                throw new Exception("Lỗi khi thêm tài khoản: " + ex.Message);
            }
            finally
            {
                // Đảm bảo đóng kết nối sau khi hoàn tất
                conn.CloseConnection();
            }
        }


        public bool UpdateTaiKhoan(TaiKhoanDTO taiKhoan)
        {
            try
            {
                conn.OpenConnection();
                string query = "UPDATE tb_User SET FULLNAME = @FULLNAME, USERNAME = @USERNAME, PASSWD = @PASSWD , IDQUYEN = @IDQUYEN WHERE UID = @UID";
                SqlCommand command = new SqlCommand(query, conn.GetConnection());
                command.Parameters.AddWithValue("@TenNguoiDung", taiKhoan.FULLNAME);
                command.Parameters.AddWithValue("@MatKhau", taiKhoan.USERNAME);
                command.Parameters.AddWithValue("@TrangThai", taiKhoan.PASSWD);
                command.Parameters.AddWithValue("@Quyen", taiKhoan.IDQUYEN);

                int result = command.ExecuteNonQuery();
                return result > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi cập nhật tài khoản: " + ex.Message);
            }
            finally
            {
                conn.CloseConnection();
            }
        }

        // Xóa tài khoản theo ID
        public bool DeleteTaiKhoan(int UID)
        {
            try
            {
                conn.OpenConnection();
                string query = "DELETE FROM tb_User WHERE UID = @UID";
                SqlCommand command = new SqlCommand(query, conn.GetConnection());
                command.Parameters.AddWithValue("@UID", UID);
                int result = command.ExecuteNonQuery();
                return result > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi xóa tài khoản: " + ex.Message);
            }
            finally
            {
                conn.CloseConnection();
            }
        }

        public TaiKhoanDTO GetTaiKhoanById(int UID)
        {
            TaiKhoanDTO taiKhoan = null;
            try
            {
                conn.OpenConnection();
                string query = "SELECT * FROM tb_User WHERE UID = @UID";
                SqlCommand command = new SqlCommand(query, conn.GetConnection());
                command.Parameters.AddWithValue("@UID", UID);
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    taiKhoan = new TaiKhoanDTO(
                         Convert.ToInt32(reader["UID"]),
                        reader["FULLNAME"].ToString(),
                        reader["USERNAME"].ToString(),
                        reader["PASSWD"].ToString(),
                        reader["IDQUYEN"].ToString()
                    );
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi lấy tài khoản theo ID: " + ex.Message);
            }
            finally
            {
                conn.CloseConnection();
            }
            return taiKhoan;
        }

        public bool Login(string USERNAME, string PASSWD)
        {
            try
            {
                conn.OpenConnection();
                string query = "SELECT COUNT(*) FROM tb_User WHERE USERNAME = @USERNAME AND PASSWD = @PASSWD";// AND IDQUYEN = @IDQUYEN
                SqlCommand command = new SqlCommand(query, conn.GetConnection());
                command.Parameters.AddWithValue("@USERNAME", USERNAME);
                command.Parameters.AddWithValue("@PASSWD", PASSWD);

                int count = (int)command.ExecuteScalar();  // Kiểm tra số lượng tài khoản khớp

                return count > 0;  // Nếu có tài khoản khớp thì trả về true
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi đăng nhập: " + ex.Message);
            }
            finally
            {
                conn.CloseConnection();
            }
        }


    }
}
