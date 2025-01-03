using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class TaiKhoanDTO
    {
        public int UID { get; set; }
        public string FULLNAME { get; set; }

        public string NGAYSINH { get; set; }

        public string EMAIL { get; set; }

        public long SDT { get; set; }

        public long CCCD { get; set; }

        public string GIOITINH { get; set; }

        public string DIACHI { get; set; }
        public string USERNAME { get; set; }
        public string PASSWD { get; set; }
        public int IDQUYEN { get; set; }

        public string NGAYVAOLAM { get; set; }

        public string PHOTO { get; set; }

        
        public TaiKhoanDTO() { }


        public TaiKhoanDTO(int UID, string FULLNAME, string USERNAME,string NGAYSINH, string EMAIL, int SDT, int CCCD, string DIACHI, string PASSWD, int IDQUYEN, string NGAYVAOLAM, string GIOITINH, string PHOTO)
        {
            UID = UID;
            FULLNAME = FULLNAME;
            NGAYSINH = NGAYSINH;
            EMAIL = EMAIL;
            SDT = SDT;
            CCCD = CCCD;
            DIACHI = DIACHI;
            USERNAME = USERNAME;
            PASSWD = PASSWD;
            IDQUYEN = IDQUYEN;
            NGAYVAOLAM = NGAYVAOLAM;
            GIOITINH = GIOITINH;
            PHOTO = PHOTO;
        }


    }
}
