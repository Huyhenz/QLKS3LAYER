using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;
using DAL;
namespace BLL
{
    public class taikhoanbus
    {
        private taikhoandal tkd;

        public taikhoanbus()
        {
            tkd = new taikhoandal();
        }

        // Lấy danh sách tất cả tài khoản
        public List<TaiKhoanDTO> GetAllTaiKhoan()
        {
            try
            {
                return tkd.GetAllTaiKhoan();
            }
            catch (Exception ex)
            {
                // Xử lý lỗi
                throw new Exception("Lỗi khi lấy danh sách tài khoản: " + ex.Message);
            }
        }

        // Thêm tài khoản mới
        public bool AddTaiKhoan(TaiKhoanDTO taiKhoan)
        {
            try
            {
                return tkd.AddTaiKhoan(taiKhoan);
            }
            catch (Exception ex)
            {
                // Xử lý lỗi
                throw new Exception("Lỗi khi thêm tài khoản: " + ex.Message);
            }
        }

        // Xóa tài khoản theo ID
        public bool DeleteTaiKhoan(int idTaiKhoan)
        {
            try
            {
                return tkd.DeleteTaiKhoan(idTaiKhoan);
            }
            catch (Exception ex)
            {
                // Xử lý lỗi
                throw new Exception("Lỗi khi xóa tài khoản: " + ex.Message);
            }
        }

        // Cập nhật thông tin tài khoản
        public bool UpdateTaiKhoan(TaiKhoanDTO taiKhoan)
        {
            try
            {
                return tkd.UpdateTaiKhoan(taiKhoan);
            }
            catch (Exception ex)
            {
                // Xử lý lỗi
                throw new Exception("Lỗi khi cập nhật tài khoản: " + ex.Message);
            }
        }

        // Lấy tài khoản theo ID
        public TaiKhoanDTO GetTaiKhoanById(int idTaiKhoan)
        {
            try
            {
                return tkd.GetTaiKhoanById(idTaiKhoan);
            }
            catch (Exception ex)
            {
                // Xử lý lỗi
                throw new Exception("Lỗi khi lấy tài khoản theo ID: " + ex.Message);
            }
        }

        public bool Login(string tenNguoiDung, string matKhau)
        {
            try
            {
                return tkd.Login(tenNguoiDung, matKhau);
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi đăng nhập: " + ex.Message);
            }
        }

    }
}
