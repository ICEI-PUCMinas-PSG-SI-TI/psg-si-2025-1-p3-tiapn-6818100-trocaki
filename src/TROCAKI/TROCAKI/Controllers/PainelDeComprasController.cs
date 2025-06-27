using Microsoft.AspNetCore.Mvc;
using TROCAKI.Models;
using TROCAKI.Repositorio;

namespace TROCAKI.Controllers
{
    public class PainelDeComprasController : Controller
    {
        private readonly PropostaDeCompraRepositorio _propostaDeCompraRepositorio;
        private readonly ListaDeDesejosRepositorio _listaDeDesejosRepositorio;
        private readonly PedidoRepositorio _pedidoRepositorio;

        public PainelDeComprasController(IConfiguration configuracao)
        {
            string stringDeConexao = configuracao.GetConnectionString("DefaultConnection");
            _propostaDeCompraRepositorio = new PropostaDeCompraRepositorio(stringDeConexao);
            _listaDeDesejosRepositorio = new ListaDeDesejosRepositorio(stringDeConexao);
            _pedidoRepositorio = new PedidoRepositorio(stringDeConexao);
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult ObterPropostas([FromBody] UsuarioIdModel usuario)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(usuario.Id))
                {
                    return Json(new
                    {
                        sucesso = false,
                        mensagem = "ID de usuário inválido."
                    });
                }

                List<PropostaDeCompraModel> propostas = _propostaDeCompraRepositorio.ObterPropostas(usuario.Id, null, null, null);

                List<ProdutoModel> listaDeDesejos = _listaDeDesejosRepositorio.ObterListaDeDesejosPorComprador(usuario.Id);

                List<PedidoModel> meusPedidos = _pedidoRepositorio.ObterPedidosPorComprador(usuario.Id);

                return Json(new
                {
                    sucesso = true,
                    listaDeDesejos,
                    propostas,
                    meusPedidos
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
