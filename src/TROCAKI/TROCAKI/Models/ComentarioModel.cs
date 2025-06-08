namespace TROCAKI.Models
{
    /// <summary>
    /// Representa um comentário feito por um usuário em um produto.
    /// </summary>
    public class ComentarioModel
    {
        // Atributos
        public const int TAM_MAX_TEXTO = 500;
        public string IdComentario { get; set; }
        public string IdUsuario { get; set; }
        public string IdProduto { get; set; }
        public string Mensagem { get; set; }

        // Métodos
        //public override int GetHashCode() { }
        //public override string ToString() { }
        //public void DeletarComentario() { }
    }
}
