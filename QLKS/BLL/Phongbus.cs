using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class Phongbus
    { 
            private PhongDAL phongDAL = new PhongDAL();

            public List<Phong> GetAllPhong()
            {
                return phongDAL.GetPhongList();
            }

            public void AddPhong(Phong phong)
            {
                phongDAL.AddPhong(phong);
            }

            public void UpdatePhong(Phong phong)
            {
                phongDAL.UpdatePhong(phong);
            }

            public Phong GetPhongByID(int ID)
            {
            return phongDAL.GetPhongById(ID);
            }
            public Phong SetRoomIDD(int roomId)
            {
            return phongDAL.SetRoomIDD(roomId);
            }
            public void AddBooking(ThongTinDP booking)
            {
            phongDAL.AddBooking(booking);
            }

    }

        public class BookingBLL
        {
            private BookingDAL bookingDAL = new BookingDAL();

            public void BookRoom(ThongTinDP booking)
            {
                bookingDAL.SaveBooking(booking);
            }
        }

    }
