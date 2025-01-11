using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;

namespace DAL
{
    public class DichVuDal
    {
        private string connectionString = "Data Source=HUYCATMOI;Initial Catalog=QLKS;Integrated Security=True;Encrypt=True;TrustServerCertificate=True;";

        public DataTable GetAllPhong()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM tb_Phong";
                SqlCommand cmd = new SqlCommand(query, conn);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                var filteredRows = dt.AsEnumerable()
                    .Where(row => row.Field<string>("TINHTRANG") == "nhận phòng");

                if (filteredRows.Any())
                {
                    return filteredRows.CopyToDataTable();
                }
                else
                {
                    return new DataTable();
                }
            }
        }
        public DataTable GetPhongById(int maPhong)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM tb_Phong WHERE IDPHONG = @MaPhong";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@MaPhong", maPhong);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                var filteredRows = dt.AsEnumerable()
                    .Where(row => row.Field<string>("TINHTRANG") == "nhận phòng");

                if (filteredRows.Any())
                {
                    return filteredRows.CopyToDataTable();
                }
                else
                {
                    return new DataTable();
                }
            }
        }

        public DataTable GetAllDichVu()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM tb_DichVu";
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }
        public DataTable GetGiaDichVuById(int idDichVu)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT GIADV FROM tb_DichVu WHERE IDDV = @IdDichVu";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@IdDichVu", idDichVu);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                return dt;
            }
        }

        public void SaveRoomServiceData(int idPhong, int tongSoDVDaSuDung, int tongSoTienDV)
        {
            string query = "INSERT INTO tb_CTDV (IDPHONG, TONGSODVDASUDUNG, TONGSOTIENDV) " +
                           "VALUES (@IDPHONG, @TONGSODVDASUDUNG, @TONGSOTIENDV)";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@IDPHONG", idPhong);
                    cmd.Parameters.AddWithValue("@TONGSODVDASUDUNG", tongSoDVDaSuDung);
                    cmd.Parameters.AddWithValue("@TONGSOTIENDV", tongSoTienDV);

                    cmd.ExecuteNonQuery();
                }
            }
        }


        public List<DichvuDTO> GetAllServices()
        {
            List<DichvuDTO> services = new List<DichvuDTO>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM tb_DichVu"; // Tùy chỉnh truy vấn theo nhu cầu
                SqlCommand cmd = new SqlCommand(query, connection);
                connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    DichvuDTO service = new DichvuDTO
                    {
                        IDDV = reader.GetInt32(0),
                        TENDV = reader.GetString(1),
                        GIADV = reader.GetInt32(2),
                    };
                    services.Add(service);
                }
            }

            return services;
        }


    }
}
