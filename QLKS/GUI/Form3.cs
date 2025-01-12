using BLL;
using DTO;
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
    public partial class Form3 : Form
    {
        private TTvaHDbus bus = new TTvaHDbus();
        public Form3()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void Form3_Load(object sender, EventArgs e)
        {
            LoadData();
            List<Phong> roomDetails = bus.GetRoomDetails();
            dataGridView1.DataSource = roomDetails;
        }
        private void LoadData()
        {
            try
            {
                List<Phong> roomDetails = bus.GetRoomDetails();
                dataGridView2.DataSource = roomDetails;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải data");
            }


        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {

            int roomId;
            if (int.TryParse(txtFullname.Text, out roomId))
            {
                List<Phong> roomDetails = bus.SearchRoomDetails(roomId);
                dataGridView1.DataSource = roomDetails;
            }
            else
            {
                MessageBox.Show("Please enter a valid room ID.");
            }
        }


    }
}

