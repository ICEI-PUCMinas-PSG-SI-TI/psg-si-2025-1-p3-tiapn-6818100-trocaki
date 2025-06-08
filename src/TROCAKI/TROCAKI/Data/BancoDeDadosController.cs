using MySql.Data.MySqlClient;

namespace TROCAKI.Data
{
    public class BancoDeDadosController
    {
        private const string DataSource = "datasource=localhost;username=root;password=;database=mydb";
        private MySqlConnection conexao;

        public bool ExecutarAcao(string sqlQuery)
        {
            try
            {
                // Criar conexão com MySQL
                conexao = new MySqlConnection();
                conexao.Open();

                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conexao;

                cmd.CommandText = sqlQuery;

                cmd.Prepare();

                MySqlCommand comando = new MySqlCommand(sqlQuery, conexao);
                MySqlDataReader reader = comando.ExecuteReader();
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
    }
}
