using MySql.Data.MySqlClient;
using TROCAKI.Models;

namespace TROCAKI.Repositorio
{
    public class CategoriaRepositorio
    {
        private readonly string _strindeDeConexao;

        public CategoriaRepositorio(string strindeDeConexao)
        {
            _strindeDeConexao = strindeDeConexao;
        }

        public List<CategoriaModel> ObterCategorias()
        {
            List<CategoriaModel> lista = new List<CategoriaModel>();

            MySqlConnection conexao = new MySqlConnection(_strindeDeConexao);
            conexao.Open();

            MySqlCommand cmd = new MySqlCommand("SELECT * FROM categorias", conexao);
            var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                lista.Add(
                    new CategoriaModel
                    {
                        Id = reader.GetString("Id"),
                        Nome = reader.GetString("Nome")
                    }
                );
            }

            return lista;
        }
    }
}
