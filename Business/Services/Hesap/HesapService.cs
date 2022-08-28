
using AppCoreV2.Business.Models;
using Business.Models.Hesap;
using DataAccess.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services
{
    public interface IHesapService
    {
        Result<KullaniciModel> Giris(KullaniciGirisModel model);
        Result Kayit(KullaniciKayitModel model);
    }
    public class HesapService : IHesapService
    {
        private readonly IKullaniciService _kullaniciService;
        public HesapService(IKullaniciService kullaniciService)
        {
            _kullaniciService = kullaniciService;
        }
        public Result<KullaniciModel> Giris(KullaniciGirisModel model)
        {
            KullaniciModel kullanici = _kullaniciService.Query().SingleOrDefault(k => k.KullaniciAdi == model.KullaniciAdi && k.Sifre == model.Sifre && k.AktifMi);
            if (kullanici == null)
                return new ErrorResult<KullaniciModel>("Bu kullanýcý adý ve þifreye ait kayýt bulunamadý!");
            return new SuccessResult<KullaniciModel>(kullanici);
        }

        public Result Kayit(KullaniciKayitModel model)
        {
            KullaniciModel kullanici = new KullaniciModel()
            {
                AktifMi = true, // kullanici kayýt yaptýðýnda aktif olsun default=true
                RolId = (int)Roller.Kullanýcý,
                KullaniciAdi = model.KullaniciAdi,
                Sifre = model.Sifre,
                KullaniciDetay = new KullaniciDetayModel()
                {
                    Cinsiyet = model.KullaniciDetay.Cinsiyet,
                    Adres = model.KullaniciDetay.Adres.Trim(),
                    Eposta = model.KullaniciDetay.Eposta.Trim(),
                    UlkeId = model.KullaniciDetay.UlkeId,
                    SehirId = model.KullaniciDetay.SehirId,                    
                }
            };
            return _kullaniciService.Add(kullanici);
        }
    }
}
