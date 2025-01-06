using BLL;
using DTO;
using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUI
{
    public partial class Form4 : Form
    {

        private Phongbus phongbus = new Phongbus();
        public Form4(int roomId)
        {

            InitializeComponent();
            SetRoomID(roomId);

        }

        private void guna2GroupBox1_Click(object sender, EventArgs e)
        {

        }

        private void guna2GroupBox3_Click(object sender, EventArgs e)
        {

        }

        private void Form4_Load(object sender, EventArgs e)
        {

        }

        public void SetRoomID(int roomId)
        {
            string connectionString = "Data Source=HUYCATMOI;Initial Catalog=QLKS;Integrated Security=True;Encrypt=True;TrustServerCertificate=True;";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT IDPHONG, TENLOAIPHONG, DONGIA FROM tb_Phong p join tb_LoaiPhong l on p.IDLOAIPHONG = l.IDLOAIPHONG WHERE IDPHONG = @IDPHONG";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@IDPHONG", roomId);

                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    txtSP.Text = reader["IDPHONG"].ToString();
                    txtLP.Text = reader["TENLOAIPHONG"].ToString();
                    txtGIA.Text = reader["DONGIA"].ToString();// Gán giá trị cho textbox
                }
            }
        }
    }
}
