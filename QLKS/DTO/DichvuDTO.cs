using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class DichvuDTO
    {
        public int IDDV { get; set; }

        public string TENDV { get; set; }

        public int GIADV { get; set; }

        public int SOLUONG { get; set; }

        public int TONGTIEN { get; set; }

        public int IDPHONG { get; set; }

        public Phong phong { get; set; }

        public DichvuDTO() { }

        public DichvuDTO(int IDDV, string TENDV, int GIADV, int SOLUONG, int TONGTIEN, int IDPHONG, Phong PHONG)
        {
            IDDV = IDDV;
            TENDV = TENDV;
            GIADV = GIADV;
            SOLUONG = SOLUONG;
            TONGTIEN = TONGTIEN;
            IDPHONG = IDPHONG;
            PHONG = PHONG;
        }

    }
}
