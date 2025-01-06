using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class LoaiPhongDTO
    {
        public int IDLOAIPHONG { get; set; }

        public string TENLOAIPHONG { get; set; }

        public int DONGIA { get; set; }
        
        public int SONGUOI { get; set; }

        public int SOGIUONG { get; set; }

        public LoaiPhongDTO() { }

        public LoaiPhongDTO(int IDLOAIPHONG, string TENLOAIPHONG, float DONGIA, int SONGUOI, int SOGIUONG)
        {
            IDLOAIPHONG = IDLOAIPHONG;
            TENLOAIPHONG = TENLOAIPHONG;
            DONGIA = DONGIA;
            SONGUOI = SONGUOI;
            SOGIUONG = SOGIUONG;
        }
    }
}
