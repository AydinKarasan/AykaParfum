using AppCoreV2.Records.Bases;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Business.Models
{
    public class KategoriModel : RecordBase
    {
        #region
        [Required(ErrorMessage = "{0} gereklidir!")] 
        [StringLength(100, ErrorMessage = "{0} maksimum {1} karakter olmalýdýr!")]
        [DisplayName("Adý")]
        public string Adi { get; set; }
        #endregion
        #region
        [DisplayName("Ürün Sayýsý")]
        public int UrunSayisiDisplay { get; set; }
        #endregion
    }
}
