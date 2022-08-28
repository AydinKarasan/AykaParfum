using Business.Models;
using Business.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;

namespace AykaParfum.ViewComponents
{
    public class AramalarViewComponent : ViewComponent 
    {
        private readonly IUrunService _urunService;
        
        public AramalarViewComponent(IUrunService urunservice)
        {
            _urunService = urunservice;
            
        }
        public ViewViewComponentResult Invoke()
        {
            
            List<UrunModel> urunler = _urunService.Query().Where(u => u.KategoriId == 1 ).ToList();
            return View(urunler);
            
            
        }

    }
}
