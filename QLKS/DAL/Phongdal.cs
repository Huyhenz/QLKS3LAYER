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
            public List<Phong> GetPhongList()
            {
                List<Phong> phongList = new List<Phong>();
                string connectionString = "Data Source=HUYCATMOI;Initial Catalog=QLKS;Integrated Security=True;Encrypt=True;TrustServerCertificate=True;";
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT * FROM tb_Phong";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Phong phong = new Phong(
                            (int)reader["IDPHONG"],
                            reader["TENPHONG"].ToString(),
                            reader["IDTANG"].ToString(),
                            reader["IDLOAIPHONG"].ToString()
                        );
                        phongList.Add(phong);
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
                    string query = "INSERT INTO Phong (IDPHONG, TENPHONG, IDTANG, IDLOAIPHONG) VALUES (@IDPHONG, @TENPHONG, @IDTANG, @IDLOAIPHONG)";
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
                string connectionString = "your_conneData Source=HUYCATMOI;Initial Catalog=QLKS;Integrated Security=True;Encrypt=True;TrustServerCertificate=True;ction_string";
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "UPDATE Phong SET TENPHONG=@TENPHONG, IDTANG=@IDTANG, IDLOAIPHONG=@IDLOAIPHONG WHERE IDPHONG=@IDPHONG";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@IDPHONG", phong.IDPHONG);
                    cmd.Parameters.AddWithValue("@TENPHONG", phong.TENPHONG);
                    cmd.Parameters.AddWithValue("@IDTANG", phong.IDTANG);
                    cmd.Parameters.AddWithValue("@IDLOAIPHONG", phong.IDLOAIPHONG);
                    cmd.ExecuteNonQuery();
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
                    string query = "INSERT INTO DatPhong (ID, IDKH, IDPHONG, NGAYDAT, NGAYTRA, SONGAYO) VALUES (@ID, @IDKH, @IDPHONG, @NGAYDAT, @NGAYTRA, @SONGAYO)";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@ID", booking.ID);
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
