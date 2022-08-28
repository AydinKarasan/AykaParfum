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

namespace Business.Services
{
    public interface IUlkeService : IService<UlkeModel, Ulke, AykaParfumContext>
    {

    }
    public class UlkeService : IUlkeService
    {
        public RepoBase<Ulke, AykaParfumContext> Repo { get; set; } = new Repo<Ulke, AykaParfumContext>();

        public Result Add(UlkeModel model)
        {
            if (Repo.Query().Any(u => u.Adi.ToLower() == model.Adi.ToLower().Trim()))
                return new ErrorResult("Bu isimle ülke bulunmaktadýr!");

            Ulke ulke = new Ulke()
            {
                Adi = model.Adi.Trim()
            };
            Repo.Add(ulke);
            return new SuccessResult("Ýþlem baþarýlý.");
        }

        public Result Delete(int id)
        {
            Ulke ulke = Repo.Query(u => u.Id == id, "Sehirler", "KullaniciDetaylar").SingleOrDefault();
            if (ulke.Sehirler != null && ulke.Sehirler.Count > 0)
                return new ErrorResult("Silinmek istenen ülkeye ait þehirler bulunmaktadýr!");
            if (ulke.KullaniciDetaylar != null && ulke.KullaniciDetaylar.Count > 0)
                return new ErrorResult("Silinmek istenen ülkeye ait kullanýcýlar bulunmaktadýr!");
            Repo.Delete(u => u.Id == id);
            return new SuccessResult("Ýþlem baþarýlý.");
        }

        public void Dispose()
        {
            Repo.Dispose();
        }

        public IQueryable<UlkeModel> Query()
        {
            return Repo.Query().OrderBy(u => u.Adi).Select(u => new UlkeModel()
            {
                Id = u.Id,
                Adi = u.Adi
            });
        }

        public Result Update(UlkeModel model)
        {
            if (Repo.Query().Any(u => u.Adi.ToLower() == model.Adi.ToLower().Trim()))
                return new ErrorResult("Bu isimle ülke bulunmaktadýr!");

            Ulke ulke = new Ulke()
            {
                Adi = model.Adi.Trim()
            };
            Repo.Update(ulke);
            return new SuccessResult("Ýþlem baþarýlý.");
        }
    }
}
