﻿using DataAccess.Enums;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Business.Models.Hesap
{
    public class KullaniciDetayModel
    {
        [Required]
        [StringLength(200)]
        [EmailAddress(ErrorMessage = "{0} formatı uygun değildir!")]
        [DisplayName("E-Posta")]
        public string Eposta { get; set; }
        public Cinsiyetler Cinsiyet { get; set; }
        [Required]
        public string Adres { get; set; }
        [Required(ErrorMessage = "Ülke seçiniz...")]
        [DisplayName("Ülke")]
        public int? UlkeId { get; set; }
        [Required(ErrorMessage = "Şehir seçiniz...")]
        [DisplayName("Şehir")]
        public int? SehirId { get; set; }
    }
}
