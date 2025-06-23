using MySql.Data.MySqlClient;
using System.Data;
using System.Text;
using TROCAKI.Models;

namespace TROCAKI.Repositorio
{
    public class PropostaDeCompraRepositorio
    {
        private readonly string _strindeDeConexao;

        public PropostaDeCompraRepositorio(string strindeDeConexao)
        {
            _strindeDeConexao = strindeDeConexao;
        }

        public void InserirProposta(NovaPropostaModel proposta)
        {
            using var conexao = new MySqlConnection(_strindeDeConexao);
            conexao.Open();

            MySqlCommand cmd = new MySqlCommand(@"
                INSERT INTO propostas_de_compra 
                (id, valor_proposto, descricao, status, produto_id, comprador_id)
                VALUES (@id, @valor_proposto, @descricao, @status, @produto_id, @comprador_id)
            ", conexao);

            cmd.Parameters.AddWithValue("@id", Guid.NewGuid().ToString());
            cmd.Parameters.AddWithValue("@valor_proposto", proposta.Valor);
            cmd.Parameters.AddWithValue("@descricao", proposta.Descricao);
            cmd.Parameters.AddWithValue("@status", "aberta");
            cmd.Parameters.AddWithValue("@produto_id", proposta.ProdutoId);
            cmd.Parameters.AddWithValue("@comprador_id", proposta.CompradorId);

            cmd.ExecuteNonQuery();
        }

        public List<PropostaDeCompraModel> ObterPropostas(string? compradorId, string? vendedorId, string? produtoId, string? status)
        {
            var propostas = new List<PropostaDeCompraModel>();

            using var conexao = new MySqlConnection(_strindeDeConexao);
            conexao.Open();

            var sql = new StringBuilder(@"
                SELECT 
                    pc.id,
                    pc.valor_proposto,
                    pc.descricao,
                    pc.status,
                    pc.Comprador_id,
                    pc.Produto_id,
                    p.nome AS Produto_nome,
                    p.valor AS Produto_valor,
                    GROUP_CONCAT(DISTINCT f.foto_base_64 SEPARATOR '||') AS Produto_fotos,
                    GROUP_CONCAT(DISTINCT CONCAT(c.id, ':', c.nome) SEPARATOR '||') AS Produto_categorias
                FROM trocaki.propostas_de_compra pc
                JOIN produtos p ON pc.Produto_Id = p.id
                LEFT JOIN fotos_dos_produtos f ON f.Produto_id = p.id
                LEFT JOIN categorias_dos_produtos cp ON cp.Produto_id = p.id
                LEFT JOIN categorias c ON c.id = cp.Categoria_id
                LEFT JOIN usuarios u ON u.id = p.Vendedor_id
                WHERE 1=1
            ");

            var cmd = new MySqlCommand();
            cmd.Connection = conexao;

            if (!string.IsNullOrWhiteSpace(compradorId))
            {
                sql.Append(" AND pc.Comprador_id = @compradorId");
                cmd.Parameters.AddWithValue("@compradorId", compradorId);
            }

            if (!string.IsNullOrWhiteSpace(vendedorId))
            {
                sql.Append(" AND p.Vendedor_id = @vendedorId");
                cmd.Parameters.AddWithValue("@vendedorId", vendedorId);
            }

            if (!string.IsNullOrWhiteSpace(produtoId))
            {
                sql.Append(" AND pc.Produto_id = @produtoId");
                cmd.Parameters.AddWithValue("@produtoId", produtoId);
            }

            if (!string.IsNullOrWhiteSpace(status))
            {
                sql.Append(" AND pc.status = @status");
                cmd.Parameters.AddWithValue("@status", status);
            }

            sql.Append(" GROUP BY pc.id;");

            cmd.CommandText = sql.ToString();

            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                var proposta = new PropostaDeCompraModel
                {
                    IdProposta = reader["id"] as string ?? "",
                    IdComprador = reader["Comprador_id"] as string ?? "",
                    Descricao = reader["descricao"] as string ?? "",
                    StatusProposta = reader["status"] as string ?? "",
                    ProdutoId = reader["Produto_id"] as string ?? "",
                    ProdutoNome = reader["Produto_nome"] as string ?? "",
                    ValorProposto = reader.IsDBNull("valor_proposto") ? 0 : reader.GetDouble("valor_proposto"),
                    ProdutoValor = reader.IsDBNull("Produto_valor") ? 0 : reader.GetDouble("Produto_valor"),
                    ProdutoFotos = new List<string>(),
                    ProdutoCategorias = new List<CategoriaModel>()
                };

                if (!reader.IsDBNull("Produto_fotos"))
                {
                    proposta.ProdutoFotos = reader.GetString("Produto_fotos")
                        .Split("||", StringSplitOptions.RemoveEmptyEntries)
                        .ToList();
                }

                if (!reader.IsDBNull("Produto_categorias"))
                {
                    var categoriasStr = reader.GetString("Produto_categorias");
                    foreach (var cat in categoriasStr.Split("||", StringSplitOptions.RemoveEmptyEntries))
                    {
                        var partes = cat.Split(':');
                        if (partes.Length == 2)
                        {
                            proposta.ProdutoCategorias.Add(new CategoriaModel
                            {
                                Id = partes[0],
                                Nome = partes[1]
                            });
                        }
                    }
                }

                propostas.Add(proposta);
            }

            return propostas;
        }
    }
}
