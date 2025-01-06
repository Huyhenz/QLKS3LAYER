using BLL;
using DTO;
using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUI
{
    public partial class Form1 : Form
    {
        private Phongbus phongbus = new Phongbus();

        public Form1()
        {
            InitializeComponent();
            //LoadPhongButtons();
            Guna2Button btn = new Guna2Button();
            btn.Click += new EventHandler(guna2Button2_Click);
            btn.Click += new EventHandler(guna2Button37_Click);
            btn.Click += new EventHandler(guna2Button45_Click);
        }

        /*private void LoadPhongButtons()
        {
            var rooms = phongbus.GetRooms();

            foreach (var room in rooms)
            {
                Guna2Button btn = new Guna2Button();
                btn.Text = room.IDPHONG.ToString();
                btn.Name = $"btnRoom{room.IDPHONG}";
                
                this.Controls.Add(btn);
            }
        }*/



        private void guna2Button2_Click(object sender, EventArgs e)
        {
            Guna2Button btn = sender as Guna2Button;
            int roomId = int.Parse(btn.Text);
            Form4 form4 = new Form4(roomId);
            form4.Show();
        }




        private void guna2Button37_Click(object sender, EventArgs e)
        {
            Guna2Button btn = sender as Guna2Button;
            int roomId = int.Parse(btn.Text);
            Form4 form4 = new Form4(roomId);
            form4.Show();
        }

        private void guna2Button45_Click(object sender, EventArgs e)
        {
            Guna2Button btn = sender as Guna2Button;
            int roomId = int.Parse(btn.Text);
            Form4 form4 = new Form4(roomId);
            form4.Show();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
                
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            Form11 f = new Form11();
            f.Show();
        }
    }
}
