using DTO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace DAL
{
    public class ThanhToanVAHoaDondal
    {
        private string connectionString = "Data Source=HUYCATMOI;Initial Catalog=QLKS;Integrated Security=True;Encrypt=True;TrustServerCertificate=True;";

        public List<ThongTinDP> GetDanhSachDatPhong()
        {
            List<ThongTinDP> danhSachDP = new List<ThongTinDP>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM tb_DatPhong";
                SqlCommand cmd = new SqlCommand(query, conn);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    ThongTinDP dp = new ThongTinDP
                    {
                        IDDP = Convert.ToInt32(reader["IDDP"]),
                        IDKH = Convert.ToInt32(reader["IDKH"]),
                        IDPHONG = Convert.ToInt32(reader["IDPHONG"]),
                        NGAYDAT = reader["NGAYDAT"].ToString(),
                        NGAYTRA = reader["NGAYTRA"].ToString(),
                        SONGAYO = Convert.ToInt32(reader["SONGAYO"])
                    };
                    danhSachDP.Add(dp);
                }
            }

            return danhSachDP;
        }

        // Tìm kiếm danh sách đặt phòng theo số phòng
        public List<ThongTinDP> TimKiemDatPhongTheoSoPhong(int soPhong)
        {
            List<ThongTinDP> danhSachDP = new List<ThongTinDP>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM tb_DatPhong WHERE IDPHONG = @IDPHONG";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@IDPHONG", soPhong);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    ThongTinDP dp = new ThongTinDP
                    {
                        IDDP = Convert.ToInt32(reader["IDDP"]),
                        IDKH = Convert.ToInt32(reader["IDKH"]),
                        IDPHONG = Convert.ToInt32(reader["IDPHONG"]),
                        NGAYDAT = reader["NGAYDAT"].ToString(),
                        NGAYTRA = reader["NGAYTRA"].ToString(),
                        SONGAYO = Convert.ToInt32(reader["SONGAYO"])
                    };
                    danhSachDP.Add(dp);
                }
            }

            return danhSachDP;
        }

        public List<CTDVDTO> GetAllDichVu()
        {
            List<CTDVDTO> dichVuList = new List<CTDVDTO>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT * FROM tb_CTDV";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            CTDVDTO dichVu = new CTDVDTO
                            {
                                IDCTDV = reader.GetInt32(reader.GetOrdinal("IDCTDV")),
                                IDPHONG= reader.GetInt32(reader.GetOrdinal("IDPHONG")),
                                TONGSODVDASUDUNG = reader.GetInt32(reader.GetOrdinal("TONGSODVDASUDUNG")),
                                TONGSOTIENDV = reader.GetInt32(reader.GetOrdinal("TONGSOTIENDV")),

                            };
                            dichVuList.Add(dichVu);
                        }
                    }
                }
            }

            return dichVuList;
        }
    }

}
