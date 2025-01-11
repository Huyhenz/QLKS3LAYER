using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
        public class ThanhToanDTO
        {
            public int IDPHONG { get; set; }
            public int IDKH { get; set; }
            public string HOTEN { get; set; }
            public string TENLOAIPHONG { get; set; }
            public int DONGIA { get; set; }
            public DateTime NGAYDEN { get; set; }
            public DateTime NGAYDI { get; set; }
            public int TONGSONGAYO { get; set; }
            public int TONGSODVDASUDUNG { get; set; }
            public int TONGSOTIENDV { get; set; }

        public ThanhToanDTO() { }

        public ThanhToanDTO(int IDPHONG, int IDKH, string HOTEN, string TENLOAIPHONG, int DONGIA, DateTime NGAYDEN, DateTime NGAYDI, int TONGSONGAYO, int TONGSODVDASUDUNG, int TONGSOTIENDV)
        {
            IDPHONG = IDPHONG;
            IDKH = IDKH;
            HOTEN = HOTEN;
            TENLOAIPHONG = TENLOAIPHONG;
            DONGIA = DONGIA;
            NGAYDEN = NGAYDEN;
            NGAYDI = NGAYDI;
            TONGSONGAYO = TONGSONGAYO;
            TONGSODVDASUDUNG = TONGSODVDASUDUNG;
            TONGSOTIENDV = TONGSOTIENDV;
        }

    }
    }

