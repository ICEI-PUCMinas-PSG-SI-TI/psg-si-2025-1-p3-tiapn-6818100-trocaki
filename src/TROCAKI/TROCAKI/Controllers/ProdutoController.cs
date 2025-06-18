using Microsoft.AspNetCore.Mvc;

namespace TROCAKI.Controllers
{
    public class ProdutoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
