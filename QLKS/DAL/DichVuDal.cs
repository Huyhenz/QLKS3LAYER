using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DichVuDal
    {
        private string connectionString = "Data Source=MSI;Initial Catalog=QLKS;Integrated Security=True;Encrypt=True;TrustServerCertificate=True;";

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
                    .Where(row => row.Field<string>("TINHTRANG") == "Nhận Phòng");

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
                    .Where(row => row.Field<string>("TINHTRANG") == "Nhận Phòng");

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
    }
}
