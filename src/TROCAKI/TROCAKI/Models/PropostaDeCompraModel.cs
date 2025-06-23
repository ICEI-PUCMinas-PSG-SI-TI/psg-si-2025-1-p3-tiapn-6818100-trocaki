namespace TROCAKI.Models
{
    /// <summary>
    /// Representa uma proposta de compra feita por um usuário para um produto.
    /// </summary>
    public class PropostaDeCompraModel
    {
        // Atributos
        public string IdProposta { get; set; }
        public string IdComprador { get; set; }
        public double ValorProposto { get; set; }
        public string Descricao { get; set; }
        public string StatusProposta { get; set; }
        public string ProdutoId { get; set; }
        public string ProdutoNome { get; set; }
        public double ProdutoValor { get; set; }
        public List<string> ProdutoFotos { get; set; }
        public List<CategoriaModel> ProdutoCategorias { get; set; }
    }
}
