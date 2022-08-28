using Business.Models;
using Business.Services;
using Microsoft.AspNetCore.Mvc;

namespace AykaParfum.Controllers.MenuControllers
{
    public class KadinParfumController : Controller
    {
        private readonly IUrunService _urunService;
        public KadinParfumController(IUrunService urunService)
        {
            _urunService = urunService;
        }

        public IActionResult Index()
        {
            List<UrunModel> kadinUrunler = _urunService.Query().Where(u => u.KategoriId == 2).ToList();
            return View(kadinUrunler);

        }
    }
}
