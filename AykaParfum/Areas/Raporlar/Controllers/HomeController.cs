using AppCoreV2.Business.Models.Ordering;
using AppCoreV2.Business.Models.Paging;
using AykaParfum.Areas.Raporlar.Models;
using Business.Models.Filters;
using Business.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AykaParfum.Areas.Raporlar.Controllers
{
    [Area("Raporlar")]
    [Authorize(Roles = "Admin")]
    public class HomeController : Controller
    {
        private readonly IUrunRaporService _urunRaporService;
        private readonly IKategoriService _kategoriService;
        private readonly IMarkaService _markaService;
        private readonly IKampanyaService _kampanyaService;

        public HomeController(IUrunRaporService urunRaporService, IKategoriService kategoriService, IMarkaService markaService, IKampanyaService kampanyaService)
        {
            _urunRaporService = urunRaporService;
            _kategoriService = kategoriService;
            _markaService = markaService;
            _kampanyaService = kampanyaService;
        }

        public IActionResult Index(UrunRaporViewModel urunRaporViewModel)
        {
            urunRaporViewModel.Filtre = urunRaporViewModel.Filtre ?? new UrunRaporFiltreModel(); // urunRaporViewModel.Filtre == null ise new le
            urunRaporViewModel.Sayfa = urunRaporViewModel.Sayfa ?? new PageModel()
            {
                PageNumber = 1,
                RecordsPerPageCount = 5
            };

            urunRaporViewModel.Sira = urunRaporViewModel.Sira ?? new OrderModel()
            {
                IsDirectionAscending = true
            };

            var result = _urunRaporService.List(urunRaporViewModel.Filtre, urunRaporViewModel.Sayfa, urunRaporViewModel.Sira);
            urunRaporViewModel.Rapor = result.Data;

            urunRaporViewModel.Kategoriler = new SelectList(_kategoriService.Query().ToList(), "Id", "Adi");
            urunRaporViewModel.Markalar = new MultiSelectList(_markaService.Query().ToList(), "Id", "Adi");
            urunRaporViewModel.Kampanyalar = new MultiSelectList(_kampanyaService.Query().ToList(), "Id" , "Adi");

            //UrunRaporViewModel viewModel = new UrunRaporViewModel()
            //{
            //    Rapor = result.Data,
            //    Kategoriler = new SelectList(_kategoriService.Query().ToList(), "Id", "Adi")
            //};
            urunRaporViewModel.Sayfalar = new SelectList(urunRaporViewModel.Sayfa.PageNumberList);
            urunRaporViewModel.SayfadakiKayitSayilari = new SelectList(urunRaporViewModel.Sayfa.RecordsPerPageCountList);

            urunRaporViewModel.Siralar = new SelectList(_urunRaporService.GetSiralar());

            return View(urunRaporViewModel);
        }
        
    }
}
