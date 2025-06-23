namespace TROCAKI.Models
{
    public class ComentarioModel
    {
        public string Id { get; set; }
        public string Texto { get; set; }
        public string Resposta { get; set; }
        public string IdProduto { get; set; }
        public string CompradorId { get; set; }
        public string CompradorNome { get; set; }
        public string VendedorId { get; set; }
        public string VendedorNome { get; set; }
    }
}
