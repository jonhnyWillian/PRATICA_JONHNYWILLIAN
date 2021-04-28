using SistemaBarbearia.DataBase;
using SistemaBarbearia.Models.Servicos;
using SistemaBarbearia.ViewModels.Servico;
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
        public bool AdicionarServico(Servico servicos)
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

        public IEnumerable<Servico> SelecionarServico()
        {
            try
            {
                Open();
                SQL = new SqlCommand(@"SELECT * FROM Servico", sqlconnection);
                SQL.CommandType = CommandType.Text;
                Dr = SQL.ExecuteReader();
                // Criando uma lista vazia
                var lista = new List<Servico>();
                while (Dr.Read())
                {
                    var servico = new Servico()
                    {
                        Id = Convert.ToInt32(Dr["Id"]),
                        dsServico = Convert.ToString(Dr["nmPais"]),
                        //vlServico = Convert.ToString(Dr["dsSigla"]),
                        dtCadastro = Convert.ToDateTime(Dr["dtCadastro"]),
                        // dtUltAlteracao = Convert.ToDateTime(Dr["dtUltAlteracao"]),
                    };
                    lista.Add(servico);
                }
                return lista;
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao selecionar o Servico: " + e.Message);
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
                    //servicoVM.Id = Convert.ToInt32(Dr["id"]);
                    //servicoVM. = Dr["nmPais"].ToString();
                    //servicoVM.dsSigla = Dr["dsSigla"].ToString();
                    //servicoVM.dtCadastro = Convert.ToDateTime(Dr["dtCadastro"]);
                    // paisVM.dtUltAlteracao = Convert.ToDateTime(Dr["dtUltAlteracao"]);
                }
                return servicoVM;
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao selecionar o Servico: " + e.Message);
            }
            finally
            {
                Close();
            }
        }

        public ServicoVM filtraPais(string nmpais)
        {
            try
            {
                Open();
                var servicoVM = new ServicoVM();
                string selectEditPais = @"SELECT* FROM SERVICO WHERE nmPais =" + nmpais;
                SQL = new SqlCommand(selectEditPais, sqlconnection);


                Dr = SQL.ExecuteReader();
                while (Dr.Read())
                {
                    //paisVM.Id = Convert.ToInt32(Dr["id"]);
                    //paisVM.nmPais = Dr["nmPais"].ToString();
                    //paisVM.dsSigla = Dr["dsSigla"].ToString();
                    //paisVM.dtCadastro = Convert.ToDateTime(Dr["dtCadastro"]);
                    //paisVM.dtUltAlteracao = Convert.ToDateTime(Dr["dtUltAlteracao"]);
                }
                return servicoVM;
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao selecionar o Servico: " + e.Message);
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
                throw new Exception("Erro ao Atualizar Servico: " + e.Message);
            }
            finally
            {
                Close();
            }
        }

        public bool ExcluirServico(int Id)
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
                throw new Exception("Erro ao excluir Pais: " + e.Message);
            }
            finally
            {
                Close();
            }
        }
    }
}