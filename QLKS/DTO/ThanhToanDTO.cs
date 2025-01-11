using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class ThanhToanDTO
    {
        public int IDTT { get; set; }

        public int IDKH { get; set; }

        public int IDPHONG { get; set; }

        public int IDLOAIPHONG { get; set; }

        public int IDDP { get; set; }
        public int IDDV { get; set; }

        public KhachHangDTO khachHang { get; set; }

        public Phong phong { get; set; }

        public LoaiPhongDTO loaiPhong { get; set; }

        public ThongTinDP datphong { get; set; }

        public DichvuDTO dichvu { get; set; }

        public ThanhToanDTO() { }

        public ThanhToanDTO(int IDTT, int IDKH, int IDPHONG, int IDLOAIPHONG, int IDDP, int IDDV, KhachHangDTO KHACHHANG, Phong PHONG, LoaiPhongDTO LOAIPHONG, ThongTinDP DATPHONG, DichvuDTO DICHVU)
        {
            IDTT = IDTT;
            IDKH = IDKH;
            IDPHONG = IDPHONG;
            IDLOAIPHONG = IDLOAIPHONG;
            IDDP = IDDP;
            IDDV = IDDV;
            KHACHHANG = KHACHHANG;
            PHONG = PHONG;
            LOAIPHONG = LOAIPHONG;
            DATPHONG = DATPHONG;
            DICHVU = DICHVU;
        }
    }
}
