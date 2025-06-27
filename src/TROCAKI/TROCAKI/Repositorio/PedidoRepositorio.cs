using MySql.Data.MySqlClient;
using TROCAKI.Models;

namespace TROCAKI.Repositorio
{
    public class PedidoRepositorio
    {
        private readonly string _stringDeConexao;

        public PedidoRepositorio(string stringDeConexao)
        {
            _stringDeConexao = stringDeConexao;
        }

        public List<PedidoModel> ObterProdutosPorVendedor(string vendedorId)
        {
            var pedidos = new List<PedidoModel>();

            try
            {
                using var conexao = new MySqlConnection(_stringDeConexao);
                conexao.Open();

                var sql = @"
                    SELECT 
                        p.id AS PedidoId,
                        p.status AS PedidoStatus,
                        prop.valor_proposto AS PropostaValorProposto,
                        comp.id AS CompradorId,
                        comp.nome AS CompradorNome,
                        comp.telefone AS CompradorTelefone,
                        comp.email AS CompradorEmail,
                        vend.id AS VendedorId,
                        vend.nome AS VendedorNome,
                        vend.telefone AS VendedorTelefone,
                        vend.email AS VendedorEmail,
                        prod.id AS ProdutoId,
                        prod.nome AS ProdutoNome,
                        prod.descricao AS ProdutoDescricao,
                        prod.valor AS ProdutoValor,
                        GROUP_CONCAT(DISTINCT foto.foto_base_64 SEPARATOR '||') AS fotos
                    FROM pedidos p
                    INNER JOIN propostas_de_compra prop ON p.Oferta_id = prop.id
                    INNER JOIN produtos prod ON prop.Produto_id = prod.id
                    LEFT JOIN fotos_dos_produtos foto ON foto.Produto_id = prod.id
                    INNER JOIN usuarios comp ON prop.Comprador_id = comp.id
                    INNER JOIN usuarios vend ON prod.Vendedor_id = vend.id
                    WHERE vend.id = @vendedorId
                    GROUP BY p.id;
                ";

                using var comando = new MySqlCommand(sql, conexao);
                comando.Parameters.AddWithValue("@vendedorId", vendedorId);

                using var reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    var pedido = new PedidoModel
                    {
                        Id = reader["PedidoId"]?.ToString(),
                        status = reader["PedidoStatus"]?.ToString(),
                        PropostaValorProposto = Convert.ToDouble(reader["PropostaValorProposto"]),
                        CompradorId = reader["CompradorId"]?.ToString(),
                        CompradorNome = reader["CompradorNome"]?.ToString(),
                        CompradorTelefone = reader["CompradorTelefone"]?.ToString(),
                        CompradorEmail = reader["CompradorEmail"]?.ToString(),
                        VendedorId = reader["VendedorId"]?.ToString(),
                        VendedorNome = reader["VendedorNome"]?.ToString(),
                        VendedorTelefone = reader["VendedorTelefone"]?.ToString(),
                        VendedorEmail = reader["VendedorEmail"]?.ToString(),
                        ProdutoId = reader["ProdutoId"]?.ToString(),
                        ProdutoNome = reader["ProdutoNome"]?.ToString(),
                        ProdutoDescricao = reader["ProdutoDescricao"]?.ToString(),
                        ProdutoValor = Convert.ToDouble(reader["ProdutoValor"]),
                        ProdutoFotos = new List<string>()
                    };

                    if (reader["fotos"] != DBNull.Value)
                    {
                        string fotosStr = reader["fotos"].ToString();
                        pedido.ProdutoFotos = fotosStr.Split("||", StringSplitOptions.RemoveEmptyEntries).ToList();
                    }

                    pedidos.Add(pedido);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao obter pedidos do vendedor - " + ex.Message);
            }

            return pedidos;
        }

        public List<PedidoModel> ObterPedidosPorComprador(string compradorId)
        {
            var pedidos = new List<PedidoModel>();

            try
            {
                using var conexao = new MySqlConnection(_stringDeConexao);
                conexao.Open();

                var sql = @"
                    SELECT 
                        pedidos.id AS PedidoId,
                        pedidos.status AS PedidoStatus,
                        propostas.valor_proposto AS PropostaValorProposto,
                        comprador.id AS CompradorId,
                        comprador.nome AS CompradorNome,
                        comprador.telefone AS CompradorTelefone,
                        comprador.email AS CompradorEmail,
                        vendedor.id AS VendedorId,
                        vendedor.nome AS VendedorNome,
                        vendedor.telefone AS VendedorTelefone,
                        vendedor.email AS VendedorEmail,
                        produto.id AS ProdutoId,
                        produto.nome AS ProdutoNome,
                        produto.descricao AS ProdutoDescricao,
                        produto.valor AS ProdutoValor,
                        GROUP_CONCAT(DISTINCT foto.foto_base_64 SEPARATOR '||') AS fotos
                    FROM pedidos
                    INNER JOIN propostas_de_compra AS propostas ON pedidos.Oferta_id = propostas.id
                    INNER JOIN produtos AS produto ON propostas.Produto_id = produto.id
                    LEFT JOIN fotos_dos_produtos foto ON foto.Produto_id = produto.id
                    INNER JOIN usuarios AS comprador ON propostas.Comprador_id = comprador.id
                    INNER JOIN usuarios AS vendedor ON produto.Vendedor_id = vendedor.id
                    WHERE comprador.id = @compradorId
                    GROUP BY pedidos.id;
                ";

                using var comando = new MySqlCommand(sql, conexao);
                comando.Parameters.AddWithValue("@compradorId", compradorId);

                using var reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    var pedido = new PedidoModel
                    {
                        Id = reader["PedidoId"]?.ToString(),
                        status = reader["PedidoStatus"]?.ToString(),
                        PropostaValorProposto = Convert.ToDouble(reader["PropostaValorProposto"]),
                        CompradorId = reader["CompradorId"]?.ToString(),
                        CompradorNome = reader["CompradorNome"]?.ToString(),
                        CompradorTelefone = reader["CompradorTelefone"]?.ToString(),
                        CompradorEmail = reader["CompradorEmail"]?.ToString(),
                        VendedorId = reader["VendedorId"]?.ToString(),
                        VendedorNome = reader["VendedorNome"]?.ToString(),
                        VendedorTelefone = reader["VendedorTelefone"]?.ToString(),
                        VendedorEmail = reader["VendedorEmail"]?.ToString(),
                        ProdutoId = reader["ProdutoId"]?.ToString(),
                        ProdutoNome = reader["ProdutoNome"]?.ToString(),
                        ProdutoDescricao = reader["ProdutoDescricao"]?.ToString(),
                        ProdutoValor = Convert.ToDouble(reader["ProdutoValor"]),
                        ProdutoFotos = new List<string>()
                    };

                    if (reader["fotos"] != DBNull.Value)
                    {
                        string fotosStr = reader["fotos"].ToString();
                        pedido.ProdutoFotos = fotosStr.Split("||", StringSplitOptions.RemoveEmptyEntries).ToList();
                    }

                    pedidos.Add(pedido);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao obter pedidos do comprador - " + ex.Message);
            }

            return pedidos;
        }
    }
}
