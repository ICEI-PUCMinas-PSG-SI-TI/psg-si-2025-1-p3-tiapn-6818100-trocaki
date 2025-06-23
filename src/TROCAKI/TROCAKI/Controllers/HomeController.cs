using Microsoft.AspNetCore.Mvc;
using TROCAKI.Models;
using TROCAKI.Repositorio;

namespace TROCAKI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ProdutoRepositorio _produtoRepositorio;
        private readonly CategoriaRepositorio _categoriaRepositorio;

        public HomeController(IConfiguration configuracao)
        {
            string stringDeConexao = configuracao.GetConnectionString("DefaultConnection");
            _produtoRepositorio = new ProdutoRepositorio(stringDeConexao);
            _categoriaRepositorio = new CategoriaRepositorio(stringDeConexao);
        }

        public IActionResult Index(string? termo, decimal? precoMin, decimal? precoMax, string? cidade, List<string>? categoriasFiltro)
        {
            // Carrega categorias para exibição nos filtros
            var categorias = _categoriaRepositorio.ObterCategorias();
            ViewBag.Categorias = categorias;

            // Decide entre retornar todos os produtos ou aplicar filtros
            List<ProdutoModel> produtos;
            bool filtrosAplicados =
                !string.IsNullOrWhiteSpace(termo) ||
                precoMin.HasValue ||
                precoMax.HasValue ||
                !string.IsNullOrWhiteSpace(cidade) ||
                (categoriasFiltro != null && categoriasFiltro.Count > 0);

            if (filtrosAplicados)
            {
                produtos = _produtoRepositorio.FiltrarProdutos(termo, precoMin, precoMax, cidade, categoriasFiltro);
            }
            else
            {
                produtos = _produtoRepositorio.ObterProdutos();
            }

            ViewBag.Produtos = produtos;

            return View();
        }
    }
}
