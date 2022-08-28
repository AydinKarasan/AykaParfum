using AppCoreV2.Business.Models;
using AppCoreV2.Business.Services.Bases;
using AppCoreV2.DataAccess.Entityframework;
using AppCoreV2.DataAccess.Entityframework.Bases;
using Business.Models;
using DataAccess.Contexts;
using DataAccess.Entities;

namespace Business.Services
{
    public interface IMarkaService : IService<MarkaModel, Marka, AykaParfumContext>
    {
    }

    public class MarkaService : IMarkaService
    {
        public RepoBase<Marka, AykaParfumContext> Repo { get; set; } = new Repo<Marka, AykaParfumContext>();

        public Result Add(MarkaModel model)
        {
            if (Repo.Query().Any(m => m.Adi.ToLower() == model.Adi.ToLower().Trim()))
                return new ErrorResult("Bu isimle marka bulunmaktadýr!");

            Marka marka = new Marka()
            {
                Adi = model.Adi.Trim()                
            };
            Repo.Add(marka);
            return new SuccessResult("Marka baþarýyla eklendi.");
        }

        public Result Delete(int id)
        {
            Marka marka = Repo.Query(m => m.Id == id, "Urunler").SingleOrDefault();
            if (marka.Urunler != null && marka.Urunler.Count > 0)
            {
                return new ErrorResult("Marka silinemez! Önce markaya ait ürünleri silmelisiniz!");
            }
            Repo.Delete(marka);
            return new SuccessResult("Marka baþarýyla silindi.");
        }

        public void Dispose()
        {
            Repo.Dispose();
        }

        public IQueryable<MarkaModel> Query()
        {
            return Repo.Query("Urunler").OrderBy(m => m.Adi).Select(m => new MarkaModel()
            {
                Id = m.Id,
                Adi = m.Adi,
                UrunSayisiDisplay = m.Urunler.Count
            });
        }

        public Result Update(MarkaModel model)
        {
            if (Repo.Query().Any(m => m.Adi.ToLower() == model.Adi.ToLower().Trim() && m.Id != model.Id))
                return new ErrorResult("Bu isimle marka bulunmaktadýr!");

            Marka marka = Repo.Query(m => m.Id == model.Id).SingleOrDefault();
            marka.Adi = model.Adi.Trim();
            Repo.Update(marka);
            return new SuccessResult("Marka baþarýyla güncellendi.");
        }
    }
}
