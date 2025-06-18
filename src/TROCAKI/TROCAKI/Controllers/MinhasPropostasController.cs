using Microsoft.AspNetCore.Mvc;

namespace TROCAKI.Controllers
{
    public class MinhasPropostasController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
