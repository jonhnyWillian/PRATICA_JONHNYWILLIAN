using SistemaBarbearia.DataBase;
using SistemaBarbearia.Models.Clientes;
using SistemaBarbearia.ViewModels.Clientes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace SistemaBarbearia.DAOs.Clientes
{
    public class ClienteDAO : DataAccess
    {
        #region INSERT UPDATE DELETE
        public bool InsertCliente(Cliente cliente)
        {

            try
            {
                string insertCliente = @"INSERT INTO CLIENTE (nmCliente,nmApelido,flSexo,nrTelefone,nrCelular,dsLougradouro,nrResidencial,dsBairro,dsComplemento,dsEmail,
                                                             nrCPF,nrRG,idCidade,idCondPagamento,dsLogin,senha,dtCadastro)
                                                    VALUES(@nmCliente,@nmApelido,@flSexo,@nrTelefone,@nrCelular,@dsLougradouro,@nrResidencial,@dsBairro,@dsComplemento,@dsEmail,
                                                             @nrCPF,@nrRG,@idCidade,@idCondPagamento,@dsLogin,@senha,@dtCadastro)";
                SQL = new SqlCommand(insertCliente, sqlconnection);
                SQL.CommandType = CommandType.Text;

                SQL.Parameters.AddWithValue("@nmCliente", cliente.nmCliente.ToUpper());
                SQL.Parameters.AddWithValue("@nmApelido", cliente.nmApelido.ToUpper());
                SQL.Parameters.AddWithValue("@flSexo", cliente.flSexo);
                SQL.Parameters.AddWithValue("@nrTelefone", cliente.nrTelefone);
                SQL.Parameters.AddWithValue("@nrCelular", cliente.nrCelular);
                SQL.Parameters.AddWithValue("@dsLougradouro", cliente.dsLougradouro.ToUpper());
                SQL.Parameters.AddWithValue("@nrResidencial", cliente.nrResidencial);
                SQL.Parameters.AddWithValue("@dsBairro", cliente.dsBairro.ToUpper());
                SQL.Parameters.AddWithValue("@dsComplemento", cliente.dsComplemento.ToUpper());
                SQL.Parameters.AddWithValue("@nrCPF", cliente.nrCPF);
                SQL.Parameters.AddWithValue("@nrRG", cliente.nrRG);
                SQL.Parameters.AddWithValue("@idCidade", cliente.cidade.IdCidade);
                //SQL.Parameters.AddWithValue("@idCondPagamento", cliente.condPagamento.Id);
                SQL.Parameters.AddWithValue("@dsLogin", cliente.dsLogin);
                SQL.Parameters.AddWithValue("@senha", cliente.senha);
                SQL.Parameters.AddWithValue("@dtCadastro", cliente.dtCadastro = DateTime.Now);

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
                throw new Exception("Erro ao Adicionar Novo Clinte: " + e.Message);
            }
            finally
            {
                Close();
            }
        }

        public bool UpdateCliente(Cliente cliente)
        {
            try
            {
                Open();
                string updateCliente = @"UPDATE ESTADO SET nmCliente = @nmCliente , nmApelido = @nmApelido, flSexo = @flSexo, nrTelefone = @nrTelefone, nrCelular = @nrCelular, 
                                                          dsLougradouro = @dsLougradouro, nrResidencial = @nrResidencial, dsBairro = @dsBairro, dsComplemento = @dsComplemento,
                                                          dsEmail = @dsEmail, nrCPF = @nrCPF, nrRG = @nrRG, idCidade = @idCidade, idCondPagamento = @idCondPagamento, 
                                                          dsLogin = @dsLogin, senha = @senha, dtUltAlteracao = @dtUltAlteracao  
                                                          WHERE IdCliente = @IdCliente";
                SqlCommand sql = new SqlCommand(updateCliente, sqlconnection);
                sql.CommandType = CommandType.Text;

                sql.Parameters.AddWithValue("@IdCliente", cliente.IdCliente);
                sql.Parameters.AddWithValue("@nmCliente", cliente.nmCliente.ToUpper());
                sql.Parameters.AddWithValue("@nmApelido", cliente.nmApelido.ToUpper());
                sql.Parameters.AddWithValue("@flSexo", cliente.flSexo);
                sql.Parameters.AddWithValue("@nrTelefone", cliente.nrTelefone);
                sql.Parameters.AddWithValue("@nrCelular", cliente.nrCelular);
                sql.Parameters.AddWithValue("@dsLougradouro", cliente.dsLougradouro);
                sql.Parameters.AddWithValue("@nrResidencial", cliente.nrResidencial);
                sql.Parameters.AddWithValue("@dsBairro", cliente.dsBairro);
                sql.Parameters.AddWithValue("@dsComplemento", cliente.dsComplemento);
                sql.Parameters.AddWithValue("@dsEmail", cliente.dsEmail);
                sql.Parameters.AddWithValue("@nrCPF", cliente.nrCPF);
                sql.Parameters.AddWithValue("@nrRG", cliente.nrRG);
                sql.Parameters.AddWithValue("@idCidade", cliente.cidade.IdCidade);
                //sql.Parameters.AddWithValue("@idCondPagamento", cliente.condPagamento.Id);
                sql.Parameters.AddWithValue("@dsLogin", cliente.dsLogin);
                sql.Parameters.AddWithValue("@senha", cliente.senha);

                sql.Parameters.AddWithValue("@dtUltAlteracao", cliente.dtUltAlteracao = DateTime.Now);

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
                throw new Exception("Erro ao Atualizar Cliente: " + e.Message);
            }
            finally
            {
                Close();
            }
        }

        public bool DeleteCliente(int Id)
        {
            try
            {
                Open();
                string deleteCliente = "DELETE FROM CLIENTE WHERE Id = @Id";
                SqlCommand sql = new SqlCommand(deleteCliente, sqlconnection);
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
                throw new Exception("Erro ao excluir CLIENTE: " + e.Message);
            }
            finally
            {
                Close();
            }
        }
        #endregion

        public IEnumerable<Cliente> SelecionarCliente()
        {
            try
            {
                Open();
                SQL = new SqlCommand(@"SELECT* FROM CLIENTE", sqlconnection);
                SQL.CommandType = CommandType.Text;
                Dr = SQL.ExecuteReader();
                // Criando uma lista vazia
                var lista = new List<Cliente>();
                while (Dr.Read())
                {
                    var cliente = new Cliente()
                    {
                        IdCliente = Convert.ToInt32(Dr["IdCliente"]),
                        nmCliente = Convert.ToString(Dr["nmCliente"]),
                        nmApelido = Convert.ToString(Dr["nmApelido"]),
                        flSexo = Convert.ToString(Dr["flSexo"]),
                        nrTelefone = Convert.ToString(Dr["nrTelefone"]),
                        nrCelular = Convert.ToString(Dr["nrCelular"]),
                        dsLougradouro = Convert.ToString(Dr["dsLougradouro"]),
                        nrResidencial = Convert.ToString(Dr["nrResidencial"]),
                        dsBairro = Convert.ToString(Dr["dsBairro"]),
                        dsComplemento = Convert.ToString(Dr["dsComplemento"]),
                        dsEmail = Convert.ToString(Dr["dsEmail"]),
                        nrCPF = Convert.ToString(Dr["nrCPF"]),
                        nrRG = Convert.ToString(Dr["nrRG"]),
                        idCidade = Convert.ToInt32(Dr["idCidade"]),
                        // idCondPagamento = Convert.ToInt32(Dr[""]),
                        dsLogin = Convert.ToString(Dr["dsLogin"]),
                        senha = Convert.ToString(Dr["senha"]),
                        dtCadastro = Dr["dtCadastro"] == DBNull.Value ? DateTime.Now : Convert.ToDateTime(Dr["dtCadastro"]),
                        dtUltAlteracao = Dr["dtUltAlteracao"] == DBNull.Value ? DateTime.Now : Convert.ToDateTime(Dr["dtUltAlteracao"]),

                };
                    lista.Add(cliente);
                }
                return lista;
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao selecionar o Cliente: " + e.Message);
            }
            finally
            {
                Close();
            }
        }

        public ClienteVM GetCliente(int? Id)
        {
            try
            {
                Open();
                var clienteVM = new ClienteVM();
                string selectEditCliente = @"SELECT* FROM CLIENTE WHERE IdCliente =" + Id;
                SQL = new SqlCommand(selectEditCliente, sqlconnection);


                Dr = SQL.ExecuteReader();
                while (Dr.Read())
                {
                    clienteVM.IdCliente = Convert.ToInt32(Dr["IdCliente"]);
                    clienteVM.nmCliente = Dr["nmEstado"].ToString();
                    clienteVM.nmApelido = Dr["nmApelido"].ToString();
                    clienteVM.flSexo = Dr["flSexo"].ToString();
                    clienteVM.nrTelefone = Dr["nrTelefone"].ToString();
                    clienteVM.nrCelular = Dr["nrCelular"].ToString();
                    clienteVM.dsLougradouro = Dr["dsLougradouro"].ToString();
                    clienteVM.nrResidencial = Dr["nrResidencial"].ToString();
                    clienteVM.dsBairro = Dr["dsBairro"].ToString();
                    clienteVM.dsComplemento = Dr["dsComplemento"].ToString();
                    clienteVM.dsEmail = Dr["dsEmail"].ToString();
                    clienteVM.nrCPF = Dr["nrCPF"].ToString();
                    clienteVM.nrRG = Dr["nrRG"].ToString();
                    clienteVM.cidade.IdCidade = Convert.ToInt32(Dr["idCidade"]);
                    clienteVM.dsLogin = Dr["dsLogin"].ToString();
                    clienteVM.senha = Dr["senha"].ToString();
                    clienteVM.dtCadastro = Dr["dtCadastro"] == DBNull.Value ? DateTime.Now : Convert.ToDateTime(Dr["dtCadastro"]);
                    clienteVM.dtUltAlteracao = Dr["dtUltAlteracao"] == DBNull.Value ? DateTime.Now : Convert.ToDateTime(Dr["dtUltAlteracao"]);
                }
                return clienteVM;
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao selecionar o Cliente: " + e.Message);
            }
            finally
            {
                Close();
            }
        }

    }
}