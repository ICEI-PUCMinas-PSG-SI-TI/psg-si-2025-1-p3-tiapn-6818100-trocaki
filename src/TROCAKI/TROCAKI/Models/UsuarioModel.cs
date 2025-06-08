using MySql.Data.MySqlClient;

namespace TROCAKI.Models
{
    /// <summary>
    /// Representa um usuário do sistema.
    /// </summary>
    public class UsuarioModel
    {
        // private string DataSource = "datasource=localhost;username=root;password=;database=mydb";
        private MySqlConnection conexao = new MySqlConnection("datasource=localhost;username=root;password=;database=mydb");

        // Constante
        public const int TAM_MAX_NOME = 50;

        // Atributos
        public string IdUsuario { get; set; }
        public string Nome { get; set; }
        public short Cpf { get; set; }
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
            try
            {
                // Criar conexão com MySQL
                // conexao = new MySqlConnection(DataSource);
                conexao.Open();

                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conexao;

                cmd.CommandText = $"INSERT INTO Usuario (id, nome, cpf, data_nascimento, telefone, cidade, foto_documento, senha, email)" +
                    $"VALUES (@id, @nome, @cpf, @data, @telefone, @cidade, @foto, @senha, @email);";

                cmd.Prepare();

                cmd.Parameters.AddWithValue("@id", GerarId());
                cmd.Parameters.AddWithValue("@nome", Nome);
                cmd.Parameters.AddWithValue("@cpf", Cpf);
                cmd.Parameters.AddWithValue("@data", DataNascimento);
                cmd.Parameters.AddWithValue("@telefone", Telefone);
                cmd.Parameters.AddWithValue("@cidade", Cidade);
                cmd.Parameters.AddWithValue("@foto", "");
                cmd.Parameters.AddWithValue("@senha", Senha);
                cmd.Parameters.AddWithValue("@email", Email);

                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                return false;
            }
            finally
            {
                conexao.Close();
            }

            return true;
        }

        private string GerarId()
        {
            return "0";
        }
    }
}
