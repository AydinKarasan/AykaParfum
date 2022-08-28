using AppCoreV2.Business.Models;
using AppCoreV2.Business.Models.Ordering;
using AppCoreV2.Business.Models.Paging;
using AppCoreV2.DataAccess.Entityframework;
using AppCoreV2.DataAccess.Entityframework.Bases;
using Business.Models.Filters;
using Business.Models.Report;
using DataAccess.Contexts;
using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services
{
    public interface IUrunRaporService
    {
        public Result<List<UrunRaporModel>> List(UrunRaporFiltreModel filtre, PageModel sayfa, OrderModel sira);
        public List<string> GetSiralar();
    }
    public class UrunRaporService : IUrunRaporService
    {
        public List<string> GetSiralar() => new List<string>() { "Ürün Adý", "Birim Fiyatý", "Stok Miktarý", "Marka" , "Kampanya"};


        public Result<List<UrunRaporModel>> List(UrunRaporFiltreModel filtre, PageModel sayfa, OrderModel sira)
        {
            return new SuccessResult<List<UrunRaporModel>>(Query(filtre, sayfa, sira).ToList());
        }
        private IQueryable<UrunRaporModel> Query(UrunRaporFiltreModel filtre, PageModel sayfa, OrderModel sira)
        {
            AykaParfumContext db = new AykaParfumContext();
            RepoBase<Urun, AykaParfumContext> urunRepo = new Repo<Urun, AykaParfumContext>(db);
            RepoBase<Kategori, AykaParfumContext> kategoriRepo = new Repo<Kategori, AykaParfumContext>(db);
            RepoBase<Marka, AykaParfumContext> markaRepo = new Repo<Marka, AykaParfumContext>(db);
            RepoBase<Kampanya, AykaParfumContext> kampanyaRepo = new Repo<Kampanya, AykaParfumContext>(db);
            RepoBase<UrunKampanya, AykaParfumContext> urunKampanyaRepo = new Repo<UrunKampanya, AykaParfumContext>(db);

            var urunQuery = urunRepo.Query();
            var kategoriQuery = kategoriRepo.Query();
            var markaQuery = markaRepo.Query();
            var urunKampanyaQuery = urunKampanyaRepo.Query();
            var kampanyaQuery = kampanyaRepo.Query();


            //left outer join sorgusu
            /*
            select * from urunler u left join kategoriler k on u.KategoriId = k.Id left join markalar m on u.Id = m.UrunId
            
            */

            var query = from urun in urunQuery
                        join kategori in kategoriQuery
                        on urun.KategoriId equals kategori.Id into kategoriler
                        from subKategoriler in kategoriler.DefaultIfEmpty()
                        join marka in markaQuery
                        on urun.MarkaId equals marka.Id into markalar
                        from subMarkalar in markalar.DefaultIfEmpty()
                        join urunKampanya in urunKampanyaQuery
                        on urun.Id equals urunKampanya.UrunId into urunKampanyalar
                        from subUrunKampanyalar in urunKampanyalar.DefaultIfEmpty()
                        join kampanya in kampanyaQuery
                        on subUrunKampanyalar.KampanyaId equals kampanya.Id into kampanyalar
                        from subKampanyalar in kampanyalar.DefaultIfEmpty()
                        select new UrunRaporModel()
                        {
                            urunAdi = urun.Adi,
                            Aciklamasi = urun.Aciklamasi,
                            BirimFiyati = urun.BirimFiyati,
                            StokMiktari = urun.StokMiktari,
                            KategoriId = urun.KategoriId,
                            BirimFiyatiDisplay = urun.BirimFiyati.ToString("C2", new CultureInfo("tr-TR")),
                            KategoriAdiDisplay = subKategoriler.Adi,
                            MarkaId = subMarkalar != null ? subMarkalar.Id : 0,
                            MarkaDisplay = subMarkalar != null ? subMarkalar.Adi : "",
                            KampanyaDisplay = subKampanyalar != null ? subKampanyalar.Adi : "",
                            KampanyaId = subKampanyalar != null ? subKampanyalar.Id : 0
                        };

            switch(sira.Expression)
            {
                case "Kategori":
                    query = sira.IsDirectionAscending ? query.OrderBy(q => q.KategoriAdiDisplay) : query.OrderByDescending(q => q.KategoriAdiDisplay);
                    break;

                case "Marka":
                    query = sira.IsDirectionAscending ? query.OrderBy(q => q.MarkaDisplay) : query.OrderByDescending(q => q.MarkaDisplay);
                    break;

                case "Kampanya":
                    query = sira.IsDirectionAscending ? query.OrderBy(q => q.KampanyaDisplay) : query.OrderByDescending(q => q.KampanyaDisplay);
                    break;

                case "Birim Fiyatý":
                    query = sira.IsDirectionAscending ? query.OrderBy(q => q.BirimFiyati) : query.OrderByDescending(q => q.BirimFiyati);
                    break;

                case "Stok Miktarý":
                    query = sira.IsDirectionAscending ? query.OrderBy(q => q.StokMiktari) : query.OrderByDescending(q => q.StokMiktari);
                    break;

                default:
                    query = sira.IsDirectionAscending ? query.OrderBy(q => q.urunAdi) : query.OrderByDescending(q => q.urunAdi);
                    break;
            }

            if (!string.IsNullOrWhiteSpace(filtre.UrunAdi))
                query = query.Where(q => q.urunAdi.ToLower().Contains(filtre.UrunAdi.ToLower().Trim()));

            if (filtre.KategoriId != null)
                query = query.Where(q => q.KategoriId == filtre.KategoriId);

            if (filtre.MarkaIdleri != null && filtre.MarkaIdleri.Count > 0)
                query = query.Where(q => filtre.MarkaIdleri.Contains(q.MarkaId));

            if (filtre.BirimFiyatiMininmum.HasValue)
                query = query.Where(q => q.BirimFiyati >= filtre.BirimFiyatiMininmum);

            if (filtre.BirimFiyatiMaximum.HasValue)
                query = query.Where(q => q.BirimFiyati <= filtre.BirimFiyatiMaximum);

            if (filtre.KampanyaIdleri != null && filtre.KampanyaIdleri.Count > 0)
                query = query.Where(q => filtre.KampanyaIdleri.Contains(q.KampanyaId));
            if (!string.IsNullOrWhiteSpace(filtre.UrunKampanyaAdi))                
                query = query.Where(q => q.urunAdi.ToLower().Contains(filtre.UrunKampanyaAdi.ToLower().Trim()) || q.KampanyaDisplay.ToLower().Contains(filtre.UrunKampanyaAdi.ToLower().Trim()));
            
            sayfa.TotalRecordsCount = query.Count();            

            query = query.Skip((sayfa.PageNumber - 1) * sayfa.RecordsPerPageCount).Take(sayfa.RecordsPerPageCount);

            return query;

        }
    }
    
}
