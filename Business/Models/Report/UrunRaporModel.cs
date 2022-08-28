using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Models.Report
{
    public class UrunRaporModel
    {
        [DisplayName("Ürün Adý")]
        public string urunAdi { get; set; }
        public string Aciklamasi { get; set; }
        public double? BirimFiyati { get; set; }
        [DisplayName("Stok Miktarý")]
        public int? StokMiktari { get; set; }        

        [DisplayName("Kategori")]
        public int? KategoriId { get; set; }
        [DisplayName("Birim Fiyatý")]
        public string BirimFiyatiDisplay { get; set; }
        [DisplayName("Kategori")]
        public string KategoriAdiDisplay { get; set; }        
        [DisplayName("Markalar")]
        public List<int> MarkaIdleri { get; set; }
        [DisplayName("Marka")]
        public string MarkaDisplay { get; set; }
        public int MarkaId { get; set; }
        [DisplayName("Kampanyalar")]
        public List<int> KampanyaIdleri { get; set; }
        [DisplayName("Kampanya")]
        public string KampanyaDisplay { get; set; }
        public int KampanyaId { get; set; }
    }
}
