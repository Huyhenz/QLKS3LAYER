using BLL;
using DevExpress.Internal;
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
    public partial class Form2 : Form
    {
        private taikhoanbus taikhoanBus;
        public Form2()
        {
            taikhoanBus = new taikhoanbus();
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            navBarGroup1.Caption = "Danh Mục"; 
            navBarGroup2.Caption = "Hệ Thống"; 

            AddItemsToNavBar(navBarGroup1, "DANHMUC"); 
            AddItemsToNavBar(navBarGroup2, "HETHONG");

        }
        private void AddItemsToNavBar(NavBarGroup navBarGroup, string parent)
        {
                List<FuncDTO> items = taikhoanBus.GetFuncItems(parent);
                foreach (FuncDTO item in items)
                {
                    NavBarItem navBarItem = new NavBarItem(item.DESCRIPTION)
                    {
                        Tag = item.FUNC_CODE
                    };
                    navBarGroup.ItemLinks.Add(navBarItem);
                }
            }
        }
    }

