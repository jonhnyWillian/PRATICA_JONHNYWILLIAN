using SistemaBarbearia.DataBase;
using SistemaBarbearia.Models.FormaPagamentos;
using SistemaBarbearia.ViewModels.FormaPagamentos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace SistemaBarbearia.DAOs.FormaPagamentos
{
    public class FormaPagamentoDAO : DataAccess    
    {
        #region INSERT UPADTE DELETE
        public bool InsertFormaPagamento(FormaPagamento formaPagamento)
        {

            try
            {
                Open();
                string insertCargo = @"INSERT INTO FormaPagamento (dsFormaPagamento, flSituacao ,dtCadastro) VALUES(@dsFormaPagamento,@flSituacao, @dtCadastro)";
                SQL = new SqlCommand(insertCargo, sqlconnection);
                SQL.CommandType = CommandType.Text;

                SQL.Parameters.AddWithValue("@dsFormaPagamento", formaPagamento.dsFormaPagamento.ToUpper());
                SQL.Parameters.AddWithValue("@flSituacao", formaPagamento.flSituacao);
                SQL.Parameters.AddWithValue("@dtCadastro", formaPagamento.dtCadastro = DateTime.Now);

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
                throw new Exception("Erro ao Adicionar Novo Forma Pagamento: " + e.Message);
            }
            finally
            {
                Close();
            }
        }

        public bool UpdateFormaPagamento(FormaPagamento formaPagamento)
        {
            try
            {
                Open();
                string updateCargo = @"UPDATE FormaPagamento SET dsFormaPagamento = @dsFormaPagamento, flSituacao = @flSituacao, dtUltAlteracao = @dtUltAlteracao  WHERE IdFormaPagamento = @IdFormaPagamento";
                SqlCommand sql = new SqlCommand(updateCargo, sqlconnection);
                sql.CommandType = CommandType.Text;

                sql.Parameters.AddWithValue("@IdFormaPagamento", formaPagamento.IdFormaPagamento);
                sql.Parameters.AddWithValue("@dsFormaPagamento", formaPagamento.dsFormaPagamento.ToUpper());
                sql.Parameters.AddWithValue("@flSituacao", formaPagamento.flSituacao);
                sql.Parameters.AddWithValue("@dtUltAlteracao", formaPagamento.dtUltAlteracao = DateTime.Now);


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
                throw new Exception("Erro ao Atualizar Forma Pagamento: " + e.Message);
            }
            finally
            {
                Close();
            }
        }

        public bool DeleteFormaPagamento(int Id)
        {
            try
            {
                Open();
                string deleteCargo = "DELETE FROM FormaPagamento WHERE IdFormaPagamento = @IdFormaPagamento";
                SqlCommand sql = new SqlCommand(deleteCargo, sqlconnection);
                sql.CommandType = CommandType.Text;

                sql.Parameters.AddWithValue("@IdFormaPagamento", Id);

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
                throw new Exception("Erro ao excluir Forma Pagamento: " + e.Message);
            }
            finally
            {
                Close();
            }
        }

        #endregion

        public IEnumerable<FormaPagamento> SelecionarFormaPagamento()
        {
            try
            {
                Open();
                SQL = new SqlCommand(@"SELECT * FROM FormaPagamento", sqlconnection);
                SQL.CommandType = CommandType.Text;
                Dr = SQL.ExecuteReader();
                // Criando uma lista vazia
                var lista = new List<FormaPagamento>();
                while (Dr.Read())
                {
                    var servico = new FormaPagamento()
                    {
                        IdFormaPagamento = Convert.ToInt32(Dr["IdFormaPagamento"]),
                        dsFormaPagamento = Convert.ToString(Dr["dsFormaPagamento"]),
                        flSituacao = Convert.ToString(Dr["flSituacao"]),
                        dtCadastro = Dr["dtCadastro"] == DBNull.Value ? DateTime.Now : Convert.ToDateTime(Dr["dtCadastro"]),
                        dtUltAlteracao = Dr["dtUltAlteracao"] == DBNull.Value ? DateTime.Now : Convert.ToDateTime(Dr["dtUltAlteracao"]),
                    };
                    lista.Add(servico);
                }
                return lista;
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao selecionar o Forma Pagamento: " + e.Message);
            }
            finally
            {
                Close();
            }
        }

        public FormaPagamentoVM GetFormaPagamento(int? Id)
        {
            try
            {
                Open();
                var formaPagamentoVM = new FormaPagamentoVM();
                string selectEditFormaPg = @"SELECT* FROM FormaPagamento WHERE IdFormaPagamento =" + Id;
                SQL = new SqlCommand(selectEditFormaPg, sqlconnection);


                Dr = SQL.ExecuteReader();
                while (Dr.Read())
                {
                    formaPagamentoVM.IdFormaPagamento = Convert.ToInt32(Dr["IdFormaPagamento"]);
                    formaPagamentoVM.dsFormaPagamento = Dr["dsFormaPagamento"].ToString();
                    formaPagamentoVM.flSituacao = Dr["flSituacao"].ToString();
                    formaPagamentoVM.dtCadastro = Dr["dtCadastro"] == DBNull.Value ? DateTime.Now : Convert.ToDateTime(Dr["dtCadastro"]);
                    formaPagamentoVM.dtUltAlteracao = Dr["dtUltAlteracao"] == DBNull.Value ? DateTime.Now : Convert.ToDateTime(Dr["dtUltAlteracao"]);
                }
                return formaPagamentoVM;
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao selecionar o Forma Pagamento: " + e.Message);
            }
            finally
            {
                Close();
            }
        }

        protected string BuscarFormaPagamento(int? id, string text)
        {
            var sqlSelectPais = string.Empty;
            var where = string.Empty;

            if (id != null)
            {
                where = "WHERE IdFormaPagamento = " + id;
            }
            if (!string.IsNullOrEmpty(text))
            {
                var query = text.Split(' ');
                foreach (var item in query)
                {
                    where += "OR dsFormaPag LIKE '%'" + item + "'%'";
                }
                where = "WHERE" + where.Remove(0, 3);
            }
            sqlSelectPais = @"SELECT * FROM FormaPagamento " + where;
            return sqlSelectPais;
        }

        public List<SelectFormaPagamentoVM> SelectFormaPagamento(int? Id, string dsFormaPag)
        {
            try
            {

                var sqlSelectformaPg = this.BuscarFormaPagamento(Id, dsFormaPag);
                Open();
                SQL = new SqlCommand(sqlSelectformaPg, sqlconnection);
                Dr = SQL.ExecuteReader();
                var list = new List<SelectFormaPagamentoVM>();

                while (Dr.Read())
                {
                    var formaPg = new SelectFormaPagamentoVM
                    {
                        Id = Convert.ToInt32(Dr["IdFormaPagamento"]),
                        Text = Convert.ToString(Dr["dsFormaPagamento"]),                       
                        dtCadastro = Dr["dtCadastro"] == DBNull.Value ? DateTime.Now : Convert.ToDateTime(Dr["dtCadastro"]),
                        dtUltAlteracao = Dr["dtUltAlteracao"] == DBNull.Value ? DateTime.Now : Convert.ToDateTime(Dr["dtUltAlteracao"]),
                    };

                    list.Add(formaPg);
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