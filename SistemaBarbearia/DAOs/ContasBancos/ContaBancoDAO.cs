using SistemaBarbearia.DataBase;
using SistemaBarbearia.Models.ContasBancos;
using SistemaBarbearia.ViewModels.ContasBancos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace SistemaBarbearia.DAOs.ContasBancos
{
    public class ContaBancoDAO  : DataAccess
    {
        #region INSERT UPDATE DELETE
        public bool InsertContaBanco(ContaBanco contaBanco)
        {

            try
            {
                Open();
                string insertContaBanco = @"INSERT INTO ContaBanco (dsConta, dsClassificacao, vlSaldo, flSituacao, dtCadastro) VALUES(@dsConta, @dsClassificacao, @vlSaldo, @flSituacao, @dtCadastro)";
                SQL = new SqlCommand(insertContaBanco, sqlconnection);
                SQL.CommandType = CommandType.Text;

                SQL.Parameters.AddWithValue("@dsConta", contaBanco.dsConta.ToUpper());
                SQL.Parameters.AddWithValue("@dsClassificacao", contaBanco.dsClassificacao.ToUpper());
                SQL.Parameters.AddWithValue("@vlSaldo", contaBanco.vlSaldo);
                SQL.Parameters.AddWithValue("@flSituacao", contaBanco.flSituacao.ToUpper());
                SQL.Parameters.AddWithValue("@dtCadastro", contaBanco.dtCadastro = DateTime.Now);

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
                throw new Exception("Erro ao Adicionar Novo Conta Bancaria: " + e.Message);
            }
            finally
            {
                Close();
            }
        }

        public bool UpdateContaBanco(ContaBanco contaBanco)
        {
            try
            {
                Open();
                string updateContaBanco = @"UPDATE ContaBanco SET dsConta = @dsConta, dsClassificacao = @dsClassificacao, vlSaldo = @vlSaldo, flSituacao = @flSituacao,  dtUltAlteracao = @dtUltAlteracao  WHERE IdConta = " + contaBanco.IdConta;
                SQL = new SqlCommand(updateContaBanco, sqlconnection);
                SQL.CommandType = CommandType.Text;

                
                SQL.Parameters.AddWithValue("@dsConta", contaBanco.dsConta.ToUpper());
                SQL.Parameters.AddWithValue("@dsClassificacao", contaBanco.dsClassificacao.ToUpper());
                SQL.Parameters.AddWithValue("@vlSaldo", contaBanco.vlSaldo);
                SQL.Parameters.AddWithValue("@flSituacao", contaBanco.flSituacao.ToUpper());
                SQL.Parameters.AddWithValue("@dtUltAlteracao", contaBanco.dtUltAlteracao = DateTime.Now);


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
                throw new Exception("Erro ao Atualizar Conta Bancaria: " + e.Message);
            }
            finally
            {
                Close();
            }
        }

        public bool DeleteContaBanco(int Id)
        {
            try
            {
                Open();
                string deleteContaBanco = "DELETE FROM ContaBanco WHERE IdConta = @Id";
                SqlCommand sql = new SqlCommand(deleteContaBanco, sqlconnection);
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
                throw new Exception("Erro ao excluir Conta Bancaria: " + e.Message);
            }
            finally
            {
                Close();
            }
        }

        #endregion

        public IEnumerable<ContaBanco> SelecionarContaBanco()
        {
            try
            {
                Open();
                SQL = new SqlCommand(@"SELECT * FROM ContaBanco", sqlconnection);
                SQL.CommandType = CommandType.Text;
                Dr = SQL.ExecuteReader();
                // Criando uma lista vazia
                var lista = new List<ContaBanco>();
                while (Dr.Read())
                {
                    var conta = new ContaBanco()
                    {
                        IdConta = Convert.ToInt32(Dr["IdConta"]),
                        dsConta = Convert.ToString(Dr["dsConta"]),
                        dsClassificacao = Convert.ToString(Dr["dsClassificacao"]),
                        vlSaldo = Convert.ToDecimal(Dr["vlSaldo"]),
                        flSituacao = Convert.ToString(Dr["flSituacao"]),
                        dtCadastro = Convert.ToDateTime(Dr["dtCadastro"]),
                        dtUltAlteracao = Dr["dtUltAlteracao"] == DBNull.Value ? DateTime.Now : Convert.ToDateTime(Dr["dtUltAlteracao"]),
                    };
                    lista.Add(conta);
                }
                return lista;
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao selecionar o Conta Bancaria: " + e.Message);
            }
            finally
            {
                Close();
            }
        }

        public ContaBancoVM GetContaBanco(int? Id)
        {
            try
            {
                Open();
                var contaBancoVM = new ContaBancoVM();
                string selectEditContaBanco = @"SELECT* FROM ContaBanco WHERE IdConta =" + Id;
                SQL = new SqlCommand(selectEditContaBanco, sqlconnection);


                Dr = SQL.ExecuteReader();
                while (Dr.Read())
                {
                    contaBancoVM.IdModelPai = Convert.ToInt32(Dr["IdConta"]);
                    contaBancoVM.dsConta = Dr["dsConta"].ToString();
                    contaBancoVM.dsClassificacao = Dr["dsClassificacao"].ToString();
                    contaBancoVM.vlSaldo = Convert.ToDecimal(Dr["vlSaldo"]);
                    contaBancoVM.flSituacao = Dr["flSituacao"].ToString();
                    contaBancoVM.dtCadastro = Convert.ToDateTime(Dr["dtCadastro"]);
                    contaBancoVM.dtUltAlteracao = Dr["dtUltAlteracao"] == DBNull.Value ? DateTime.Now : Convert.ToDateTime(Dr["dtUltAlteracao"]);
                }
                return contaBancoVM;
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao selecionar o Conta Bancaria: " + e.Message);
            }
            finally
            {
                Close();
            }
        }

        protected string BuscarCargo(int? IdConta, string dsConta)
        {
            var sqlSelectConta = string.Empty;
            var where = string.Empty;

            if (IdConta != null)
            {
                where = "WHERE IdConta = " + IdConta;
            }
            if (!string.IsNullOrEmpty(dsConta))
            {
                var query = dsConta.Split(' ');
                foreach (var item in query)
                {
                    where += "OR dsConta LIKE '%'" + item + "'%'";
                }
                where = "WHERE" + where.Remove(0, 3);
            }
            sqlSelectConta = @"SELECT * FROM ContaBanco " + where;
            return sqlSelectConta;
        }

        public List<SelectContaBancoVM> SelectContaBanco(int? IdConta, string dsConta)
        {
            try
            {

                var sqlSelectConta = this.BuscarCargo(IdConta, dsConta);
                Open();
                SQL = new SqlCommand(sqlSelectConta, sqlconnection);
                Dr = SQL.ExecuteReader();
                var list = new List<SelectContaBancoVM>();

                while (Dr.Read())
                {
                    var conta= new SelectContaBancoVM
                    {
                        IdConta = Convert.ToInt32(Dr["IdConta"]),
                        dsConta = Convert.ToString(Dr["dsConta"]),
                        
                       
                    };
                    list.Add(conta);
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