using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
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
        public List<ThongTinDP> GetBookings()
        {
            return phongDAL.GetBookings();
        }
        public void UpdateBooking(ThongTinDP booking)
        {
            phongDAL.UpdateBooking(booking);
        }
        public void DeleteBooking(int idPhong, int idKhachHang)
        {
            phongDAL.DeleteBooking(idPhong, idKhachHang);
        }

        public void UpdateTinhTrangPhong(int idPhong, string tinhTrang)
        {
            phongDAL.UpdateTinhTrangPhong(idPhong, tinhTrang);
        }
        public void UpdateTinhTrangPhongsaukhidat(string tinhtrang)
        {
            phongDAL.UpdateTinhTrangPhongsaukhidat(tinhtrang);
        }

        public void UpdateRoomBooking(int oldRoomId, int newRoomId)
        {
            phongDAL.UpdateRoomBooking(oldRoomId, newRoomId);
        }


        public List<Phong> GetRooms()
        {
            return phongDAL.GetRooms();
        }
        public List<Phong> GetAllRooms()
        {
            return phongDAL.GetAllRooms();
        }
    }
}
