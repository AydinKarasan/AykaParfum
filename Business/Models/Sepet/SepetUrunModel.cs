using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Models.Sepet
{
    public class SepetUrunModel
    {
        public int UrunId { get; set; }
        public int KullaniciId { get; set; }
        [DisplayName("�r�n Ad�")]
        public string UrunAdi { get; set; }
        [DisplayName("Birim Fiyat�")]
        public double BirimFiyati { get; set; }
    }
}
