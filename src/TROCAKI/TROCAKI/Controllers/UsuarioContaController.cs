using Microsoft.AspNetCore.Mvc;
using TROCAKI.Models;
using TROCAKI.Repositorio;

namespace TROCAKI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class UsuarioContaController : Controller
    {
        private readonly UsuarioContaRepositorio _repositorio;

        public UsuarioContaController(IConfiguration configuracao)
        {
            string stringDeConexao = configuracao.GetConnectionString("DefaultConnection");
            _repositorio = new UsuarioContaRepositorio(stringDeConexao);
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost("cadastrar")]
        public IActionResult Login([FromBody] CadastroUsuarioModel cadastro)
        {
            string userId = _repositorio.CadastrarUsuario(
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
        public IActionResult Login([FromBody] LoginUsuarioModel login)
        {
            string userId = _repositorio.BuscarPorEmailSenha(login.Email, login.Senha);

            if (userId == null)
                return Unauthorized(new { mensagem = "E-mail ou senha inválidos." });

            return Ok(new { id = userId });
        }
    }
}
