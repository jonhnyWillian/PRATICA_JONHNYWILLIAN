using SistemaBarbearia.DataBase;
using SistemaBarbearia.Models.ContasPagar;
using SistemaBarbearia.ViewModels.ContasPagar;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace SistemaBarbearia.DAOs.ContasPagar
{
    public class ContaPagarDAO : DataAccess
    {

        #region INSERT UPDATE DELETE 

        public void PagarCompra(ContaPagar contaPagar, string nrModelo, string nrSerie, int nrNota, int IdFornecedor, int nrParcela)
        {

            var swhere = " WHERE ContasPagar.nrModelo = '" + nrModelo + "' AND ContasPagar.nrSerie = '" + nrSerie + "' AND ContasPagar.nrNota = " + nrNota + " AND ContasPagar.IdFornecedor = " + IdFornecedor + " AND ContasPagar.nrParcela = " + nrParcela;

            var swhereCompra = " WHERE Compra.nrModelo = '" + nrModelo + "' AND Compra.nrSerie = '" + nrSerie + "' AND Compra.nrNota = " + nrNota + " AND Compra.IdFornecedor = " + IdFornecedor;

            var pagar = "UPDATE ContasPagar SET dtPagamento = " + this.FormatDate(DateTime.Now) + ", flSituacao = 'P', IdConta =" + contaPagar.ContaBancaria.IdConta + swhere;

            var contaBanco = "UPDATE ContaBanco SET vlSaldo -= " + this.FormatDecimal(contaPagar.vlParcela) + "WHERE ContaBanco.idConta = " + contaPagar.ContaBancaria.IdConta;

            var compra = "UPDATE Compra SET flSituacao = 'P' " + swhereCompra;

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


                    command.CommandText = compra;
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

        public List<ContaPagar> SelecionarCompra()
        {
            try
            {
                Open();
                SQL = new SqlCommand(@"SELECT* FROM ContasPagar 
                                                INNER JOIN Fornecedor ON ContasPagar.idFornecedor = Fornecedor.idFornecedor 
                                                INNER JOIN Compra ON Compra.IdFornecedor = Fornecedor.IdFornecedor                                                        
                                                                                                                   ", sqlconnection);
                SQL.CommandType = CommandType.Text;
                Dr = SQL.ExecuteReader();
                // Criando uma lista vazia
                var lista = new List<ContaPagar>();
                while (Dr.Read())
                {
                    var c = new ContaPagar()
                    {
                        Fornecedor = new ViewModels.Fornecedores.SelectFornecedorVM
                        {
                            IdFornecedor = Convert.ToInt32(Dr["IdFornecedor"]),
                            nmNome = Convert.ToString(Dr["nmNome"]),
                        },

                        nrModelo = Convert.ToString(Dr["nrModelo"]),
                        nrSerie = Convert.ToString(Dr["nrSerie"]),
                        nrNota = Convert.ToInt32(Dr["nrNota"]),
                        nrParcela = Convert.ToInt32(Dr["nrParcela"]),
                        vlParcela = Convert.ToDecimal(Dr["vlParcela"]),
                        flSituacao = Convert.ToString(Dr["flSituacao"]),
                        dtVencimento = Dr["dtVencimento"] == DBNull.Value ? DateTime.Now : Convert.ToDateTime(Dr["dtVencimento"]),
                        dtEmissao = Dr["dtEmissao"] == DBNull.Value ? DateTime.Now : Convert.ToDateTime(Dr["dtEmissao"]),

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

        public ContaPagarVM GetContaPagar(string filter, string nmModelo, string nrSerie, int nrNota, int? nrParcela, int? IdFornecedor)
        {
            try
            {
                Open();
                var contaVM = new ContaPagarVM();
                var sql = this.BuscarCompra(filter, nmModelo, nrSerie, nrNota, IdFornecedor, nrParcela);

                SQL = new SqlCommand(sql, sqlconnection);
                Dr = SQL.ExecuteReader();
                while (Dr.Read())
                {

                    contaVM.nrModelo = Convert.ToString(Dr["ContaPagar_nrModelo"]);
                    contaVM.nrNota = Convert.ToInt32(Dr["ContaPagar_nrNota"]);
                    contaVM.nrSerie = Convert.ToString(Dr["ContaPagar_nrSerie"]);
                    contaVM.vlParcela = Convert.ToDecimal(Dr["ContaPagar_vlParcela"]);
                    contaVM.nrParcela = Convert.ToInt32(Dr["ContaPagar_NrParcela"]);
                    contaVM.flSituacao = Convert.ToString(Dr["ContaPagar_Situacao"]);
                    contaVM.dtVencimento = Dr["ContaPagar_DataVencimento"] == DBNull.Value ? DateTime.Now : Convert.ToDateTime(Dr["ContaPagar_DataVencimento"]);

                    contaVM.dtPagamento = Dr["ContaPagar_DataPagamento"] == DBNull.Value ? DateTime.Now : Convert.ToDateTime(Dr["ContaPagar_DataPagamento"]);

                    contaVM.Fornecedor = new ViewModels.Fornecedores.SelectFornecedorVM
                    {
                        IdFornecedor = Convert.ToInt32(Dr["ContaPagar_Fornecedor_ID"]),
                        nmNome = Convert.ToString(Dr["ContaPagar_Fornecedor_Nome"])
                    };
                    contaVM.formaPag = new ViewModels.FormaPagamentos.SelectFormaPagamentoVM
                    {
                        Id = Convert.ToInt32(Dr["ContaPagar_FormaPagamento_ID"]),
                        Text = Convert.ToString(Dr["ContaPagar_FormaPagamento_dsFormaPagamento"]),
                    };                   

                }
                return contaVM;
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
        public ContaPagarVM GetContaDetais(string filter, string nmModelo, string nrSerie, int nrNota, int? nrParcela, int? IdFornecedor)
        {
            try
            {
                Open();
                var contaVM = new ContaPagarVM();
                var sql = this.BuscarCompraDetais(filter, nmModelo, nrSerie, nrNota, IdFornecedor, nrParcela);

                SQL = new SqlCommand(sql, sqlconnection);
                Dr = SQL.ExecuteReader();
                while (Dr.Read())
                {

                    contaVM.nrModelo = Convert.ToString(Dr["ContaPagar_nrModelo"]);
                    contaVM.nrNota = Convert.ToInt32(Dr["ContaPagar_nrNota"]);
                    contaVM.nrSerie = Convert.ToString(Dr["ContaPagar_nrSerie"]);
                    contaVM.vlParcela = Convert.ToDecimal(Dr["ContaPagar_vlParcela"]);
                    contaVM.nrParcela = Convert.ToInt32(Dr["ContaPagar_NrParcela"]);
                    contaVM.flSituacao = Convert.ToString(Dr["ContaPagar_Situacao"]);
                    contaVM.dtVencimento = Dr["ContaPagar_DataVencimento"] == DBNull.Value ? DateTime.Now : Convert.ToDateTime(Dr["ContaPagar_DataVencimento"]);

                    contaVM.dtPagamento = Dr["ContaPagar_DataPagamento"] == DBNull.Value ? DateTime.Now : Convert.ToDateTime(Dr["ContaPagar_DataPagamento"]);

                    contaVM.Fornecedor = new ViewModels.Fornecedores.SelectFornecedorVM
                    {
                        IdFornecedor = Convert.ToInt32(Dr["ContaPagar_Fornecedor_ID"]),
                        nmNome = Convert.ToString(Dr["ContaPagar_Fornecedor_Nome"])
                    };
                    contaVM.formaPag = new ViewModels.FormaPagamentos.SelectFormaPagamentoVM
                    {
                        Id = Convert.ToInt32(Dr["ContaPagar_FormaPagamento_ID"]),
                        Text = Convert.ToString(Dr["ContaPagar_FormaPagamento_dsFormaPagamento"]),
                    };

                    contaVM.ContaBancaria = new ViewModels.ContasBancos.SelectContaBancoVM
                    {
                        IdConta = Convert.ToInt32(Dr["ContaBanco_ID"]),
                        dsConta = Convert.ToString(Dr["ContaBanco_dsConta"])
                    };

                    contaVM.dtCadastro = Dr["ContaPagar_DataVencimento"] == DBNull.Value ? DateTime.Now : Convert.ToDateTime(Dr["ContaPagar_DataVencimento"]);

                    contaVM.dtUltAlteracao = Dr["ContaPagar_DataPagamento"] == DBNull.Value ? DateTime.Now : Convert.ToDateTime(Dr["ContaPagar_DataPagamento"]);



                }
                return contaVM;
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

        protected string BuscarCompraDetais(string filter, string nrModelo, string nrSerie, int? nrNota, int? nrParcela, int? IdFornecedor)
        {

            var sql = string.Empty;
            var swhere = string.Empty;
            if (!string.IsNullOrEmpty(nrModelo))
            {
                swhere += " AND ContasPagar.nrModelo = '" + nrModelo + "'";
            }
            if (!string.IsNullOrEmpty(nrSerie))
            {
                swhere += " AND ContasPagar.nrSerie = '" + nrSerie + "'";
            }
            if (nrParcela != null)
            {
                swhere += " AND ContasPagar.nrParcela = " + nrParcela;
            }
            if (nrNota != null)
            {
                swhere += " AND ContasPagar.nrNota = " + nrNota;
            }
            if (nrNota != null)
            {
                swhere += " AND ContasPagar.IdFornecedor = " + IdFornecedor;
            }


            if (!string.IsNullOrEmpty(filter))
            {
                var filterQ = filter.Split(' ');
                foreach (var word in filterQ)
                {
                    swhere += " OR Fornecedor.nmNome LIKE'%" + word + "%'";
                }
            }

            if (!string.IsNullOrEmpty(swhere))
                swhere = " WHERE " + swhere.Remove(0, 4);
            sql = @"
                 SELECT
	                Fornecedor.IdFornecedor AS ContaPagar_Fornecedor_ID,
	                Fornecedor.nmNome AS ContaPagar_Fornecedor_Nome,
	                ContasPagar.IdFormaPagamento AS ContaPagar_FormaPagamento_ID,
	                FormaPagamento.dsFormaPagamento AS ContaPagar_FormaPagamento_dsFormaPagamento,	                
					ContasPagar.nrparcela AS ContaPagar_NrParcela,
	                ContasPagar.vlparcela AS ContaPagar_vlParcela,
	                ContasPagar.dtvencimento AS ContaPagar_DataVencimento,
	                ContasPagar.dtpagamento AS ContaPagar_DataPagamento,
	                ContasPagar.flSituacao AS ContaPagar_Situacao,
	                ContasPagar.nrModelo AS ContaPagar_nrModelo,
	                ContasPagar.nrSerie AS ContaPagar_nrSerie,
	                ContasPagar.nrNota AS ContaPagar_nrNota,
	                ContasPagar.IdConta AS ContaBanco_ID,
	                ContaBanco.dsConta AS ContaBanco_dsConta
    	                   
	            FROM ContasPagar
	            INNER JOIN Fornecedor on ContasPagar.IdFornecedor = Fornecedor.IdFornecedor
	            INNER JOIN FormaPagamento on ContasPagar.IdFormaPagamento = FormaPagamento.IdFormaPagamento
				LEFT JOIN ContaBanco ON ContasPagar.IdConta = ContaBanco.IdConta
                " + swhere + ";";
            return sql;
        }

        protected string BuscarCompra(string filter, string nrModelo, string nrSerie, int? nrNota, int? nrParcela, int? IdFornecedor)
        {
           
            var sql = string.Empty;
            var swhere = string.Empty;
            if (!string.IsNullOrEmpty(nrModelo))
            {
                swhere += " AND ContasPagar.nrModelo = '" + nrModelo + "'";
            }
            if (!string.IsNullOrEmpty(nrSerie))
            {
                swhere += " AND ContasPagar.nrSerie = '" + nrSerie + "'";
            }
            if (nrParcela != null)
            {
                swhere += " AND ContasPagar.nrParcela = " + nrParcela;
            }
            if (nrNota != null)
            {
                swhere += " AND ContasPagar.nrNota = " + nrNota;
            }
            if (nrNota != null)
            {
                swhere += " AND ContasPagar.IdFornecedor = " + IdFornecedor;
            }
          

            if (!string.IsNullOrEmpty(filter))
            {
                var filterQ = filter.Split(' ');
                foreach (var word in filterQ)
                {
                    swhere += " OR Fornecedor.nmNome LIKE'%" + word + "%'";
                }
            }

            if (!string.IsNullOrEmpty(swhere))
                swhere = " WHERE " + swhere.Remove(0, 4);
            sql = @"
                 SELECT
	                Fornecedor.IdFornecedor AS ContaPagar_Fornecedor_ID,
	                Fornecedor.nmNome AS ContaPagar_Fornecedor_Nome,
	                ContasPagar.IdFormaPagamento AS ContaPagar_FormaPagamento_ID,
	                FormaPagamento.dsFormaPagamento AS ContaPagar_FormaPagamento_dsFormaPagamento,	                
					ContasPagar.nrparcela AS ContaPagar_NrParcela,
	                ContasPagar.vlparcela AS ContaPagar_vlParcela,
	                ContasPagar.dtvencimento AS ContaPagar_DataVencimento,
	                ContasPagar.dtpagamento AS ContaPagar_DataPagamento,
	                ContasPagar.flSituacao AS ContaPagar_Situacao,
	                ContasPagar.nrModelo AS ContaPagar_nrModelo,
	                ContasPagar.nrSerie AS ContaPagar_nrSerie,
	                ContasPagar.nrNota AS ContaPagar_nrNota,
	                ContasPagar.IdConta AS ContaBanco_ID,
	                ContaBanco.dsConta AS ContaBanco_dsConta
    	                   
	            FROM ContasPagar
	            INNER JOIN Fornecedor on ContasPagar.IdFornecedor = Fornecedor.IdFornecedor
	            INNER JOIN FormaPagamento on ContasPagar.IdFormaPagamento = FormaPagamento.IdFormaPagamento
				LEFT JOIN ContaBanco ON ContasPagar.IdConta = ContaBanco.IdConta
                " + swhere + ";";
            return sql;
        }

    }
}