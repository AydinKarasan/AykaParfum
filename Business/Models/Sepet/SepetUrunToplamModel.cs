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
        [DisplayName("Ürün Adý")]
        public string UrunAdi { get; set; }
        public double ToplamUrunFiyati { get; set; }
        [DisplayName("Toplam Ürün Fiyatý")]
        public string ToplamUrunFiyatiDisplay { get; set; }
        [DisplayName("Toplam Ürün Adedi")]
        public int ToplamUrunAdedi { get; set; }
    }
}
