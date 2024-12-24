using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class FuncDTO
    {
        public string FUNC_CODE { get; set; }
        public string DESCRIPTION { get; set; }

        public FuncDTO() { }


        public FuncDTO(string FUNC_CODE, string DESCRIPTION)
        {
            FUNC_CODE = FUNC_CODE;
            DESCRIPTION = DESCRIPTION;

        }




    }
}
