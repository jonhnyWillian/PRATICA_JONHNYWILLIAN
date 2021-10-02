using SistemaBarbearia.DataBase;
using SistemaBarbearia.Models.Agendamentos;
using SistemaBarbearia.ViewModels.Agendamentos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace SistemaBarbearia.DAOs.Agendamentos
{
    public class AgendamentosDAO : DataAccess
    {
        #region INSERT UPDATE DELETE

        public bool InsertAgendamento(Agenda agendamento)
        {
            try
            {
                Open();
                string insertAgenda = @"INSERT INTO Agenda (dtAgendamento, hrAgendamnto, IdServico, IdFuncionario, IdCliente, dtCadastro) 
                                               VALUES(@dtAgendamento, @hrAgendamnto, @IdServico, @IdFuncionario, @IdCliente, @dtCadastro)";
                SQL = new SqlCommand(insertAgenda, sqlconnection);
                SQL.CommandType = CommandType.Text;

                SQL.Parameters.AddWithValue("@dtAgendamento", agendamento.dtAgendamento);
                SQL.Parameters.AddWithValue("@hrAgendamnto", agendamento.hrAgendamento);
                SQL.Parameters.AddWithValue("@IdServico", agendamento.Servico.IdServico);
                SQL.Parameters.AddWithValue("@IdFuncionario", agendamento.Funcionario.IdFuncionario);
                SQL.Parameters.AddWithValue("@IdCliente", agendamento.Cliente.IdCliente);
                SQL.Parameters.AddWithValue("@dtCadastro", agendamento.dtCadastro = DateTime.Now);

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
                throw new Exception("Erro ao Adicionar Novo Agendamento: " + e.Message);
            }
            finally
            {
                Close();
            }
        }

        public bool UpdateAgendamento(Agenda agendamento)
        {
            try
            {
                Open();
                string updateAgenda = @"UPDATE Agenda SET dtAgendamento = @dtAgendamento, hrAgendamnto = @hrAgendamnto, IdServico = @IdServico, 
                                                               IdFuncionario = @IdFuncionario, IdCliente @IdCliente, dtUltAlteracao = @dtUltAlteracao  WHERE idAgenda =" + agendamento.IdAgenda;
                SqlCommand sql = new SqlCommand(updateAgenda, sqlconnection);
                sql.CommandType = CommandType.Text;

                sql.Parameters.AddWithValue("@dtAgendamento", agendamento.dtAgendamento);
                sql.Parameters.AddWithValue("@hrAgendamnto", agendamento.hrAgendamento);
                sql.Parameters.AddWithValue("@IdServico", agendamento.Servico.IdServico);
                sql.Parameters.AddWithValue("@IdFuncionario", agendamento.Funcionario.IdFuncionario);
                sql.Parameters.AddWithValue("@IdCliente", agendamento.Cliente.IdCliente);
                sql.Parameters.AddWithValue("@dtUltAlteracao", agendamento.dtUltAlteracao = DateTime.Now);


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
                throw new Exception("Erro ao Atualizar Agenda: " + e.Message);
            }
            finally
            {
                Close();
            }
        }

        public bool DeleteAgendamento(int Id)
        {
            try
            {
                Open();
                string deleteAgenda = "DELETE FROM Agenda WHERE idAgenda = @idAgenda";
                SqlCommand sql = new SqlCommand(deleteAgenda, sqlconnection);
                sql.CommandType = CommandType.Text;

                sql.Parameters.AddWithValue("@idAgenda", Id);

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
                throw new Exception("Erro ao excluir Agenda: " + e.Message);
            }
            finally
            {
                Close();
            }
        }
        #endregion


        public IEnumerable<Agenda> SelecionarAgendamento()
        {
            try
            {
                Open();
                SQL = new SqlCommand(@"SELECT * FROM Agenda", sqlconnection);
                SQL.CommandType = CommandType.Text;
                Dr = SQL.ExecuteReader();
                // Criando uma lista vazia
                var lista = new List<Agenda>();
                while (Dr.Read())
                {
                    var cidade = new Agenda()
                    {
                        IdAgenda = Convert.ToInt32(Dr["idAgenda"]),
                        idCliente = Convert.ToInt32(Dr["idCliente"]),
                        idFuncionario = Convert.ToInt32(Dr["idFuncionario"]),
                        idServico = Convert.ToInt32(Dr["idServico"]),

                        dtAgendamento = Dr["dtAgendamento"] == DBNull.Value ? DateTime.Now : Convert.ToDateTime(Dr["dtAgendamento"]),
                        hrAgendamento = Convert.ToString(Dr["hrAgendamento"]),
                    
                        dtCadastro = Dr["dtCadastro"] == DBNull.Value ? DateTime.Now : Convert.ToDateTime(Dr["dtCadastro"]),
                        dtUltAlteracao = Dr["dtUltAlteracao"] == DBNull.Value ? DateTime.Now : Convert.ToDateTime(Dr["dtUltAlteracao"]),
                    };
                    lista.Add(cidade);
                }
                return lista;
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao selecionar o Agendamento: " + e.Message);
            }
            finally
            {
                Close();
            }
        }

        public AgendamentoVW GetAgendamento(int? Id)
        {
            try
            {
                Open();
                var agendaVM = new AgendamentoVW();
                string selectEditCidade = @"SELECT* FROM Agenda WHERE idAgenda =" + Id;
                SQL = new SqlCommand(selectEditCidade, sqlconnection);


                Dr = SQL.ExecuteReader();
                while (Dr.Read())
                {
                    agendaVM.IdModelPai = Convert.ToInt32(Dr["idAgenda"]);
                    agendaVM.Cliente.IdCliente = Convert.ToInt32(Dr["IdCliente"]);
                    agendaVM.Servico.IdServico = Convert.ToInt32(Dr["IdServico"]);
                    agendaVM.Funcionario.IdFuncionario = Convert.ToInt32(Dr["IdFuncionario"]);

                    agendaVM.dtCadastro = Dr["dtCadastro"] == DBNull.Value ? DateTime.Now : Convert.ToDateTime(Dr["dtCadastro"]);
                    agendaVM.dtUltAlteracao = Dr["dtUltAlteracao"] == DBNull.Value ? DateTime.Now : Convert.ToDateTime(Dr["dtUltAlteracao"]);


                }
                return agendaVM;
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

  
       
        
    }
}