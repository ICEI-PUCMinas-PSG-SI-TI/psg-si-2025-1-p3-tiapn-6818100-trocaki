namespace TROCAKI.Models
{
    public class PedidoModel
    {
        // Atributos
        public string Id { get; set; }
        public string status { get; set; }
        public double PropostaValorProposto { get; set; }
        public string CompradorId { get; set; }
        public string CompradorNome { get; set; }
        public string CompradorTelefone { get; set; }
        public string CompradorEmail { get; set; }
        public string VendedorId { get; set; }
        public string VendedorNome { get; set; }
        public string VendedorTelefone { get; set; }
        public string VendedorEmail { get; set; }
        public string ProdutoId { get; set; }
        public string ProdutoNome { get; set; }
        public string ProdutoDescricao { get; set; }
        public double ProdutoValor { get; set; }
        public List<string> ProdutoFotos { get; set; }
    }
}
