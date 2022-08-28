using Business.Models;
using Business.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;

namespace AykaParfum.ViewComponents
{
    public class CokSatanUrunlerViewComponent : ViewComponent
    {
        private readonly IUrunService _urunService;

        public CokSatanUrunlerViewComponent(IUrunService urunservice)
        {
            _urunService = urunservice;

        }
        public ViewViewComponentResult Invoke()
        {

            List<UrunModel> cokSatanUrunler = _urunService.Query().OrderByDescending(u => u.StokMiktari).Take(8).ToList();
            return View(cokSatanUrunler);


        }
    }
}
