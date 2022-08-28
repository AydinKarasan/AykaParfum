using Business.Models;
using Business.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AykaParfum.Controllers
{
    [Authorize]
    public class KampanyalarController : Controller
    {
        private readonly IKampanyaService _kampanyaService;
        public KampanyalarController(IKampanyaService kampanyaService)
        {
            _kampanyaService = kampanyaService;
        }

        // GET: KampanyalarController
        public IActionResult Index()
        {
            List<KampanyaModel> kampanyaList = _kampanyaService.Query().ToList();

            return View(kampanyaList);
        }

        // GET: KampanyalarController/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return View("Hata", "Id gereklidir!");
            }
            var kampanya = _kampanyaService.Query().SingleOrDefault(k => k.Id == id);
            if (kampanya == null)
            {
                return View("Hata", "Kayıt bulunamadı!");
            }
            return View(kampanya);
        }

        // GET: KampanyalarController/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: KampanyalarController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(KampanyaModel kampanya)
        {
            if (ModelState.IsValid)
            {
                var result = _kampanyaService.Add(kampanya);
                if (result.IsSuccessful)
                    return RedirectToAction(nameof(Index));
                ModelState.AddModelError("", result.Message);
            }
            return View(kampanya);
        }

        // GET: KampanyalarController/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return View("Hata", "Id gereklidir!");
            }
            var kampanya = _kampanyaService.Query().SingleOrDefault(k => k.Id == id);
            if (kampanya == null)
            {
                return View("Hata", "Kayıt bulunamadı!");
            }
            return View(kampanya);
        }

        // POST: KampanyalarController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, KampanyaModel kampanya)
        {
            if (ModelState.IsValid)
            {
                var result = _kampanyaService.Update(kampanya);
                if (result.IsSuccessful)
                    return RedirectToAction(nameof(Index));
                ModelState.AddModelError("", result.Message);
            }
            return View(kampanya);
        }

        // GET: KampanyalarController/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return View("Hata", "Id gereklidir!");
            }
            var kampanya = _kampanyaService.Query().SingleOrDefault(k => k.Id == id);

            if (kampanya == null)
            {
                return View("Hata", "Ürün bulunamadı!");
            }
            return View(kampanya);
        }

        // POST: KampanyalarController/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var result = _kampanyaService.Delete(id);
            TempData["Mesaj"] = result.Message;
            return RedirectToAction(nameof(Index));
        }
    }
}
