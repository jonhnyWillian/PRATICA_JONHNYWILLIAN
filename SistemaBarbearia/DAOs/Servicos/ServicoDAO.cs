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
        public bool InsertServico(Servico servicos)
        {

            try
            {
                Open();
                string insertServico = @"INSERT INTO Servico (dsServico, vlServico, dtCadastro) VALUES(@dsServico, @vlServico, @dtCadastro)";
                SQL = new SqlCommand(insertServico, sqlconnection);
                SQL.CommandType = CommandType.Text;

                SQL.Parameters.AddWithValue("@dsServico", servicos.dsServico.ToUpper());
                SQL.Parameters.AddWithValue("@vlServico", servicos.vlServico);
                SQL.Parameters.AddWithValue("@dtCadastro", servicos.dtCadastro = DateTime.Now);

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
                string updateServico = @"UPDATE SERVICO SET dsServico = @dsServico, vlServico = @vlServico, dtUltAlteracao = @dtUltAlteracao  WHERE id = @id";
                SqlCommand sql = new SqlCommand(updateServico, sqlconnection);
                sql.CommandType = CommandType.Text;

                sql.Parameters.AddWithValue("@id", servico.Id);
                sql.Parameters.AddWithValue("@dsServico", servico.dsServico.ToUpper());
                sql.Parameters.AddWithValue("@vlServico", servico.vlServico);
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
                string deleteServico = "DELETE FROM SERVICO WHERE Id = @Id";
                SqlCommand sql = new SqlCommand(deleteServico, sqlconnection);
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
                throw new Exception("Erro ao excluir Serviço: " + e.Message);
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
                SQL = new SqlCommand(@"SELECT * FROM Serviço", sqlconnection);
                SQL.CommandType = CommandType.Text;
                Dr = SQL.ExecuteReader();
                // Criando uma lista vazia
                var lista = new List<Servico>();
                while (Dr.Read())
                {
                    var servico = new Servico()
                    {
                        Id = Convert.ToInt32(Dr["Id"]),
                        dsServico = Convert.ToString(Dr["dsServico"]),
                        vlServico = Convert.ToDecimal(Dr["vlServico"]),
                        dtCadastro = Convert.ToDateTime(Dr["dtCadastro"]),
                        // dtUltAlteracao = Convert.ToDateTime(Dr["dtUltAlteracao"]),
                    };
                    lista.Add(servico);
                }
                return lista;
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

        public ServicoVM GetServico(int? Id)
        {
            try
            {
                Open();
                var servicoVM = new ServicoVM();
                string selectEditServico = @"SELECT* FROM Servico WHERE id =" + Id;
                SQL = new SqlCommand(selectEditServico, sqlconnection);


                Dr = SQL.ExecuteReader();
                while (Dr.Read())
                {
                    servicoVM.Id = Convert.ToInt32(Dr["id"]);
                    servicoVM.dsServico = Dr["dsServico"].ToString();
                    servicoVM.vlServico = Convert.ToDecimal(Dr["vlServico"]);
                    servicoVM.dtCadastro = Convert.ToDateTime(Dr["dtCadastro"]);
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

       
    }
}