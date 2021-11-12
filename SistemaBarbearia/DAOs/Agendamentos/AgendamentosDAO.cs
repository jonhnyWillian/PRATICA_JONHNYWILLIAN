using SistemaBarbearia.DataBase;
using SistemaBarbearia.Models.Agendamentos;
using SistemaBarbearia.Models.Clientes;
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
                string insertAgenda = @"INSERT INTO Agenda (dtAgendamento, hrAgendamento, IdServico, IdFuncionario, IdCliente,flSituacao, dtCadastro) 
                                               VALUES(@dtAgendamento, @hrAgendamento, @IdServico, @IdFuncionario, @IdCliente, @flSituacao, @dtCadastro)";
                SqlCommand sql = new SqlCommand(insertAgenda, sqlconnection);
                sql.CommandType = CommandType.Text;
                
                sql.Parameters.AddWithValue("@dtAgendamento", agendamento.dtAgendamento);
                sql.Parameters.AddWithValue("@hrAgendamento", agendamento.flhoraAgendamento);
                sql.Parameters.AddWithValue("@IdServico", agendamento.Servico.IdServico);
                sql.Parameters.AddWithValue("@flSituacao", agendamento.flSituacao);
                sql.Parameters.AddWithValue("@IdFuncionario", agendamento.Funcionario.IdFuncionario);
                sql.Parameters.AddWithValue("@IdCliente", agendamento.Cliente.IdCliente);
                sql.Parameters.AddWithValue("@dtCadastro", agendamento.dtCadastro = DateTime.Now);

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
                string updateAgenda = @"UPDATE Agenda SET dtAgendamento = @dtAgendamento, hrAgendamento = @hrAgendamento, IdServico = @IdServico, 
                                                          IdFuncionario = @IdFuncionario, IdCliente = @IdCliente, flSituacao = @flSituacao, dtUltAlteracao = @dtUltAlteracao  WHERE idAgenda =" + agendamento.IdAgenda;
                SqlCommand sql = new SqlCommand(updateAgenda, sqlconnection);
                sql.CommandType = CommandType.Text;

                sql.Parameters.AddWithValue("@dtAgendamento", agendamento.dtAgendamento);
                sql.Parameters.AddWithValue("@hrAgendamento", agendamento.flhoraAgendamento);
                sql.Parameters.AddWithValue("@IdServico", agendamento.IdServico);
                sql.Parameters.AddWithValue("@flSituacao", agendamento.flSituacao);
                sql.Parameters.AddWithValue("@IdFuncionario", agendamento.IdFuncionario);
                sql.Parameters.AddWithValue("@IdCliente", agendamento.IdCliente);                
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

        public bool CancelarAgendamento(Agenda agendamento)
        {
            try
            {
                Open();
                string updateAgenda = @"UPDATE Agenda SET  flSituacao = @flSituacao, dtUltAlteracao = @dtUltAlteracao WHERE idAgenda =" + agendamento.IdAgenda;
                SqlCommand sql = new SqlCommand(updateAgenda, sqlconnection);
                sql.CommandType = CommandType.Text;

                sql.Parameters.AddWithValue("@flSituacao", agendamento.flSituacao);
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
                string deleteAgenda = "DELETE FROM Agenda WHERE idAgenda = "+ Id;
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


        public bool realizarVenda(Agenda agenda)
        {
            int i = 0;
            Open();
            SqlTransaction sqlTransaction = sqlconnection.BeginTransaction();
            SqlCommand command;
            try
            {
                command = sqlconnection.CreateCommand();
                command.Transaction = sqlTransaction;
                command.CommandText = "INSERT INTO VENDA ( nrModelo , nrSerie , nrNota , flSituacao , IdCliente , IdFuncionario , IdCondPag , dtEmissao , dtCadastro , dtUltAlteracao, vlTotal  )" +
                                                   "VALUES(@nrModelo , @nrSerie , @nrNota , @flSituacao , @IdCliente , @IdFuncionario , @IdCondPag , @dtEmissao , @dtCadastro , @dtUltAlteracao, @vlTotal );"; 

                command.Parameters.AddWithValue("@nrModelo", agenda.nrModelo);
                command.Parameters.AddWithValue("@nrSerie", agenda.nrSerie);
                command.Parameters.AddWithValue("@nrNota", agenda.nrNota);
                command.Parameters.AddWithValue("@flSituacao", agenda.flSituacao = "A");
                command.Parameters.AddWithValue("@IdCliente", agenda.Cliente.IdCliente);
                command.Parameters.AddWithValue("@IdFuncionario", agenda.Funcionario.IdFuncionario);
                command.Parameters.AddWithValue("@IdCondPag", agenda.CondicaoPagamento.Id);
                command.Parameters.AddWithValue("@dtEmissao", agenda.dtAgendamento );
                command.Parameters.AddWithValue("@vlTotal", agenda.vlTotal);
                command.Parameters.AddWithValue("@dtCadastro", agenda.dtCadastro = DateTime.Now);
                command.Parameters.AddWithValue("@dtUltAlteracao", agenda.dtUltAlteracao = DateTime.Now);


                i = command.ExecuteNonQuery();
                foreach (var item in agenda.ProdutosCompra)
                {

                    command.CommandText = "INSERT INTO ProdutoVenda ( nrModelo, nrSerie,nrNota,IdProduto,nrQtd,vlVenda,txDesconto)" +
                                                       " VALUES(@nrModelo, @nrSerie, @nrNota, @IdProduto, @nrQtd, @vlVenda, @txDesconto)";

                    command.Parameters.Clear();
                    command.Parameters.AddWithValue("@nrModelo", agenda.nrModelo);
                    command.Parameters.AddWithValue("@nrSerie", agenda.nrSerie);
                    command.Parameters.AddWithValue("@nrNota", agenda.nrNota);
                    command.Parameters.AddWithValue("@IdProduto", item.IdProduto);                    
                    command.Parameters.AddWithValue("@nrQtd", item.nrQtd);                    
                    command.Parameters.AddWithValue("@vlVenda", item.vlVenda);                    

                    i = command.ExecuteNonQuery();
                }
                foreach (var item in agenda.ParcelasVenda)
                {
                    command = sqlconnection.CreateCommand();
                    command.Transaction = sqlTransaction;
                    command.CommandText = "INSERT INTO ContasPagar  ( nrModelo,  nrSerie,  nrNota, IdFornecedor, IdFormaPagamento, vlParcela,  flSituacao, dtVencimento, nrparcela  )" +
                                                         "VALUES(@nrModelo, @nrSerie, @nrNota, @IdFornecedor, @IdFormaPagamento, @vlParcela,  @flSituacao, @dtVencimento, @nrparcela )";

                    command.Parameters.AddWithValue("@nrModelo", agenda.nrModelo);
                    command.Parameters.AddWithValue("@nrSerie", agenda.nrSerie);
                    command.Parameters.AddWithValue("@nrNota", agenda.nrNota);
                    command.Parameters.AddWithValue("@IdFornecedor", agenda.Funcionario.IdFuncionario);
                    command.Parameters.AddWithValue("@IdFormaPagamento", item.IdFormaPagamento);
                    command.Parameters.AddWithValue("@vlParcela", item.vlParcela);
                    command.Parameters.AddWithValue("@flSituacao", item.flSituacao = "A");
                    command.Parameters.AddWithValue("@dtVencimento", item.dtVencimento);
                    command.Parameters.AddWithValue("@nrparcela", item.nrParcela);
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
                throw new Exception("Erro ao inserir o Compra: " + ex.Message);
            }
            finally
            {
                Close();
            }
        }

        #endregion

     

        public List<Agenda> SelecionarAgendamento()
        {
            try
            {
                Open();
                SQL = new SqlCommand(@"SELECT * FROM Agenda INNER JOIN Cliente ON Agenda.idCliente = Cliente.idCliente", sqlconnection);
                SQL.CommandType = CommandType.Text;
                Dr = SQL.ExecuteReader();
                // Criando uma lista vazia
                var lista = new List<Agenda>();
                while (Dr.Read())
                {
                    var horario = new Agenda()
                    {
                        IdAgenda = Convert.ToInt32(Dr["idAgenda"]),
                        Cliente = new ViewModels.Clientes.SelectClienteVM
                        {
                            IdCliente = Convert.ToInt32(Dr["idCliente"]),
                            nmCliente = Convert.ToString(Dr["nmCliente"]),
                        },

                        flSituacao = Convert.ToString(Dr["flSituacao"]),
                       // IdCliente = Convert.ToInt32(Dr["idCliente"]),
                        IdFuncionario = Convert.ToInt32(Dr["idFuncionario"]),
                        IdServico = Convert.ToInt32(Dr["idServico"]),

                        dtAgendamento = Dr["dtAgendamento"] == DBNull.Value ? DateTime.Now : Convert.ToDateTime(Dr["dtAgendamento"]),
                        flhoraAgendamento = Convert.ToString(Dr["hrAgendamento"]),

                        dtCadastro = Dr["dtCadastro"] == DBNull.Value ? DateTime.Now : Convert.ToDateTime(Dr["dtCadastro"]),
                        dtUltAlteracao = Dr["dtUltAlteracao"] == DBNull.Value ? DateTime.Now : Convert.ToDateTime(Dr["dtUltAlteracao"]),
                    };
                    lista.Add(horario);
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

        public Agenda GetAgendamento(int? Id)
        {
            try
            {
                Open();
                var agendaVM = new Agenda();
                string selectEditCidade = @"SELECT* FROM Agenda WHERE idAgenda =" + Id;
                SQL = new SqlCommand(selectEditCidade, sqlconnection);


                Dr = SQL.ExecuteReader();
                while (Dr.Read())
                {
                    agendaVM.IdAgenda = Convert.ToInt32(Dr["idAgenda"]);
                    agendaVM.dtAgendamento = Dr["dtAgendamento"] == DBNull.Value ? DateTime.Now : Convert.ToDateTime(Dr["dtAgendamento"]);
                    agendaVM.IdCliente = Convert.ToInt32(Dr["IdCliente"]);
                    agendaVM.IdServico = Convert.ToInt32(Dr["IdServico"]);
                    agendaVM.IdFuncionario = Convert.ToInt32(Dr["IdFuncionario"]);
                    agendaVM.flhoraAgendamento = Convert.ToString(Dr["hrAgendamento"]);
                    agendaVM.flSituacao = Convert.ToString(Dr["flSituacao"]);
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

        public bool validHorario(string dtAgendamento, string hora, int idFuncionario)
        {
            string sql = @"SELECT * FROM Agenda WHERE dtAgendamento = '" + dtAgendamento + 
                                             "' AND hrAgendamento = '" + hora + 
                                             "' AND idFuncionario = '" + idFuncionario + 
                                             "' AND flSituacao = 'A'" ;            
            Open();
            SqlCommand query = new SqlCommand(sql, sqlconnection);
            var exist = query.ExecuteScalar();
            Close();
            if (exist == null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        public bool validNotaVenda(string nrModelo, string nrSerie, int nrNota, int idCliente)
        {
            string sql = "SELECT * FROM Venda WHERE nrModelo = '" + nrModelo + "' AND nrSerie = '" + nrSerie + "' AND nrNota = " + nrNota + " AND idCliente = " + idCliente;
            Open();
            SqlCommand query = new SqlCommand(sql, sqlconnection);
            var exist = query.ExecuteScalar();
            Close();
            if (exist == null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}