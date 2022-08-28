using Business.Models;
using Business.Services;
using Microsoft.AspNetCore.Mvc;

namespace AykaParfum.Controllers
{
    public class ErkekParfumController : Controller
    {

        private readonly IUrunService _urunService;
        public ErkekParfumController(IUrunService urunService)
        {
            _urunService = urunService;           
        }

        public IActionResult Index()
        {
            List<UrunModel> erkekUrunler = _urunService.Query().Where(u => u.KategoriId == 1).ToList();
            return View(erkekUrunler);
            
        }
    }
}
