using Microsoft.AspNetCore.Mvc;
using TROCAKI.Models;
using TROCAKI.Repositorio;

namespace TROCAKI.Controllers
{
    public class UsuarioContaController : Controller
    {
        private readonly UsuarioContaRepositorio _repositorio;

        public UsuarioContaController(IConfiguration configuracao)
        {
            string stringDeConexao = configuracao.GetConnectionString("DefaultConnection");
            _repositorio = new UsuarioContaRepositorio(stringDeConexao);
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Cadastrar([FromBody] CadastroUsuarioModel cadastro)
        {
            try
            {
                string userId = _repositorio.CadastrarUsuario(cadastro);
                return Json(new { sucesso = true, id = userId });
            }
            catch (Exception ex)
            {
                return Json(new { sucesso = false, mensagem = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult Logar([FromBody] LoginUsuarioModel login)
        {
            try
            {
                string userId = _repositorio.BuscarPorEmailSenha(login);
                return Json(new { sucesso = true, id = userId });
            }
            catch (Exception ex)
            {
                return Json(new { sucesso = false, mensagem = ex.Message });
            }
        }
    }
}
