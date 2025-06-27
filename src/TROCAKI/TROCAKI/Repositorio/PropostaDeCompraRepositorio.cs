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
	                propostaDeCompra.id,
                    propostaDeCompra.valor_proposto,
                    propostaDeCompra.descricao,
                    propostaDeCompra.status,
                    propostaDeCompra.Produto_id,
                    produto.nome AS Produto_nome,
                    produto.valor AS Produto_valor,
                    GROUP_CONCAT(DISTINCT fotoDoProduto.foto_base_64 SEPARATOR '||') AS Produto_fotos,
	                GROUP_CONCAT(DISTINCT CONCAT(categoria.id, ':', categoria.nome) SEPARATOR '||') AS Produto_categorias,
                    propostaDeCompra.Comprador_id,
                    comprador.nome as Comprador_nome,
                    comprador.telefone as Comprador_telefone,
                    comprador.email as Comprador_email,
                    produto.Vendedor_id,
                    vendedor.nome as Vendedor_nome,
                    vendedor.telefone as Vendedor_telefone,
                    vendedor.email as Vendedor_email
                FROM trocaki.propostas_de_compra propostaDeCompra
                JOIN produtos produto ON propostaDeCompra.Produto_Id = produto.id
                LEFT JOIN fotos_dos_produtos fotoDoProduto ON fotoDoProduto.Produto_id = produto.id
                LEFT JOIN categorias_dos_produtos categoriaDoProduto ON categoriaDoProduto.Produto_id = produto.id
                LEFT JOIN categorias categoria ON categoria.id = categoriaDoProduto.Categoria_id
                LEFT JOIN usuarios vendedor ON vendedor.id = produto.Vendedor_id
                LEFT JOIN usuarios comprador ON comprador.id = propostaDeCompra.Comprador_id
                WHERE 1=1
            ");

            var cmd = new MySqlCommand();
            cmd.Connection = conexao;

            if (!string.IsNullOrWhiteSpace(compradorId))
            {
                sql.Append(" AND propostaDeCompra.Comprador_id = @compradorId");
                cmd.Parameters.AddWithValue("@compradorId", compradorId);
            }

            if (!string.IsNullOrWhiteSpace(vendedorId))
            {
                sql.Append(" AND produto.Vendedor_id = @vendedorId");
                cmd.Parameters.AddWithValue("@vendedorId", vendedorId);
            }

            if (!string.IsNullOrWhiteSpace(produtoId))
            {
                sql.Append(" AND propostaDeCompra.Produto_id = @produtoId");
                cmd.Parameters.AddWithValue("@produtoId", produtoId);
            }

            if (!string.IsNullOrWhiteSpace(status))
            {
                sql.Append(" AND propostaDeCompra.status = @status");
                cmd.Parameters.AddWithValue("@status", status);
            }

            sql.Append(" GROUP BY propostaDeCompra.id;");

            cmd.CommandText = sql.ToString();

            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                var proposta = new PropostaDeCompraModel
                {
                    PropostaId = reader["id"] as string ?? "",
                    ValorProposto = reader.IsDBNull("valor_proposto") ? 0 : reader.GetDouble("valor_proposto"),
                    Descricao = reader["descricao"] as string ?? "",
                    PropostaStatus = reader["status"] as string ?? "",
                    ProdutoId = reader["Produto_id"] as string ?? "",
                    ProdutoNome = reader["Produto_nome"] as string ?? "",
                    ProdutoValor = reader.IsDBNull("Produto_valor") ? 0 : reader.GetDouble("Produto_valor"),
                    ProdutoFotos = new List<string>(),
                    ProdutoCategorias = new List<CategoriaModel>(),
                    CompradorId = reader["Comprador_id"] as string ?? "",
                    CompradorNome = reader["Comprador_nome"] as string ?? "",
                    CompradorTelefone = reader["Comprador_telefone"] as string ?? "",
                    CompradorEmail = reader["Comprador_email"] as string ?? "",
                    VendedorId = reader["Vendedor_id"] as string ?? "",
                    VendedorNome = reader["Vendedor_nome"] as string ?? "",
                    VendedorTelefone = reader["Vendedor_telefone"] as string ?? "",
                    VendedorEmail = reader["Vendedor_email"] as string ?? "",
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

        public void AceitarProposta(string propostaId)
        {
            using var conexao = new MySqlConnection(_strindeDeConexao);
            conexao.Open();

            using var transacao = conexao.BeginTransaction();

            try
            {
                // 1. Buscar produto_id da proposta aceita
                string buscarProdutoQuery = "SELECT Produto_id FROM propostas_de_compra WHERE id = @id";
                string produtoId;

                using (var cmdBuscar = new MySqlCommand(buscarProdutoQuery, conexao, transacao))
                {
                    cmdBuscar.Parameters.AddWithValue("@id", propostaId);
                    var result = cmdBuscar.ExecuteScalar();
                    if (result == null) throw new Exception("Proposta não encontrada.");
                    produtoId = Convert.ToString(result);
                }

                // 2. Atualizar proposta escolhida para 'fechada'
                string updateAceita = "UPDATE propostas_de_compra SET status = 'fechada' WHERE id = @id";
                using (var cmdUpdateAceita = new MySqlCommand(updateAceita, conexao, transacao))
                {
                    cmdUpdateAceita.Parameters.AddWithValue("@id", propostaId);
                    cmdUpdateAceita.ExecuteNonQuery();
                }

                // 3. Atualizar demais propostas do mesmo produto para 'rejeitada'
                string updateOutras = @"
                    UPDATE propostas_de_compra 
                    SET status = 'rejeitada' 
                    WHERE produto_id = @produtoId AND id != @id AND status != 'rejeitada'
                ";
                using (var cmdUpdateOutras = new MySqlCommand(updateOutras, conexao, transacao))
                {
                    cmdUpdateOutras.Parameters.AddWithValue("@produtoId", produtoId);
                    cmdUpdateOutras.Parameters.AddWithValue("@id", propostaId);
                    cmdUpdateOutras.ExecuteNonQuery();
                }

                // 4. Criar registro na tabela pedidos
                string insertPedido = @"
                    INSERT INTO pedidos (id, status, oferta_id) 
                    VALUES (@id, 'fechado', @oferta_id)
                ";
                using (var cmdInsertPedido = new MySqlCommand(insertPedido, conexao, transacao))
                {
                    cmdInsertPedido.Parameters.AddWithValue("@id", Guid.NewGuid().ToString());
                    cmdInsertPedido.Parameters.AddWithValue("@oferta_id", propostaId);
                    cmdInsertPedido.ExecuteNonQuery();
                }

                // 5. Atualizar o status do produto para 'fechado'
                string updateProduto = "UPDATE produtos SET status = 'fechado' WHERE id = @produtoId";
                using (var cmdUpdateProduto = new MySqlCommand(updateProduto, conexao, transacao))
                {
                    cmdUpdateProduto.Parameters.AddWithValue("@produtoId", produtoId);
                    cmdUpdateProduto.ExecuteNonQuery();
                }

                transacao.Commit();
            }
            catch
            {
                transacao.Rollback();
                throw;
            }
        }

        public void RejeitarProposta(string propostaId)
        {
            using var conexao = new MySqlConnection(_strindeDeConexao);
            conexao.Open();

            MySqlCommand cmd = new MySqlCommand(@"UPDATE propostas_de_compra SET status = 'rejeitada' WHERE id = @id", conexao);

            cmd.Parameters.AddWithValue("@id", propostaId);

            cmd.ExecuteNonQuery();
        }
    }
}

//using MySql.Data.MySqlClient;
//using System.Data;
//using System.Text;
//using TROCAKI.Models;

//namespace TROCAKI.Repositorio
//{
//    public class PropostaDeCompraRepositorio
//    {
//        private readonly string _strindeDeConexao;

//        public PropostaDeCompraRepositorio(string strindeDeConexao)
//        {
//            _strindeDeConexao = strindeDeConexao;
//        }

//        public bool InserirProposta(NovaPropostaModel proposta, out string erro)
//        {
//            erro = string.Empty;

//            try
//            {
//                using var conexao = new MySqlConnection(_strindeDeConexao);
//                conexao.Open();

//                using var cmd = new MySqlCommand(@"
//                    INSERT INTO propostas_de_compra 
//                    (id, valor_proposto, descricao, status, produto_id, comprador_id)
//                    VALUES (@id, @valor_proposto, @descricao, @status, @produto_id, @comprador_id)"
//                , conexao);

//                cmd.Parameters.AddWithValue("@id", Guid.NewGuid().ToString());
//                cmd.Parameters.AddWithValue("@valor_proposto", proposta.Valor);
//                cmd.Parameters.AddWithValue("@descricao", proposta.Descricao);
//                cmd.Parameters.AddWithValue("@status", "aberta");
//                cmd.Parameters.AddWithValue("@produto_id", proposta.ProdutoId);
//                cmd.Parameters.AddWithValue("@comprador_id", proposta.CompradorId);

//                int linhasAfetadas = cmd.ExecuteNonQuery();
//                if (linhasAfetadas == 0)
//                {
//                    erro = "A proposta não foi inserida.";
//                    return false;
//                }

//                return true;
//            }
//            catch (Exception ex)
//            {
//                erro = "Erro ao inserir proposta: " + ex.Message;
//                return false;
//            }
//        }

//        public List<PropostaDeCompraModel> ObterPropostas(string? compradorId, string? vendedorId, string? produtoId, string? status, out string erro)
//        {
//            erro = string.Empty;
//            var propostas = new List<PropostaDeCompraModel>();

//            try
//            {
//                using var conexao = new MySqlConnection(_strindeDeConexao);
//                conexao.Open();

//                var sql = new StringBuilder(@"
//            SELECT 
//                propostaDeCompra.id,
//                propostaDeCompra.valor_proposto,
//                propostaDeCompra.descricao,
//                propostaDeCompra.status,
//                propostaDeCompra.Produto_id,
//                produto.nome AS Produto_nome,
//                produto.valor AS Produto_valor,
//                GROUP_CONCAT(DISTINCT fotoDoProduto.foto_base_64 SEPARATOR '||') AS Produto_fotos,
//                GROUP_CONCAT(DISTINCT CONCAT(categoria.id, ':', categoria.nome) SEPARATOR '||') AS Produto_categorias,
//                propostaDeCompra.Comprador_id,
//                comprador.nome AS Comprador_nome,
//                comprador.telefone AS Comprador_telefone,
//                comprador.email AS Comprador_email,
//                produto.Vendedor_id,
//                vendedor.nome AS Vendedor_nome,
//                vendedor.telefone AS Vendedor_telefone,
//                vendedor.email AS Vendedor_email
//            FROM propostas_de_compra propostaDeCompra
//            JOIN produtos produto ON propostaDeCompra.Produto_Id = produto.id
//            LEFT JOIN fotos_dos_produtos fotoDoProduto ON fotoDoProduto.Produto_id = produto.id
//            LEFT JOIN categorias_dos_produtos categoriaDoProduto ON categoriaDoProduto.Produto_id = produto.id
//            LEFT JOIN categorias categoria ON categoria.id = categoriaDoProduto.Categoria_id
//            LEFT JOIN usuarios vendedor ON vendedor.id = produto.Vendedor_id
//            LEFT JOIN usuarios comprador ON comprador.id = propostaDeCompra.Comprador_id
//            WHERE 1=1
//        ");

//                using var cmd = new MySqlCommand();
//                cmd.Connection = conexao;

//                if (!string.IsNullOrWhiteSpace(compradorId))
//                {
//                    sql.Append(" AND propostaDeCompra.Comprador_id = @compradorId");
//                    cmd.Parameters.AddWithValue("@compradorId", compradorId);
//                }

//                if (!string.IsNullOrWhiteSpace(vendedorId))
//                {
//                    sql.Append(" AND produto.Vendedor_id = @vendedorId");
//                    cmd.Parameters.AddWithValue("@vendedorId", vendedorId);
//                }

//                if (!string.IsNullOrWhiteSpace(produtoId))
//                {
//                    sql.Append(" AND propostaDeCompra.Produto_id = @produtoId");
//                    cmd.Parameters.AddWithValue("@produtoId", produtoId);
//                }

//                if (!string.IsNullOrWhiteSpace(status))
//                {
//                    sql.Append(" AND propostaDeCompra.status = @status");
//                    cmd.Parameters.AddWithValue("@status", status);
//                }

//                sql.Append(" GROUP BY propostaDeCompra.id;");

//                cmd.CommandText = sql.ToString();

//                using var reader = cmd.ExecuteReader();

//                while (reader.Read())
//                {
//                    var proposta = new PropostaDeCompraModel
//                    {
//                        PropostaId = reader["id"] as string ?? "",
//                        ValorProposto = reader.IsDBNull("valor_proposto") ? 0 : reader.GetDouble("valor_proposto"),
//                        Descricao = reader["descricao"] as string ?? "",
//                        PropostaStatus = reader["status"] as string ?? "",
//                        ProdutoId = reader["Produto_id"] as string ?? "",
//                        ProdutoNome = reader["Produto_nome"] as string ?? "",
//                        ProdutoValor = reader.IsDBNull("Produto_valor") ? 0 : reader.GetDouble("Produto_valor"),
//                        ProdutoFotos = new List<string>(),
//                        ProdutoCategorias = new List<CategoriaModel>(),
//                        CompradorId = reader["Comprador_id"] as string ?? "",
//                        CompradorNome = reader["Comprador_nome"] as string ?? "",
//                        CompradorTelefone = reader["Comprador_telefone"] as string ?? "",
//                        CompradorEmail = reader["Comprador_email"] as string ?? "",
//                        VendedorId = reader["Vendedor_id"] as string ?? "",
//                        VendedorNome = reader["Vendedor_nome"] as string ?? "",
//                        VendedorTelefone = reader["Vendedor_telefone"] as string ?? "",
//                        VendedorEmail = reader["Vendedor_email"] as string ?? "",
//                    };

//                    // Fotos do produto
//                    if (!reader.IsDBNull("Produto_fotos"))
//                    {
//                        proposta.ProdutoFotos = reader.GetString("Produto_fotos")
//                            .Split("||", StringSplitOptions.RemoveEmptyEntries)
//                            .ToList();
//                    }

//                    // Categorias do produto
//                    if (!reader.IsDBNull("Produto_categorias"))
//                    {
//                        var categoriasStr = reader.GetString("Produto_categorias");
//                        foreach (var cat in categoriasStr.Split("||", StringSplitOptions.RemoveEmptyEntries))
//                        {
//                            var partes = cat.Split(':');
//                            if (partes.Length == 2)
//                            {
//                                proposta.ProdutoCategorias.Add(new CategoriaModel
//                                {
//                                    Id = partes[0],
//                                    Nome = partes[1]
//                                });
//                            }
//                        }
//                    }

//                    propostas.Add(proposta);
//                }

//                return propostas;
//            }
//            catch (Exception ex)
//            {
//                erro = "Erro ao obter propostas: " + ex.Message;
//                return new List<PropostaDeCompraModel>();
//            }
//        }

//        public bool AceitarProposta(string propostaId, out string erro)
//        {
//            erro = string.Empty;

//            using var conexao = new MySqlConnection(_strindeDeConexao);
//            conexao.Open();

//            using var transacao = conexao.BeginTransaction();

//            try
//            {
//                // Buscar o ProdutoId da proposta
//                string produtoId;
//                using (var cmdBuscar = new MySqlCommand("SELECT Produto_id FROM propostas_de_compra WHERE id = @id", conexao, transacao))
//                {
//                    cmdBuscar.Parameters.AddWithValue("@id", propostaId);
//                    var result = cmdBuscar.ExecuteScalar();

//                    if (result == null)
//                        throw new Exception("Proposta não encontrada.");

//                    produtoId = result.ToString();
//                }

//                // Fechar a proposta selecionada
//                using (var cmdFechar = new MySqlCommand("UPDATE propostas_de_compra SET status = 'fechada' WHERE id = @id", conexao, transacao))
//                {
//                    cmdFechar.Parameters.AddWithValue("@id", propostaId);
//                    cmdFechar.ExecuteNonQuery();
//                }

//                // Rejeitar demais propostas
//                using (var cmdRejeitar = new MySqlCommand(@"
//                    UPDATE propostas_de_compra
//                    SET status = 'rejeitada'
//                    WHERE produto_id = @produtoId AND id != @id AND status != 'rejeitada'", conexao, transacao))
//                {
//                    cmdRejeitar.Parameters.AddWithValue("@produtoId", produtoId);
//                    cmdRejeitar.Parameters.AddWithValue("@id", propostaId);
//                    cmdRejeitar.ExecuteNonQuery();
//                }

//                // Criar pedido
//                using (var cmdPedido = new MySqlCommand(@"
//                    INSERT INTO pedidos (id, status, oferta_id)
//                    VALUES (@id, 'fechado', @oferta_id)", conexao, transacao))
//                {
//                    cmdPedido.Parameters.AddWithValue("@id", Guid.NewGuid().ToString());
//                    cmdPedido.Parameters.AddWithValue("@oferta_id", propostaId);
//                    cmdPedido.ExecuteNonQuery();
//                }

//                // Fechar produto
//                using (var cmdProduto = new MySqlCommand("UPDATE produtos SET status = 'fechado' WHERE id = @produtoId", conexao, transacao))
//                {
//                    cmdProduto.Parameters.AddWithValue("@produtoId", produtoId);
//                    cmdProduto.ExecuteNonQuery();
//                }

//                transacao.Commit();
//                return true;
//            }
//            catch (Exception ex)
//            {
//                transacao.Rollback();
//                erro = "Erro ao aceitar proposta: " + ex.Message;
//                return false;
//            }
//        }

//        public bool RejeitarProposta(string propostaId, out string erro)
//        {
//            erro = string.Empty;

//            try
//            {
//                using var conexao = new MySqlConnection(_strindeDeConexao);
//                conexao.Open();

//                using var cmd = new MySqlCommand("UPDATE propostas_de_compra SET status = 'rejeitada' WHERE id = @id", conexao);
//                cmd.Parameters.AddWithValue("@id", propostaId);

//                int linhasAfetadas = cmd.ExecuteNonQuery();
//                if (linhasAfetadas == 0)
//                {
//                    erro = "Proposta não encontrada ou já rejeitada.";
//                    return false;
//                }

//                return true;
//            }
//            catch (Exception ex)
//            {
//                erro = "Erro ao rejeitar proposta: " + ex.Message;
//                return false;
//            }
//        }

//    }
//}

