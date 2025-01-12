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
            private ThanhToanVAHoaDondal _dataAccess = new ThanhToanVAHoaDondal();

            public List<Phong> GetRoomDetails(int? roomId = null)
            {
                return _dataAccess.GetRoomDetails(roomId);
            }

          
        }
    }




