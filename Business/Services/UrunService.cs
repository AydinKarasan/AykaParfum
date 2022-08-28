using AppCoreV2.Business.Models;
using AppCoreV2.Business.Services.Bases;
using AppCoreV2.DataAccess.Entityframework;
using AppCoreV2.DataAccess.Entityframework.Bases;
using AppCoreV2.Utils;
using Business.Models;
using DataAccess.Contexts;
using DataAccess.Entities;
using System.Globalization;

namespace Business.Services
{
    public interface IUrunService : IService<UrunModel, Urun, AykaParfumContext>
    {
        void DeleteImage(int id);
    }
    public class UrunService : IUrunService
    {
        private readonly AykaParfumContext _db;
        public RepoBase<Urun, AykaParfumContext> Repo { get; set; } = new Repo<Urun, AykaParfumContext>();
        public RepoBase<Kategori, AykaParfumContext> KategoriRepo { get; set; } = new Repo<Kategori, AykaParfumContext>();
        public RepoBase<Marka, AykaParfumContext> MarkaRepo { get; set; } = new Repo<Marka, AykaParfumContext>();
        public RepoBase<Kampanya, AykaParfumContext> KampanyaRepo { get; set; } = new Repo<Kampanya, AykaParfumContext>();
        public RepoBase<UrunKampanya, AykaParfumContext> UrunKampanyaRepo { get; set; } = new Repo<UrunKampanya, AykaParfumContext>();

        public UrunService()
        {
            _db = new AykaParfumContext();
            Repo = new Repo<Urun, AykaParfumContext>(_db);
            KategoriRepo = new Repo<Kategori, AykaParfumContext>(_db);
            MarkaRepo = new Repo<Marka, AykaParfumContext>(_db);
            KampanyaRepo = new Repo<Kampanya, AykaParfumContext>(_db);
            UrunKampanyaRepo = new Repo<UrunKampanya, AykaParfumContext>(_db);
        }

        public Result Add(UrunModel model)
        {
            if (Repo.Query().Any(u => u.Adi.ToLower() == model.Adi.ToLower().Trim()))
                return new ErrorResult("Bu isime sahip ürün bulunmaktadýr!");
            Urun urun = new Urun()
            {
                Adi = model.Adi.Trim(),
                Aciklamasi = model.Aciklamasi?.Trim(),
                BirimFiyati = model.BirimFiyati.Value,
                PiyasaSatisFiyati = model.PiyasaSatisFiyati.Value,
                KategoriId = model.KategoriId.Value,                
                StokMiktari = model.StokMiktari.Value,
                MarkaId = model.MarkaId.Value,
                Imaj = model.Imaj,
                ImajUzantisi = model.ImajUzantisi,
                UrunKampanyalar = model.KampanyaIdleri?.Select(kampanyaId => new UrunKampanya() {KampanyaId = kampanyaId }).ToList()
            };
            
            Repo.Add(urun);
            model.Id = urun.Id; // wepApi için gerekecek 
            return new SuccessResult("Ýþlem baþarýlý.");
        }

        public Result Delete(int id)
        {
            UrunKampanyaRepo.Delete(uk => uk.UrunId == id);
            Repo.Delete(u => u.Id == id);
            return new SuccessResult("Ýþlem baþarýlý.");
        }

        public void DeleteImage(int id)
        {
            var urun = Repo.Query().SingleOrDefault(u => u.Id == id);
            urun.Imaj = null;
            urun.ImajUzantisi = null;
            Repo.Update(urun);
        }

        public void Dispose()
        {
            Repo.Dispose();
        }

        public IQueryable<UrunModel> Query()
        {
            return Repo.Query("Kategori", "Marka", "UrunKampanyalar").OrderBy(u => u.Adi).Select(u => new UrunModel()
            {
                Id = u.Id,
                Adi = u.Adi,
                Aciklamasi = u.Aciklamasi,                
                BirimFiyati = u.BirimFiyati,
                StokMiktari = u.StokMiktari,
                KategoriId = u.KategoriId,
                MarkaId = u.MarkaId,                
                BirimFiyatiDisplay = u.BirimFiyati.ToString("C2", new CultureInfo("tr-TR")),
                PiyasaSatisFiyatiDisplay = u.PiyasaSatisFiyati.ToString("C2", new CultureInfo("tr-TR")),
                KategoriAdiDisplay = u.Kategori.Adi,
                MarkaAdiDisplay = u.Marka.Adi,
                KampanyalarDisplay = u.UrunKampanyalar.Select(uk => uk.Kampanya.Adi).ToList(),
                KampanyaDisplay = String.Join("<br /" , u.UrunKampanyalar.Select(uk => uk.Kampanya.Adi)),
                
                Imaj = u.Imaj,
                ImajUzantisi = u.ImajUzantisi,
                ImgSrcDisplay = u.Imaj == null ? null : FileUtil.GetContentType(u.ImajUzantisi, true, true) + Convert.ToBase64String(u.Imaj)
            });
        }

        public Result Update(UrunModel model)
        {
            if (Repo.Query().Any(u => u.Adi.ToLower() == model.Adi.ToLower().Trim() && u.Id != model.Id))
                return new ErrorResult("Bu isime sahip ürün bulunmaktadýr!");

            Urun urun = Repo.Query().SingleOrDefault(u => u.Id == model.Id);
            UrunKampanyaRepo.Delete(uk => uk.UrunId == model.Id);

            urun.Adi = model.Adi.Trim();
            urun.Aciklamasi = model.Aciklamasi?.Trim();
            urun.BirimFiyati = model.BirimFiyati.Value;
            urun.PiyasaSatisFiyati = model.PiyasaSatisFiyati.Value;
            urun.StokMiktari = model.StokMiktari.Value;
            urun.KategoriId = model.KategoriId.Value;
            urun.MarkaId = model.MarkaId.Value;
            urun.UrunKampanyalar = model.KampanyaIdleri?.Select(kampanyaId => new UrunKampanya() { KampanyaId = kampanyaId }).ToList();

            if (model.Imaj != null)
            {
                urun.Imaj = model.Imaj;
                urun.ImajUzantisi = model.ImajUzantisi;
            }

            Repo.Update(urun);
            return new SuccessResult("Ýþlem baþarýlý.");
        }
    }
}
