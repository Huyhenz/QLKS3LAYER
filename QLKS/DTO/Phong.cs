using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class Phong
    {
        public int IDPHONG { get; set; }

        public string TENPHONG { get; set; }

        public string IDTANG { get; set; }

        public string IDLOAIPHONG { get; set; }

        public Phong() { }

        public Phong(int IDPHONG, string TENPHONG, string IDTANG, string IDLOAIPHONG)
        {
            IDPHONG = IDPHONG;
            TENPHONG = TENPHONG;
            IDTANG = IDTANG;
            IDLOAIPHONG = IDLOAIPHONG;
        }
    }
}
