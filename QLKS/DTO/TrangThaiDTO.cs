using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class TrangThaiDTO
    {
        public int IDTT { get; set; }

        public string TRANGTHAI { get; set; }

        public TrangThaiDTO() { }

        public TrangThaiDTO(int IDTT, string TRANGTHAI)
        {
            IDTT = IDTT;
            TRANGTHAI = TRANGTHAI;
        }
    }
}
