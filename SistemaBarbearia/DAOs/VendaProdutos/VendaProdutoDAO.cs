using SistemaBarbearia.DataBase;
using SistemaBarbearia.Models.Vendas;
using SistemaBarbearia.ViewModels.Agendamentos;
using SistemaBarbearia.ViewModels.Vendas;
using System;
using System.Collections.Generic;
using System.Data;
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
                    command.CommandText = "INSERT INTO Venda ( nrModelo, nrSerie,  flSituacao, IdCliente,  IdCondPag, dtEmissao, dtCadastro,  vlTotal )" +
                                                       "VALUES(@nrModelo, @nrSerie, @flSituacao, @IdCliente,  @IdCondPag, @dtEmissao, @dtCadastro, @vlTotal );SELECT CAST(SCOPE_IDENTITY() AS int)";

                    command.Parameters.AddWithValue("@nrModelo", venda.VendaServico.nrModelo);
                    command.Parameters.AddWithValue("@nrSerie", venda.VendaServico.nrSerie);
                    command.Parameters.AddWithValue("@flSituacao", venda.flSituacao = "A");
                    command.Parameters.AddWithValue("@IdCliente", venda.Cliente.IdCliente);
                    //command.Parameters.AddWithValue("@IdAgenda", id);

                    command.Parameters.AddWithValue("@IdCondPag", venda.CondicaoPagamento.Id);
                    command.Parameters.AddWithValue("@dtEmissao", venda.dtAgendamento = DateTime.Now);
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

                    command.CommandText = "INSERT INTO Venda ( nrModelo, nrSerie,  flSituacao, IdCliente, IdCondPag, dtEmissao, dtCadastro,  dtUltAlteracao, vlTotal)" +
                                                      "VALUES(@nrModelo, @nrSerie, @flSituacao, @IdCliente,@IdCondPag, @dtEmissao, @dtCadastro, @dtUltAlteracao, @vlTotal);SELECT CAST(SCOPE_IDENTITY() AS int)";

                    command.Parameters.Clear();
                    command.Parameters.AddWithValue("@nrModelo", venda.VendaProduto.nrModelo);
                    command.Parameters.AddWithValue("@nrSerie", venda.VendaProduto.nrSerie);
                    command.Parameters.AddWithValue("@flSituacao", venda.flSituacao);
                    command.Parameters.AddWithValue("@IdCliente", venda.Cliente.IdCliente);
                    command.Parameters.AddWithValue("@IdCondPag", venda.CondicaoPagamento.Id);
                    //command.Parameters.AddWithValue("@IdAgenda", id);
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


        public void updateAgenda(int id)
        {
           
            Open();
            var swhere = " WHERE Agenda.IdAgenda = " + id;
            var agenda = "UPDATE Agenda SET flSituacao = 'R' " + swhere;
            try
            {
                SqlCommand sql = new SqlCommand(agenda, sqlconnection);
                sql.CommandType = CommandType.Text;
                sql.ExecuteNonQuery();

            }
            catch (Exception ex)
            {              
                throw new Exception("Erro: " + ex.Message);
            }
            finally
            {
                Close();
            }

        }


        public bool InsertVendaProduto(Venda venda)
        {
            int i = 0;
            Open();
            SqlTransaction sqlTransaction = sqlconnection.BeginTransaction();
            SqlCommand command;
            try
            {
                command = sqlconnection.CreateCommand();
                command.Transaction = sqlTransaction;
                command.CommandText = "INSERT INTO Venda ( nrModelo, nrSerie,  flSituacao, IdCliente, IdCondPag, dtEmissao, vlTotal, dtCadastro,  dtUltAlteracao)" +
                                                   "VALUES(@nrModelo, @nrSerie, @flSituacao, @IdCliente,@IdCondPag, @dtEmissao,@vlTotal, @dtCadastro, @dtUltAlteracao);SELECT CAST(SCOPE_IDENTITY() AS int)";

                command.Parameters.Clear();
                command.Parameters.AddWithValue("@nrModelo", venda.nrModelo);
                command.Parameters.AddWithValue("@nrSerie", venda.nrSerie);
                command.Parameters.AddWithValue("@flSituacao", venda.flSituacao);
                command.Parameters.AddWithValue("@IdCliente", venda.Cliente.IdCliente);
                command.Parameters.AddWithValue("@IdCondPag", venda.CondicaoPagamento.Id);
                command.Parameters.AddWithValue("@dtEmissao", venda.dtNota);
                command.Parameters.AddWithValue("@vlTotal", venda.vlTotalProduto);
                command.Parameters.AddWithValue("@dtcadastro", venda.dtCadastro = DateTime.Now);
                command.Parameters.AddWithValue("@dtUltAlteracao", venda.dtUltAlteracao = DateTime.Now);

                Int32 idvendaProduto = Convert.ToInt32(command.ExecuteScalar());

                foreach (var item in venda.ProdutosCompra)
                {

                    command.CommandText = "INSERT INTO ProdutoVenda ( nrModelo, nrSerie, nrNota, IdProduto, nrQtd, vlVenda)" +
                                                       " VALUES( @nrModelo, @nrSerie, @nrNota, @IdProduto, @nrQtd, @vlVenda)";

                    command.Parameters.Clear();
                    command.Parameters.AddWithValue("@nrModelo", venda.nrModelo);
                    command.Parameters.AddWithValue("@nrSerie", venda.nrSerie);
                    command.Parameters.AddWithValue("@nrNota", idvendaProduto);
                    command.Parameters.AddWithValue("@IdProduto", item.IdProduto);
                    command.Parameters.AddWithValue("@nrQtd", item.nrQtd);
                    command.Parameters.AddWithValue("@vlVenda", item.vlVenda);

                    i = command.ExecuteNonQuery();
                }


                foreach (var item in venda.ParcelasVenda)
                {
                    command = sqlconnection.CreateCommand();
                    command.Transaction = sqlTransaction;
                    command.CommandText = "INSERT INTO ContasReceber  ( nrModelo , nrSerie , nrNota , IdCliente , IdFormaPagamento ,  nrparcela , vlParcela , flSituacao , dtVencimento)" +
                                                         "VALUES(@nrModelo , @nrSerie , @nrNota , @IdCliente , @IdFormaPagamento ,  @nrparcela , @vlParcela , @flSituacao , @dtVencimento)";
                    command.Parameters.Clear();
                    command.Parameters.AddWithValue("@nrModelo", venda.nrModelo);
                    command.Parameters.AddWithValue("@nrSerie", venda.nrSerie);
                    command.Parameters.AddWithValue("@nrNota", idvendaProduto);
                    command.Parameters.AddWithValue("@IdCliente", venda.Cliente.IdCliente);
                    command.Parameters.AddWithValue("@IdFormaPagamento", item.IdFormaPagamento);
                    command.Parameters.AddWithValue("@nrparcela", item.nrParcela);
                    command.Parameters.AddWithValue("@vlParcela", item.vlParcela);
                    command.Parameters.AddWithValue("@flSituacao", item.flSituacao = "A");
                    command.Parameters.AddWithValue("@dtVencimento", item.dtVencimento);

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

        public bool cancelarVenda(Venda venda)
        {
            try
            {
                Open();
                string updateVenda = @"UPDATE Venda SET  flSituacao = @flSituacao, dtUltAlteracao = @dtUltAlteracao 
                                               WHERE nrNota =" + venda.nrNota + "AND nrModelo = " + venda.nrModelo + "AND nrSerie =" + venda.nrSerie;

                SqlCommand sql = new SqlCommand(updateVenda, sqlconnection);
                sql.CommandType = CommandType.Text;

                sql.Parameters.AddWithValue("@flSituacao", venda.flSituacao = "C");
                sql.Parameters.AddWithValue("@dtUltAlteracao", venda.dtUltAlteracao = DateTime.Now);

                int i = sql.ExecuteNonQuery();

                if (i >= 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao Atualizar Venda: " + e.Message);
            }
            finally
            {
                Close();
            }
        }

        public bool cancelarContasReceber(Venda venda)
        {
            try
            {
                Open();
                string updateContasReceber = @"UPDATE ContasReceber SET  flSituacao = @flSituacao
                                               WHERE nrNota =" + venda.nrNota +
                                              "AND nrModelo = " + venda.nrModelo +
                                              "AND nrSerie =" + venda.nrSerie +
                                              "AND IdCliente =" + venda.IdCliente;

                SqlCommand sql = new SqlCommand(updateContasReceber, sqlconnection);
                sql.CommandType = CommandType.Text;

                sql.Parameters.AddWithValue("@flSituacao", venda.flSituacao = "C");

                int i = sql.ExecuteNonQuery();

                if (i >= 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao Atualizar Contas Receber: " + e.Message);
            }
            finally
            {
                Close();
            }
        }

        #endregion


        public List<Venda> SelecionarVenda()
        {
            try
            {
                Open();
                SQL = new SqlCommand(@"SELECT * FROM Venda INNER JOIN Cliente ON Venda.IdCliente = Cliente.IdCliente", sqlconnection);
                SQL.CommandType = CommandType.Text;
                Dr = SQL.ExecuteReader();
                var lista = new List<Venda>();
                while (Dr.Read())
                {
                    var v = new Venda()
                    {
                        Cliente = new ViewModels.Clientes.SelectClienteVM
                        {
                            IdCliente = Convert.ToInt32(Dr["IdCliente"]),
                            nmCliente = Convert.ToString(Dr["nmCliente"]),
                        },
                        flSituacao = Convert.ToString(Dr["flSituacao"]),
                        nrModelo = Convert.ToString(Dr["nrModelo"]),
                        nrSerie = Convert.ToString(Dr["nrSerie"]),
                        nrNota = Convert.ToInt32(Dr["nrNota"]),
                        dtNota = Dr["dtEmissao"] == DBNull.Value ? DateTime.Now : Convert.ToDateTime(Dr["dtEmissao"]),

                    };
                    lista.Add(v);
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


        public VendaVM GetVenda(string filter, string nmModelo, string nrSerie, int nrNota, int? IdCliente)
        {
            try
            {
                Open();
                var compraVM = new VendaVM();
                var sql = this.BuscarVenda(filter, nmModelo, nrSerie, nrNota, IdCliente);
                var sqlProduto = this.BuscarProdutos(nmModelo, nrSerie, nrNota);
                var sqlParcela = this.BuscarParcelas(nmModelo, nrSerie, nrNota, IdCliente);

                var listProdutos = new List<VendaVM.ProdutosVM>();
                var listParcelas = new List<VendaVM.ParcelasVM>();

                SQL = new SqlCommand(sql + sqlProduto + sqlParcela, sqlconnection);
                Dr = SQL.ExecuteReader();
                while (Dr.Read())
                {

                    compraVM.nrModelo = Convert.ToString(Dr["Venda_Modelo"]);
                    compraVM.nrNota = Convert.ToInt32(Dr["Venda_Nota"]);
                    compraVM.nrSerie = Convert.ToString(Dr["Venda_Serie"]);

                    compraVM.dtNota = Dr["Venda_dtEmissao"] == DBNull.Value ? DateTime.Now : Convert.ToDateTime(Dr["Venda_dtEmissao"]);

                    compraVM.Cliente = new ViewModels.Clientes.SelectClienteVM
                    {
                        IdCliente = Convert.ToInt32(Dr["Cliente_ID"]),
                        nmCliente = Convert.ToString(Dr["Cliente_nmNome"])
                    };
                    compraVM.CondicaoPagamento = new ViewModels.CondPagamentos.SelectCondPagamentoVM
                    {
                        Id = Convert.ToInt32(Dr["CondicaoPagamento_ID"]),
                        Text = Convert.ToString(Dr["CondicaoPagamento_Nome"]),
                    };


                }
                if (Dr.NextResult())
                {
                    while (Dr.Read())
                    {
                        var produto = new VendaVM.ProdutosVM
                        {
                            IdProduto = Convert.ToInt32(Dr["ProdutoVenda_ID"]),
                            nmProduto = Convert.ToString(Dr["ProdutoVenda_nmProduto"]),
                            nrQtd = Convert.ToDecimal(Dr["ProdutoVenda_nrQtd"]),
                            vlVenda = Convert.ToDecimal(Dr["ProdutoVenda_vlVenda"]),
                        };
                        listProdutos.Add(produto);
                    }
                }

                if (Dr.NextResult())
                {
                    while (Dr.Read())
                    {
                        var parcela = new VendaVM.ParcelasVM
                        {
                            IdFormaPagamento = Convert.ToInt32(Dr["FormaPagamento_ID"]),
                            dsFormaPagamento = Convert.ToString(Dr["FormaPagamento_dsForma"]),
                            nrParcela = Convert.ToDouble(Dr["ContasReceber_NrParcela"]),
                            vlParcela = Convert.ToDecimal(Dr["ContasReceber_VlParcela"]),
                            dtVencimento = Convert.ToDateTime(Dr["ContasReceber_DtVencimento"]),

                        };
                        listParcelas.Add(parcela);
                    }
                }
                compraVM.ProdutosCompra = listProdutos;
                compraVM.ParcelasVenda = listParcelas;
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

        protected string BuscarVenda(string filter, string nrModelo, string nrSerie, int? nrNota, int? IdCliente)
        {
            var sql = string.Empty;
            var swhere = string.Empty;
            if (!string.IsNullOrEmpty(nrModelo))
            {
                swhere += " AND Venda.nrModelo = '" + nrModelo + "'";
            }
            if (!string.IsNullOrEmpty(nrSerie))
            {
                swhere += " AND Venda.nrSerie = '" + nrSerie + "'";
            }
            if (nrNota != null)
            {
                swhere += " AND Venda.nrNota = " + nrNota;
            }
            if (nrNota != null)
            {
                swhere += " AND Venda.IdCliente = " + IdCliente;
            }

            if (!string.IsNullOrEmpty(filter))
            {
                var filterQ = filter.Split(' ');
                foreach (var word in filterQ)
                {
                    swhere += " OR Cliente.nmCliente LIKE'%" + word + "%'";
                }
            }

            if (!string.IsNullOrEmpty(swhere))
                swhere = " WHERE " + swhere.Remove(0, 4);
            sql = @"
                 SELECT	                     
	                    Venda.flSituacao  AS Venda_flSituacao,
	                    Venda.nrModelo AS Venda_Modelo,
	                    Venda.nrSerie AS Venda_Serie,
	                    Venda.nrNota AS Venda_Nota,
	                    Venda.dtEmissao AS Venda_dtEmissao,  
	                    Venda.dtCadastro AS Venda_dtCadastro,
                        Venda.vlTotal AS Venda_vlTotal,
	                    Venda.IdCliente AS Cliente_ID,
	                    Cliente.nmCliente AS Cliente_nmNome,
	                    Venda.IdCondPag AS CondicaoPagamento_ID,
	                    CondPagamento.dsCondPag AS CondicaoPagamento_Nome

	            FROM Venda
					INNER JOIN Cliente on Venda.IdCliente = Cliente.IdCliente
					INNER JOIN CondPagamento on Venda.IdCondPag = CondPagamento.IdCondPag
                " + swhere + ";";
            return sql;
        }

        protected string BuscarProdutos(string nrModelo, string nrSerie, int? nrNota)
        {
            var sql = string.Empty;

            sql = @"SELECT
	                    ProdutoVenda.nrModelo AS ProdutoVenda_Modelo,
	                    ProdutoVenda.nrSerie AS ProdutoVenda_Modelo,
	                    ProdutoVenda.nrNota AS ProdutoVenda_Modelo,	 
	                    ProdutoVenda.IdProduto AS ProdutoVenda_ID,
	                    Produto.dsProduto AS ProdutoVenda_nmProduto,
	                    ProdutoVenda.nrQtd AS ProdutoVenda_nrQtd,	                 
	                    ProdutoVenda.vlVenda AS ProdutoVenda_vlVenda
	                FROM ProdutoVenda
	                    INNER JOIN Produto on ProdutoVenda.IdProduto = Produto.IdProduto

                    WHERE ProdutoVenda.nrModelo = '" + nrModelo + "' AND ProdutoVenda.nrSerie = '" + nrSerie + "' AND ProdutoVenda.nrNota = " + nrNota;

            ;
            return sql;
        }

        protected string BuscarParcelas(string nrModelo, string nrSerie, int? nrNota, int? IdCliente)
        {
            var sql = string.Empty;
            sql = @"
                   SELECT  
			            ContasReceber.IdCliente AS ContasReceber_Fornecedor_ID ,
	                    ContasReceber.IdFormaPagamento AS FormaPagamento_ID,
	                    Formapagamento.dsFormaPagamento AS FormaPagamento_dsForma,
	                    ContasReceber.nrparcela AS ContasReceber_NrParcela,
	                    ContasReceber.vlparcela AS ContasReceber_VlParcela,
	                    ContasReceber.dtvencimento AS ContasReceber_DtVencimento,
	                      
	                    ContasReceber.nrModelo AS ContasReceber_nrModelo,
	                    ContasReceber.nrSerie AS ContasReceber_nrSerie,
	                    ContasReceber.nrNota AS ContasReceber_nrNota
                    FROM ContasReceber
	                    INNER JOIN Formapagamento on ContasReceber.IdFormaPagamento = FormaPagamento.IdFormaPagamento
                        WHERE ContasReceber.nrModelo = '" + nrModelo + "' AND ContasReceber.nrSerie = '" + nrSerie + "' AND ContasReceber.nrNota = " + nrNota + " AND ContasReceber.IdCliente = " + IdCliente;
            return sql;
        }


    }
}