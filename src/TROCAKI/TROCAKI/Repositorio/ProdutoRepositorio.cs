using MySql.Data.MySqlClient;
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

        public List<CategoriaModel> ObterCaracteristicas()
        {
            List<CategoriaModel> lista = new List<CategoriaModel>();

            MySqlConnection conexao = new MySqlConnection(_strindeDeConexao);
            conexao.Open();

            MySqlCommand cmd = new MySqlCommand("SELECT * FROM categorias", conexao);
            var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                lista.Add(
                    new CategoriaModel
                    {
                        Id = reader.GetString("Id"),
                        Nome = reader.GetString("Nome")
                    }
                );
            }

            return lista;
        }

        public string CadastrarProduto(ProdutoModel produto)
        {
            MySqlConnection conexao = new MySqlConnection(_strindeDeConexao);
            conexao.Open();

            MySqlTransaction transacao = conexao.BeginTransaction();

            string idProduto = Guid.NewGuid().ToString();

            try
            {
                // Inserir produto
                MySqlCommand cmdProduto = new MySqlCommand(
                    @"INSERT INTO produtos (id, valor, nome, descricao, status, vendedor_id)
                    VALUES (@id, @valor, @nome, @descricao, @status, @vendedor_id)",
                    conexao, transacao
                );

                cmdProduto.Parameters.AddWithValue("@id", idProduto);
                cmdProduto.Parameters.AddWithValue("@nome", produto.Valor);
                cmdProduto.Parameters.AddWithValue("@email", produto.Nome);
                cmdProduto.Parameters.AddWithValue("@cpf", produto.Descricao);
                cmdProduto.Parameters.AddWithValue("@telefone", produto.Status);
                cmdProduto.Parameters.AddWithValue("@dataNascimento", produto.IdVendedor);

                // Inserir categorias
                foreach (CategoriaModel categoria in produto.Categorias)
                {
                    var cmdCategoria = new MySqlCommand(
                        @"INSERT INTO `categorias_dos_produtos` (Categoria_Id, Produto_Id)
                        VALUES (@CategoriaId, @ProdutoId);", conexao, transacao
                    );

                    cmdCategoria.Parameters.AddWithValue("@CategoriaId", categoria.Id);
                    cmdCategoria.Parameters.AddWithValue("@ProdutoId", idProduto);

                    cmdCategoria.ExecuteNonQuery();
                }

                // Inserir fotos
                foreach (string fotoBase64 in produto.Fotos)
                {
                    var cmdFoto = new MySqlCommand(@"
                        INSERT INTO `fotos_dos_produtos` (Produto_Id, FotoBase64)
                        VALUES (@ProdutoId, @FotoBase64);", conexao, transacao);

                    cmdFoto.Parameters.AddWithValue("@ProdutoId", idProduto);
                    cmdFoto.Parameters.AddWithValue("@foto_base_64", fotoBase64);

                    cmdFoto.ExecuteNonQuery();
                }

                transacao.Commit();

                return idProduto;
            }
            catch (Exception ex)
            {
                transacao.Rollback();
                throw new Exception("Erro ao inserir produto e dados relacionados: " + ex.Message);
            }
        }
    }
}
