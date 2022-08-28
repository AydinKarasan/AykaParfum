using AppCoreV2.Business.Models;
using AppCoreV2.Business.Services.Bases;
using AppCoreV2.DataAccess.Entityframework;
using AppCoreV2.DataAccess.Entityframework.Bases;
using Business.Models;
using DataAccess.Contexts;
using DataAccess.Entities;

namespace Business.Services
{
    public interface IKategoriService : IService<KategoriModel, Kategori, AykaParfumContext>
    {
    }

    public class KategoriService : IKategoriService
    {
        public RepoBase<Kategori, AykaParfumContext> Repo { get; set; } = new Repo<Kategori, AykaParfumContext>();

        public Result Add(KategoriModel model)
        {
            if (Repo.Query().Any(k => k.Adi.ToLower() == model.Adi.ToLower().Trim()))
                return new ErrorResult("Bu isimle kategori bulunmaktadýr!");

            Kategori kategori = new Kategori()
            {                
                Adi = model.Adi.Trim()
            };
            Repo.Add(kategori);
            return new SuccessResult("Ýþlem baþarýlý.");
        }

        public Result Delete(int id)
        {
            Kategori kategori = Repo.Query(k => k.Id == id, "Urunler").SingleOrDefault();
            if (kategori.Urunler != null && kategori.Urunler.Count > 0)
            {
                return new ErrorResult("Kategori silinemez! Önce kategoriye ait ürünleri silmelisiniz!");
            }
            Repo.Delete(kategori);            
            return new SuccessResult("Ýþlem baþarýlý.");
        }

        public void Dispose()
        {
            Repo.Dispose();
        }

        public IQueryable<KategoriModel> Query()
        {
            return Repo.Query("Urunler").OrderBy(k => k.Adi).Select(k => new KategoriModel()
            {
                Id = k.Id,
                Adi = k.Adi,
                UrunSayisiDisplay = k.Urunler.Count
            });
        }

        public Result Update(KategoriModel model)
        {
            if (Repo.Query().Any(k => k.Adi.ToLower() == model.Adi.ToLower().Trim() && k.Id != model.Id))
                return new ErrorResult("Bu isimle kategori bulunmaktadýr!");

            Kategori entity = Repo.Query(k => k.Id == model.Id).SingleOrDefault();
            entity.Adi = model.Adi.Trim();            
            Repo.Update(entity);
            return new SuccessResult("Ýþlem baþarýlý.");
        }
    }
}
