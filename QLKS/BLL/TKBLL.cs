using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;
using DAL;
namespace BLL
{
    public class TKBLL
    {
        TKAcess tkac = new TKAcess();
        public string CheckLogin(TaiKhoan tk)
        {
           //Kiểm tra nghiệp vụ
           if(tk.USERNAME == "")
            {
                return "Username required";
            }

           if(tk.PASSWD == "")
            {
                return "Password required";
            }

           string info = tkac.CheckLogin(tk);
            return info;
        }
    }
}
