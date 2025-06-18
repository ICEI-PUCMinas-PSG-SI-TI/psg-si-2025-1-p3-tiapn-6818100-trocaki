using Microsoft.AspNetCore.Mvc;
using TROCAKI.Models;
using TROCAKI.Repositorio;

namespace TROCAKI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CadastrarProdutoController : Controller
    {
        private readonly ProdutoRepositorio _repositorio;

        public CadastrarProdutoController(IConfiguration configuracao)
        {
            string stringDeConexao = configuracao.GetConnectionString("DefaultConnection");
            _repositorio = new ProdutoRepositorio(stringDeConexao);
        }

        [HttpGet]
        public IActionResult Index()
        {
            var caracteristicas = _repositorio.ObterCaracteristicas();
            ViewBag.Caracteristicas = caracteristicas;

            return View();
        }

        [HttpPost("cadastrar")]
        public IActionResult Cadastrar([FromBody] CriarProdutoModel produto)
        {
            string produtoId = _repositorio.CadastrarProduto(produto);

            if (produtoId == null)
                return Unauthorized(new { mensagem = "Não foi possível cadastrar o produto." });

            return Ok(new { id = produtoId });
        }
    }
}
