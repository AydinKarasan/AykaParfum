using Business.Services.Hesap;
using Microsoft.AspNetCore.Mvc;

namespace AykaParfum.Controllers
{
    [Route("[controller]")]
    public class SehirlerAjaxController : Controller
    {
        private readonly ISehirService _sehirService;
        public SehirlerAjaxController(ISehirService sehirService)
        {
            _sehirService = sehirService;
        }
        
        [Route("SehirlerGet/{ulkeId?}")]
        public IActionResult SehirlerGet(int? UlkeId) //SehirlerAjax/SehirlerGet/1
        {
            var sehirler = _sehirService.Query().Where(s => s.UlkeId == UlkeId).ToList();
            return Json(sehirler);
        }
    }
}
