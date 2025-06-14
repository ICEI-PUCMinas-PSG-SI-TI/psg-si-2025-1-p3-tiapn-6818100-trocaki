using Microsoft.AspNetCore.Mvc;
using TROCAKI.Models;

namespace TROCAKI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContaController : Controller
    {
        [HttpPost("cadastrar")]
        public IActionResult Login([FromBody] CadastroModel cadastro)
        {
            string userId = CadastroModel.CadastrarUsuario(
                cadastro.Nome,
                cadastro.Email,
                cadastro.Cpf,
                cadastro.Telefone,
                cadastro.DataNascimento,
                cadastro.Cidade,
                cadastro.FotoDocumento,
                cadastro.Senha
            );

            if (userId == null)
                return Unauthorized(new { mensagem = "Não foi possível cadastrar o usuário." });

            return Ok(new { id = userId });
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginModel login)
        {
            string userId = LoginModel.BuscarPorEmailSenha(login.Email, login.Senha);

            if (userId == null)
                return Unauthorized(new { mensagem = "E-mail ou senha inválidos." });

            return Ok(new { id = userId });
        }
    }
}
