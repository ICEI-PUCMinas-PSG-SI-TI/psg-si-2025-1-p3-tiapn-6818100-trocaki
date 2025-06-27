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

        public List<ProdutoModel> ObterListaDeDesejosPorComprador(string compradorId)
        {
            var lista = new List<ProdutoModel>();

            try
            {
                using var conexao = new MySqlConnection(_strindeDeConexao);
                conexao.Open();

                string sql = @"
                    SELECT 
                        p.id, p.nome, p.valor, p.descricao, p.status,
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
                    WHERE l.Comprador_Id = @compradorId
                    GROUP BY p.id;
                ";

                using var cmd = new MySqlCommand(sql, conexao);
                cmd.Parameters.AddWithValue("@compradorId", compradorId);

                using var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    var produto = new ProdutoModel
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
                        VendedorCidade = reader.GetString("Vendedor_cidade")
                    };

                    if (!reader.IsDBNull("fotos"))
                        produto.Fotos = reader.GetString("fotos").Split("||", StringSplitOptions.RemoveEmptyEntries).ToList();

                    if (!reader.IsDBNull("categorias"))
                    {
                        string[] categorias = reader.GetString("categorias").Split("||", StringSplitOptions.RemoveEmptyEntries);
                        foreach (string cat in categorias)
                        {
                            var partes = cat.Split(':');
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
            catch (Exception ex)
            {
                throw new Exception("Erro ao buscar lista de desejos: " + ex.Message);
            }
        }

        public bool ProdutoEstaNaListaDeDesejos(string compradorId, string produtoId)
        {
            try
            {
                using var conexao = new MySqlConnection(_strindeDeConexao);
                conexao.Open();

                string sql = @"
                    SELECT 1
                    FROM listas_de_desejos
                    WHERE Comprador_Id = @compradorId AND Produto_Id = @produtoId
                    LIMIT 1;
                ";

                using var cmd = new MySqlCommand(sql, conexao);
                cmd.Parameters.AddWithValue("@compradorId", compradorId);
                cmd.Parameters.AddWithValue("@produtoId", produtoId);

                using var reader = cmd.ExecuteReader();
                return reader.Read();
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao verificar produto na lista de desejos: " + ex.Message);
            }
        }

        public void AdicionarProdutoNaListaDeDesejos(string compradorId, string produtoId)
        {
            try
            {
                using var conexao = new MySqlConnection(_strindeDeConexao);
                conexao.Open();

                using var cmd = new MySqlCommand(@"
                    INSERT INTO listas_de_desejos (Comprador_id, Produto_id)
                    VALUES (@compradorId, @produtoId)"
                , conexao);

                cmd.Parameters.AddWithValue("@compradorId", compradorId);
                cmd.Parameters.AddWithValue("@produtoId", produtoId);

                cmd.ExecuteNonQuery();
            }
            catch (MySqlException ex) when (ex.Number == 1062)
            {
                throw new Exception("Este produto já está na sua lista de desejos.");
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao adicionar produto à lista de desejos: " + ex.Message);
            }
        }


        public void RemoverProdutoDaListaDeDesejos(string compradorId, string produtoId)
        {
            try
            {
                using var conexao = new MySqlConnection(_strindeDeConexao);
                conexao.Open();

                using var cmd = new MySqlCommand(@"
                    DELETE FROM listas_de_desejos
                    WHERE Comprador_Id = @compradorId AND Produto_Id = @produtoId",
                conexao);

                cmd.Parameters.AddWithValue("@compradorId", compradorId);
                cmd.Parameters.AddWithValue("@produtoId", produtoId);

                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao remover produto da lista de desejos: " + ex.Message);
            }
        }

    }
}
