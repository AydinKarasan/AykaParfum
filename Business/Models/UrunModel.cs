using AppCoreV2.Records.Bases;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace Business.Models
{
    public class UrunModel : RecordBase
    {
        #region
        [Required(ErrorMessage = "{0} gereklidir!")]
        [StringLength(300, ErrorMessage = "{0} maksimum {1} karakter olmalýdýr!")] 
        [DisplayName("Adý")]
        public string Adi { get; set; }
        [StringLength(500, ErrorMessage = "{0} maksimum {1} karakter olmalýdýr!")] 
        [DisplayName("Açýklamasý")]
        public string Aciklamasi { get; set; }
        [Required(ErrorMessage = "{0} gereklidir!")]
        [DisplayName("Birim Fiyatý")]
        [Range(0, double.MaxValue, ErrorMessage = "{0} pozitif bir deðer olmalýdýr!")]
        public double? BirimFiyati { get; set; }
        [Required(ErrorMessage = "{0} gereklidir!")]
        [DisplayName("Piyasa Satýþ Fiyatý")]
        [Range(0, double.MaxValue, ErrorMessage = "{0} pozitif bir deðer olmalýdýr!")]       
        public double? PiyasaSatisFiyati { get; set; }
        [Required(ErrorMessage = "{0} gereklidir!")]
        [DisplayName("Stok Miktarý")]
        public int? StokMiktari { get; set; }
        public int? MarkaId { get; set; }        
        public int? KategoriId { get; set; }
        #endregion
        #region
        [Column(TypeName = "image")]
        [DisplayName("resim")]
        public byte[] Imaj { get; set; }
        [StringLength(5)]
        public string ImajUzantisi { get; set; }
        [DisplayName("resim")]
        public string ImgSrcDisplay { get; set; }
        [DisplayName("Birim Fiyatý")]
        public string BirimFiyatiDisplay { get; set; }
        [DisplayName("Piyasa Satýþ Fiyatý")]
        public string PiyasaSatisFiyatiDisplay { get; set; }
        [DisplayName("Kategori")]
        public string KategoriAdiDisplay { get; set; }
        [DisplayName("Kampanyalarlar")]
        public List<int> KampanyaIdleri { get; set; }
        [DisplayName("Kampanya")]
        public string KampanyaDisplay { get; set; }
        [DisplayName("Marka")]
        public string MarkaAdiDisplay { get; set; }
        
        public List<string> MarkalarDisplay { get; set; }
        public List<string> KampanyalarDisplay { get; set; }


        #endregion
    }
    public class UrunModelComparer : IEqualityComparer<UrunModel>
    {
        public bool Equals(UrunModel x, UrunModel y)
        {
            if (x.Id == y.Id)
                return true;
            return false;
        }

        public int GetHashCode([DisallowNull] UrunModel obj)
        {
            return obj.Id.GetHashCode();
        }
    }
}
