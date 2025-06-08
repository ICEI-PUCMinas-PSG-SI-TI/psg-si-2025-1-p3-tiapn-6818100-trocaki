namespace TROCAKI.Models
{
    /// <summary>
    /// Representa uma proposta de compra feita por um usuário para um produto.
    /// </summary>
    public class PropostaDeCompraModel
    {
        // Atributos
        public string IdProposta { get; set; }
        public string IdProduto { get; set; }
        public string IdComprador { get; set; }
        public string Especificar { get; set; }
        public double ValorProposto { get; set; }
        public string StatusProposta { get; set; }

        // Métodos
        //public override int GetHashCode() { }
        //public override string ToString() { }
        //public void MudarStatus() { }
    }
}
