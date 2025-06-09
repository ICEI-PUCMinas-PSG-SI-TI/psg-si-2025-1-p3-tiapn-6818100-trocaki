using Microsoft.AspNetCore.Mvc;
using TROCAKI.Models;

namespace TROCAKI.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Entrar()
        {
            return View();
        }

        public IActionResult Cadastrar()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Cadastrar(UsuarioModel usuario)
        {
            usuario.CadastrarUsuario();

            return RedirectToAction("Index");
        }
    }
}
