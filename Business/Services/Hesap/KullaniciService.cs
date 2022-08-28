using AppCoreV2.Business.Models;
using AppCoreV2.Business.Services.Bases;
using AppCoreV2.DataAccess.Entityframework;
using AppCoreV2.DataAccess.Entityframework.Bases;
using Business.Models.Hesap;
using DataAccess.Contexts;
using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services
{
    public interface IKullaniciService : IService<KullaniciModel, Kullanici, AykaParfumContext>
    {

    }

    public class KullaniciService : IKullaniciService
    {
        public RepoBase<Kullanici, AykaParfumContext> Repo { get; set; } = new Repo<Kullanici, AykaParfumContext>();

        public Result Add(KullaniciModel model)
        {
            if (Repo.Query().Any(k => k.KullaniciAdi == model.KullaniciAdi))
                return new ErrorResult("Bu isimle kullanýcý bulunmaktadýr!");
            if (Repo.Query().Any(k => k.KullaniciDetay.Eposta == model.KullaniciDetay.Eposta))
                return new ErrorResult("Bu e-postaya sahip kullanýcý bulunmaktadýr!");

            Kullanici kullanici = new Kullanici()
            {
                KullaniciAdi = model.KullaniciAdi,
                Sifre = model.Sifre,
                AktifMi = model.AktifMi,
                RolId = model.RolId,                                
                KullaniciDetay = new KullaniciDetay()
                {
                    Cinsiyet = model.KullaniciDetay.Cinsiyet,
                    Eposta = model.KullaniciDetay.Eposta.Trim(),
                    Adres = model.KullaniciDetay.Adres.Trim(),
                    UlkeId = model.KullaniciDetay.UlkeId.Value,
                    SehirId = model.KullaniciDetay.SehirId.Value,
                }
            };
            Repo.Add(kullanici);
            return new SuccessResult("Ýþlem baþarýlý.");
        }

        public Result Delete(int id)
        {

            throw new NotImplementedException();

            //Repo.Delete(k => k.Id == id);
            //return new SuccessResult("Ýþlem baþarýlý.");
        }

        public void Dispose()
        {
            Repo.Dispose();
        }

        public IQueryable<KullaniciModel> Query()
        {
            return Repo.Query("Rol").Select(k => new KullaniciModel()
            {
                Id = k.Id,
                KullaniciAdi = k.KullaniciAdi,
                Sifre = k.Sifre,
                AktifMi = k.AktifMi,    
                RolId = k.RolId,
                RolAdiDisplay = k.Rol.Adi
            });
        }

        public Result Update(KullaniciModel model)
        {

            throw new NotImplementedException();


            //if (Repo.Query().Any(k => k.KullaniciAdi == model.KullaniciAdi))
            //    return new ErrorResult("Bu isimle kullanýcý bulunmaktadýr!");
            //if (Repo.Query().Any(k => k.KullaniciDetay.Eposta == model.KullaniciDetay.Eposta))
            //    return new ErrorResult("Bu e-postaya sahip kullanýcý bulunmaktadýr!");

            //Kullanici kullanici = new Kullanici()
            //{
            //    KullaniciAdi = model.KullaniciAdi,
            //    Sifre = model.Sifre,
            //    AktifMi = model.AktifMi,
            //    RolId = model.RolId,
            //    KullaniciDetay = new KullaniciDetay()
            //    {
            //        Cinsiyet = model.KullaniciDetay.Cinsiyet,
            //        Eposta = model.KullaniciDetay.Eposta.Trim(),
            //        Adres = model.KullaniciDetay.Adres.Trim(),
            //        UlkeId = model.KullaniciDetay.UlkeId.Value,
            //        SehirId = model.KullaniciDetay.SehirId.Value,
            //    }
            //};
            //Repo.Update(kullanici);
            //return new SuccessResult("Ýþlem baþarýlý.");
        }
    }
}
