using SistemaBarbearia.DataBase;
using SistemaBarbearia.Models.Servicos;
using SistemaBarbearia.ViewModels.Servicos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace SistemaBarbearia.DAOs.Servicos
{
    public class ServicoDAO : DataAccess
    {

        #region INSERT UPDATE DELETE 
        public bool InsertServico(Servico servico)
        {

            try
            {
                Open();
                string insertServico = @"INSERT INTO Servico (dsServico, vlServico, dtCadastro) VALUES(@dsServico, @vlServico, @dtCadastro)";
                SQL = new SqlCommand(insertServico, sqlconnection);
                SQL.CommandType = CommandType.Text;

                SQL.Parameters.AddWithValue("@dsServico", servico.dsServico.ToUpper());
                SQL.Parameters.AddWithValue("@vlServico", ((object)servico.vlServico) != DBNull.Value ? ((object)servico.vlServico) : 0);
                SQL.Parameters.AddWithValue("@dtCadastro", servico.dtCadastro = DateTime.Now);

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
                throw new Exception("Erro ao Adicionar Novo Servico: " + e.Message);
            }
            finally
            {
                Close();
            }
        }

        public bool UpdateServico(Servico servico)
        {
            try
            {
                Open();
                string updateServico = @"UPDATE SERVICO SET dsServico = @dsServico, vlServico = @vlServico, dtUltAlteracao = @dtUltAlteracao  WHERE IdServico = @IdServico";
                SqlCommand sql = new SqlCommand(updateServico, sqlconnection);
                sql.CommandType = CommandType.Text;

                sql.Parameters.AddWithValue("@IdServico", servico.IdServico);
                sql.Parameters.AddWithValue("@dsServico", servico.dsServico.ToUpper());
                sql.Parameters.AddWithValue("@vlServico", ((object)servico.vlServico) != DBNull.Value ? ((object)servico.vlServico) : 0);
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
                throw new Exception("Erro ao Atualizar Serviço: " + e.Message);
            }
            finally
            {
                Close();
            }
        }

        public bool DeleteServico(int Id)
        {
            try
            {
                Open();
                string deleteServico = "DELETE FROM SERVICO WHERE IdServico = @IdServico";
                SqlCommand sql = new SqlCommand(deleteServico, sqlconnection);
                sql.CommandType = CommandType.Text;

                sql.Parameters.AddWithValue("@IdServico", Id);

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
                throw new Exception("Erro ao excluir SERVICO: " + e.Message);
            }
            finally
            {
                Close();
            }
        }

        #endregion

        public IEnumerable<Servico> SelecionarServico()
        {
            try
            {
                Open();
                SQL = new SqlCommand(@"SELECT * FROM SERVICO", sqlconnection);
                SQL.CommandType = CommandType.Text;
                Dr = SQL.ExecuteReader();
                // Criando uma lista vazia
                var lista = new List<Servico>();
                while (Dr.Read())
                {
                    var servico = new Servico()
                    {
                        IdServico = Convert.ToInt32(Dr["IdServico"]),
                        dsServico = Convert.ToString(Dr["dsServico"]),
                        vlServico = Convert.ToDecimal(Dr["vlServico"]),
                        dtCadastro = Dr["dtCadastro"] == DBNull.Value ? DateTime.Now : Convert.ToDateTime(Dr["dtCadastro"]),
                        dtUltAlteracao = Dr["dtUltAlteracao"] == DBNull.Value ? DateTime.Now : Convert.ToDateTime(Dr["dtUltAlteracao"]),
                    };
                    lista.Add(servico);
                }
                return lista;
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao selecionar o SERVICO: " + e.Message);
            }
            finally
            {
                Close();
            }
        }

        public ServicoVM GetServico(int? Id)
        {
            try
            {
                Open();
                var servicoVM = new ServicoVM();
                string selectEditServico = @"SELECT* FROM SERVICO WHERE IdServico =" + Id;
                SQL = new SqlCommand(selectEditServico, sqlconnection);


                Dr = SQL.ExecuteReader();
                while (Dr.Read())
                {
                    servicoVM.IdServico = Convert.ToInt32(Dr["IdServico"]);
                    servicoVM.dsServico = Dr["dsServico"].ToString();
                    servicoVM.vlServico = Dr["vlServico"] == DBNull.Value ? 0 : Convert.ToDecimal(Dr["vlServico"]);
                    servicoVM.dtCadastro = Dr["dtCadastro"] == DBNull.Value ? DateTime.Now : Convert.ToDateTime(Dr["dtCadastro"]);
                    servicoVM.dtUltAlteracao = Dr["dtUltAlteracao"] == DBNull.Value ? DateTime.Now : Convert.ToDateTime(Dr["dtUltAlteracao"]);

                }
                return servicoVM;
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao selecionar o Serviço: " + e.Message);
            }
            finally
            {
                Close();
            }
        }


        protected string BuscarServico(int? id, string text)
        {
            var sqlSelectPais = string.Empty;
            var where = string.Empty;

            if (id != null)
            {
                where = "WHERE IdServico = " + id;
            }
            if (!string.IsNullOrEmpty(text))
            {
                var query = text.Split(' ');
                foreach (var item in query)
                {
                    where += "OR dsServico LIKE '%'" + item + "'%'";
                }
                where = "WHERE" + where.Remove(0, 3);
            }
            sqlSelectPais = @"SELECT * FROM Servico " + where;
            return sqlSelectPais;
        }

        public List<SelectServicoVM> SelectServico(int? idServico, string dsServico)
        {
            try
            {

                var sqlSelectServico = this.BuscarServico(idServico, dsServico);
                Open();
                SQL = new SqlCommand(sqlSelectServico, sqlconnection);
                Dr = SQL.ExecuteReader();
                var list = new List<SelectServicoVM>();

                while (Dr.Read())
                {
                    var servico = new SelectServicoVM
                    {
                        IdServico = Convert.ToInt32(Dr["IdServico"]),
                        dsServico = Convert.ToString(Dr["dsServico"]),

                    };

                    list.Add(servico);
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