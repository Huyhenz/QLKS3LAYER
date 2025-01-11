using DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DAL
{
    public class ThanhToanVAHoaDondal
    {
        private string connectionString = "Data Source=HUYCATMOI;Initial Catalog=QLKS;Integrated Security=True;Encrypt=True;TrustServerCertificate=True;";

        public List<Phong> GetRoomDetails()
        {
            List<Phong> roomDetails = new List<Phong>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = @"
                    SELECT 
                        *
                    FROM  tb_Phong p
                    ";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Phong detail = new Phong
                            {
                                IDPHONG = reader.GetInt32(reader.GetOrdinal("IDPHONG")),
                                TINHTRANG = reader.GetString(reader.GetOrdinal("TINHTRANG")),
                                //IDKH = reader.GetInt32(reader.GetOrdinal("IDKH")),
                                //HOTEN = reader.GetString(reader.GetOrdinal("HOTEN")),
                                //TENLOAIPHONG = reader.GetString(reader.GetOrdinal("TENLOAIPHONG")),
                                //DONGIA = reader.GetInt32(reader.GetOrdinal("DONGIA")),
                                //NGAYDEN = reader.GetDateTime(reader.GetOrdinal("NGAYDEN")),
                                //NGAYDI = reader.GetDateTime(reader.GetOrdinal("NGAYDI")),
                                //TONGSONGAYO = reader.GetInt32(reader.GetOrdinal("TONGSONGAYO")),
                                //TONGSODVDASUDUNG = reader.GetInt32(reader.GetOrdinal("TONGSODVDASUDUNG")),
                                //TONGSOTIENDV = reader.GetInt32(reader.GetOrdinal("TONGSOTIENDV"))
                            };
                            roomDetails.Add(detail);
                        }
                    }
                }
            }

            return roomDetails;
        }
    }
}
