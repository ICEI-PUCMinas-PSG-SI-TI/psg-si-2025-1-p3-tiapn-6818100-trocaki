namespace TROCAKI.Models
{
    public class UsuarioModel
    {
        // Constante
        public const int TAM_MAX_NOME = 50;

        // Atributos
        public string Id { get; set; }
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public string FotoDocumento { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public DateTime DataNascimento { get; set; }
        public string Cidade { get; set; }
    }
}
