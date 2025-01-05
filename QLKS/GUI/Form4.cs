using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUI
{
    public partial class Form4 : Form
    {
        private int roomId;
        public Form4(int roomId)
        {

            InitializeComponent();
            this.roomId = roomId;
            LoadRoomDetails();
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
        private void LoadRoomDetails()
        {
            // Hiển thị chi tiết phòng dựa trên roomId
            txtSP.Text = roomId.ToString();
        }
    }
}
