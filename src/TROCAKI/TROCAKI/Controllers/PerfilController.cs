using Microsoft.AspNetCore.Mvc;
using TROCAKI.Models;
using TROCAKI.Repositorio;

namespace TROCAKI.Controllers
{
    public class PerfilController : Controller
    {
        private readonly UsuarioContaRepositorio _usuarioContaRepositorio;

        public PerfilController(IConfiguration configuracao)
        {
            string stringDeConexao = configuracao.GetConnectionString("DefaultConnection");
            _usuarioContaRepositorio = new UsuarioContaRepositorio(stringDeConexao);
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult ObterInformacoesDoUsuario([FromBody] BuscaUsuarioModel usuarioBuscado)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(usuarioBuscado.UserId))
                {
                    return Json(new
                    {
                        sucesso = false,
                        mensagem = "ID de usuário inválido."
                    });
                }

                // Buscar o usuário
                var usuario = _usuarioContaRepositorio.ObterUsuarioPorId(usuarioBuscado.UserId);
                if (usuario == null)
                {
                    return Json(new
                    {
                        sucesso = false,
                        mensagem = "Usuário não encontrado."
                    });
                }

                return Json(new
                {
                    sucesso = true,
                    usuario
                });
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    sucesso = false,
                    mensagem = "Erro ao obter informações do usuário: " + ex.Message
                });
            }
        }

    }
}
