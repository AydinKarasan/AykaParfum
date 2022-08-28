using AppCoreV2.Records.Bases;
using DataAccess.Entities.Hesap;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entities
{
    public class Kullanici : RecordBase
    {
        [Required]
        [StringLength(30)]
        public string KullaniciAdi { get; set; }
        [Required]
        [StringLength(8)]
        public string Sifre { get; set; }
        public bool AktifMi { get; set; } 
        public KullaniciDetay KullaniciDetay { get; set; }
        public int RolId { get; set; } 
        public Rol Rol { get; set; } 
    }
}
