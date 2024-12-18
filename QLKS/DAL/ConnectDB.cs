using DTO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
namespace DAL
{
    public class ConnectionDB
    {
        //Tạo chuỗi kết nối database
        public static SqlConnection Connect()
        {
            string strcon = @"Data Source=HUYCATMOI;Initial Catalog=QLKS;Integrated Security=True;Encrypt=True;TrustServerCertificate=True;";
            SqlConnection cn = new SqlConnection(strcon); //Khởi tạo connect
            return cn;
        }
    }
    public class ConnectDB
    {
        public static string CheckLoginDTO(TaiKhoan tk)
        {
            string user = null;
            //Hàm connect tới CSDL
            SqlConnection conn = ConnectionDB.Connect();
            conn.Open();
            SqlCommand command = new SqlCommand("proc_login", conn);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@user", tk.USERNAME);
            command.Parameters.AddWithValue("@pass", tk.PASSWD);
            //Kiểm tra quyền khi thêm 1 parameter..
            command.Connection = conn;
            SqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    if (reader.GetFieldType(0) == typeof(string)) 
                    { 
                        user = reader.GetString(0);
                    } 
                    else 
                    { 
                        user = Convert.ToString(reader.GetValue(0)); 
                    }
                }
                reader.Close();
                conn.Close();

            }
            else
            {
                return "Tài khoản hoặc mật khẩu không chính xác";
            }
            return user;
        }
    }       
}
