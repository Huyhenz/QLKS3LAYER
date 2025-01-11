using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class DichVuBus
    {
        private DichVuDal dichvuDAL;

        public DichVuBus()
        {
            dichvuDAL = new DichVuDal();
        }

        public DataTable GetAllPhong()
        {
            return dichvuDAL.GetAllPhong();
        }
        public DataTable GetPhongById(int maPhong)
        {
            return dichvuDAL.GetPhongById(maPhong);
        }
        public DataTable GetAllDichVu()
        {
            return dichvuDAL.GetAllDichVu();
        }
    }
}
