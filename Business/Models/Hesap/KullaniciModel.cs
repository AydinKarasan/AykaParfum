using AppCoreV2.Records.Bases;
using System.ComponentModel.DataAnnotations;

namespace Business.Models.Hesap
{
    public class KullaniciModel : RecordBase
    {
        [Required]
        [StringLength(30)]
        public string KullaniciAdi { get; set; }
        [Required]
        [StringLength(8)]
        public string Sifre { get; set; }
        public bool AktifMi { get; set; } 
        public int RolId { get; set; } 

        public string RolAdiDisplay { get; set; }
        public KullaniciDetayModel KullaniciDetay { get; set; }
    }
}
