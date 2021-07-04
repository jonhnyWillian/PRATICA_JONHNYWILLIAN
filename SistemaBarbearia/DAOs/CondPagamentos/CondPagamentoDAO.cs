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

            try
            {
                Open();                                                                                //IdCondicaoPagParc,
                string insertCondPagamento = @"INSERT INTO CondPagamento (dsCondPag,txJuro,txMulta,txDesconto,IdFormaPagamento,dtCadastro) 
                                                    VALUES(@dsCondPag, @txJuro, @txMulta, @txDesconto, @IdFormaPagamento,  @dtCadastro)";
                SQL = new SqlCommand(insertCondPagamento, sqlconnection);
                SQL.CommandType = CommandType.Text;

                SQL.Parameters.AddWithValue("@dsCondPag", condPagamento.dsCondPag.ToUpper());
                SQL.Parameters.AddWithValue("@txJuro", condPagamento.txJuro);
                SQL.Parameters.AddWithValue("@txMulta", condPagamento.txMulta);
                SQL.Parameters.AddWithValue("@txDesconto", condPagamento.txDesconto);
                SQL.Parameters.AddWithValue("@IdFormaPagamento", condPagamento.formaPagamento.Id);
                //SQL.Parameters.AddWithValue("@IdCondicaoPagParc", condPagamento.CondPagamentoParcela.id);


                SQL.Parameters.AddWithValue("@dtCadastro", condPagamento.dtCadastro = DateTime.Now);

                int i = SQL.ExecuteNonQuery();

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
                throw new Exception("Erro ao Adicionar Novo Condição de Pagamento: " + e.Message);
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
                string updateCondPagamento = @"UPDATE CondPagamento SET dsCondPag = @dsCondPag, txJuro = @txJuro, txMulta = @txMulta, txDesconto = @txDesconto, IdFormaPagamento = @IdFormaPagamento 
                                                                        ,dtUltAlteracao = @dtUltAlteracao  WHERE IdCondPag =" + condPagamento.IdCondPag;
                SqlCommand sql = new SqlCommand(updateCondPagamento, sqlconnection);
                sql.CommandType = CommandType.Text;

                SQL.Parameters.AddWithValue("@IdCondPag", condPagamento.IdCondPag);
                SQL.Parameters.AddWithValue("@dsCondPag", condPagamento.dsCondPag.ToUpper());
                SQL.Parameters.AddWithValue("@txJuro", condPagamento.txJuro);
                SQL.Parameters.AddWithValue("@txMulta", condPagamento.txMulta);
                SQL.Parameters.AddWithValue("@txDesconto", condPagamento.txDesconto);
                SQL.Parameters.AddWithValue("@IdFormaPagamento", condPagamento.formaPagamento.Id);
                //SQL.Parameters.AddWithValue("@IdCondicaoPagParc", condPagamento.CondPagamentoParcela.id);
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
                        txDesconto = Convert.ToDecimal(Dr["txDesconto"]),
                        txMulta = Convert.ToDecimal(Dr["txMulta"]),
                        txJuro = Convert.ToDecimal(Dr["txJuro"]),
                      

                        idFormaPagamento = Convert.ToInt32(Dr["idFormaPagamento"]),



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

        public CondPagamentoVM GetCondPagamento(int? Id)
        {
            try
            {
                Open();
                var condPagamentoVM = new CondPagamentoVM();
                string selectEditEstado = @"SELECT* FROM CondPagamento WHERE IdCondPag =" + Id;
                SQL = new SqlCommand(selectEditEstado, sqlconnection);
                Dr = SQL.ExecuteReader();
                while (Dr.Read())
                {
                    condPagamentoVM.IdModelPai = Convert.ToInt32(Dr["IdCondPag"]);
                    condPagamentoVM.dsCondPag = Convert.ToString(Dr["dsCondPag"]);
                    condPagamentoVM.txJuro = Convert.ToDecimal(Dr["txJuro"]);
                    condPagamentoVM.txMulta = Convert.ToDecimal(Dr["txMulta"]);
                    
                    condPagamentoVM.dtCadastro = Dr["dtCadastro"] == DBNull.Value ? DateTime.Now : Convert.ToDateTime(Dr["dtCadastro"]);
                    condPagamentoVM.dtUltAlteracao = Dr["dtUltAlteracao"] == DBNull.Value ? DateTime.Now : Convert.ToDateTime(Dr["dtUltAlteracao"]);

                    condPagamentoVM.formaPagamento = new SistemaBarbearia.ViewModels.FormaPagamentos.SelectFormaPagamentoVM
                    {
                        Id = Convert.ToInt32(Dr["IdCondPag"]),
                        //Text = Convert.ToString(Dr["nmPais"]),

                    };


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