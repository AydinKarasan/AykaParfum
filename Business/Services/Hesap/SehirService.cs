using AppCoreV2.Business.Models;
using AppCoreV2.Business.Services.Bases;
using AppCoreV2.DataAccess.Entityframework;
using AppCoreV2.DataAccess.Entityframework.Bases;
using Business.Models.Hesap;
using DataAccess.Contexts;
using DataAccess.Entities.Hesap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Hesap
{
    public interface ISehirService : IService<SehirModel, Sehir, AykaParfumContext>
    {

    }
    public class SehirService : ISehirService
    {
        public RepoBase<Sehir, AykaParfumContext> Repo { get; set; } = new Repo<Sehir, AykaParfumContext>();

        public Result Add(SehirModel model)
        {
            if (Repo.Query().Any(s => s.Adi.ToLower() == model.Adi.ToLower().Trim()))
                return new ErrorResult("Bu isimle sehir bulunmaktadýr!");

            Sehir sehir = new Sehir()
            {
                Adi = model.Adi.Trim()
            };
            Repo.Add(sehir);
            return new SuccessResult("Ýþlem baþarýlý.");
        }

        public Result Delete(int id)
        {
            Sehir sehir = Repo.Query(s => s.Id == id, "KullaniciDetaylar").SingleOrDefault();            
            if (sehir.KullaniciDetaylar != null && sehir.KullaniciDetaylar.Count > 0)
                return new ErrorResult("Silinmek istenen þehre ait kullanýcýlar bulunmaktadýr!");
            Repo.Delete(s => s.Id == id);
            return new SuccessResult("Ýþlem baþarýlý.");
        }

        public void Dispose()
        {
            Repo.Dispose();
        }

        public IQueryable<SehirModel> Query()
        {
            return Repo.Query().OrderBy(s => s.Adi).Select(s => new SehirModel()
            {
                Id = s.Id,
                Adi = s.Adi,                
                UlkeId = s.UlkeId
            });
        }

        public Result Update(SehirModel model)
        {
            if (Repo.Query().Any(s => s.Adi.ToLower() == model.Adi.ToLower().Trim()))
                return new ErrorResult("Bu isimle sehir bulunmaktadýr!");

            Sehir sehir = new Sehir()
            {
                Adi = model.Adi.Trim()
            };
            Repo.Update(sehir);
            return new SuccessResult("Ýþlem baþarýlý.");
        }
    }
}
