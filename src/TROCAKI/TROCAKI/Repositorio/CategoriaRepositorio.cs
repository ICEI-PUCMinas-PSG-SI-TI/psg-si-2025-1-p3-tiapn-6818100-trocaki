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
            var lista = new List<CategoriaModel>();

            try
            {
                using var conexao = new MySqlConnection(_strindeDeConexao);
                conexao.Open();

                var query = "SELECT * FROM categorias";
                using var cmd = new MySqlCommand(query, conexao);
                using var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    lista.Add(new CategoriaModel
                    {
                        Id = reader.GetString("Id"),
                        Nome = reader.GetString("Nome")
                    });
                }

                return lista;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao obter categorias: " + ex.Message);
            }
        }
    }
}
