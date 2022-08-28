
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Business.Models.Hesap;
using Business.Services;

namespace AykaParfum.Controllers
{
    [Authorize(Roles = "Admin")]
    public class KullanicilarController : Controller
    {
        // Add service injections here
        private readonly IKullaniciService _kullaniciService;

        public KullanicilarController(IKullaniciService kullaniciService)
        {
            _kullaniciService = kullaniciService;
        }

        // GET: Kullanicilar
        public IActionResult Index()
        {
            var kullaniciList = _kullaniciService.Query().ToList(); // TODO: Add get list service logic here
            return View(kullaniciList);
        }

        // GET: Kullanicilar/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return View("Hata", "Id gereklidir!");
            }
            var kullanici = _kullaniciService.Query().SingleOrDefault(k => k.Id == id); // TODO: Add get item service logic here
            if (kullanici == null)
            {
                return View("Hata", "Kayıt bulunamadı!");
            }
            return View(kullanici);
        }

        // GET: Kullanicilar/Create
        public IActionResult Create()
        {
            // Add get related items service logic here to set ViewData if necessary and update null parameter in SelectList with these items
            ViewData["RolId"] = new SelectList(_kullaniciService.Query().ToList(), "Id", "Adi");
            return View();
        }

        // POST: Kullanicilar/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(KullaniciModel kullanici)
        {
            if (ModelState.IsValid)
            {
                var result = _kullaniciService.Add(kullanici);
                if (result.IsSuccessful)
                    return RedirectToAction(nameof(Index));
                ModelState.AddModelError("", result.Message);
            }
            // Add get related items service logic here to set ViewData if necessary and update null parameter in SelectList with these items
            ViewData["RolId"] = new SelectList(_kullaniciService.Query().ToList(), "Id", "Adi", kullanici.RolId);
            return View(kullanici);
        }

        // GET: Kullanicilar/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return View("Hata", "Id gereklidir!");
            }
            var kullanici = _kullaniciService.Query().SingleOrDefault(k => k.Id == id); // TODO: Add get item service logic here
            if (kullanici == null)
            {
                return View("Hata", "Kayıt bulunamadı!");
            }
            // Add get related items service logic here to set ViewData if necessary and update null parameter in SelectList with these items
            ViewData["RolId"] = new SelectList(_kullaniciService.Query().ToList(), "Id", "Adi", kullanici.RolId);
            return View(kullanici);
        }

        // POST: Kullanicilar/Edit
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, KullaniciModel kullanici)
        {
            if (ModelState.IsValid)
            {
                var result = _kullaniciService.Update(kullanici);
                if (result.IsSuccessful)
                    return RedirectToAction(nameof(Index));
                ModelState.AddModelError("", result.Message);
            }
            // Add get related items service logic here to set ViewData if necessary and update null parameter in SelectList with these items
            //ViewData["RolId"] = new SelectList(_kullaniciService.Query().ToList(), "Id", "Adi", kullanici.RolId);
            return View(kullanici);
        }

        // GET: Kullanicilar/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return View("Hata", "Id gereklidir!");
            }
            var kullanici = _kullaniciService.Query().SingleOrDefault(k => k.Id == id); // TODO: Add get item service logic here
            if (kullanici == null)
            {
                return View("Hata", "Kullanıcı bulunamadı!");
            }
            return View(kullanici);
        }

        // POST: Kullanicilar/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var result = _kullaniciService.Delete(id);
            TempData["Mesaj"] = result.Message;
            return RedirectToAction(nameof(Index));
        }
    }
}
