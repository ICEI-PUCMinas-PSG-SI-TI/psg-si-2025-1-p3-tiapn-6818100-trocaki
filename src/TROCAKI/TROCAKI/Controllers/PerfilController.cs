using Microsoft.AspNetCore.Mvc;
using TROCAKI.Models;
using TROCAKI.Repositorio;

namespace TROCAKI.Controllers
{
    public class PerfilController : Controller
    {
        private readonly ListaDeDesejosRepositorio _listaDeDesejosRepositorio;
        private readonly ProdutoRepositorio _produtoRepositorio;
        private readonly UsuarioContaRepositorio _usuarioContaRepositorio;

        public PerfilController(IConfiguration configuracao)
        {
            string stringDeConexao = configuracao.GetConnectionString("DefaultConnection");
            _usuarioContaRepositorio = new UsuarioContaRepositorio(stringDeConexao);
            _listaDeDesejosRepositorio = new ListaDeDesejosRepositorio(stringDeConexao);
            _produtoRepositorio = new ProdutoRepositorio(stringDeConexao);
        }

        [HttpPost]
        public JsonResult ObterInformacoesDoUsuario([FromBody] BuscaUsuarioModel usuarioBuscado)
        {
            // Buscar o usuário
            UsuarioModel usuario = _usuarioContaRepositorio.ObterUsuarioPorId(usuarioBuscado.UserId);
            if (usuario == null) return Json(new { erro = usuarioBuscado.UserId });

            // Buscar produtos do usuário (vendedor)
            List<ProdutoModel> meusProdutos = _produtoRepositorio.ObterProdutosPorVendedor(usuarioBuscado.UserId);

            // Buscar lista de desejos
            List<ProdutoModel> listaDeDesejos = _listaDeDesejosRepositorio.ObterListaDeDesejosPorComprador(usuarioBuscado.UserId);

            return Json(new
            {
                usuario,
                meusProdutos,
                listaDeDesejos
            });
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
