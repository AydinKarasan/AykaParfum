using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Models.Sepet
{
    public class SepetUrunToplamModel
    {
        public int UrunId { get; set; }
        public int KullaniciId { get; set; }
        [DisplayName("�r�n Ad�")]
        public string UrunAdi { get; set; }
        public double ToplamUrunFiyati { get; set; }
        [DisplayName("Toplam �r�n Fiyat�")]
        public string ToplamUrunFiyatiDisplay { get; set; }
        [DisplayName("Toplam �r�n Adedi")]
        public int ToplamUrunAdedi { get; set; }
    }
}
