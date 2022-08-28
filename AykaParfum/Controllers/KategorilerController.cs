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
using Business.Models;
using AppCoreV2.Business.Models;
using Microsoft.AspNetCore.Authorization;

namespace AykaParfum.Controllers
{
    [Authorize(Roles = "Admin")]
    public class KategorilerController : Controller
    {
        // Add service injections here
        private readonly IKategoriService _kategoriService;

        public KategorilerController(IKategoriService kategoriService)
        {
            _kategoriService = kategoriService;
        }

        // GET: Kategoriler
        public IActionResult Index()
        {
            List<KategoriModel> kategoriList = _kategoriService.Query().ToList(); // TODO: Add get list service logic here
            return View(kategoriList);
        }

        // GET: Kategoriler/Details/5
        public IActionResult Details(int? id)
        {
            if (!id.HasValue)
                return View("Hata!", "Id gereklidir!");
            KategoriModel kategori = _kategoriService.Query().SingleOrDefault(k => k.Id == id); // TODO: Add get item service logic here
            if (kategori == null)
            {
                return View("Hata!", "Kategori bulunamadı!");
            }
            return View(kategori);
        }

        // GET: Kategoriler/Create
        public IActionResult Create()
        {
            // Add get related items service logic here to set ViewData if necessary and update null parameter in SelectList with these items
            return View();
        }

        // POST: Kategoriler/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(KategoriModel kategori)
        {
            if (ModelState.IsValid)
            {
                Result result = _kategoriService.Add(kategori);
                if (result.IsSuccessful)
                    return RedirectToAction(nameof(Index));
                ModelState.AddModelError("", result.Message);
                
            }
            // Add get related items service logic here to set ViewData if necessary and update null parameter in SelectList with these items
            
            return View(kategori);
        }

        // GET: Kategoriler/Edit/5
        public IActionResult Edit(int? id)
        {
            if (!id.HasValue)
            {
                return View("Hata", "Id gereklidir!");
            }
            KategoriModel kategori = _kategoriService.Query().SingleOrDefault(k => k.Id == id.Value); ; // TODO: Add get item service logic here
            if (kategori == null)
            {
                return View("Hata!", "Kategori bulunamadı!");
            }
            // Add get related items service logic here to set ViewData if necessary and update null parameter in SelectList with these items
            return View(kategori);
        }

        // POST: Kategoriler/Edit
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(KategoriModel kategori)
        {
            if (ModelState.IsValid)
            {
                Result result = _kategoriService.Update(kategori);
                if (result.IsSuccessful)
                    return RedirectToAction(nameof(Index));
                ModelState.AddModelError("", result.Message);
                
            }
            // Add get related items service logic here to set ViewData if necessary and update null parameter in SelectList with these items
            return View(kategori);
        }

        // GET: Kategoriler/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
                return View("Hata!", "Id gereklidir!");

            KategoriModel kategori = _kategoriService.Query().SingleOrDefault(k => k.Id == id); // TODO: Add get item service logic here
            if (kategori == null)
            {
                return View("Hata", "Kategori bulunamadı!");
            }
            return View(kategori);
        }

        // POST: Kategoriler/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            Result result = _kategoriService.Delete(id);
            TempData["Mesaj"] = result.Message;
            return RedirectToAction(nameof(Index));
        }
	}
}
