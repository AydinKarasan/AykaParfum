using AppCoreV2.Records.Bases;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Business.Models
{
    public class KategoriModel : RecordBase
    {
        #region
        [Required(ErrorMessage = "{0} gereklidir!")] 
        [StringLength(100, ErrorMessage = "{0} maksimum {1} karakter olmal�d�r!")]
        [DisplayName("Ad�")]
        public string Adi { get; set; }
        #endregion
        #region
        [DisplayName("�r�n Say�s�")]
        public int UrunSayisiDisplay { get; set; }
        #endregion
    }
}
