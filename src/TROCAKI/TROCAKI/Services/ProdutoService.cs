using MySql.Data.MySqlClient;
using TROCAKI.Models;

public class ProdutoService
{
    private readonly string _connectionString;

    public ProdutoService(string connectionString)
    {
        _connectionString = connectionString;
    }

    public List<ProdutoModel> ObterProdutos()
    {
        List<ProdutoModel> lista = new List<ProdutoModel>();

        MySqlConnection conn = new MySqlConnection(_connectionString);
        conn.Open();

        string sql = "SELECT * FROM produtos";

        MySqlCommand cmd = new MySqlCommand(sql, conn);
        using var reader = cmd.ExecuteReader();

        while (reader.Read())
        {
            lista.Add(new ProdutoModel
            {
                Id = reader.GetString("id"),
                Nome = reader.GetString("nome"),
                Valor = reader.GetDouble("valor"),
                Status = reader.GetString("status")
            });
        }

        return lista;
    }
}
