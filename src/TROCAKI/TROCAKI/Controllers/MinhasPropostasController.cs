using Microsoft.AspNetCore.Mvc;
using TROCAKI.Models;
using TROCAKI.Repositorio;

namespace TROCAKI.Controllers
{
    public class MinhasPropostasController : Controller
    {
        private readonly PropostaDeCompraRepositorio _repositorio;

        public MinhasPropostasController(IConfiguration configuracao)
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
            List<PropostaDeCompraModel> propostas = _repositorio.ObterPropostas(usuario.Id, null, null, null);

            return Json(new { propostas });
        }
    }
}
