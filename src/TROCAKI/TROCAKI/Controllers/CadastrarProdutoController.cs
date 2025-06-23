using Microsoft.AspNetCore.Mvc;
using TROCAKI.Models;
using TROCAKI.Repositorio;

namespace TROCAKI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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

        [HttpGet]
        public IActionResult Index()
        {
            List<CategoriaModel> categorias = _categoriaRepositorio.ObterCategorias();
            ViewBag.Caracteristicas = categorias;

            return View();
        }

        [HttpPost("cadastrar")]
        public IActionResult Cadastrar([FromBody] CriarProdutoModel produto)
        {
            string produtoId = _produtoRepositorio.CadastrarProduto(produto);

            if (produtoId == null)
                return Unauthorized(new { mensagem = "Não foi possível cadastrar o produto." });

            return Ok(new { id = produtoId });
        }
    }
}
