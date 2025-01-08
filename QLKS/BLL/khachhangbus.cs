using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class khachhangbus
    {
        private KhachHangDAL khachHangDAL = new KhachHangDAL();
        public void AddCustomer(KhachHangDTO customer)
        {
            khachHangDAL.AddCustomer(customer);
        }

        public List<KhachHangDTO> GetCustomers()
        {
            return khachHangDAL.GetCustomers();
        }
    }
}
