using AppCoreV2.Records.Bases;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entities.Hesap
{
    public class Rol : RecordBase
    {
        [Required]
        [StringLength(20)]
        public string Adi { get; set; }
        public List<Kullanici> Kullanicilar { get; set; }
    }
}
