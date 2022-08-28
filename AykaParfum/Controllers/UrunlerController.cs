using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DataAccess.Contexts;
using DataAccess.Entities;
using Business.Services;
using Microsoft.AspNetCore.Authorization;
using Business.Models;
using AykaParfum.Settings;
using AppCoreV2.Utils;

namespace AykaParfum.Controllers
{
    [Authorize]
    public class UrunlerController : Controller
    {
        // Add service injections here
        private readonly IUrunService _urunService; 
        private readonly IKategoriService _kategoriService;
        private readonly IMarkaService _markaService;

        public UrunlerController(IUrunService urunService, IKategoriService kategoriService, IMarkaService markaService)
        {
            _urunService = urunService;
            _kategoriService = kategoriService;
            _markaService = markaService;
        }

        // GET: Urunler
        [AllowAnonymous]
        public IActionResult Index()
        {
            List<UrunModel> urunList = _urunService.Query().ToList(); // TODO: Add get list service logic here
            return View(urunList);
        }

        // GET: Urunler/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return View("Hata", "Kayıt bulunamadı!"); //404 
            }
            UrunModel urun = _urunService.Query().SingleOrDefault(u => u.Id == id.Value); // TODO: Add get item service logic here
            if (urun == null)
            {
                return View("Hata", "Kayıt bulunamadı!");
            }
            return View(urun);
        }

        // GET: Urunler/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            // Add get related items service logic here to set ViewData if necessary and update null parameter in SelectList with these items
            ViewData["KategoriId"] = new SelectList(_kategoriService.Query().ToList(), "Id", "Adi");
            ViewData["MarkaId"] = new SelectList(_markaService.Query().ToList(), "Id", "Adi");
            return View();
        }

        // POST: Urunler/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public IActionResult Create(UrunModel urun, IFormFile imaj)
        {
            if (ModelState.IsValid)
            {
                if (ImajDosyasiniGuncelle(urun, imaj) == false)
                {
                    ModelState.AddModelError("", $"Dosya uzantıları : {AppSettings.ImajUzantilari} ve dosya boyutu maksimum : {AppSettings.ImajBoyutu} MB olmalıdır!");
                }
                else
                {
                    var result = _urunService.Add(urun);
                    if (result.IsSuccessful)
                        return RedirectToAction(nameof(Index));
                    ModelState.AddModelError("", result.Message);
                }
                ViewData["KategoriId"] = new SelectList(_kategoriService.Query().ToList(), "Id", "Adi", urun.KategoriId);
                return RedirectToAction(nameof(Index));
            }
            // Add get related items service logic here to set ViewData if necessary and update null parameter in SelectList with these items
            ViewData["KategoriId"] = new SelectList(_kategoriService.Query().ToList(), "Id", "Adi", urun.KategoriId);
            ViewData["MarkaId"] = new SelectList(_markaService.Query().ToList(), "Id", "Adi", urun.MarkaId);
            return View(urun);
        }
        private bool? ImajDosyasiniGuncelle(UrunModel model, IFormFile yuklenenImaj)
        {
            #region //Dosya validasyonu
            bool? sonuc = null;

            if (yuklenenImaj != null && yuklenenImaj.Length > 0) // yüklenen imaj verisi varmı validasyonu
            {
                sonuc = FileUtil.CheckFileExtension(yuklenenImaj.FileName, AppSettings.ImajUzantilari).IsSuccessful;

                if (sonuc == true) //  imaj boyutunun validasyonu 
                {
                    sonuc = FileUtil.CheckFileLenght(yuklenenImaj.Length, AppSettings.ImajBoyutu).IsSuccessful;
                }
            }
            #endregion

            #region //Dosya kayıt
            if (sonuc == true)
            {
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    yuklenenImaj.CopyTo(memoryStream);
                    model.Imaj = memoryStream.ToArray();
                    model.ImajUzantisi = Path.GetExtension(yuklenenImaj.FileName);
                }
            }
            #endregion

            return sonuc;
        }

        // GET: Urunler/Edit/5
        [Authorize(Roles = "Admin")]
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return View("Hata", "Id gereklidir!");
            }
            UrunModel urun = _urunService.Query().SingleOrDefault(u => u.Id == id); // TODO: Add get item service logic here
            if (urun == null)
            {
                return View("Hata", "Ürün bulunamadı!");
            }
            // Add get related items service logic here to set ViewData if necessary and update null parameter in SelectList with these items
            ViewData["KategoriId"] = new SelectList(_kategoriService.Query().ToList(), "Id", "Adi", urun.KategoriId);
            ViewData["MarkaId"] = new SelectList(_markaService.Query().ToList(), "Id", "Adi", urun.MarkaId);
            return View(urun);
        }

        // POST: Urunler/Edit
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public IActionResult Edit(UrunModel urun, IFormFile imaj)
        {
            if (ModelState.IsValid)
            {
                // TODO: Add update service logic here
                if (ImajDosyasiniGuncelle(urun, imaj) == false)
                {
                    ModelState.AddModelError("", $"Dosya uzantıları : {AppSettings.ImajUzantilari} ve dosya boyutu maksimum : {AppSettings.ImajBoyutu} MB olmalıdır!");
                }
                else
                {
                    var result = _urunService.Update(urun);
                    if (result.IsSuccessful)
                    {
                        TempData["Mesaj"] = result.Message;
                        return RedirectToAction(nameof(Index));
                    }
                    ModelState.AddModelError("", result.Message);
                }                
            }
            // Add get related items service logic here to set ViewData if necessary and update null parameter in SelectList with these items
            ViewData["KategoriId"] = new SelectList(_kategoriService.Query().ToList(), "Id", "Adi", urun.KategoriId);
            ViewData["MarkaId"] = new SelectList(_markaService.Query().ToList(), "Id", "Adi", urun.MarkaId);
            return View(urun);
        }

        // GET: Urunler/Delete/5
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return View("Hata", "Id gereklidir!");
            }
            UrunModel urun = _urunService.Query().SingleOrDefault(u => u.Id == id); // TODO: Add get item service logic here
            if (urun == null)
            {
                return View("Hata", "Ürün bulunamadı!");
            }
            return View(urun);
        }

        // POST: Urunler/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        //[Authorize(Roles = "Admin")]
        public IActionResult DeleteConfirmed(int id)
        {
            // TODO: Add delete service logic here
            var result = _urunService.Delete(id);
            TempData["Mesaj"] = result.Message;
            return RedirectToAction(nameof(Index));            
        }

        [Authorize(Roles = "Admin")]
        public IActionResult DeleteImage(int urunId)
        {
            _urunService.DeleteImage(urunId);
            return RedirectToAction(nameof(Details), new { id = urunId });
        }
        public IActionResult DownloadImage(int urunId)
        {
            var urun = _urunService.Query().SingleOrDefault(u => u.Id == urunId);
            if (urun == null)
                return View("Hata", "Ürün bulunamadı!");
            string fileName = "Product" + urun.ImajUzantisi;            
            return File(urun.Imaj, FileUtil.GetContentType(urun.ImajUzantisi), fileName);
        }
    }
}
