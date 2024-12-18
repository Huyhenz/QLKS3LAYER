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
        public string USERNAME { get; set; }
        public string PASSWD { get; set; }
        public string MACTY { get; set; }
        public string MADVI { get; set; }
        public string ISGROUP { get; set; }
        public string DISABLED { get; set; }
        public int IDQUYEN { get; set; }

        public TaiKhoanDTO() { }


        public TaiKhoanDTO(int id, string FULLNAME, string USERNAME, string MATKHAU, string IDQUYEN)
        {
            UID = id;
            FULLNAME = FULLNAME;
            USERNAME = USERNAME;
            PASSWD = PASSWD;
            IDQUYEN = IDQUYEN;
        }


    }
}
