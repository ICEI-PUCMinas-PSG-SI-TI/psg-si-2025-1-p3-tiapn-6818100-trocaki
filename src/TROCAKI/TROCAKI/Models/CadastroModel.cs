using MySql.Data.MySqlClient;

namespace TROCAKI.Models
{
    public class CadastroModel
    {
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Cpf { get; set; }
        public string Telefone { get; set; }
        public string DataNascimento { get; set; }
        public string Cidade { get; set; }
        public string FotoDocumento { get; set; }
        public string Senha { get; set; }

        public static string CadastrarUsuario(string nome, string email, string cpf, string telefone,
                                 string dataNascimento, string cidade, string fotoDocumento, string senha)
        {
            try
            {
                string DataSource = "datasource=localhost;username=root;password=;database=mydb";
                MySqlConnection conexao = new MySqlConnection(DataSource);
                conexao.Open();

                string query = @"INSERT INTO usuário (id, nome, email, cpf, telefone, data_nascimento, cidade, foto_documento, senha)
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
                return null;
            }

        }
    }
}
