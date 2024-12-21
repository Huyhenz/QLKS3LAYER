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
                        Convert.ToDateTime(reader["NGAYSINH"]),
                        reader["EMAIL"].ToString(),
                        Convert.ToInt32(reader["SDT"]),
                        Convert.ToInt32(reader["CCCD"]),
                        reader["DIACHI"].ToString(),
                        reader["PASSWD"].ToString(),
                        Convert.ToInt32(reader["IDQUYEN"])
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
                string query = "INSERT INTO tb_User (FULLNAME, NGAYSINH, EMAIL, SDT, CCCD, DIACHI, USERNAME, PASSWD, IDQUYEN) " +
                               "VALUES ( @FULLNAME,@NGAYSINH, @EMAIL, @SDT, @CCCD, @DIACHI, @USERNAME, @PASSWD, @IDQUYEN)";

                // Tạo SqlCommand và truyền query vào
                SqlCommand command = new SqlCommand(query, conn.GetConnection());

                // Thêm tham số vào command với kiểu dữ liệu rõ ràng
                command.Parameters.Add("@FULLNAME", SqlDbType.NVarChar).Value = taiKhoan.FULLNAME;
                command.Parameters.Add("@NGAYSINH", SqlDbType.NVarChar).Value = taiKhoan.NGAYSINH;
                command.Parameters.Add("@EMAIL", SqlDbType.NVarChar).Value = taiKhoan.EMAIL;
                command.Parameters.Add("@SDT", SqlDbType.NVarChar).Value = taiKhoan.SDT;
                command.Parameters.Add("@CCCD", SqlDbType.NVarChar).Value = taiKhoan.CCCD;
                command.Parameters.Add("@DIACHI", SqlDbType.NVarChar).Value = taiKhoan.DIACHI;
                command.Parameters.Add("@USERNAME", SqlDbType.NVarChar).Value = taiKhoan.USERNAME;
                command.Parameters.Add("@PASSWD", SqlDbType.NVarChar).Value = taiKhoan.PASSWD;
                command.Parameters.Add("@IDQUYEN", SqlDbType.Int).Value = taiKhoan.IDQUYEN;


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
                        Convert.ToDateTime(reader["NGAYSINH"]),
                        reader["EMAIL"].ToString(),
                        Convert.ToChar(reader["SDT"]),
                        Convert.ToChar(reader["CCCD"]),
                        reader["DIACHI"].ToString(),
                        reader["PASSWD"].ToString(),
                        Convert.ToInt32(reader["IDQUYEN"])
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

        public TaiKhoanDTO Login(string USERNAME, string PASSWD)
        {
            try
            {
                conn.OpenConnection();
                string query = "SELECT * FROM tb_User WHERE USERNAME = @USERNAME AND PASSWD = @PASSWD";
                SqlCommand command = new SqlCommand(query, conn.GetConnection());
                command.Parameters.AddWithValue("@USERNAME", USERNAME);
                command.Parameters.AddWithValue("@PASSWD", PASSWD);
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    TaiKhoanDTO taiKhoan = new TaiKhoanDTO
                    {
                        UID = Convert.ToInt32(reader["UID"]),
                        FULLNAME = reader["FULLNAME"].ToString(),
                        USERNAME = reader["USERNAME"].ToString(),
                        NGAYSINH = Convert.ToDateTime(reader["NGAYSINH"]),
                        EMAIL = reader["EMAIL"].ToString(),
                        SDT = Convert.ToInt64(reader["SDT"]),
                        CCCD = Convert.ToInt64(reader["CCCD"]),
                        DIACHI = reader["DIACHI"].ToString(),
                        PASSWD = reader["PASSWD"].ToString(),
                        IDQUYEN = Convert.ToInt32(reader["IDQUYEN"])
                    };
                    reader.Close();
                    return taiKhoan;
                }
                else
                {
                    reader.Close();
                    return null;
                }
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
