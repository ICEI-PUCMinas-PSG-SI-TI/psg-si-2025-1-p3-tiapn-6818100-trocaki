namespace TROCAKI.Models
{
    /// <summary>
    /// Representa um produto disponível para venda.
    /// </summary>
    public class ProdutoModel
    {
        // Atributos
        public string Id { get; set; }
        public string Nome { get; set; }
        public double Valor { get; set; }
        public string Descricao { get; set; }
        public string Status { get; set; }
        public List<string> Fotos { get; set; }
        public List<CategoriaModel> Categorias { get; set; }
        public string VendedorId { get; set; }
        public string VendedorNome { get; set; }
        public string VendedorEmail { get; set; }
        public string VendedorTelefone { get; set; }
        public string VendedorCidade { get; set; }
    }
}
