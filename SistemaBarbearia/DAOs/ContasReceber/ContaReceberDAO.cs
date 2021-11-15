using SistemaBarbearia.DataBase;
using SistemaBarbearia.Models.ContaReceber;
using SistemaBarbearia.ViewModels.ContasReceber;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace SistemaBarbearia.DAOs.ContasReceber
{
    public class ContaReceberDAO : DataAccess
    {
        #region INSERT UPDATE DELETE 

        public void PagarContaReceber(ContaReceber ContaReceber, string nrModelo, string nrSerie, int nrNota, int IdFornecedor, int nrParcela)
        {

            var swhere = " WHERE ContasReceber.nrModelo = '" + nrModelo + "' AND ContasReceber.nrSerie = '" + nrSerie + "' AND ContasReceber.nrNota = " + nrNota + " AND ContasReceber.IdFornecedor = " + IdFornecedor + " AND ContasReceber.nrParcela = " + nrParcela;

            var pagar = "UPDATE ContasReceber SET dtPagamento = " + this.FormatDate(DateTime.Now) + ", flSituacao = 'P', IdConta =" + ContaReceber.ContaBancaria.IdConta + swhere;

            var contaBanco = "UPDATE ContaBanco SET vlSaldo += " + this.FormatDecimal(ContaReceber.vlParcela) + "WHERE ContaBanco.idConta = " + ContaReceber.ContaBancaria.IdConta;


            using (sqlconnection)
            {
                Open();
                SqlTransaction sqlTransaction = sqlconnection.BeginTransaction();
                SqlCommand command = sqlconnection.CreateCommand();
                try
                {
                    command.Transaction = sqlTransaction;

                    command.CommandText = pagar;
                    command.ExecuteNonQuery();

                    command.CommandText = contaBanco;
                    command.ExecuteNonQuery();

                    sqlTransaction.Commit();

                }
                catch (Exception ex)
                {
                    sqlTransaction.Rollback();
                    throw new Exception("Erro ao Pagar a Compra: " + ex.Message);
                }
                finally
                {
                    Close();
                }
            }
        }

        #endregion

        public List<ContaReceber> SelecionarContaReceber()
        {
            try
            {
                Open();
                SQL = new SqlCommand(@"SELECT* FROM ContasReceber 
		                                            INNER JOIN Cliente ON ContasReceber.IdCliente = Cliente.IdCliente ", sqlconnection);
                SQL.CommandType = CommandType.Text;
                Dr = SQL.ExecuteReader();
                // Criando uma lista vazia
                var lista = new List<ContaReceber>();
                while (Dr.Read())
                {
                    var c = new ContaReceber()
                    {
                        Cliente = new ViewModels.Clientes.SelectClienteVM
                        {
                            IdCliente = Convert.ToInt32(Dr["IdCliente"]),
                            nmCliente = Convert.ToString(Dr["nmCliente"]),
                        },

                        nrModelo = Convert.ToString(Dr["nrModelo"]),
                        nrSerie = Convert.ToString(Dr["nrSerie"]),
                        nrNota = Convert.ToInt32(Dr["nrNota"]),
                        nrParcela = Convert.ToInt32(Dr["nrParcela"]),
                        vlParcela = Convert.ToDecimal(Dr["vlParcela"]),
                        flSituacao = Convert.ToString(Dr["flSituacao"]),
                        dtVencimento = Dr["dtVencimento"] == DBNull.Value ? DateTime.Now : Convert.ToDateTime(Dr["dtVencimento"]),
                       

                    };
                    lista.Add(c);
                }
                return lista;
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao selecionar o Venda: " + e.Message);
            }
            finally
            {
                Close();
            }
        }

        public ContaReceberVW GetContaReceber(string filter, string nmModelo, string nrSerie, int nrNota, int? nrParcela, int? IdCliente)
        {
            try
            {
                Open();
                var contaReceberVM = new ContaReceberVW();
                var sql = this.BuscarContaRecever(filter, nmModelo, nrSerie, nrNota, IdCliente, nrParcela);

                SQL = new SqlCommand(sql, sqlconnection);
                Dr = SQL.ExecuteReader();
                while (Dr.Read())
                {

                    contaReceberVM.nrModelo = Convert.ToString(Dr["ContasReceber_nrModelo"]);
                    contaReceberVM.nrNota = Convert.ToInt32(Dr["ContasReceber_nrNota"]);
                    contaReceberVM.nrSerie = Convert.ToString(Dr["ContasReceber_nrSerie"]);
                    contaReceberVM.vlParcela = Convert.ToDecimal(Dr["ContasReceber_vlParcela"]);
                    contaReceberVM.nrParcela = Convert.ToInt32(Dr["ContasReceber_NrParcela"]);
                    contaReceberVM.flSituacao = Convert.ToString(Dr["ContasReceber_Situacao"]);
                    contaReceberVM.dtVencimento = Dr["ContasReceber_DataVencimento"] == DBNull.Value ? DateTime.Now : Convert.ToDateTime(Dr["ContaPagar_DataVencimento"]);

                    contaReceberVM.dtPagamento = Dr["ContasReceber_DataPagamento"] == DBNull.Value ? DateTime.Now : Convert.ToDateTime(Dr["ContaPagar_DataPagamento"]);

                    contaReceberVM.Cliente = new ViewModels.Clientes.SelectClienteVM
                    {
                        IdCliente = Convert.ToInt32(Dr["ContasReceber_Cliente_ID"]),
                        nmCliente = Convert.ToString(Dr["ContasReceber_Cliente_Nome"])
                    };
                    contaReceberVM.FormaPag = new ViewModels.FormaPagamentos.SelectFormaPagamentoVM
                    {
                        Id = Convert.ToInt32(Dr["ContasReceber_FormaPagamento_ID"]),
                        Text = Convert.ToString(Dr["ContasReceber_FormaPagamento_dsFormaPagamento"]),
                    };

                }
                return contaReceberVM;
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

        protected string BuscarContaRecever(string filter, string nrModelo, string nrSerie, int? nrNota, int? nrParcela, int? IdCliente)
        {
            var sql = string.Empty;
            var swhere = string.Empty;
            if (!string.IsNullOrEmpty(nrModelo))
            {
                swhere += " AND ContasReceber.nrModelo = '" + nrModelo + "'";
            }
            if (!string.IsNullOrEmpty(nrSerie))
            {
                swhere += " AND ContasReceber.nrSerie = '" + nrSerie + "'";
            }
            if (nrParcela != null)
            {
                swhere += " AND ContasReceber.nrNota = " + nrParcela;
            }
            if (nrNota != null)
            {
                swhere += " AND ContasReceber.nrNota = " + nrNota;
            }
            if (nrNota != null)
            {
                swhere += " AND ContasReceber.IdCliente = " + IdCliente;
            }

            if (!string.IsNullOrEmpty(filter))
            {
                var filterQ = filter.Split(' ');
                foreach (var word in filterQ)
                {
                    swhere += " OR  Cliente.nmCliente LIKE'%" + word + "%'";
                }
            }

            if (!string.IsNullOrEmpty(swhere))
                swhere = " WHERE " + swhere.Remove(0, 4);
            sql = @"
                SELECT
		                Cliente.IdCliente AS ContasReceber_Cliente_ID,
		                Cliente.nmCliente AS ContasReceber_Cliente_Nome,
		                ContasReceber.IdFormaPagamento AS  ContasReceber_FormaPagamento_ID,
		                FormaPagamento.dsFormaPagamento AS ContasReceber_FormaPagamento_dsFormaPagamento,	                
		                ContasReceber.nrparcela AS ContasReceber_NrParcela,
		                ContasReceber.vlparcela AS ContasReceber_vlParcela,
		                ContasReceber.dtvencimento AS ContasReceber_DataVencimento,
		                ContasReceber.dtpagamento AS ContasReceber_DataPagamento,
		                ContasReceber.flSituacao AS ContasReceber_Situacao,
		                ContasReceber.nrModelo AS ContasReceber_nrModelo,
		                ContasReceber.nrSerie AS ContasReceber_nrSerie,
		                ContasReceber.nrNota AS ContasReceber_nrNota,
		                ContasReceber.IdConta AS ContaBanco_ID,
		                ContaBanco.dsConta AS ContaBanco_dsConta
    	                   
		        FROM ContasReceber
			            INNER JOIN Cliente on ContasReceber.IdCliente = Cliente.IdCliente
			            INNER JOIN FormaPagamento on ContasReceber.IdFormaPagamento = FormaPagamento.IdFormaPagamento
			            LEFT JOIN ContaBanco ON ContasReceber.IdConta = ContaBanco.IdConta
                " + swhere + ";";
            return sql;
        }
    }
}