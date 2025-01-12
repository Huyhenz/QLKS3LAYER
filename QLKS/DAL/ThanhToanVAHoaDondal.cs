using DTO;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace DAL
{
    public class ThanhToanVAHoaDondal
    {
        private string connectionString = "Data Source=MSI;Initial Catalog=QLKS;Integrated Security=True;Encrypt=True;TrustServerCertificate=True;";

        public List<Phong> GetRoomDetails(int? roomId = null)
        {
            List<Phong> roomDetails = new List<Phong>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = @"
            SELECT 
                p.IDPHONG, p.TENPHONG, p.IDTANG, p.IDLOAIPHONG, p.TINHTRANG,
                kh.IDKH, kh.HOTEN, kh.CCCD, kh.DIENTHOAI, kh.EMAIL, kh.DIACHI, kh.NGAYSINH, kh.LOAIKH, kh.GHICHU,
                dv.IDDV, dv.TENDV, dv.GIADV, dv.SOLUONG, dv.TONGTIEN, dv.IDPHONG
            FROM tb_Phong p
            LEFT JOIN tb_KhachHang kh ON p.IDPHONG = kh.IDPHONG
            LEFT JOIN tb_Dichvu dv ON p.IDPHONG = dv.IDPHONG
        ";

                if (roomId.HasValue)
                {
                    query += " WHERE p.IDPHONG = @roomId";
                }

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    if (roomId.HasValue)
                    {
                        cmd.Parameters.AddWithValue("@roomId", roomId.Value);
                    }

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            // Khởi tạo đối tượng Phong
                            Phong phong = new Phong
                            {
                                IDPHONG = reader.GetInt32(reader.GetOrdinal("IDPHONG")),
                                TENPHONG = reader.GetString(reader.GetOrdinal("TENPHONG")),
                                IDTANG = reader.GetInt32(reader.GetOrdinal("IDTANG")),
                                IDLOAIPHONG = reader.GetInt32(reader.GetOrdinal("IDLOAIPHONG")),
                                TINHTRANG = reader.GetString(reader.GetOrdinal("TINHTRANG"))
                            };

                            // Khởi tạo đối tượng KhachHangDTO (nếu dữ liệu tồn tại)
                            KhachHangDTO khachHang = null;
                            if (!reader.IsDBNull(reader.GetOrdinal("IDKH")))
                            {
                                khachHang = new KhachHangDTO
                                {
                                    IDKH = reader.GetInt32(reader.GetOrdinal("IDKH")),
                                    HOTEN = reader.GetString(reader.GetOrdinal("HOTEN")),
                                    CCCD = reader.GetInt64(reader.GetOrdinal("CCCD")),
                                    DIENTHOAI = reader.GetInt64(reader.GetOrdinal("DIENTHOAI")),
                                    EMAIL = reader.GetString(reader.GetOrdinal("EMAIL")),
                                    DIACHI = reader.GetString(reader.GetOrdinal("DIACHI")),
                                    NGAYSINH = reader.GetDateTime(reader.GetOrdinal("NGAYSINH")).ToString("yyyy-MM-dd"),
                                    LOAIKH = reader.GetString(reader.GetOrdinal("LOAIKH")),
                                    GHICHU = reader.GetString(reader.GetOrdinal("GHICHU"))
                                };
                            }

                            // Khởi tạo đối tượng DichvuDTO (nếu dữ liệu tồn tại)
                            DichvuDTO dichVu = null;
                            if (!reader.IsDBNull(reader.GetOrdinal("IDDV")))
                            {
                                dichVu = new DichvuDTO
                                {
                                    IDDV = reader.GetInt32(reader.GetOrdinal("IDDV")),
                                    TENDV = reader.GetString(reader.GetOrdinal("TENDV")),
                                    GIADV = reader.GetInt32(reader.GetOrdinal("GIADV")),
                                    SOLUONG = reader.GetInt32(reader.GetOrdinal("SOLUONG")),
                                    TONGTIEN = reader.GetInt32(reader.GetOrdinal("TONGTIEN")),
                                    IDPHONG = reader.GetInt32(reader.GetOrdinal("IDPHONG"))
                                };
                            }

                            // Thêm dữ liệu vào danh sách (tuỳ ý kết hợp KhachHangDTO và DichvuDTO vào Phong)
                            roomDetails.Add(phong);
                        }
                    }
                }
            }

            return roomDetails;
        }

    }
}
