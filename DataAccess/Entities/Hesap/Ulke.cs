using AppCoreV2.Records.Bases;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entities.Hesap
{
    public class Ulke : RecordBase
    {
        [Required]
        [StringLength(100)]
        public string Adi { get; set; }
        public List<Sehir> Sehirler { get; set; }
        public List<KullaniciDetay> KullaniciDetaylar { get; set; }
    }
}
