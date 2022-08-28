
using Business.Models.Sepet;
using Business.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;

namespace AykaParfum.Areas.Sepet.Controllers
{
    
    [Area("Sepet")]
    public class HomeController : Controller
    {
        private  IUrunService _urunService;

        public HomeController(IUrunService urunService)
        {
            _urunService = urunService;
        }

        public IActionResult SepeteEkle(int urunId)
        {
            var urun = _urunService.Query().SingleOrDefault(u => u.Id == urunId);
            if (urun.StokMiktari == 0)
            {
                TempData["Mesaj"] = "Ürün stokta yoktur!";
                return RedirectToAction("Index", "Urunler");
            }
            string json;

            SepetUrunModel sepetUrun;
            List<SepetUrunModel> sepet = new List<SepetUrunModel>();
            if (HttpContext.Session.GetString("sepetkey") != null)
            {
                json = HttpContext.Session.GetString("sepetkey");
                sepet = JsonConvert.DeserializeObject<List<SepetUrunModel>>(json);
            }

            sepetUrun = new SepetUrunModel()
            {
                UrunId = urun.Id,
                UrunAdi = urun.Adi,
                BirimFiyati = urun.BirimFiyati ?? 0,
                KullaniciId = Convert.ToInt32(User.Claims.SingleOrDefault(c => c.Type == ClaimTypes.Sid).Value)
            };
            if (sepet.Count(s => s.UrunId == urunId) > urun.StokMiktari)
            {
                TempData["Mesaj"] = "Yeterli stok bulunmamaktadır!";
                return RedirectToAction("Index", "Urunler");
            }
            else
            {
                sepet.Add(sepetUrun);
            }
            json = JsonConvert.SerializeObject(sepet);
            HttpContext.Session.SetString("sepetkey", json);
            return RedirectToAction("Index", "Urunler");
        }

        public IActionResult SepetGetir()
        {
            List<SepetUrunModel> sepet = new List<SepetUrunModel>();
            if (HttpContext.Session.GetString("sepetkey") != null)
            {
                string json = HttpContext.Session.GetString("sepetkey");
                sepet = JsonConvert.DeserializeObject<List<SepetUrunModel>>(json);
            }

            sepet = sepet.Where(s => s.KullaniciId == Convert.ToInt32(User.Claims.SingleOrDefault(c => c.Type == ClaimTypes.Sid).Value)).ToList();
            List<SepetUrunToplamModel> sepetToplam = (from s in sepet                                                         
                                                         group s by new { s.UrunId, s.KullaniciId, s.UrunAdi }
                                                         into sGroupBy
                                                          select new SepetUrunToplamModel()
                                                          {
                                                              UrunId = sGroupBy.Key.UrunId,
                                                              KullaniciId = sGroupBy.Key.KullaniciId,
                                                              UrunAdi = sGroupBy.Key.UrunAdi,
                                                              ToplamUrunFiyati = sGroupBy.Sum(sgb => sgb.BirimFiyati),
                                                              ToplamUrunFiyatiDisplay = sGroupBy.Sum(sgb => sgb.BirimFiyati).ToString("C2"),
                                                              ToplamUrunAdedi = sGroupBy.Count()
                                                          }).ToList();
            sepetToplam = sepetToplam.OrderBy(sgb => sgb.UrunAdi).ToList();

            return View("SepetGetirToplam", sepetToplam);
        }

        public IActionResult Sil(int urunId, int kullaniciId)
        {
            var sepet = new List<SepetUrunModel>();
            string sepetJson = HttpContext.Session.GetString("sepetkey");
            if(!string.IsNullOrWhiteSpace(sepetJson))
            {
                sepet = JsonConvert.DeserializeObject<List<SepetUrunModel>>(sepetJson);
            }
            var urun = sepet.FirstOrDefault(s => s.UrunId == urunId && s.KullaniciId == kullaniciId);
            if(urun != null)
            {
                sepet.Remove(urun);
                sepetJson = JsonConvert.SerializeObject(sepet);
                HttpContext.Session.SetString("sepetkey", sepetJson);
            }
            return RedirectToAction(nameof(SepetGetir));
        }

        public IActionResult Temizle()
        {
            var sepet = new List<SepetUrunModel>();
            string sepetJson = HttpContext.Session.GetString("sepetkey");
            if (!string.IsNullOrWhiteSpace(sepetJson))
            {
                sepet = JsonConvert.DeserializeObject<List<SepetUrunModel>>(sepetJson);
            }
            var kullaniciSepeti = sepet.Where(s => s.KullaniciId == Convert.ToInt32(User.Claims.SingleOrDefault(c => c.Type == ClaimTypes.Sid).Value)).ToList();
            foreach (var urun in kullaniciSepeti)
            {
                sepet.Remove(urun);
            }
            sepetJson = JsonConvert.SerializeObject(sepet);
            HttpContext.Session.SetString("sepetkey", sepetJson);

            return RedirectToAction(nameof(SepetGetir));


        }
    }
}
