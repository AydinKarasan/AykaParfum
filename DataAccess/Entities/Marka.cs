using AppCoreV2.Records.Bases;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entities
{
    public class Marka : RecordBase
    {
        [Required]
        [StringLength(100)]
        public string Adi { get; set; }
        public List<Urun> Urunler { get; set; } //one to many //bir markanýn birden çok ürünü olabilir
    }
}
