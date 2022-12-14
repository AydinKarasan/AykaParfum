
using Business.Models.Hesap;
using Business.Services;
using Business.Services.Hesap;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;

namespace AykaParfum.Controllers
{    
    public class HesaplarController : Controller
    {
        private readonly IHesapService _hesapService;
        private readonly IUlkeService _ulkeService;
        private readonly ISehirService _sehirService;
        public HesaplarController(IHesapService hesapService, IUlkeService ulkeService, ISehirService sehirService)
        {
            _hesapService = hesapService;
            _ulkeService = ulkeService;
            _sehirService = sehirService;
        }
        public IActionResult Giris()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Giris(KullaniciGirisModel model)
        {
            if (ModelState.IsValid)
            {
                var result = _hesapService.Giris(model);
                if (result.IsSuccessful)
                {
                    List<Claim> claims = new List<Claim>()
                    {
                        new Claim(ClaimTypes.Name, result.Data.KullaniciAdi),
                        new Claim(ClaimTypes.Role, result.Data.RolAdiDisplay),
                        new Claim(ClaimTypes.Sid, result.Data.Id.ToString())//sepet için kullanıcı İd yi burada bir kere sorgulayıp tut sepette kullan
                    };
                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var principal = new ClaimsPrincipal(identity);
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", result.Message);
            }

            return View(model);
        }
        public IActionResult Kayit()
        {
            ViewBag.Ulkeler = new SelectList(_ulkeService.Query().ToList(), "Id", "Adi");
            ViewBag.Sehirler = new SelectList(_sehirService.Query().ToList(), "Id", "Adi");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Kayit(KullaniciKayitModel model)
        {
            if (ModelState.IsValid)
            {
                var result = _hesapService.Kayit(model);
                if (result.IsSuccessful)
                    return RedirectToAction(nameof(Giris));
                ViewBag.Message = result.Message;
            }
            ViewBag.Ulkeler = new SelectList(_ulkeService.Query().ToList(), "Id", "Adi", model.KullaniciDetay.UlkeId);
            ViewBag.Sehirler = new SelectList(_sehirService.Query().Where(s => s.UlkeId == model.KullaniciDetay.UlkeId).ToList(), "Id", "Adi", model.KullaniciDetay.SehirId);
            return View(model);
        }
        public IActionResult YetkisizIslem()
        {
            return View("Hata", "Yetkisiz işlem!");
        }
        public async Task<IActionResult> Cikis()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }
    }
}
