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

        public bool UpdateFormaPagamento(FormaPagamento servico)
        {
            try
            {
                Open();
                string updateCargo = @"UPDATE FormaPagamento SET dsFormaPagamento = @dsCargo, flSituacao = @flSituacao, dtUltAlteracao = @dtUltAlteracao  WHERE id = @id";
                SqlCommand sql = new SqlCommand(updateCargo, sqlconnection);
                sql.CommandType = CommandType.Text;

                sql.Parameters.AddWithValue("@id", servico.Id);
                sql.Parameters.AddWithValue("@dsFormaPagamento", servico.dsFormaPagamento.ToUpper());
                sql.Parameters.AddWithValue("@flSituacao", servico.flSituacao);
                sql.Parameters.AddWithValue("@dtUltAlteracao", servico.dtUltAlteracao = DateTime.Now);


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
                string deleteCargo = "DELETE FROM FormaPagamento WHERE Id = @Id";
                SqlCommand sql = new SqlCommand(deleteCargo, sqlconnection);
                sql.CommandType = CommandType.Text;

                sql.Parameters.AddWithValue("@Id", Id);

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
                        Id = Convert.ToInt32(Dr["Id"]),
                        dsFormaPagamento = Convert.ToString(Dr["dsFormaPagamento"]),
                        flSituacao = Convert.ToString(Dr["flSituacao"]),
                        dtCadastro = Convert.ToDateTime(Dr["dtCadastro"]),
                        // dtUltAlteracao = Convert.ToDateTime(Dr["dtUltAlteracao"]),
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
                string selectEditFormaPg = @"SELECT* FROM FormaPagamento WHERE id =" + Id;
                SQL = new SqlCommand(selectEditFormaPg, sqlconnection);


                Dr = SQL.ExecuteReader();
                while (Dr.Read())
                {
                    formaPagamentoVM.Id = Convert.ToInt32(Dr["id"]);
                    formaPagamentoVM.dsFormaPagamento = Dr["dsFormaPagamento"].ToString();
                    formaPagamentoVM.flSituacao = Dr["flSituacao"].ToString();
                    formaPagamentoVM.dtCadastro = Convert.ToDateTime(Dr["dtCadastro"]);

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

        
    }
}