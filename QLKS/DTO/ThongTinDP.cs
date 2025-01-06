using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class ThongTinDP
    {
        public int IDDP { get; set; }

        public int IDKH { get; set; }
        public int IDPHONG { get; set; }

        public string NGAYDAT { get; set; }

        public string NGAYTRA { get; set; }

        public int SONGAYO { get; set; }


        public ThongTinDP()
        {

        }

        public ThongTinDP(int IDDP, int IDKH, int IDPHONG, string NGAYDAT, string NGAYTRA, int SONGAYO)
        {
            IDDP = IDDP;
            IDKH = IDKH;
            IDPHONG = IDPHONG;
            NGAYDAT = NGAYDAT;
            NGAYTRA = NGAYTRA;
            SONGAYO = SONGAYO;


        }



    }
}
