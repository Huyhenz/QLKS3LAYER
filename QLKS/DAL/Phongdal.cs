using DTO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class PhongDAL
    {
        private string connectionString = "Data Source=HUYCATMOI;Initial Catalog=QLKS;Integrated Security=True;Encrypt=True;TrustServerCertificate=True;";

        public List<Phong> GetPhongList()
        {
            List<Phong> phongList = new List<Phong>();
            string connectionString = "Data Source=HUYCATMOI;Initial Catalog=QLKS;Integrated Security=True;Encrypt=True;TrustServerCertificate=True;";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT p.IDPHONG, p.TENPHONG, p.IDTANG, p.IDLOAIPHONG, l.TENLOAIPHONG, l.DONGIA, l.SONGUOI, l.SOGIUONG " +
                               "FROM tb_Phong p " +
                               "JOIN tb_LoaiPhong l ON p.IDLOAIPHONG = l.IDLOAIPHONG";
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                try
                {
                    while (reader.Read())
                    {
                        LoaiPhongDTO loaiPhong = new LoaiPhongDTO(
                            (int)reader["IDLOAIPHONG"],
                            reader["TENLOAIPHONG"].ToString(),
                            (int)reader["DONGIA"], // Sử dụng kiểu int
                            (int)reader["SONGUOI"],
                            (int)reader["SOGIUONG"]
                        );

                        Phong phong = new Phong(
                            (int)reader["IDPHONG"],
                            reader["TENPHONG"].ToString(),
                            reader["IDTANG"].ToString(),
                            reader["IDLOAIPHONG"].ToString(), // Thêm IDLOAIPHONG vào constructor
                            loaiPhong
                        );
                        phongList.Add(phong);
                    }
                }
                catch (InvalidCastException ex)
                {
                    Console.WriteLine("Lỗi khi chuyển đổi kiểu dữ liệu: " + ex.Message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Lỗi khi truy vấn dữ liệu: " + ex.Message);
                }
            }
            return phongList;
        }






        public void AddPhong(Phong phong)
        {
            string connectionString = "Data Source=HUYCATMOI;Initial Catalog=QLKS;Integrated Security=True;Encrypt=True;TrustServerCertificate=True;";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "INSERT INTO tb_Phong (IDPHONG, TENPHONG, IDTANG, IDLOAIPHONG) VALUES (@IDPHONG, @TENPHONG, @IDTANG, @IDLOAIPHONG)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@IDPHONG", phong.IDPHONG);
                cmd.Parameters.AddWithValue("@TENPHONG", phong.TENPHONG);
                cmd.Parameters.AddWithValue("@IDTANG", phong.IDTANG);
                cmd.Parameters.AddWithValue("@IDLOAIPHONG", phong.IDLOAIPHONG);
                cmd.ExecuteNonQuery();
            }
        }

        public void UpdatePhong(Phong phong)
        {
            string connectionString = "Data Source=HUYCATMOI;Initial Catalog=QLKS;Integrated Security=True;Encrypt=True;TrustServerCertificate=True;ction_string";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "UPDATE tb_Phong SET TENPHONG=@TENPHONG, IDTANG=@IDTANG, IDLOAIPHONG=@IDLOAIPHONG WHERE IDPHONG=@IDPHONG";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@IDPHONG", phong.IDPHONG);
                cmd.Parameters.AddWithValue("@TENPHONG", phong.TENPHONG);
                cmd.Parameters.AddWithValue("@IDTANG", phong.IDTANG);
                cmd.Parameters.AddWithValue("@IDLOAIPHONG", phong.IDLOAIPHONG);
                cmd.ExecuteNonQuery();
            }
        }
        public Phong GetPhongById(int idPhong)
        {
            Phong phong = null;
            string connectionString = "Data Source=HUYCATMOI;Initial Catalog=QLKS;Integrated Security=True;Encrypt=True;TrustServerCertificate=True;";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT p.IDPHONG, p.IDLOAIPHONG, l.TENLOAIPHONG, l.DONGIA " +
                               "FROM tb_Phong p " +
                               "JOIN tb_LoaiPhong l ON p.IDLOAIPHONG = l.IDLOAIPHONG " +
                               "WHERE p.IDPHONG = @IDPHONG";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@IDPHONG", idPhong);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            LoaiPhongDTO loaiPhong = new LoaiPhongDTO(
                                Convert.ToInt32(reader["IDLOAIPHONG"]),
                                reader["TENLOAIPHONG"].ToString(),
                                Convert.ToInt32(reader["DONGIA"]),
                                0, // Không cần các thuộc tính khác
                                0  // Không cần các thuộc tính khác
                            );

                            phong = new Phong(
                                Convert.ToInt32(reader["IDPHONG"]),
                                "", // Không cần TENPHONG
                                "", // Không cần IDTANG
                                reader["IDLOAIPHONG"].ToString(),
                                loaiPhong
                            );
                        }
                    }
                }
            }
            return phong;
        }
        public Phong SetRoomIDD(int roomId)
        {
            Phong phong = null;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT IDPHONG, TENLOAIPHONG, TENPHONG, DONGIA FROM tb_Phong p " +
                               "JOIN tb_LoaiPhong l ON p.IDLOAIPHONG = l.IDLOAIPHONG WHERE IDPHONG = @IDPHONG";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@IDPHONG", roomId);

                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    LoaiPhongDTO loaiPhong = new LoaiPhongDTO
                    {
                        IDLOAIPHONG = reader.GetInt32(reader.GetOrdinal("IDPHONG")),
                        TENLOAIPHONG = reader["TENLOAIPHONG"].ToString(),
                        DONGIA = Convert.ToInt32(reader["DONGIA"])
                    };

                    phong = new Phong
                    {
                        IDPHONG = Convert.ToInt32(reader["IDPHONG"]),
                        TENPHONG = reader["TENPHONG"].ToString(),
                        LoaiPhong = loaiPhong
                    };
                }
            }

            return phong;      
    }

        public void AddBooking(ThongTinDP booking)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "INSERT INTO tb_DatPhong (IDKH, IDPHONG, NGAYDAT, NGAYTRA, SONGAYO) VALUES (@IDKH, @IDPHONG, @NGAYDAT, @NGAYTRA, @SONGAYO)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@IDKH", booking.IDKH);
                cmd.Parameters.AddWithValue("@IDPHONG", booking.IDPHONG);
                cmd.Parameters.AddWithValue("@NGAYDAT", booking.NGAYDAT);
                cmd.Parameters.AddWithValue("@NGAYTRA", booking.NGAYTRA);
                cmd.Parameters.AddWithValue("@SONGAYO", booking.SONGAYO);
                cmd.ExecuteNonQuery();
            }
        }





    }









}

public class BookingDAL
    {
        public void SaveBooking(ThongTinDP booking)
        {
            string connectionString = "Data Source=HUYCATMOI;Initial Catalog=QLKS;Integrated Security=True;Encrypt=True;TrustServerCertificate=True;";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "INSERT INTO tb_DatPhong (ID, IDKH, IDPHONG, NGAYDAT, NGAYTRA, SONGAYO) VALUES (@ID, @IDKH, @IDPHONG, @NGAYDAT, @NGAYTRA, @SONGAYO)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@ID", booking.IDDP);
                cmd.Parameters.AddWithValue("@IDKH", booking.IDKH);
                cmd.Parameters.AddWithValue("@IDPHONG", booking.IDPHONG);
                cmd.Parameters.AddWithValue("@NGAYDAT", booking.NGAYDAT);
                cmd.Parameters.AddWithValue("@NGAYTRA", booking.NGAYTRA);
                cmd.Parameters.AddWithValue("@SONGAYO", booking.SONGAYO);
                cmd.ExecuteNonQuery();
            }
        }
    }
