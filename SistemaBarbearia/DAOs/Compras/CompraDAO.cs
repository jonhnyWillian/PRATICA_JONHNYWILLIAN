using SistemaBarbearia.DataBase;
using SistemaBarbearia.Models.Compras;
using System;
using System.Collections.Generic;
using System.Data;
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
                command.CommandText = "INSERT INTO Compra (nrModelo,  nrSerie,  nrNota,  idFornecedor,  IdCondPag,  dtEmissao,  dtEntrega, flSituacao  )" +
                                                   "VALUES(@nrModelo, @nrSerie, @nrNota, @idFornecedor, @IdCondPag, @dtEmissao, @dtEntrega, @flSituacao);SELECT CAST(SCOPE_IDENTITY() AS int)";

                command.Parameters.AddWithValue("@nrModelo", ((object)compra.nrModelo) != DBNull.Value );
                command.Parameters.AddWithValue("@nrSerie", ((object)compra.nrSerie) != DBNull.Value );
                command.Parameters.AddWithValue("@nrNota", ((object)compra.nrNota) != DBNull.Value );
                command.Parameters.AddWithValue("@idFornecedor", compra.Fornecedor.IdFornecedor);
                command.Parameters.AddWithValue("@IdCondPag", compra.CondicaoPagamento.Id);
                command.Parameters.AddWithValue("@dtEmissao", compra.dtEmissao);
                command.Parameters.AddWithValue("@dtEntrega", compra.dtEntrega);                
                command.Parameters.AddWithValue("@flSituacao", compra.flSituacao);                
                //command.Parameters.AddWithValue("@dtcadastro", ((object)compra.dtCadastro) ?? DBNull.Value);


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

                //command.CommandText = "UPDATE Produto SET nrQtd += nrQtd@, vlUltCompra += @vlUltCompra WHERE idProduto = @IdProduto";
                //command.Parameters.Clear();
                //command.Parameters.AddWithValue("@nrQtd", ((object)item.nrQtd) != DBNull.Value ? ((object)item.nrQtd) : 0);
                //command.Parameters.AddWithValue("@vlUltCompra", ((object)item.vlCompra) != DBNull.Value ? ((object)item.vlCompra) : 0);
                //command.Parameters.AddWithValue("@IdProduto", item.IdProduto);
                //i = command.ExecuteNonQuery();

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
        #endregion


        public List<Compra> SelecionarCompra()
        {
            try
            {
                Open();
                SQL = new SqlCommand(@"SELECT* FROM Compra INNER JOIN Fornecedor ON Compra.idFornecedor = Fornecedor.idFornecedor ", sqlconnection);
                SQL.CommandType = CommandType.Text;
                Dr = SQL.ExecuteReader();
                // Criando uma lista vazia
                var lista = new List<Compra>();
                while (Dr.Read())
                {
                    var c = new Compra()
                    {
                        IdCompra = Convert.ToInt32(Dr["IdCompra"]),
                        Fornecedor = new ViewModels.Fornecedores.SelectFornecedorVM
                        {
                            IdFornecedor = Convert.ToInt32(Dr["IdFornecedor"]),
                            nmNome = Convert.ToString(Dr["nmNome"]),
                        },
                        nrModelo = Convert.ToString(Dr["nrModelo"]),
                        nrSerie = Convert.ToString(Dr["nrSerie"]),
                        nrNota = Convert.ToInt32(Dr["nrNota"]),
                        dtEmissao = Dr["dtEmissao"] == DBNull.Value ? DateTime.Now : Convert.ToDateTime(Dr["dtEmissao"]),
                        //dtEntrega = Dr["dtEntrega"] == DBNull.Value ? DateTime.Now : Convert.ToDateTime(Dr["dtEntrega"]),

                        //  dtCadastro = Dr["dtCadastro"] == DBNull.Value ? DateTime.Now : Convert.ToDateTime(Dr["dtCadastro"]),
                        //dtUltAlteracao = Dr["dtUltAlteracao"] == DBNull.Value ? DateTime.Now : Convert.ToDateTime(Dr["dtUltAlteracao"]),
                    };
                    lista.Add(c);
                }
                return lista;
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao selecionar o Compra: " + e.Message);
            }
            finally
            {
                Close();
            }
        }


     

        public Compra GetCompra(int? Id)
        {
            try
            {
                Open();
                var compraVM = new Compra();
                string select = @"SELECT* FROM Compra  WHERE IdCompra =" + Id;
                SQL = new SqlCommand(select, sqlconnection);


                Dr = SQL.ExecuteReader();
                while (Dr.Read())
                {
                    compraVM.IdCompra = Convert.ToInt32(Dr["IdCompra"]);
                    compraVM.dtEmissao = Dr["dtEmissao"] == DBNull.Value ? DateTime.Now : Convert.ToDateTime(Dr["dtEmissao"]);
                    compraVM.dtEntrega = Dr["dtEntrega"] == DBNull.Value ? DateTime.Now : Convert.ToDateTime(Dr["dtEmissao"]);
                    compraVM.nrModelo = Convert.ToString(Dr["hrAgendamento"]);
                    compraVM.nrNota = Convert.ToInt32(Dr["hrAgendamento"]);
                    compraVM.nrSerie = Convert.ToString(Dr["hrAgendamento"]);
                    compraVM.IdCondPag = Convert.ToInt32(Dr["hrAgendamento"]);
                    compraVM.IdProduto = Convert.ToInt32(Dr["hrAgendamento"]);
                    compraVM.IdFornecedor = Convert.ToInt32(Dr["hrAgendamento"]);
                    compraVM.vlTotal = Convert.ToDecimal(Dr["hrAgendamento"]);
                    
                    //compraVM.dtCadastro = Dr["dtCadastro"] == DBNull.Value ? DateTime.Now : Convert.ToDateTime(Dr["dtCadastro"]);
                    //compraVM.dtUltAlteracao = Dr["dtUltAlteracao"] == DBNull.Value ? DateTime.Now : Convert.ToDateTime(Dr["dtUltAlteracao"]);


                }
                return compraVM;
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao selecionar o Agenda: " + e.Message);
            }
            finally
            {
                Close();
            }
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
    }
}