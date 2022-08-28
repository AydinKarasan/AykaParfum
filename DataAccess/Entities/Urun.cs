using AppCoreV2.Records.Bases;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Entities
{
    public class Urun : RecordBase
    {
        [Required] 
        [StringLength(300)] 
        public string Adi { get; set; }
        [StringLength(500)] 
        public string Aciklamasi { get; set; }
        public double BirimFiyati { get; set; }
        public double PiyasaSatisFiyati { get; set; }
        public int StokMiktari { get; set; }
        public int MarkaId { get; set; }
        public Marka Marka { get; set; } //bir ürünün bir markasý olabilir
        public int KategoriId { get; set; }
        public Kategori Kategori { get; set; } //bir ürünün bir kategorisi olabilir
        [Column(TypeName = "image")]
        public byte[] Imaj { get; set; }
        [StringLength(5)]
        public string ImajUzantisi { get; set; }
        public List<UrunKampanya> UrunKampanyalar { get; set; } //many to many
    }
}
