using Business.Models;
using Business.Services;
using Microsoft.AspNetCore.Mvc;

namespace AykaParfum.Controllers.MenuControllers
{
    public class UnisexParfumController : Controller
    {
        private readonly IUrunService _urunService;
        public UnisexParfumController(IUrunService urunService)
        {
            _urunService = urunService;
        }

        public IActionResult Index()
        {
            List<UrunModel> unisexUrunler = _urunService.Query().Where(u => u.KategoriId == 3).ToList();
            return View(unisexUrunler);

        }
    }
}
