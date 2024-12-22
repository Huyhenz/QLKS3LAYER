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
        private SqlConnection connection;

        private string connectionstring = @"Data Source=MSI;Initial Catalog=KhachSan;Integrated Security=True;TrustServerCertificate=True;";
        public ConnectionDB()
        {
            connection = new SqlConnection(connectionstring);
        }

        public void OpenConnection()
        {
            if (connection.State == System.Data.ConnectionState.Closed)
            {
                try
                {
                    connection.Open();
                    Console.WriteLine("Kết nối thành công!");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Lỗi kết nối: " + ex.Message);
                }
            }
        }
        public void CloseConnection()
        {
            if (connection.State == System.Data.ConnectionState.Open)
            {
                connection.Close();
            }
        }

        public SqlConnection GetConnection()
        {
            return connection;
        }
    }
}
