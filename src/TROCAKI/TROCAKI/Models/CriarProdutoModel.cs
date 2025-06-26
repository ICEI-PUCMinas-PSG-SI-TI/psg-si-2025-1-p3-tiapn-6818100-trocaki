namespace TROCAKI.Models
{
    public class CriarProdutoModel
    {
        public string Nome { get; set; }
        public double Valor { get; set; }
        public string Descricao { get; set; }
        public List<string> Fotos { get; set; }
        public List<CategoriaModel> Categorias { get; set; }
        public string VendedorId { get; set; }
    }
}
