using AppCoreV2.Records.Bases;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Models
{
    public class KampanyaModel : RecordBase
    {
        [Required(ErrorMessage = "{0} gereklidir!")] 
        [StringLength(200, ErrorMessage = "{0} maksimum {1} karakter olmal�d�r!")]
        [DisplayName("Ad�")]
        public string Adi { get; set; }
        [StringLength(500, ErrorMessage = "{0} maksimum {1} karakter olmal�d�r!")]
        [DisplayName("A��klamas�")]
        public string Aciklamasi { get; set; }
        [DisplayName("Aktif")]
        public bool AktifMi { get; set; } = true;        
        [DisplayName("Aktif")]
        public string AktifMiDisplay { get; set; }
    }
}
