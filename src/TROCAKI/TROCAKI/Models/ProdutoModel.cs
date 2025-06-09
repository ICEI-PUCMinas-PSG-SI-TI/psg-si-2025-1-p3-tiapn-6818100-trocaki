namespace TROCAKI.Models
{
    /// <summary>
    /// Representa um produto disponível para venda.
    /// </summary>
    public class ProdutoModel
    {
        // Constante
        public const int TAM_MAX_NOME = 50;

        // Atributos
        public string IdProduto { get; set; }
        public string Nome { get; set; }
        public double Valor { get; set; }
        public bool PedidoAberto { get; set; }
        public List<string> FotosProduto { get; set; }
        public List<CategoriaModel> CategoriaProduto { get; set; }

        // Métodos
        //public override int GetHashCode() { }
        //public override string ToString() { }
        //public void MudarStatusPedido() { }
        //public void AdicionarFoto(string url) { }
        //public void RemoverFoto() { }
        //public void AdicionarCategoria(Categoria categoria) { }
        //public void RemoverCategoria(Categoria categoria) { }
        //public void ReceberComentario(Comentario comentario) { }
        //public void RemoverComentario(Comentario comentario) { }
    }
}
