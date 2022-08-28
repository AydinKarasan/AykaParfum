using DataAccess.Entities.Hesap;
using DataAccess.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entities
{
    public class KullaniciDetay
    {
        [Key]
        public int KullaniciId { get; set; }
        public Kullanici Kullanici { get; set; }
        public Cinsiyetler Cinsiyet { get; set; }
        [Required]
        [StringLength(200)]
        public string Eposta { get; set; }
        [Required]
        public string Adres { get; set; }
        public int UlkeId { get; set; }
        public Ulke Ulke { get; set; }
        public int SehirId { get; set; }
        public Sehir Sehir { get; set; }
    }
}
