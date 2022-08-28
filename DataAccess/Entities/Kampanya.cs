using AppCoreV2.Records.Bases;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entities
{
    public class Kampanya : RecordBase
    {
        [Required]
        [StringLength(200)]
        public string Adi { get; set; }
        [StringLength(500)]
        public string Aciklamasi { get; set; }
        public bool AktifMi { get; set; }        
        public List<UrunKampanya> UrunKampanyalar { get; set; }
    }
}
