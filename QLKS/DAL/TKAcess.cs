using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;
using System.Data.SqlClient;
namespace DAL
{
    public class TKAcess : ConnectDB
    {
        public string CheckLogin(TaiKhoan tk)
        {
            string info = CheckLoginDTO(tk);
            return info;
        }
    }
}
