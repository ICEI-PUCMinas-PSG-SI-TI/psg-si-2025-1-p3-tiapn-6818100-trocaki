using MySql.Data.MySqlClient;
using TROCAKI.Models;

namespace TROCAKI.Repositorio
{
    public class UsuarioContaRepositorio
    {
        private readonly string _strindeDeConexao;

        public UsuarioContaRepositorio(string strindeDeConexao)
        {
            _strindeDeConexao = strindeDeConexao;
        }

        public string CadastrarUsuario(CadastroUsuarioModel cadastro)
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
                cmd.Parameters.AddWithValue("@nome", cadastro.Nome);
                cmd.Parameters.AddWithValue("@email", cadastro.Email);
                cmd.Parameters.AddWithValue("@cpf", cadastro.Cpf);
                cmd.Parameters.AddWithValue("@telefone", cadastro.Telefone);
                cmd.Parameters.AddWithValue("@dataNascimento", cadastro.DataNascimento);
                cmd.Parameters.AddWithValue("@cidade", cadastro.Cidade);
                cmd.Parameters.AddWithValue("@fotoDocumento", cadastro.FotoDocumento);
                cmd.Parameters.AddWithValue("@senha", cadastro.Senha);

                int rowsAffected = cmd.ExecuteNonQuery();

                return id;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao cadastrar usuário: " + ex.Message);
            }

        }

        public string BuscarPorEmailSenha(LoginUsuarioModel login)
        {
            MySqlConnection conexao = new MySqlConnection(_strindeDeConexao);
            conexao.Open();

            var cmd = new MySqlCommand("SELECT * FROM usuarios WHERE email = @Email AND senha = @Senha", conexao);
            cmd.Parameters.AddWithValue("@Email", login.Email);
            cmd.Parameters.AddWithValue("@Senha", login.Senha);

            using var reader = cmd.ExecuteReader();

            if (reader.Read()) return reader["id"].ToString();

            return null;
        }

        public UsuarioModel ObterUsuarioPorId(string userId)
        {
            MySqlConnection conexao = new MySqlConnection(_strindeDeConexao);
            conexao.Open();

            var sql = "SELECT * FROM usuarios WHERE id = @userId";
            using var cmd = new MySqlCommand(sql, conexao);
            cmd.Parameters.AddWithValue("@userId", userId);

            using var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                return new UsuarioModel
                {
                    Id = reader.GetString("id"),
                    Nome = reader.GetString("nome"),
                    Cpf = reader.GetString("cpf"),
                    FotoDocumento = reader.GetString("foto_documento"),
                    Email = reader.GetString("email"),
                    Telefone = reader.GetString("telefone"),
                    Cidade = reader.GetString("cidade"),
                    DataNascimento = reader.GetDateTime("data_nascimento")
                };
            }

            return null;
        }
    }
}
