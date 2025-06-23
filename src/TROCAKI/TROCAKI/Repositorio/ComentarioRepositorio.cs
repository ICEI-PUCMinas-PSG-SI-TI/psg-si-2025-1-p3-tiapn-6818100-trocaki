using MySql.Data.MySqlClient;
using TROCAKI.Models;

namespace TROCAKI.Repositorio
{
    public class ComentarioRepositorio
    {
        private readonly string _strindeDeConexao;

        public ComentarioRepositorio(string strindeDeConexao)
        {
            _strindeDeConexao = strindeDeConexao;
        }

        public List<ComentarioModel> ObterComentariosDoProduto(string produtoId)
        {
            List<ComentarioModel> lista = new List<ComentarioModel>();

            using var conexao = new MySqlConnection(_strindeDeConexao);
            conexao.Open();

            string sql = @"
                SELECT
                    c.id,
                    c.texto,
                    c.resposta,
                    c.Produto_Id,
                    p.vendedor_id AS vendedor_id,
                    u_vendedor.nome AS vendedor_nome,
                    c.Comprador_Id AS comprador_id,
                    u_comprador.nome AS comprador_nome
                FROM comentarios c
                JOIN produtos p ON c.Produto_Id = p.id
                JOIN usuarios u_vendedor ON p.vendedor_id = u_vendedor.id
                JOIN usuarios u_comprador ON c.Comprador_Id = u_comprador.id
                WHERE c.Produto_Id = @produtoId;
            ";

            MySqlCommand cmd = new MySqlCommand(sql, conexao);
            cmd.Parameters.AddWithValue("@produtoId", produtoId);

            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                ComentarioModel comentario = new ComentarioModel
                {
                    Id = reader.GetString("id"),
                    Texto = reader.GetString("texto"),
                    Resposta = reader.GetString("resposta"),
                    IdProduto = reader.GetString("Produto_Id"),
                    CompradorId = reader.GetString("comprador_id"),
                    CompradorNome = reader.GetString("comprador_nome"),
                    VendedorId = reader.GetString("vendedor_id"),
                    VendedorNome = reader.GetString("vendedor_nome")
                };

                lista.Add(comentario);
            }

            return lista;
        }
    }
}
