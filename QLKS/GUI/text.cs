using Guna.UI2.WinForms;
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
    public partial class text : Form
    {
        public text()
        {
            InitializeComponent();
        }

        private void text_Load(object sender, EventArgs e)
        {
            Guna2Button btn = new Guna2Button();
            btn.FillColor = Color.White;
        }
    }
}
