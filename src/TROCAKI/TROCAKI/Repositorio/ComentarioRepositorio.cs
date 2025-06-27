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
                    ProdutoId = reader.GetString("Produto_Id"),
                    CompradorId = reader.GetString("comprador_id"),
                    CompradorNome = reader.GetString("comprador_nome"),
                    VendedorId = reader.GetString("vendedor_id"),
                    VendedorNome = reader.GetString("vendedor_nome")
                };

                lista.Add(comentario);
            }

            return lista;
        }

        public void InserirComentario(CriarComentarioModel comentario)
        {
            using var conexao = new MySqlConnection(_strindeDeConexao);
            conexao.Open();

            string query = @"
                INSERT INTO comentarios (id, texto, resposta, Produto_id, Comprador_id)
                VALUES (@id, @texto, '', @produto_id, @comprador_id)
            ";

            using var cmd = new MySqlCommand(query, conexao);
            cmd.Parameters.AddWithValue("@id", Guid.NewGuid().ToString());
            cmd.Parameters.AddWithValue("@texto", comentario.Texto);
            cmd.Parameters.AddWithValue("@produto_id", comentario.ProdutoId);
            cmd.Parameters.AddWithValue("@comprador_id", comentario.CompradorId);

            cmd.ExecuteNonQuery();
        }

        public void ResponderComentario(RespostaComentarioModel model)
        {
            using var conexao = new MySqlConnection(_strindeDeConexao);
            conexao.Open();

            var query = @"
                UPDATE comentarios
                SET resposta = @resposta
                WHERE id = @comentarioId
            ";

            using var cmd = new MySqlCommand(query, conexao);
            cmd.Parameters.AddWithValue("@resposta", model.Resposta);
            cmd.Parameters.AddWithValue("@comentarioId", model.ComentarioId);

            cmd.ExecuteNonQuery();
        }
    }
}
