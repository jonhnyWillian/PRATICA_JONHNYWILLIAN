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

                command.CommandText = "INSERT INTO CondPagamentoParcela (IdCondPag, IdFormaPagamento,nrParcela,qtdDias,txPercentual)" +
                                                                 " VALUES(@IdCondPag, @IdFormaPagamento, @nrParcela, @qtdDias, @txPercentual)";

                foreach (var item in condPagamento.CondicaoForma)
                {

                    //command = sqlconnection.CreateCommand();
                    //command.Transaction = sqlTransaction;
                    command.Parameters.Clear();
                    command.Parameters.AddWithValue("@IdCondPag", idRetorno);
                    command.Parameters.AddWithValue("@IdFormaPagamento", item.IdFormaPagamento);
                    command.Parameters.AddWithValue("@nrParcela", ((object)item.txPercentual) != DBNull.Value ? ((object)item.txPercentual) : 0);
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
                string updateCondPagamento = @"UPDATE CondPagamento SET dsCondPag = @dsCondPag, txJuro = @txJuro, txMulta = @txMulta
                                                                        ,dtUltAlteracao = @dtUltAlteracao  WHERE IdCondPag = @IdCondPag";
                SqlCommand sql = new SqlCommand(updateCondPagamento, sqlconnection);
                sql.CommandType = CommandType.Text;

                sql.Parameters.AddWithValue("@dsCondPag", condPagamento.dsCondPag);
                sql.Parameters.AddWithValue("@txJuro", condPagamento.txJuro);
                sql.Parameters.AddWithValue("@txMulta", condPagamento.txMulta);
                sql.Parameters.AddWithValue("@dtUltAlteracao", condPagamento.dtUltAlteracao = DateTime.Now);

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


        public List<CondPagamentoParcela> GetParcelas(int? IdCondPag)
        {
            try
            {
                Open();
                var _where = string.Empty;
                _where = " WHERE IdCondPag = " + IdCondPag;
                SQL = new SqlCommand("SELECT * FROM CondPagamentoParcela LEFT JOIN FormaPagamento on CondPagamentoParcela.IdFormaPagamento = FormaPagamento.IdFormaPag" + _where, sqlconnection);
                Dr = SQL.ExecuteReader();
                List<CondPagamentoParcela> parcelas = new List<CondPagamentoParcela>();
                while (Dr.Read())
                {
                    //IdCondPag, IdFormaPagamento,nrParcela,qtdDias,txPercentual)
                    var obj = new CondPagamentoParcela()
                    {
                        IdCondPag = Convert.ToInt32(Dr["IdCondPag"]),
                        IdFormaPagamento = Convert.ToInt32(Dr["IdFormaPagamento"]),
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