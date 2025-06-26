using MySql.Data.MySqlClient;
using System.Data;
using TROCAKI.Models;

namespace TROCAKI.Repositorio
{
    public class ProdutoRepositorio
    {
        private readonly string _strindeDeConexao;

        public ProdutoRepositorio(string strindeDeConexao)
        {
            _strindeDeConexao = strindeDeConexao;
        }

        public List<ProdutoModel> ObterProdutosPorVendedor(string vendedorId)
        {
            var lista = new List<ProdutoModel>();

            try
            {
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
                    FROM produtos p
                    LEFT JOIN fotos_dos_produtos f ON f.Produto_id = p.id
                    LEFT JOIN categorias_dos_produtos cp ON cp.Produto_id = p.id
                    LEFT JOIN categorias c ON c.id = cp.Categoria_id
                    LEFT JOIN usuarios u ON u.id = p.Vendedor_id
                    WHERE p.Vendedor_id = @vendedorId
                    GROUP BY p.id;
                ";

                using var cmd = new MySqlCommand(sql, conexao);
                cmd.Parameters.AddWithValue("@vendedorId", vendedorId);

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
                    {
                        string fotosStr = reader.GetString("fotos");
                        produto.Fotos = fotosStr.Split("||", StringSplitOptions.RemoveEmptyEntries).ToList();
                    }

                    if (!reader.IsDBNull("categorias"))
                    {
                        string categoriasStr = reader.GetString("categorias");
                        var categorias = categoriasStr.Split("||", StringSplitOptions.RemoveEmptyEntries);

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
                throw new Exception("Erro ao obter produtos do vendedor: " + ex.Message);
            }
        }

        public string CadastrarProduto(CriarProdutoModel produto)
        {
            using var conexao = new MySqlConnection(_strindeDeConexao);
            conexao.Open();
            using var transacao = conexao.BeginTransaction();

            string idProduto = Guid.NewGuid().ToString();

            try
            {
                // Inserir produto
                var cmdProduto = new MySqlCommand(@"
                    INSERT INTO produtos (id, valor, nome, descricao, status, Vendedor_id)
                    VALUES (@id, @valor, @nome, @descricao, @status, @vendedor_id)",
                conexao, transacao);

                cmdProduto.Parameters.AddWithValue("@id", idProduto);
                cmdProduto.Parameters.AddWithValue("@valor", produto.Valor);
                cmdProduto.Parameters.AddWithValue("@nome", produto.Nome);
                cmdProduto.Parameters.AddWithValue("@descricao", produto.Descricao);
                cmdProduto.Parameters.AddWithValue("@status", "aberto");
                cmdProduto.Parameters.AddWithValue("@vendedor_id", produto.VendedorId);

                cmdProduto.ExecuteNonQuery();

                // Inserir categorias
                foreach (var categoria in produto.Categorias)
                {
                    var cmdCategoria = new MySqlCommand(@"
                        INSERT INTO categorias_dos_produtos (Categoria_id, Produto_id)
                        VALUES (@CategoriaId, @ProdutoId)",
                    conexao, transacao);

                    cmdCategoria.Parameters.AddWithValue("@CategoriaId", categoria.Id);
                    cmdCategoria.Parameters.AddWithValue("@ProdutoId", idProduto);

                    cmdCategoria.ExecuteNonQuery();
                }

                // Inserir fotos
                foreach (var fotoBase64 in produto.Fotos)
                {
                    var cmdFoto = new MySqlCommand(@"
                        INSERT INTO fotos_dos_produtos (Produto_id, foto_base_64)
                        VALUES (@ProdutoId, @foto)",
                    conexao, transacao);

                    cmdFoto.Parameters.AddWithValue("@ProdutoId", idProduto);
                    cmdFoto.Parameters.AddWithValue("@foto", fotoBase64);

                    cmdFoto.ExecuteNonQuery();
                }

                transacao.Commit();
                return idProduto;
            }
            catch (Exception ex)
            {
                transacao.Rollback();
                throw new Exception("Erro ao cadastrar produto: " + ex.Message);
            }
        }

        public List<ProdutoModel> ObterProdutos()
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
                FROM produtos p
                LEFT JOIN fotos_dos_produtos f ON f.Produto_id = p.id
                LEFT JOIN categorias_dos_produtos cp ON cp.Produto_id = p.id
                LEFT JOIN categorias c ON c.id = cp.Categoria_id
                LEFT JOIN usuarios u ON u.id = p.Vendedor_id
                WHERE status = 'aberto'
                GROUP BY p.id;
            ";

            MySqlCommand cmd = new MySqlCommand(sql, conexao);
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

        public ProdutoModel? ObterProduto(string produtoId)
        {
            ProdutoModel? produto = null;

            MySqlConnection conexao = new MySqlConnection(_strindeDeConexao);
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
                FROM produtos p
                LEFT JOIN fotos_dos_produtos f ON f.Produto_id = p.id
                LEFT JOIN categorias_dos_produtos cp ON cp.Produto_id = p.id
                LEFT JOIN categorias c ON c.id = cp.Categoria_id
                LEFT JOIN usuarios u ON u.id = p.Vendedor_id
                WHERE p.id = @produtoId
                GROUP BY p.id;
            ";

            MySqlCommand cmd = new MySqlCommand(sql, conexao);
            cmd.Parameters.AddWithValue("@produtoId", produtoId);

            using var reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                produto = new ProdutoModel
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

                // Fotos base64
                if (!reader.IsDBNull("fotos"))
                {
                    string fotosStr = reader.GetString("fotos");
                    produto.Fotos = fotosStr.Split("||", StringSplitOptions.RemoveEmptyEntries).ToList();
                }

                // Categorias
                if (!reader.IsDBNull("categorias"))
                {
                    string categoriasStr = reader.GetString("categorias");
                    var categorias = categoriasStr.Split("||", StringSplitOptions.RemoveEmptyEntries);

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
            }

            return produto;
        }

        public List<ProdutoModel> FiltrarProdutos(string? termo, decimal? precoMin, decimal? precoMax, string? cidade, List<string>? categorias)
        {
            List<ProdutoModel> produtos = ObterProdutos(); // Puxa todos os produtos completos

            // Aplicar filtros em memória com LINQ
            if (!string.IsNullOrWhiteSpace(termo))
            {
                string termoLower = termo.ToLower();
                produtos = produtos
                    .Where(p =>
                        (!string.IsNullOrEmpty(p.Nome) && p.Nome.ToLower().Contains(termoLower)) ||
                        (!string.IsNullOrEmpty(p.Descricao) && p.Descricao.ToLower().Contains(termoLower))
                    )
                    .ToList();
            }

            if (precoMin.HasValue)
            {
                produtos = produtos.Where(p => p.Valor >= (double)precoMin.Value).ToList();
            }

            if (precoMax.HasValue)
            {
                produtos = produtos.Where(p => p.Valor <= (double)precoMax.Value).ToList();
            }

            if (!string.IsNullOrWhiteSpace(cidade))
            {
                string cidadeLower = cidade.ToLower();
                produtos = produtos
                    .Where(p => p.VendedorCidade?.ToLower().Contains(cidadeLower) == true)
                    .ToList();
            }

            if (categorias != null && categorias.Count > 0)
            {
                var categoriasNormalizadas = categorias.Select(c => c.Trim().ToLowerInvariant()).ToList();

                produtos = produtos
                    .Where(p =>
                        p.Categorias.Any(c =>
                            !string.IsNullOrWhiteSpace(c.Id) &&
                            categoriasNormalizadas.Contains(c.Id.Trim().ToLowerInvariant())
                        )
                    )
                    .ToList();
            }

            return produtos;
        }
    }
}
