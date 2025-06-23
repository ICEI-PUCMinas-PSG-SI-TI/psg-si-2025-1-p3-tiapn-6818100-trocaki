using Microsoft.AspNetCore.Mvc;
using TROCAKI.Models;
using TROCAKI.Repositorio;

namespace TROCAKI.Controllers
{
    public class ProdutoController : Controller
    {
        private readonly ComentarioRepositorio _comentarioRepositorio;
        private readonly ListaDeDesejosRepositorio _listaDeDesejosRepositorio;
        private readonly ProdutoRepositorio _produtoRepositorio;
        private readonly PropostaDeCompraRepositorio _propostaDeCompraRepositorio;

        public ProdutoController(IConfiguration configuracao)
        {
            string stringDeConexao = configuracao.GetConnectionString("DefaultConnection");
            _comentarioRepositorio = new ComentarioRepositorio(stringDeConexao);
            _listaDeDesejosRepositorio = new ListaDeDesejosRepositorio(stringDeConexao);
            _produtoRepositorio = new ProdutoRepositorio(stringDeConexao);
            _propostaDeCompraRepositorio = new PropostaDeCompraRepositorio(stringDeConexao);
        }

        public IActionResult Index(string id)
        {
            ProdutoModel produto = _produtoRepositorio.ObterProduto(id);
            ViewBag.Produto = produto;

            List<ComentarioModel> comentarios = _comentarioRepositorio.ObterComentariosDoProduto(id);
            ViewBag.Comentarios = comentarios;

            return View();
        }

        [HttpPost]
        public JsonResult ObterInformacoesDoUsuario([FromBody] ProdutoDesejoModel request)
        {
            List<PropostaDeCompraModel> propostas = _propostaDeCompraRepositorio.ObterPropostas(request.CompradorId, null, request.ProdutoId, null);

            bool estaNaLista = _listaDeDesejosRepositorio.ProdutoEstaNaListaDeDesejos(request.CompradorId, request.ProdutoId);

            return Json(new
            {
                sucesso = true,
                estaNaLista,
                propostas
            });
        }

        [HttpPost]
        public JsonResult AdicionarProdutoNaListaDeDesejos([FromBody] ProdutoDesejoModel request)
        {
            try
            {
                _listaDeDesejosRepositorio.AdicionarProdutoNaListaDeDesejos(request.CompradorId, request.ProdutoId);
                return Json(new { sucesso = true, mensagem = "Produto adicionado à lista de desejos." });
            }
            catch (Exception ex)
            {
                return Json(new { sucesso = false, mensagem = "Erro ao adicionar produto: " + ex.Message });
            }
        }

        [HttpPost]
        public JsonResult RemoverProdutoDaListaDeDesejos([FromBody] ProdutoDesejoModel request)
        {
            try
            {
                _listaDeDesejosRepositorio.RemoverProdutoDaListaDeDesejos(request.CompradorId, request.ProdutoId);
                return Json(new { sucesso = true, mensagem = "Produto removido da lista de desejos." });
            }
            catch (Exception ex)
            {
                return Json(new { sucesso = false, mensagem = "Erro ao remover: " + ex.Message });
            }
        }

        [HttpPost]
        public JsonResult CriarNovaProposta([FromBody] NovaPropostaModel proposta)
        {
            try
            {
                _propostaDeCompraRepositorio.InserirProposta(proposta);

                return Json(new { sucesso = true });
            }
            catch (Exception ex)
            {
                return Json(new { sucesso = false, erro = ex.Message });
            }
        }
    }
}
