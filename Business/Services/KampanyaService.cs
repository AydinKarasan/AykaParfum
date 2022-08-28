using AppCoreV2.Business.Models;
using AppCoreV2.Business.Services.Bases;
using AppCoreV2.DataAccess.Entityframework;
using AppCoreV2.DataAccess.Entityframework.Bases;
using Business.Models;
using DataAccess.Contexts;
using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services
{
    public interface IKampanyaService : IService<KampanyaModel, Kampanya, AykaParfumContext>
    {
        
    }

    public class KampanyaService : IKampanyaService
    {
        public RepoBase<Kampanya, AykaParfumContext> Repo { get; set; } = new Repo<Kampanya, AykaParfumContext>();

        public Result Add(KampanyaModel model)
        {
            if (Repo.EntityExists(k => k.Adi.ToLower() == model.Adi.ToLower().Trim()))
                return new ErrorResult("Bu Kampanya adýna sahip kayýt bulunmaktadýr!");
            Kampanya entity = new Kampanya()
            {
                Adi = model.Adi.Trim(),
                Aciklamasi = model.Aciklamasi.Trim(),                
                AktifMi = model.AktifMi,
            };
            Repo.Add(entity);
            return new SuccessResult("Ýþlem baþarýlý.");
        }

        public Result Delete(int id)
        {
            
            Kampanya entity = Repo.Query(k => k.Id == id, "UrunKampanyalar").SingleOrDefault(); 
            if (entity.UrunKampanyalar != null && entity.UrunKampanyalar.Count > 0)
            {
                RepoBase<UrunKampanya, AykaParfumContext> urunKampanyaRepo = new Repo<UrunKampanya, AykaParfumContext>();
                foreach (var urunKampanya in entity.UrunKampanyalar)
                {
                    urunKampanyaRepo.Delete(urunKampanya, false);
                }
                urunKampanyaRepo.Save(); 
                urunKampanyaRepo.Dispose(); 

            }
            Repo.Delete(entity);
            return new SuccessResult("Ýþlem baþarýlý.");
        }

        public void Dispose()
        {
            Repo.Dispose();
        }

        public IQueryable<KampanyaModel> Query()
        {
            return Repo.Query().OrderBy(k => k.Adi).Select(k => new KampanyaModel()
            {
                Id = k.Id,
                Adi = k.Adi,
                Aciklamasi = k.Aciklamasi,  
                AktifMi = k.AktifMi,
                AktifMiDisplay = k.AktifMi ? "Evet" : "Hayýr"
            });
        }

        public Result Update(KampanyaModel model)
        {
            if (Repo.EntityExists(k => k.Adi.ToLower() == model.Adi.ToLower().Trim() && k.Id != model.Id))
                return new ErrorResult("Bu Kampanya adýna sahip kayýt bulunmaktadýr!");
            Kampanya entity = Repo.Query().SingleOrDefault(k => k.Id == model.Id);

            entity.Adi = model.Adi.Trim();
            entity.Aciklamasi = model.Aciklamasi.Trim();            
            entity.AktifMi = model.AktifMi;

            Repo.Update(entity);
            return new SuccessResult("Ýþlem baþarýlý.");
        }
    }
}
