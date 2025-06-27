using Microsoft.AspNetCore.Mvc;
using TROCAKI.Models;
using TROCAKI.Repositorio;

namespace TROCAKI.Controllers
{
    public class PainelDeVendasController : Controller
    {
        private readonly PropostaDeCompraRepositorio _propostaDeCompraRepositorio;
        private readonly ProdutoRepositorio _produtoRepositorio;
        private readonly PedidoRepositorio _pedidoRepositorio;

        public PainelDeVendasController(IConfiguration configuracao)
        {
            string stringDeConexao = configuracao.GetConnectionString("DefaultConnection");
            _propostaDeCompraRepositorio = new PropostaDeCompraRepositorio(stringDeConexao);
            _produtoRepositorio = new ProdutoRepositorio(stringDeConexao);
            _pedidoRepositorio = new PedidoRepositorio(stringDeConexao);
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

            try
            {
                List<PropostaDeCompraModel> propostas = _propostaDeCompraRepositorio.ObterPropostas(compradorId: null, vendedorId: usuario.Id, produtoId: null, status: "aberta");

                List<ProdutoModel> meusProdutos = _produtoRepositorio.ObterProdutosPorVendedor(usuario.Id);

                List<PedidoModel> pedidosGerados = _pedidoRepositorio.ObterProdutosPorVendedor(usuario.Id);

                return Json(new
                {
                    sucesso = true,
                    propostas,
                    meusProdutos,
                    pedidosGerados
                });
            }
            catch (Exception ex)
            {
                return Json(new { sucesso = false, mensagem = "Erro ao obter informações do usuário.", detalhes = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult AceitarProposta([FromBody] PropostaDeCompraIdModel model)
        {
            if (string.IsNullOrWhiteSpace(model?.Id))
                return Json(new { sucesso = false, mensagem = "ID da proposta inválido." });

            try
            {
                _propostaDeCompraRepositorio.AceitarProposta(model.Id);
                return Json(new { sucesso = true });
            }
            catch (Exception ex)
            {
                return Json(new { sucesso = false, mensagem = "Erro ao aceitar proposta.", detalhes = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult RejeitarProposta([FromBody] PropostaDeCompraIdModel model)
        {
            if (string.IsNullOrWhiteSpace(model?.Id))
                return Json(new { sucesso = false, mensagem = "ID da proposta inválido." });

            try
            {
                _propostaDeCompraRepositorio.RejeitarProposta(model.Id);
                return Json(new { sucesso = true });
            }
            catch (Exception ex)
            {
                return Json(new { sucesso = false, mensagem = "Erro ao rejeitar proposta.", detalhes = ex.Message });
            }
        }
    }
}
