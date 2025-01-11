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
    public class TTvaHDbus
    {
        private ThanhToanVAHoaDondal t = new ThanhToanVAHoaDondal();

        public List<Phong> GetRoomDetails()
        {
            // Gọi DAL để lấy dữ liệu và trả về
            return t.GetRoomDetails();
        }
    }
}
