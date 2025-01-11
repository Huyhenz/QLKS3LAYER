using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class HoaDonDTO
    {
        public int IDHD { get; set; }

        public int IDDP { get; set; }

        public int IDKH { get; set; }

        public int IDPHONG { get; set; }

        public int IDLOAIPHONG { get; set; }

        public int UID { get; set; }

        public int IDDV { get; set; }

        public int IDCTDV { get; set; }

        public Phong phong { get; set; }

        public LoaiPhongDTO loaiphong { get; set; }

        public KhachHangDTO khachhang { get; set; }

        public ThongTinDP thongtindp { get; set; }

        public DichvuDTO dichvu { get; set; }

        public CTDVDTO ct { get; set; }

        public TaiKhoanDTO taikhoan { get; set; }

        public int TONGBILL { get; set; }
        public HoaDonDTO() { }

        public HoaDonDTO(int IDHD, int IDDP, int IDKH, int IDPHONG, int IDLOAIPHONG, int UID, int IDDV, int IDCTDV, Phong PHONG, LoaiPhongDTO LOAIPHONG, KhachHangDTO KHACHHANG, ThongTinDP THONGTINDP, DichvuDTO DICHVU, CTDVDTO CT, TaiKhoanDTO TAIKHOAN, int TONGBILL)
        {
            IDHD = IDHD;
            IDDP = IDDP;
            IDKH = IDKH;
            IDPHONG = IDPHONG;
            IDLOAIPHONG = IDLOAIPHONG;
            UID = UID;
            IDDV = IDDV;
            IDCTDV = IDCTDV;
            PHONG = PHONG;
            LOAIPHONG = LOAIPHONG;
            KHACHHANG = KHACHHANG;
            THONGTINDP = THONGTINDP;
            DICHVU = DICHVU;
            CT = CT;
            TAIKHOAN = TAIKHOAN;
            TONGBILL = TONGBILL;
        }
    }
}
