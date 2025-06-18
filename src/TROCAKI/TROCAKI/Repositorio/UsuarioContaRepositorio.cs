using MySql.Data.MySqlClient;

namespace TROCAKI.Repositorio
{
    public class UsuarioContaRepositorio
    {
        private readonly string _strindeDeConexao;

        public UsuarioContaRepositorio(string strindeDeConexao)
        {
            _strindeDeConexao = strindeDeConexao;
        }

        public string CadastrarUsuario(string nome, string email, string cpf, string telefone,
                                 string dataNascimento, string cidade, string fotoDocumento, string senha)
        {
            try
            {
                MySqlConnection conexao = new MySqlConnection(_strindeDeConexao);
                conexao.Open();

                string query = @"INSERT INTO usuarios (id, nome, email, cpf, telefone, data_nascimento, cidade, foto_documento, senha)
                             VALUES (@id, @nome, @email, @cpf, @telefone, @dataNascimento, @cidade, @fotoDocumento, @senha)";

                using var cmd = new MySqlCommand(query, conexao);

                string id = Guid.NewGuid().ToString();

                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@nome", nome);
                cmd.Parameters.AddWithValue("@email", email);
                cmd.Parameters.AddWithValue("@cpf", cpf);
                cmd.Parameters.AddWithValue("@telefone", telefone);
                cmd.Parameters.AddWithValue("@dataNascimento", dataNascimento);
                cmd.Parameters.AddWithValue("@cidade", cidade);
                cmd.Parameters.AddWithValue("@fotoDocumento", fotoDocumento);
                cmd.Parameters.AddWithValue("@senha", senha);

                int rowsAffected = cmd.ExecuteNonQuery();

                return id;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao cadastrar usuário: " + ex.Message);
            }

        }

        public string BuscarPorEmailSenha(string email, string senha)
        {
            MySqlConnection conexao = new MySqlConnection(_strindeDeConexao);
            conexao.Open();

            var cmd = new MySqlCommand("SELECT * FROM usuarios WHERE email = @Email AND senha = @Senha", conexao);
            cmd.Parameters.AddWithValue("@Email", email);
            cmd.Parameters.AddWithValue("@Senha", senha);

            using var reader = cmd.ExecuteReader();

            if (reader.Read()) return reader["id"].ToString();

            return null;
        }
    }
}
