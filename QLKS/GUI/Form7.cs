using BLL;
using DevExpress.XtraNavBar;
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
    public partial class Form7 : Form
    {
        private taikhoanbus taikhoanBus;
        public Form7()
        {
            taikhoanBus = new taikhoanbus();
            InitializeComponent();
        }

        private void Form7_Load(object sender, EventArgs e)
        {
            navBarGroup1.Caption = "Danh Mục";
            navBarGroup2.Caption = "Hệ Thống";
            navBarGroup3.Caption = "Thiết Bị";

            AddItemsToNavBar(navBarGroup1, "DANHMUC");
            AddItemsToNavBar(navBarGroup2, "HETHONG");

            
                navBarGroup1.SmallImageIndex = 0;
                navBarGroup2.SmallImageIndex = 0;
                navBarGroup3.SmallImageIndex = 0;
            
        }


        private void AddItemsToNavBar(NavBarGroup navBarGroup, string parent)
        {
            List<FuncDTO> items = taikhoanBus.GetFuncItems(parent);
            int imageIndex = 0;
            foreach (FuncDTO item in items)
            {
                NavBarItem navBarItem = new NavBarItem(item.DESCRIPTION)
                {
                    Tag = item.FUNC_CODE,
                    SmallImageIndex = imageIndex

                };

                navBarGroup.ItemLinks.Add(navBarItem);
                imageIndex = (imageIndex + 1) % imageList1.Images.Count;
            }
        }

        private void navMain_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            string func_code = e.Link.Item.Tag.ToString();
            MessageBox.Show(func_code);
        }
    }
}
