using DTO;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace DAL
{
    public class ThanhToanVAHoaDondal
    {
        private string connectionString = "Data Source=HUYCATMOI;Initial Catalog=QLKS;Integrated Security=True;Encrypt=True;TrustServerCertificate=True;";

        public List<Phong> GetRoomDetails(int? maPhong = null)
        {
            List<Phong> chiTietPhong = new List<Phong>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string truyVan = @"
            SELECT 
                p.IDPHONG, p.TENPHONG, p.IDTANG, p.IDLOAIPHONG, p.TINHTRANG,
                kh.IDKH, kh.HOTEN, kh.CCCD, kh.DIENTHOAI, kh.EMAIL, kh.DIACHI, kh.NGAYSINH, kh.LOAIKH, kh.GHICHU,
                dv.IDDV, dv.TENDV, dv.GIADV
            FROM tb_Phong p
            LEFT JOIN tb_DatPhong dp ON p.IDPHONG = dp.IDPHONG
            LEFT JOIN tb_KhachHang kh ON dp.IDKH = kh.IDKH
            LEFT JOIN tb_CTDV ct on p.IDPHONG = ct.IDPHONG
            LEFT JOIN tb_Dichvu dv ON ct.IDDV = dv.IDDV 
            ";
            
                if (maPhong.HasValue)
                {
                    truyVan += " and p.IDPHONG = @maPhong";
                }

                using (SqlCommand cmd = new SqlCommand(truyVan, conn))
                {
                    if (maPhong.HasValue)
                    {
                        cmd.Parameters.AddWithValue("@maPhong", maPhong.Value);
                    }

                    using (SqlDataReader docGia = cmd.ExecuteReader())
                    {
                        while (docGia.Read())
                        {
                            Phong phong = new Phong
                            {
                                IDPHONG = docGia.GetInt32(docGia.GetOrdinal("IDPHONG")),
                                TENPHONG = docGia.GetString(docGia.GetOrdinal("TENPHONG")),
                                IDTANG = docGia.GetInt32(docGia.GetOrdinal("IDTANG")),
                                IDLOAIPHONG = docGia.GetInt32(docGia.GetOrdinal("IDLOAIPHONG")),
                                TINHTRANG = docGia.GetString(docGia.GetOrdinal("TINHTRANG"))
                            };

                            KhachHangDTO khachHang = null;
                            if (!docGia.IsDBNull(docGia.GetOrdinal("IDKH")))
                            {
                                khachHang = new KhachHangDTO
                                {
                                    IDKH = docGia.GetInt32(docGia.GetOrdinal("IDKH")),
                                    HOTEN = docGia.GetString(docGia.GetOrdinal("HOTEN")),
                                    CCCD = docGia.GetInt64(docGia.GetOrdinal("CCCD")),
                                    DIENTHOAI = docGia.GetInt64(docGia.GetOrdinal("DIENTHOAI")),
                                    EMAIL = docGia.GetString(docGia.GetOrdinal("EMAIL")),
                                    //DIACHI = docGia.GetString(docGia.GetOrdinal("DIACHI")),
                                    NGAYSINH = docGia.GetDateTime(docGia.GetOrdinal("NGAYSINH")).ToString("yyyy-MM-dd"),
                                    LOAIKH = docGia.GetString(docGia.GetOrdinal("LOAIKH")),
                                    GHICHU = docGia.GetString(docGia.GetOrdinal("GHICHU"))
                                };
                            }

                            DichvuDTO dichVu = null;
                            if (!docGia.IsDBNull(docGia.GetOrdinal("IDDV")))
                            {
                                dichVu = new DichvuDTO
                                {
                                    IDDV = docGia.GetInt32(docGia.GetOrdinal("IDDV")),
                                    TENDV = docGia.GetString(docGia.GetOrdinal("TENDV")),
                                    GIADV = docGia.GetInt32(docGia.GetOrdinal("GIADV")),
                                    SOLUONG = docGia.IsDBNull(docGia.GetOrdinal("SOLUONG")) ? 0 : docGia.GetInt32(docGia.GetOrdinal("SOLUONG")),
                                    TONGTIEN = docGia.IsDBNull(docGia.GetOrdinal("TONGTIEN")) ? 0 : docGia.GetInt32(docGia.GetOrdinal("TONGTIEN")),
                                    IDPHONG = docGia.IsDBNull(docGia.GetOrdinal("IDPHONG")) ? 0 : docGia.GetInt32(docGia.GetOrdinal("IDPHONG"))
                                };
                            }

                            chiTietPhong.Add(phong);
                        }
                    }
                }
            }

            return chiTietPhong;
        }


    }
}
