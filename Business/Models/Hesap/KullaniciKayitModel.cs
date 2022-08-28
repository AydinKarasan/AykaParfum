using Business.Models.Hesap;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Models.Hesap
{
    public class KullaniciKayitModel
    {
        [Required(ErrorMessage = "{0} zorunludur!")]
        [MinLength(5, ErrorMessage = "{0} minimum {1} karakter olmal�d�r!")]
        [MaxLength(30, ErrorMessage = "{0} maksimum {1} karakter olmal�d�r!")]
        [DisplayName("Kullan�c� Ad�")]
        public string KullaniciAdi { get; set; }
        [Required(ErrorMessage = "{0} zorunludur!")]
        [MinLength(4, ErrorMessage = "{0} minimum {1} karakter olmal�d�r!")]
        [MaxLength(8, ErrorMessage = "{0} maksimum {1} karakter olmal�d�r!")]
        [DisplayName("�ifre")]
        public string Sifre { get; set; }
        [Required]
        [StringLength(8)]
        [Compare("Sifre", ErrorMessage = "�ifre ile �ifre tekrar ayn� olmal�d�r")]
        public string SifreOnay { get; set; }
        public KullaniciDetayModel KullaniciDetay { get; set; }
    }
}
