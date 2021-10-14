using SistemaBarbearia.DataBase;
using SistemaBarbearia.Models.Compras;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace SistemaBarbearia.DAOs.Compras
{
    public class CompraDAO : DataAccess
    {
        #region INSERT UPDATE DELETE

        public bool InsertCompra(Compra compra)
        {
            int i = 0;
            Open();
            SqlTransaction sqlTransaction = sqlconnection.BeginTransaction();
            SqlCommand command;
            try
            {
                command = sqlconnection.CreateCommand();
                command.Transaction = sqlTransaction;
                command.CommandText = "INSERT INTO Compra (nrModelo , nrSerie , nrNota , idFornecedor , idCondicaoPagamento , dtEmissao , dtChegada ,flSituacao,dtCadastro )" +
                                                         " VALUES(@nrModelo, @nrSerie, @nrNota, @idFornecedor, @idCondicaoPagamento, @dtEmissao, @dtChegada, @flSituacao, @dtCadastro);SELECT CAST(SCOPE_IDENTITY() AS int)";

                command.Parameters.AddWithValue("@nrModelo", ((object)compra.nrModelo) != DBNull.Value );
                command.Parameters.AddWithValue("@nrSerie", ((object)compra.nrSerie) != DBNull.Value );
                command.Parameters.AddWithValue("@nrNota", ((object)compra.nrNota) != DBNull.Value );
                command.Parameters.AddWithValue("@idFornecedor", compra.Fornecedor.IdFornecedor);
                command.Parameters.AddWithValue("@idCondicaoPagamento", compra.CondicaoPagamento.Id);
                command.Parameters.AddWithValue("@dtEmissao", ((object)compra.dtEmissao) != DBNull.Value );
                command.Parameters.AddWithValue("@dtChegada", ((object)compra.dtEntrega) != DBNull.Value );
                command.Parameters.AddWithValue("@flSituacao", ((object)compra.flSituacao) );                
                command.Parameters.AddWithValue("@dtcadastro", ((object)compra.dtCadastro) ?? DBNull.Value);


                Int32 id = Convert.ToInt32(command.ExecuteScalar());

                command.CommandText = "INSERT INTO ProdutoCompras ( IdCompra, IdProduto, qtd, vlCompra, vlVenda, txDesconto )" +
                                                          " VALUES(@IdCompra, @IdProduto, @qtd, @vlCompra, @vlVenda, @txDesconto  )";

                foreach (var item in compra.ProdutosCompra)
                {
                    command.Parameters.Clear();
                    command.Parameters.AddWithValue("@IdCompra", id);
                    command.Parameters.AddWithValue("@IdProduto", item.IdProduto);
                    command.Parameters.AddWithValue("@qtd", ((object)item.nrQtd) != DBNull.Value ? ((object)item.nrQtd) : 0);
                    command.Parameters.AddWithValue("@vlCompra", ((object)item.vlCompra) != DBNull.Value ? ((object)item.vlCompra) : 0);
                    command.Parameters.AddWithValue("@vlVenda", ((object)item.vlVenda) != DBNull.Value ? ((object)item.vlVenda) : 0);
                    command.Parameters.AddWithValue("@txDesconto", ((object)item.txDesconto) != DBNull.Value ? ((object)item.txDesconto) : 0);
                    i = command.ExecuteNonQuery();

                }

                //IdCompra , IdFornecedor , IdFormaPagamento , nrparcela, vlParcela , flSituacao , dtVencimento 
                command.CommandText = "INSERT INTO ContasPagar ( IdCompra, IdFornecedor, IdFormaPagamento, nrparcela, vlParcela, flSituacao, dtVencimento )" +
                                                         " VALUES(@IdCompra, @IdFornecedor, @IdFormaPagamento, @nrparcela, @vlParcela, @flSituacao, @dtVencimento  )";

                foreach (var item in compra.ParcelasCompra)
                {
                    command.Parameters.Clear();
                    command.Parameters.AddWithValue("@IdCompra", id);
                    command.Parameters.AddWithValue("@IdFornecedor", compra.Fornecedor.IdFornecedor);
                    command.Parameters.AddWithValue("@IdFormaPagamento", item.idFormaPagamento);
                    command.Parameters.AddWithValue("@nrparcela", ((object)item.nrParcela) != DBNull.Value );
                    command.Parameters.AddWithValue("@vlParcela", ((object)item.vlParcela) != DBNull.Value ? ((object)item.vlParcela) : 0);
                    command.Parameters.AddWithValue("@flSituacao", ((object)item.flSituacao) != DBNull.Value);
                    command.Parameters.AddWithValue("@dtVencimento", ((object)item.dtVencimento) ?? DBNull.Value);

                    i = command.ExecuteNonQuery();

                }
                sqlTransaction.Commit();

                if (i >= 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                sqlTransaction.Rollback();
                throw new Exception("Erro ao inserir o Compra: " + ex.Message);
            }
            finally
            {
                Close();
            }
        }
        public bool UpdateCompra(Compra compra)
        {
            return true;
        }
        public bool DeleteCompra(Compra compra)
        {
            return true;
        }

        public bool validNota(string modelo, string serie, int nrNota, int idFornecedor)
        {
            string sql = "SELECT * FROM Compra WHERE nrModelo = '" + modelo + "' AND nrSerie = '" + serie + "' AND nrNota = " + nrNota + " AND idFornecedor = " + idFornecedor;
            Open();
            SqlCommand query = new SqlCommand(sql, sqlconnection);
            var exist = query.ExecuteScalar();
            Close();
            if (exist == null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion
    }
}