using MySql.Data.MySqlClient;

namespace TROCAKI.Models
{
    /// <summary>
    /// Representa um usuário do sistema.
    /// </summary>
    public class UsuarioModel
    {
        // Constante
        public const int TAM_MAX_NOME = 50;

        // Atributos
        public string IdUsuario { get; set; }
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public DateTime DataNascimento { get; set; }
        public string Cidade { get; set; }
        public string Senha { get; set; }
        public string Telefone { get; set; }
        public string FotoDocumento { get; set; }
        public string Email { get; set; }
        public List<ProdutoModel> ListaDesejos { get; set; }

        // Métodos
        //public override int GetHashCode() { }
        //public override string ToString() { }
        //public void AdicionarProdutoSite(Produto produto) { }
        //public void RemoverProdutoSite(Produto produto) { }
        //public void AdicionarComentario(Comentario comentario) { }
        //public void RemoverComentario(Comentario comentario) { }
        //public void RealizarPedido(Produto produto) { }
        //public void AdicionarNaListaDesejos(Produto produto) { }
        //public void RemoverDaListaDesejos(Produto produto) { }

        public bool CadastrarUsuario()
        {
            string DataSource = "datasource=localhost;username=root;password=;database=mydb";

            // Criar conexão com MySQL
            MySqlConnection conexao = new MySqlConnection(DataSource);
            conexao.Open();

            string sql = $"INSERT INTO usuário (id, nome, cpf, data_nascimento, telefone, cidade, foto_documento, senha, email)" +
                $" VALUES ('{GerarId()}', '{Nome}', '{Cpf}', '2000-01-15', '{Telefone}', '{Cidade}', 'base64_ou_binario_do_documento', '{Senha}', '{Email}')";

            MySqlCommand comando = new MySqlCommand(sql, conexao);
            comando.ExecuteNonQuery();

            conexao.Close();

            return true;
        }

        private string GerarId()
        {
            return Guid.NewGuid().ToString();
        }
    }
}
