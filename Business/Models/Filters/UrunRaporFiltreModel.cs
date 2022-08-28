using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Models.Filters
{
    public class UrunRaporFiltreModel
    {
        [DisplayName("Ürün Adý")]
        public string UrunAdi { get; set; }
        [DisplayName("Kategori")]
        public int? KategoriId { get; set; }
        [DisplayName("Birim Fiyatý")]
        public double? BirimFiyatiMininmum { get; set; }
        public double? BirimFiyatiMaximum { get; set; }        
        [DisplayName("Marka")]
        public List<int> MarkaIdleri { get; set; }
        [DisplayName("Kampanya")]
        public List<int> KampanyaIdleri { get; set; }
        public string UrunKampanyaAdi { get; set; }

    }
}
