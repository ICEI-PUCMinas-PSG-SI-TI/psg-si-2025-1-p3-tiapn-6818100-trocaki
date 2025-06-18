using Microsoft.AspNetCore.Mvc;

namespace TROCAKI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ProdutoService _produtoService;

        public HomeController(IConfiguration configuration)
        {
            string connStr = configuration.GetConnectionString("DefaultConnection");
            _produtoService = new ProdutoService(connStr);
        }
        public IActionResult Index()
        {
            var produtos = _produtoService.ObterProdutos();
            return View(produtos);
        }
    }
}
