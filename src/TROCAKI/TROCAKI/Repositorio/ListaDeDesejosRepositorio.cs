using MySql.Data.MySqlClient;
using System.Data;
using TROCAKI.Models;

namespace TROCAKI.Repositorio
{
    public class ListaDeDesejosRepositorio
    {
        private readonly string _strindeDeConexao;

        public ListaDeDesejosRepositorio(string strindeDeConexao)
        {
            _strindeDeConexao = strindeDeConexao;
        }

        public List<ProdutoModel> ObterListaDeDesejosPorComprador(string vendedorId)
        {
            List<ProdutoModel> lista = new List<ProdutoModel>();

            using var conexao = new MySqlConnection(_strindeDeConexao);
            conexao.Open();

            string sql = @"
                SELECT 
                    p.id,
                    p.nome,
                    p.valor,
                    p.descricao,
                    p.status,
                    GROUP_CONCAT(DISTINCT f.foto_base_64 SEPARATOR '||') AS fotos,
                    GROUP_CONCAT(DISTINCT CONCAT(c.id, ':', c.nome) SEPARATOR '||') AS categorias,
                    p.Vendedor_id,
                    u.nome AS Vendedor_nome,
                    u.email AS Vendedor_email,
                    u.telefone AS Vendedor_telefone,
                    u.cidade AS Vendedor_cidade
                FROM listas_de_desejos l
                JOIN produtos p ON p.id = l.Produto_Id
                LEFT JOIN fotos_dos_produtos f ON f.Produto_id = p.id
                LEFT JOIN categorias_dos_produtos cp ON cp.Produto_id = p.id
                LEFT JOIN categorias c ON c.id = cp.Categoria_id
                LEFT JOIN usuarios u ON u.id = p.Vendedor_id
                WHERE l.Comprador_Id = @vendedorId
                GROUP BY p.id;
            ";

            MySqlCommand cmd = new MySqlCommand(sql, conexao);
            cmd.Parameters.AddWithValue("@vendedorId", vendedorId);

            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                ProdutoModel produto = new ProdutoModel
                {
                    Id = reader.GetString("id"),
                    Nome = reader.GetString("nome"),
                    Valor = reader.GetDouble("valor"),
                    Descricao = reader.GetString("descricao"),
                    Status = reader.GetString("status"),
                    Fotos = new List<string>(),
                    Categorias = new List<CategoriaModel>(),
                    VendedorId = reader.GetString("Vendedor_id"),
                    VendedorNome = reader.GetString("Vendedor_nome"),
                    VendedorEmail = reader.GetString("Vendedor_email"),
                    VendedorTelefone = reader.GetString("Vendedor_telefone"),
                    VendedorCidade = reader.GetString("Vendedor_cidade"),
                };

                // Fotos base64 separadas por "||"
                if (!reader.IsDBNull("fotos"))
                {
                    string fotosStr = reader.GetString("fotos");
                    produto.Fotos = fotosStr.Split("||", StringSplitOptions.RemoveEmptyEntries).ToList();
                }

                // Categorias no formato "id:nome||id:nome"
                if (!reader.IsDBNull("categorias"))
                {
                    string categoriasStr = reader.GetString("categorias");
                    string[] categorias = categoriasStr.Split("||", StringSplitOptions.RemoveEmptyEntries);

                    foreach (string cat in categorias)
                    {
                        string[] partes = cat.Split(':');
                        if (partes.Length == 2)
                        {
                            produto.Categorias.Add(new CategoriaModel
                            {
                                Id = partes[0],
                                Nome = partes[1]
                            });
                        }
                    }
                }

                lista.Add(produto);
            }

            return lista;
        }

        public bool ProdutoEstaNaListaDeDesejos(string compradorId, string produtoId)
        {
            MySqlConnection conexao = new MySqlConnection(_strindeDeConexao);
            conexao.Open();

            string sql = @"
                SELECT 1
                FROM listas_de_desejos
                WHERE Comprador_Id = @compradorId AND Produto_Id = @produtoId
                LIMIT 1;
            ";

            MySqlCommand cmd = new MySqlCommand(sql, conexao);
            cmd.Parameters.AddWithValue("@compradorId", compradorId);
            cmd.Parameters.AddWithValue("@produtoId", produtoId);

            using var reader = cmd.ExecuteReader();
            return reader.Read();
        }

        public void AdicionarProdutoNaListaDeDesejos(string compradorId, string produtoId)
        {
            using var conexao = new MySqlConnection(_strindeDeConexao);
            conexao.Open();

            MySqlCommand cmd = new MySqlCommand(@"
                    INSERT INTO listas_de_desejos (Comprador_id, Produto_id)
                    VALUES (@compradorId, @produtoId)
                ", conexao);

            cmd.Parameters.AddWithValue("@compradorId", compradorId);
            cmd.Parameters.AddWithValue("@produtoId", produtoId);

            cmd.ExecuteNonQuery();
        }

        public void RemoverProdutoDaListaDeDesejos(string compradorId, string produtoId)
        {
            using var conexao = new MySqlConnection(_strindeDeConexao);
            conexao.Open();

            var sql = @"
                DELETE FROM listas_de_desejos
                WHERE Comprador_Id = @compradorId AND Produto_Id = @produtoId;
            ";

            using var cmd = new MySqlCommand(sql, conexao);
            cmd.Parameters.AddWithValue("@compradorId", compradorId);
            cmd.Parameters.AddWithValue("@produtoId", produtoId);
            cmd.ExecuteNonQuery();
        }
    }
}
