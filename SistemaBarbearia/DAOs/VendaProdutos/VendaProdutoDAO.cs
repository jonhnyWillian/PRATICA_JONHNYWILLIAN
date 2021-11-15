using SistemaBarbearia.DataBase;
using SistemaBarbearia.Models.Vendas;
using SistemaBarbearia.ViewModels.Agendamentos;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace SistemaBarbearia.DAOs.VendaProdutos
{
    public class VendaProdutoDAO : DataAccess
    {
        #region INSERT UPDATE DELETE

        public bool insertVenda(int id, AgendamentoVW venda)
        {
            int i = 0;
            Open();
            SqlTransaction sqlTransaction = sqlconnection.BeginTransaction();
            SqlCommand command;
            try
            {
                command = sqlconnection.CreateCommand();
                command.Transaction = sqlTransaction;
                if (venda.VendaServico.nrModelo != null)
                {
                    command.CommandText = "INSERT INTO Venda ( nrModelo, nrSerie,  flSituacao, IdCliente,  IdCondPag, dtEmissao, dtCadastro,  vlTotal, IdAgenda)" +
                                                       "VALUES(@nrModelo, @nrSerie, @flSituacao, @IdCliente,  @IdCondPag, @dtEmissao, @dtCadastro, @vlTotal, @IdAgenda);SELECT CAST(SCOPE_IDENTITY() AS int)";

                    command.Parameters.AddWithValue("@nrModelo", venda.VendaServico.nrModelo);
                    command.Parameters.AddWithValue("@nrSerie", venda.VendaServico.nrSerie);
                    command.Parameters.AddWithValue("@flSituacao", venda.flSituacao = "A");
                    command.Parameters.AddWithValue("@IdCliente", venda.Cliente.IdCliente);
                    command.Parameters.AddWithValue("@IdAgenda", id);

                    command.Parameters.AddWithValue("@IdCondPag", venda.CondicaoPagamento.Id);
                    command.Parameters.AddWithValue("@dtEmissao", venda.dtAgendamento = DateTime.Now) ;
                    command.Parameters.AddWithValue("@vlTotal", venda.vlTotalProduto);
                    command.Parameters.AddWithValue("@dtcadastro", venda.dtCadastro = DateTime.Now);
                    command.Parameters.AddWithValue("@dtUltAlteracao", venda.dtUltAlteracao = DateTime.Now);

                    Int32 idvendaServico = Convert.ToInt32(command.ExecuteScalar());

                    command.CommandText = "INSERT INTO ServicoVenda ( nrModelo , nrSerie , nrNota , IdServico , vlVenda)" +
                                                           " VALUES( @nrModelo , @nrSerie , @nrNota , @IdServico , @vlVenda)";

                    command.Parameters.Clear();
                    command.Parameters.AddWithValue("@nrModelo", venda.VendaServico.nrModelo);
                    command.Parameters.AddWithValue("@nrSerie", venda.VendaServico.nrSerie);
                    command.Parameters.AddWithValue("@nrNota", idvendaServico);
                    command.Parameters.AddWithValue("@IdServico", venda.Servico.IdServico);
                    command.Parameters.AddWithValue("@vlVenda", venda.vlTotalServico);

                    //i = command.ExecuteNonQuery();

                    foreach (var item in venda.ParcelasVenda)
                    {
                        command = sqlconnection.CreateCommand();
                        command.Transaction = sqlTransaction;
                        command.CommandText = "INSERT INTO ContasReceber  ( nrModelo, nrSerie, nrNota, IdCliente, IdFormaPagamento,  nrparcela, vlParcela, flSituacao, dtVencimento )" +
                                                                   "VALUES(@nrModelo, @nrSerie, @nrNota, @IdCliente, @IdFormaPagamento,  @nrparcela, @vlParcela, @flSituacao, @dtVencimento )";

                        command.Parameters.Clear();
                        command.Parameters.AddWithValue("@nrModelo", venda.VendaServico.nrModelo);
                        command.Parameters.AddWithValue("@nrSerie", venda.VendaServico.nrSerie);
                        command.Parameters.AddWithValue("@nrNota", idvendaServico);
                        command.Parameters.AddWithValue("@IdCliente", venda.Cliente.IdCliente);
                        command.Parameters.AddWithValue("@IdFormaPagamento", item.IdFormaPagamento);
                        command.Parameters.AddWithValue("@vlParcela", item.vlParcela);
                        command.Parameters.AddWithValue("@flSituacao", item.flSituacao = "A");
                        command.Parameters.AddWithValue("@dtVencimento", item.dtVencimento);
                        command.Parameters.AddWithValue("@nrparcela", item.nrParcela);

                        i = command.ExecuteNonQuery();
                    }

                }


                if (venda.VendaProduto.nrModelo != null)
                {

                    command.CommandText = "INSERT INTO Venda ( nrModelo, nrSerie,  flSituacao, IdCliente, IdCondPag, dtEmissao, dtCadastro,  dtUltAlteracao, vlTotal, IdAgenda)" +
                                                      "VALUES(@nrModelo, @nrSerie, @flSituacao, @IdCliente,@IdCondPag, @dtEmissao, @dtCadastro, @dtUltAlteracao, @vlTotal, @IdAgenda);SELECT CAST(SCOPE_IDENTITY() AS int)";

                    command.Parameters.Clear();
                    command.Parameters.AddWithValue("@nrModelo", venda.VendaProduto.nrModelo);
                    command.Parameters.AddWithValue("@nrSerie", venda.VendaProduto.nrSerie);
                    command.Parameters.AddWithValue("@flSituacao", venda.flSituacao);
                    command.Parameters.AddWithValue("@IdCliente", venda.Cliente.IdCliente);                    
                    command.Parameters.AddWithValue("@IdCondPag", venda.CondicaoPagamento.Id);
                    command.Parameters.AddWithValue("@IdAgenda", id);
                    command.Parameters.AddWithValue("@dtEmissao", venda.dtAgendamento);
                    command.Parameters.AddWithValue("@vlTotal", venda.vlTotalServico);
                    command.Parameters.AddWithValue("@dtcadastro", venda.dtCadastro = DateTime.Now);
                    command.Parameters.AddWithValue("@dtUltAlteracao", venda.dtUltAlteracao = DateTime.Now);

                    Int32 idvendaProduto = Convert.ToInt32(command.ExecuteScalar());

                    //i = command.ExecuteNonQuery();

                    foreach (var item in venda.ProdutosCompra)
                    {

                        command.CommandText = "INSERT INTO ProdutoVenda ( nrModelo, nrSerie, nrNota, IdProduto, nrQtd, vlVenda)" +
                                                           " VALUES( @nrModelo, @nrSerie, @nrNota, @IdProduto, @nrQtd, @vlVenda)";

                        command.Parameters.Clear();
                        command.Parameters.AddWithValue("@nrModelo", venda.VendaProduto.nrModelo);
                        command.Parameters.AddWithValue("@nrSerie", venda.VendaProduto.nrSerie);
                        command.Parameters.AddWithValue("@nrNota", idvendaProduto);
                        command.Parameters.AddWithValue("@IdProduto", item.IdProduto);
                        command.Parameters.AddWithValue("@nrQtd", item.nrQtd);
                        command.Parameters.AddWithValue("@vlVenda", item.vlVenda);
                        
                        i = command.ExecuteNonQuery();
                    }


                    foreach (var item in venda.ParcelasVendaProduto)
                    {
                        command = sqlconnection.CreateCommand();
                        command.Transaction = sqlTransaction;
                        command.CommandText = "INSERT INTO ContasReceber  ( nrModelo , nrSerie , nrNota , IdCliente , IdFormaPagamento ,  nrparcela , vlParcela , flSituacao , dtVencimento)" +
                                                             "VALUES(@nrModelo , @nrSerie , @nrNota , @IdCliente , @IdFormaPagamento ,  @nrparcela , @vlParcela , @flSituacao , @dtVencimento)";
                        command.Parameters.Clear();
                        command.Parameters.AddWithValue("@nrModelo", venda.VendaProduto.nrModelo);
                        command.Parameters.AddWithValue("@nrSerie", venda.VendaProduto.nrSerie);
                        command.Parameters.AddWithValue("@nrNota", idvendaProduto);
                        command.Parameters.AddWithValue("@IdCliente", venda.Cliente.IdCliente);
                        command.Parameters.AddWithValue("@IdFormaPagamento", item.IdFormaPagamento);
                        command.Parameters.AddWithValue("@vlParcela", item.vlParcela);
                        command.Parameters.AddWithValue("@flSituacao", item.flSituacao = "A");
                        command.Parameters.AddWithValue("@dtVencimento", item.dtVencimento);
                        command.Parameters.AddWithValue("@nrparcela", item.nrParcela);
                        i = command.ExecuteNonQuery();


                    }

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
                throw new Exception("Erro ao inserir o venda: " + ex.Message);
            }
            finally
            {
                Close();
            }
        }

        #endregion
    }
}