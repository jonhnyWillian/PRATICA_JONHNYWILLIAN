using SistemaBarbearia.DataBase;
using SistemaBarbearia.Models.CondPagamento;
using SistemaBarbearia.ViewModels.CondPagamentos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace SistemaBarbearia.DAOs.CondPagamentos
{
    public class CondPagamentoDAO : DataAccess
    {
        #region INSERT UPDATE DELETE
        public bool InsertCondPagamento(CondPagamento condPagamento)
        {
            int i = 0;
            Open();
            SqlTransaction sqlTransaction = sqlconnection.BeginTransaction();
            SqlCommand command;
            try
            {
                command = sqlconnection.CreateCommand();
                command.Transaction = sqlTransaction;
                command.CommandText = "INSERT INTO CondPagamento (dsCondPag,txJuro,txMulta,dtCadastro)" +
                                                         " VALUES(@dsCondPag, @txJuro, @txMulta, @dtCadastro);SELECT CAST(SCOPE_IDENTITY() AS int)";

                command.Parameters.AddWithValue("@dsCondPag", condPagamento.dsCondPag.ToUpper());
                command.Parameters.AddWithValue("@txJuro", ((object)condPagamento.txJuro) != DBNull.Value ? ((object)condPagamento.txJuro) : 0);
                command.Parameters.AddWithValue("@txMulta", ((object)condPagamento.txMulta) != DBNull.Value ? ((object)condPagamento.txMulta) : 0);
                command.Parameters.AddWithValue("@dtcadastro", ((object)condPagamento.dtCadastro) ?? DBNull.Value);
                //command.Parameters.AddWithValue("@IdCondPag", condPagamento.IdCondPag);

                Int32 idRetorno = Convert.ToInt32(command.ExecuteScalar());

                foreach (var item in condPagamento.CondicaoForma)
                {
                    command = sqlconnection.CreateCommand();
                    command.Transaction = sqlTransaction;
                    command.CommandText = "INSERT INTO CondPagamentoParcela (CondPag_id, FormaPagamento_id,nrParcela,qtdDias,txPercentual)" +
                                                               " VALUES(@CondPag_id, @FormaPagamento_id, @nrParcela, @qtdDias, @txPercentual)";
                 
                    command.Parameters.Clear();
                    command.Parameters.AddWithValue("@CondPag_id", idRetorno);
                    command.Parameters.AddWithValue("@FormaPagamento_id", item.IdFormaPagamento);
                    command.Parameters.AddWithValue("@nrParcela", ((object)item.nrParcela) != DBNull.Value ? ((object)item.nrParcela) : 0);
                    command.Parameters.AddWithValue("@qtdDias", ((object)item.qtdDias) != DBNull.Value ? ((object)item.qtdDias) : 0);
                    command.Parameters.AddWithValue("@txPercentual", ((object)item.txPercentual) != DBNull.Value ? ((object)item.txPercentual) : 0);
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
                throw new Exception("Erro ao inserir o Condicao de pagamento: " + ex.Message);
            }
            finally
            {
                Close();
            }
        }

        public bool UpdateCondPagamento(CondPagamento condPagamento)
        {
          
            try
            {
                Open();               
                string deleteParcelas = "DELETE FROM CondPagamentoParcela WHERE CondPag_id = @CondPag_id";
                SqlCommand sql = new SqlCommand(deleteParcelas, sqlconnection);
                sql.CommandType = CommandType.Text;
                sql.Parameters.AddWithValue("@IdCondPag", condPagamento.IdCondPag);

                string updateCondPagamento = @"UPDATE CondPagamento SET dsCondPag = @dsCondPag, txJuro = @txJuro, txMulta = @txMulta  ,dtUltAlteracao = @dtUltAlteracao  WHERE IdCondPag = " + condPagamento.IdCondPag;
                sql = new SqlCommand(updateCondPagamento, sqlconnection);          

                sql.Parameters.AddWithValue("@IdCondPag", condPagamento.IdCondPag);
                sql.Parameters.AddWithValue("@dsCondPag", condPagamento.dsCondPag);
                sql.Parameters.AddWithValue("@txJuro", condPagamento.txJuro);
                sql.Parameters.AddWithValue("@txMulta", condPagamento.txMulta);
                sql.Parameters.AddWithValue("@dtUltAlteracao", condPagamento.dtUltAlteracao = DateTime.Now);


                foreach (var item in condPagamento.CondicaoForma)
                {
                    
                    sql.Parameters.AddWithValue("@CondPag_id", condPagamento.IdCondPag);
                    sql.Parameters.AddWithValue("@FormaPagamento_id", item.IdFormaPagamento);
                    sql.Parameters.AddWithValue("@nrParcela", ((object)item.nrParcela) != DBNull.Value ? ((object)item.nrParcela) : 0);
                    sql.Parameters.AddWithValue("@qtdDias", ((object)item.qtdDias) != DBNull.Value ? ((object)item.qtdDias) : 0);
                    sql.Parameters.AddWithValue("@txPercentual", ((object)item.txPercentual) != DBNull.Value ? ((object)item.txPercentual) : 0);
                    sql.CommandType = CommandType.Text;
                    sql.ExecuteNonQuery();

                }

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
                throw new Exception("Erro ao Atualizar Condição de Pagamento: " + e.Message);
            }
            finally
            {
                Close();
            }
        }

        public bool DeleteCondPagamento(int Id)
        {
            try
            {
                Open();
                string deleteCondPagamento = "DELETE FROM CondPagamento WHERE IdCondPag = @IdCondPag";
                SqlCommand sql = new SqlCommand(deleteCondPagamento, sqlconnection);
                sql.CommandType = CommandType.Text;

                sql.Parameters.AddWithValue("@IdCondPag", Id);

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
                throw new Exception("Erro ao excluir Condição de Pagamento: " + e.Message);
            }
            finally
            {
                Close();
            }
        }

        #endregion

        public IEnumerable<CondPagamento> SelecionarCondPagamento()
        {
            try
            {
                Open();
                SQL = new SqlCommand(@"SELECT * FROM CondPagamento ", sqlconnection);
                SQL.CommandType = CommandType.Text;
                Dr = SQL.ExecuteReader();
                // Criando uma lista vazia
                var lista = new List<CondPagamento>();
                while (Dr.Read())
                {
                    var CondPagamento = new CondPagamento()
                    {
                        IdCondPag = Convert.ToInt32(Dr["IdCondPag"]),
                        dsCondPag = Convert.ToString(Dr["dsCondPag"]),
                        //txDesconto = Convert.ToDecimal(Dr["txDesconto"]),
                        //txMulta = Convert.ToDecimal(Dr["txMulta"]),
                        txJuro = Convert.ToDecimal(Dr["txJuro"]),
                        dtCadastro = Dr["dtCadastro"] == DBNull.Value ? DateTime.Now : Convert.ToDateTime(Dr["dtCadastro"]),
                        dtUltAlteracao = Dr["dtUltAlteracao"] == DBNull.Value ? DateTime.Now : Convert.ToDateTime(Dr["dtUltAlteracao"]),
                    };
                    lista.Add(CondPagamento);
                }
                return lista;
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao selecionar a Condição de Pagamento: " + e.Message);
            }
            finally
            {
                Close();
            }
        }

        private string BuscaCondPag(int? id, string filter)
        {
            var sql = string.Empty;
            var swhere = string.Empty;
            if (id != null)
            {
                swhere = " WHERE CondPagamento.IdCondPag = " + id;
            }
            if (!string.IsNullOrEmpty(filter))
            {
                var filterQ = filter.Split(' ');
                foreach (var word in filterQ)
                {
                    swhere += " OR CondPagamento.dsCondPag LIKE'%" + word + "%'";
                }
                swhere = " WHERE " + swhere.Remove(0, 3);
            }
            sql = @"
                   SELECT
	                    CondPagamento.IdCondPag AS CondicaoPagamento_ID,
	                    CondPagamento.dsCondPag AS CondicaoPagamento_Nome,
	                    CondPagamento.txJuro AS CondicaoPagamento_TaxaJuros,
	                    CondPagamento.txMulta AS CondicaoPagamento_Multa,
	                    CondPagamento.dtCadastro AS CondicaoPagamento_DataCadastro,
	                    CondPagamento.dtUltAlteracao AS CondicaoPagamento_DataUltAlteracao
                    FROM CondPagamento" + swhere + ";";
            return sql;
        }

        private string BuscaParcelas(int? id)
        {
            var sql = string.Empty;

            sql = @"
                     SELECT
	                      CondPagamentoParcela.CondPag_Id AS CondicaoParcela_ID,
	                      CondPagamentoParcela.FormaPagamento_id AS Condicao_FormaPag_ID,
	                      FormaPagamento.dsFormaPagamento AS Condicao_FormaPag,
	                      CondPagamentoParcela.nrParcela AS Parcela_Nr,
	                      CondPagamentoParcela.qtdDias AS Parcela_QtDias,
	                      CondPagamentoParcela.txPercentual AS Parcela_TaxaPercentual
                     FROM CondPagamentoParcela
	                      INNER JOIN FormaPagamento on CondPagamentoParcela.FormaPagamento_id = FormaPagamento.IdFormaPag
                    WHERE CondPagamentoParcela.CondPag_id = " + id
            ;
            return sql;
        }



        public CondPagamento GetCondPagamento(int? Id)
        {
            try
            {
                Open();
                var condPagamentoVM = new CondPagamento();
                string selectEditEstado = @"SELECT* FROM CondPagamento WHERE IdCondPag =" + Id;
                SQL = new SqlCommand(selectEditEstado, sqlconnection);
                Dr = SQL.ExecuteReader();
                while (Dr.Read())
                {
                    condPagamentoVM.IdCondPag = Convert.ToInt32(Dr["IdCondPag"]);
                    condPagamentoVM.dsCondPag = Convert.ToString(Dr["dsCondPag"]);
                    condPagamentoVM.txJuro = Convert.ToDecimal(Dr["txJuro"]);
                    condPagamentoVM.txMulta = Convert.ToDecimal(Dr["txMulta"]);
                    condPagamentoVM.CondicaoForma = this.GetParcelas(Id);
                    //condPagamentoVM.dtCadastro = Dr["dtCadastro"] == DBNull.Value ? DateTime.Now : Convert.ToDateTime(Dr["dtCadastro"]);
                    //condPagamentoVM.dtUltAlteracao = Dr["dtUltAlteracao"] == DBNull.Value ? DateTime.Now : Convert.ToDateTime(Dr["dtUltAlteracao"]);




                }
                return condPagamentoVM;
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao selecionar o Condição de Pagamento: " + e.Message);
            }
            finally
            {
                Close();
            }
        }




        public CondPagamento GetCondPagamentoParcela(int? Id)
        {
            try
            {
                var condPag = new CondPagamento();
                if (Id != null)
                {
                    Open();
                    var sql = this.BuscaCondPag(Id, null);
                    var sqlParcela = this.BuscaParcelas(Id);
                    var lista = new List<CondPagamento.CondicaoPagamentoVM>();
                    SqlCommand query = new SqlCommand(sql + sqlParcela, sqlconnection);
                    Dr = query.ExecuteReader();
                    while (Dr.Read())
                    {
                        condPag.IdCondPag = Convert.ToInt32(Dr["CondicaoPagamento_ID"]);
                        condPag.dsCondPag = Convert.ToString(Dr["CondicaoPagamento_Nome"]);
                        condPag.txJuro = Convert.ToDecimal(Dr["CondicaoPagamento_TaxaJuros"]);
                        condPag.txMulta = Convert.ToDecimal(Dr["CondicaoPagamento_Multa"]);
                        //condPag.dtCadastro = Convert.ToDateTime(Dr["CondicaoPagamento_DataCadastro"]);
                        //condPag.dtUltAlteracao = Convert.ToDateTime(Dr["CondicaoPagamento_DataUltAlteracao"]);
                    };

                    if (Dr.NextResult())
                    {
                        while (Dr.Read())
                        {
                            var item = new CondPagamento.CondicaoPagamentoVM()
                            {
                                IdCondPag = Convert.ToInt32(Dr["CondicaoParcela_ID"]),
                                idFormaPagamento = Convert.ToInt32(Dr["Condicao_FormaPag_ID"]),
                                dsFormaPagamento = Convert.ToString(Dr["Condicao_FormaPag"]),
                                nrParcela = Convert.ToInt16(Dr["Parcela_Nr"]),
                                qtdDias = Convert.ToInt16(Dr["Parcela_QtDias"]),
                                txPercentual = Convert.ToDecimal(Dr["Parcela_TaxaPercentual"])
                            };
                            lista.Add(item);
                        }
                    }
                    condPag.ListCondicao = lista;
                }
                return condPag;
            }
          
            catch (Exception e)
            {
                throw new Exception("Erro ao selecionar o Condição de Pagamento: " + e.Message);
            }
            finally
            {
                Close();
            }
        }


        public List<CondPagamentoParcela> GetParcelas(int? IdCondPag)
        {
            try
            {
                Open();
                var _where = string.Empty;
                _where = " WHERE CondPag_id = " + IdCondPag;
                SQL = new SqlCommand("SELECT * FROM CondPagamentoParcela LEFT JOIN FormaPagamento on CondPagamentoParcela.FormaPagamento_id = FormaPagamento.IdFormaPag" + _where, sqlconnection);
                Dr = SQL.ExecuteReader();
                List<CondPagamentoParcela> parcelas = new List<CondPagamentoParcela>();
                while (Dr.Read())
                {
                    //IdCondPag, IdFormaPagamento,nrParcela,qtdDias,txPercentual)
                    var obj = new CondPagamentoParcela()
                    {
                        IdCondPag = Convert.ToInt32(Dr["CondPag_id"]),
                        IdFormaPagamento = Convert.ToInt32(Dr["FormaPagamento_id"]),
                        dsFormaPagamento = Convert.ToString(Dr["dsFormaPagamento"]),
                        nrParcela = (short) Convert.ToInt32(Dr["nrParcela"]),
                        qtdDias = (short)Convert.ToInt32(Dr["qtdDias"]),
                        txPercentual = Convert.ToDecimal(Dr["txPercentual"]),
                    };
                    parcelas.Add(obj);
                }

                return parcelas;
            }
            catch (Exception error)
            {
                throw new Exception(error.Message);
            }

        }


        protected string BuscarCondPagamento(int? id, string text)
        {
            var sqlSelectPais = string.Empty;
            var where = string.Empty;

            if (id != null)
            {
                where = "WHERE IdCondPag = " + id;
            }
            if (!string.IsNullOrEmpty(text))
            {
                var query = text.Split(' ');
                foreach (var item in query)
                {
                    where += "OR dsCondPag LIKE '%'" + item + "'%'";
                }
                where = "WHERE" + where.Remove(0, 3);
            }
            sqlSelectPais = @"SELECT * FROM CondPagamento " + where;
            return sqlSelectPais;
        }

        public List<SelectCondPagamentoVM> SelectCondPagamento(int? id, string text)
        {
            try
            {

                var sqlSelectCondPagamento = this.BuscarCondPagamento(id, text);
                Open();
                SQL = new SqlCommand(sqlSelectCondPagamento, sqlconnection);
                Dr = SQL.ExecuteReader();
                var list = new List<SelectCondPagamentoVM>();

                while (Dr.Read())
                {
                    var cond = new SelectCondPagamentoVM
                    {
                        Id = Convert.ToInt32(Dr["IdCondPag"]),
                        Text = Convert.ToString(Dr["dsCondPag"]),

                    };

                    list.Add(cond);
                }

                return list;
            }
            catch (Exception error)
            {
                throw new Exception(error.Message);
            }
            finally
            {
                Close();
            }
        }


       
    }
}