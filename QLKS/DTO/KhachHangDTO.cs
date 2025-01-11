using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class KhachHangDTO
    {
        public int IDKH { get; set; }
        public string HOTEN { get; set; }   

        public long CCCD { get; set; }

        public long DIENTHOAI { get; set; } 

        public string EMAIL { get; set; }

        public string DIACHI { get; set; }

        public string NGAYSINH { get; set; }    

        public string LOAIKH { get; set; }

        public string GHICHU { get; set; }


        public KhachHangDTO() { }

        public KhachHangDTO(int IDKH, string HOTEN, long CCCD, long DIENTHOAI, string EMAIL, string DIACHI, string NGAYSINH, string LOAIKH, string GHICHU)
        {
            IDKH = IDKH;
            HOTEN = HOTEN;
            CCCD = CCCD;
            DIENTHOAI = DIENTHOAI;
            EMAIL = EMAIL;
            DIACHI = DIACHI;
            NGAYSINH = NGAYSINH;
            LOAIKH = LOAIKH;
            GHICHU = GHICHU;
        }
    }
}
