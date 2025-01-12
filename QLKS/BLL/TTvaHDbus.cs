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
            private ThanhToanVAHoaDondal dpDAL = new ThanhToanVAHoaDondal();

            // Lấy danh sách đặt phòng
            public List<ThongTinDP> GetDanhSachDatPhong()
            {
                return dpDAL.GetDanhSachDatPhong();
            }

            // Tìm kiếm danh sách đặt phòng theo số phòng
            public List<ThongTinDP> TimKiemDatPhongTheoSoPhong(int soPhong)
            {
                return dpDAL.TimKiemDatPhongTheoSoPhong(soPhong);
            }
        }
    }





