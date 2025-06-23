using Microsoft.AspNetCore.Mvc;
using TROCAKI.Models;
using TROCAKI.Repositorio;

namespace TROCAKI.Controllers
{
    public class PropostasRecebidasController : Controller
    {
        private readonly PropostaDeCompraRepositorio _repositorio;

        public PropostasRecebidasController(IConfiguration configuracao)
        {
            string stringDeConexao = configuracao.GetConnectionString("DefaultConnection");
            _repositorio = new PropostaDeCompraRepositorio(stringDeConexao);
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult ObterPropostas([FromBody] UsuarioIdModel usuario)
        {
            if (string.IsNullOrWhiteSpace(usuario?.Id))
                return Json(new { sucesso = false, mensagem = "ID do usuário inválido." });

            var propostas = _repositorio.ObterPropostas(compradorId: null, vendedorId: usuario.Id, produtoId: null, status: "aberta");

            return Json(new { sucesso = true, propostas });
        }
    }
}
