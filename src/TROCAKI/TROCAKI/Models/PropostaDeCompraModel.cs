namespace TROCAKI.Models
{
    /// <summary>
    /// Representa uma proposta de compra feita por um usuário para um produto.
    /// </summary>
    public class PropostaDeCompraModel
    {
        // Atributos
        public string PropostaId { get; set; }
        public double ValorProposto { get; set; }
        public string Descricao { get; set; }
        public string PropostaStatus { get; set; }
        public string ProdutoId { get; set; }
        public string ProdutoNome { get; set; }
        public double ProdutoValor { get; set; }
        public List<string> ProdutoFotos { get; set; }
        public List<CategoriaModel> ProdutoCategorias { get; set; }
        public string CompradorId { get; set; }
        public string CompradorNome { get; set; }
        public string CompradorTelefone { get; set; }
        public string CompradorEmail { get; set; }
        public string VendedorId { get; set; }
        public string VendedorNome { get; set; }
        public string VendedorTelefone { get; set; }
        public string VendedorEmail { get; set; }
    }
}
