using AppCoreV2.Business.Models.Ordering;
using AppCoreV2.Business.Models.Paging;
using Business.Models.Filters;
using Business.Models.Report;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;

namespace AykaParfum.Areas.Raporlar.Models
{
    public class UrunRaporViewModel
    {
        public List<UrunRaporModel> Rapor { get; set; }
        public UrunRaporFiltreModel Filtre { get; set; }
        public SelectList Kategoriler { get; set; }
        public MultiSelectList Markalar { get; set; }
        public MultiSelectList Kampanyalar { get; set; }
        public PageModel Sayfa { get; set; }
        public SelectList Sayfalar { get; set; }
        public SelectList SayfadakiKayitSayilari { get; set; }
        public OrderModel Sira { get; set; }
        public SelectList Siralar { get; set; }

        #region


        public double? PiyasaSatisFiyati { get; set; }
        [DisplayName("Piyasa Satış Fiyatı")]
        public string PiyasaSatisFiyatiDisplay { get; set; }

        #endregion

    }
}
