using AppCoreV2.Records.Bases;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Entities
{
    public class Kategori : RecordBase
    {
        [Required]
        [StringLength(100)]
        public string Adi { get; set; }
        public List<Urun> Urunler { get; set; } //one to many //bir kategorinin birden çok ürünü olabilir
    }
}
