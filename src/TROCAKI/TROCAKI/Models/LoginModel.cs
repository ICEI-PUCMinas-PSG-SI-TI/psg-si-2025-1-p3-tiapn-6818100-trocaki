using MySql.Data.MySqlClient;

namespace TROCAKI.Models
{
    public class LoginModel
    {
        public string Email { get; set; }
        public string Senha { get; set; }

        public static String BuscarPorEmailSenha(string email, string senha)
        {
            string DataSource = "datasource=localhost;username=root;password=;database=mydb";
            MySqlConnection conexao = new MySqlConnection(DataSource);
            conexao.Open();

            var cmd = new MySqlCommand("SELECT * FROM usuário WHERE email = @Email AND senha = @Senha", conexao);
            cmd.Parameters.AddWithValue("@Email", email);
            cmd.Parameters.AddWithValue("@Senha", senha);

            using var reader = cmd.ExecuteReader();

            if (reader.Read()) return reader["id"].ToString();

            return null;
        }
    }
}
