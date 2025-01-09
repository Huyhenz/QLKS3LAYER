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
            
        public int IDTANG { get; set; }

        public int IDLOAIPHONG { get; set; }

        public LoaiPhongDTO LoaiPhong { get; set; }

        public int IDTT { get; set; }

        public Phong() { }

        public Phong(int IDPHONG, string TENPHONG, string IDTANG, string IDLOAIPHONG, LoaiPhongDTO LOAIPHONG, int IDTT)
        {
            IDPHONG = IDPHONG;
            TENPHONG = TENPHONG;
            IDTT = IDTT;
            IDTANG = IDTANG;
            IDLOAIPHONG = IDLOAIPHONG;
            LOAIPHONG = LOAIPHONG;
        }
    }
}
