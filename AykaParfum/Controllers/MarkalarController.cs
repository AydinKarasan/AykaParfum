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
    [Authorize]
    public class MarkalarController : Controller
    {
        // Add service injections here
        private readonly IMarkaService _markaService;

        public MarkalarController(IMarkaService markaService)
        {
            _markaService = markaService;
        }

        // GET: Markalar
        public IActionResult Index()
        {
            List<MarkaModel> markaList = _markaService.Query().ToList(); // TODO: Add get list service logic here
            return View(markaList);
        }

        // GET: Markalar/Details/5
        public IActionResult Details(int? id)
        {
            if (!id.HasValue)
                return View("Hata!", "Id gereklidir!");
            MarkaModel marka = _markaService.Query().SingleOrDefault(m => m.Id == id); // TODO: Add get item service logic here
            if (marka == null)
            {
                return View("Hata!", "Marka bulunamadı!");
            }
            return View(marka);
        }

        // GET: Markalar/Create
        public IActionResult Create()
        {
            // Add get related items service logic here to set ViewData if necessary and update null parameter in SelectList with these items
            return View();
        }

        // POST: Markalar/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(MarkaModel marka)
        {
            if (ModelState.IsValid)
            {
                Result result = _markaService.Add(marka);
                if (result.IsSuccessful)
                    return RedirectToAction(nameof(Index));
                ModelState.AddModelError("", result.Message);
            }
            // Add get related items service logic here to set ViewData if necessary and update null parameter in SelectList with these items
            return View(marka);
        }

        // GET: Markalar/Edit/5
        public IActionResult Edit(int? id)
        {
            if (!id.HasValue)
            {
                return View("Hata", "Id gereklidir!");
            }
            MarkaModel marka = _markaService.Query().SingleOrDefault(m => m.Id == id); // TODO: Add get item service logic here
            if (marka == null)
            {
                return View("Hata!", "Marka bulunamadı!");
            }
            // Add get related items service logic here to set ViewData if necessary and update null parameter in SelectList with these items
            return View(marka);
        }

        // POST: Markalar/Edit
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(MarkaModel marka)
        {
            if (ModelState.IsValid)
            {
                Result result = _markaService.Add(marka);
                if (result.IsSuccessful)
                    return RedirectToAction(nameof(Index));
                ModelState.AddModelError("", result.Message);
            }
            // Add get related items service logic here to set ViewData if necessary and update null parameter in SelectList with these items
            return View(marka);
        }

        // GET: Markalar/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
                return View("Hata!", "Id gereklidir!");
            MarkaModel marka = _markaService.Query().SingleOrDefault(m => m.Id == id); // TODO: Add get item service logic here
            if (marka == null)
            {
                return View("Hata!", "Marka bulunamadı!");
            }
            return View(marka);
        }

        // POST: Markalar/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            Result result = _markaService.Delete(id);
            TempData["Mesaj"] = result.Message;
            return RedirectToAction(nameof(Index));
        }
	}
}
