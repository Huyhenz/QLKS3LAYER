using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class CTDVDTO
    {
        public int IDCTDV { get; set; }

        public int IDDV { get; set; }

        public int IDPHONG { get; set; }

        public int TONGSODVDASUDUNG { get; set; }

        public int TONGSOTIENDV { get; set; }

        public Phong phong { get; set; }

        public DichvuDTO dichvu { get; set; }

        public CTDVDTO() { }

        public CTDVDTO(int IDCTDV, int IDDV, int IDPHONG, int TONGSODVDASUDUNG, int TONGSOTIENDV, Phong PHONG, DichvuDTO DICHVU)
        {
            IDCTDV = IDCTDV;
            IDDV = IDDV;
            IDPHONG = IDPHONG;
            TONGSODVDASUDUNG = TONGSODVDASUDUNG;
            TONGSOTIENDV = TONGSOTIENDV;
            PHONG = PHONG;
            DICHVU = DICHVU;
        }
    }
}
