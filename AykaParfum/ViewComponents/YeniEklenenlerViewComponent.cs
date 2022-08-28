using Business.Models;
using Business.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;

namespace AykaParfum.ViewComponents
{
    public class YeniEklenenlerViewComponent : ViewComponent
    {
        private readonly IUrunService _urunService;

        public YeniEklenenlerViewComponent(IUrunService urunservice)
        {
            _urunService = urunservice;

        }
        public ViewViewComponentResult Invoke()
        {

            List<UrunModel> yeniEklenenUrunler = _urunService.Query().OrderByDescending(u => u.Id).Take(8).ToList();
            return View(yeniEklenenUrunler);           


        }
    }
}

