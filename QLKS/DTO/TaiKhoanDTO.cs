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

        public DateTime NGAYSINH { get; set; }

        public string EMAIL { get; set; }

        public long SDT { get; set; }

        public long CCCD { get; set; }

        public string DIACHI { get; set; }
        public string USERNAME { get; set; }
        public string PASSWD { get; set; }
        public string MACTY { get; set; }
        public string MADVI { get; set; }
        public bool ISGROUP { get; set; }
        public bool DISABLED { get; set; }
        public int IDQUYEN { get; set; }

        public TaiKhoanDTO() { }


        public TaiKhoanDTO(int UID, string FULLNAME, string USERNAME,DateTime NGAYSINH, string EMAIL, int SDT, int CCCD, string DIACHI, string PASSWD, int IDQUYEN)
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
        }


    }
}
