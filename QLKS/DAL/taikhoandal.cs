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
                    TaiKhoanDTO taiKhoan = new TaiKhoanDTO
                    {
                        UID = reader["UID"] != DBNull.Value ? Convert.ToInt32(reader["UID"]) : 0,
                        FULLNAME = reader["FULLNAME"] != DBNull.Value ? reader["FULLNAME"].ToString() : string.Empty,
                        USERNAME = reader["USERNAME"] != DBNull.Value ? reader["USERNAME"].ToString() : string.Empty,
                        NGAYSINH = reader["NGAYSINH"] != DBNull.Value ? reader["NGAYSINH"].ToString() : string.Empty,
                        EMAIL = reader["EMAIL"] != DBNull.Value ? reader["EMAIL"].ToString() : string.Empty,
                        SDT = reader["SDT"] != DBNull.Value ? Convert.ToInt64(reader["SDT"]) : 0,
                        CCCD = reader["CCCD"] != DBNull.Value ? Convert.ToInt64(reader["CCCD"]) : 0,
                        DIACHI = reader["DIACHI"] != DBNull.Value ? reader["DIACHI"].ToString() : string.Empty,
                        PASSWD = reader["PASSWD"] != DBNull.Value ? reader["PASSWD"].ToString() : string.Empty,
                        IDQUYEN = reader["IDQUYEN"] != DBNull.Value ? Convert.ToInt32(reader["IDQUYEN"]) : 0
                    };
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
                    // Kiểm tra giá trị SDT và CCCD hợp lệ
                    if (taiKhoan.SDT > Int64.MaxValue || taiKhoan.CCCD > Int64.MaxValue)
                    {
                        throw new Exception("Giá trị SDT hoặc CCCD vượt quá giới hạn cho phép");
                    }

                    // Mở kết nối
                    conn.OpenConnection();  // Nếu conn là đối tượng kết nối của bạn
                    string query = "INSERT INTO tb_User (FULLNAME, NGAYSINH, EMAIL, SDT, CCCD, DIACHI, USERNAME, PASSWD, IDQUYEN) " +
                                   "VALUES (@FULLNAME, @NGAYSINH, @EMAIL, @SDT, @CCCD, @DIACHI, @USERNAME, @PASSWD, @IDQUYEN)";

                    // Tạo SqlCommand và truyền query vào
                    SqlCommand command = new SqlCommand(query, conn.GetConnection());

                    // Chuyển đổi từ string sang DateTime
                    DateTime ngaySinh;
                    if (!DateTime.TryParseExact(taiKhoan.NGAYSINH, "yyyy-MM-dd", null, System.Globalization.DateTimeStyles.None, out ngaySinh))
                    {
                        throw new Exception("Ngày sinh không hợp lệ");
                    }

                    // Thêm tham số vào command với kiểu dữ liệu rõ ràng
                    command.Parameters.Add("@FULLNAME", SqlDbType.NVarChar).Value = taiKhoan.FULLNAME ?? string.Empty;
                    command.Parameters.Add("@NGAYSINH", SqlDbType.Date).Value = ngaySinh;
                    command.Parameters.Add("@EMAIL", SqlDbType.NVarChar).Value = taiKhoan.EMAIL ?? string.Empty;
                    command.Parameters.Add("@SDT", SqlDbType.BigInt).Value = taiKhoan.SDT;
                    command.Parameters.Add("@CCCD", SqlDbType.BigInt).Value = taiKhoan.CCCD;
                    command.Parameters.Add("@DIACHI", SqlDbType.NVarChar).Value = taiKhoan.DIACHI ?? string.Empty;
                    command.Parameters.Add("@USERNAME", SqlDbType.NVarChar).Value = taiKhoan.USERNAME ?? string.Empty;
                    command.Parameters.Add("@PASSWD", SqlDbType.NVarChar).Value = taiKhoan.PASSWD ?? string.Empty;
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

                    // Chuyển đổi từ string sang DateTime và kiểm tra giá trị hợp lệ
                    DateTime ngaySinh;
                    if (!DateTime.TryParseExact(taiKhoan.NGAYSINH, "yyyy-MM-dd", null, System.Globalization.DateTimeStyles.None, out ngaySinh))
                    {
                        throw new Exception("Ngày sinh không hợp lệ");
                    }

                    string query = "UPDATE tb_User SET FULLNAME = @FULLNAME, NGAYSINH = @NGAYSINH, EMAIL = @EMAIL, SDT = @SDT, CCCD = @CCCD, DIACHI = @DIACHI, USERNAME = @USERNAME, PASSWD = @PASSWD, IDQUYEN = @IDQUYEN WHERE UID = @UID";
                    SqlCommand command = new SqlCommand(query, conn.GetConnection());

                    // Thêm tất cả các tham số vào lệnh SQL
                    command.Parameters.Add("@FULLNAME", SqlDbType.NVarChar).Value = taiKhoan.FULLNAME ?? string.Empty;
                    command.Parameters.Add("@NGAYSINH", SqlDbType.Date).Value = ngaySinh; // Chuyển đổi string sang DateTime và sử dụng kiểu date
                    command.Parameters.Add("@EMAIL", SqlDbType.NVarChar).Value = taiKhoan.EMAIL ?? string.Empty;
                    command.Parameters.Add("@SDT", SqlDbType.BigInt).Value = taiKhoan.SDT;
                    command.Parameters.Add("@CCCD", SqlDbType.BigInt).Value = taiKhoan.CCCD;
                    command.Parameters.Add("@DIACHI", SqlDbType.NVarChar).Value = taiKhoan.DIACHI ?? string.Empty;
                    command.Parameters.Add("@USERNAME", SqlDbType.NVarChar).Value = taiKhoan.USERNAME ?? string.Empty;
                    command.Parameters.Add("@PASSWD", SqlDbType.NVarChar).Value = taiKhoan.PASSWD ?? string.Empty;
                    command.Parameters.Add("@IDQUYEN", SqlDbType.Int).Value = taiKhoan.IDQUYEN;
                    command.Parameters.Add("@UID", SqlDbType.Int).Value = taiKhoan.UID;

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
                        reader["NGAYSINH"] != DBNull.Value ? reader["NGAYSINH"].ToString() : string.Empty, // Xử lý dưới dạng chuỗi
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
                string query = "SELECT * FROM tb_User WHERE USERNAME COLLATE Latin1_General_CS_AS = @USERNAME AND PASSWD COLLATE Latin1_General_CS_AS = @PASSWD"; SqlCommand command = new SqlCommand(query, conn.GetConnection());
                command.Parameters.AddWithValue("@USERNAME", USERNAME);
                command.Parameters.AddWithValue("@PASSWD", PASSWD);
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    TaiKhoanDTO taiKhoan = new TaiKhoanDTO
                    {
                        UID = reader["UID"] != DBNull.Value ? Convert.ToInt32(reader["UID"]) : 0,
                        FULLNAME = reader["FULLNAME"] != DBNull.Value ? reader["FULLNAME"].ToString() : string.Empty,
                        USERNAME = reader["USERNAME"] != DBNull.Value ? reader["USERNAME"].ToString() : string.Empty,
                        NGAYSINH = reader["NGAYSINH"] != DBNull.Value ? reader["NGAYSINH"].ToString() : string.Empty, // Xử lý dưới dạng chuỗi                        EMAIL = reader["EMAIL"] != DBNull.Value ? reader["EMAIL"].ToString() : string.Empty,
                        SDT = reader["SDT"] != DBNull.Value ? Convert.ToInt64(reader["SDT"]) : 0,
                        CCCD = reader["CCCD"] != DBNull.Value ? Convert.ToInt64(reader["CCCD"]) : 0,
                        DIACHI = reader["DIACHI"] != DBNull.Value ? reader["DIACHI"].ToString() : string.Empty,
                        PASSWD = reader["PASSWD"] != DBNull.Value ? reader["PASSWD"].ToString() : string.Empty,
                        IDQUYEN = reader["IDQUYEN"] != DBNull.Value ? Convert.ToInt32(reader["IDQUYEN"]) : 0
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
            public List<FuncDTO> GetFuncItems(string parents)
            {
                List<FuncDTO> items = new List<FuncDTO>();
                try
                {
                    string query = "SELECT FUNC_CODE, DESCRIPTION FROM tb_Func WHERE PARENT = @PARENT AND ISGROUP = 0 AND MENU = 1";
                    conn.OpenConnection();
                    SqlCommand command = new SqlCommand(query, conn.GetConnection());
                    command.Parameters.AddWithValue("@PARENT", parents);
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        FuncDTO item = new FuncDTO
                    {
                            FUNC_CODE = reader["FUNC_CODE"].ToString(),
                            DESCRIPTION = reader["DESCRIPTION"].ToString()
                        };
                        items.Add(item);
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Lỗi khi lấy dữ liệu: " + ex.Message);
                }
                finally
                {
                    conn.CloseConnection();
                }
                return items;
            }
        }
    }
