using Microsoft.AspNetCore.Mvc;
using TROCAKI.Models;
using TROCAKI.Repositorio;

namespace TROCAKI.Controllers
{
    public class CadastrarProdutoController : Controller
    {
        private readonly ProdutoRepositorio _produtoRepositorio;
        private readonly CategoriaRepositorio _categoriaRepositorio;

        public CadastrarProdutoController(IConfiguration configuracao)
        {
            string stringDeConexao = configuracao.GetConnectionString("DefaultConnection");
            _produtoRepositorio = new ProdutoRepositorio(stringDeConexao);
            _categoriaRepositorio = new CategoriaRepositorio(stringDeConexao);
        }

        public IActionResult Index()
        {
            try
            {
                var categorias = _categoriaRepositorio.ObterCategorias();
                ViewBag.Caracteristicas = categorias;
                return View();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { sucesso = false, mensagem = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult Cadastrar([FromBody] CriarProdutoModel produto)
        {
            try
            {
                var produtoId = _produtoRepositorio.CadastrarProduto(produto);

                return Json(new { sucesso = true, id = produtoId });
            }
            catch (Exception ex)
            {
                return Json(new { sucesso = false, mensagem = ex.Message });
            }
        }
    }
}
