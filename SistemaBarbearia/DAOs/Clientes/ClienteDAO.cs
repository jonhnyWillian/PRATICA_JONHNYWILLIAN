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
                Open();
                string insertCliente = @"INSERT INTO CLIENTE (nmCliente,nmApelido,flSexo,nrTelefone,nrCelular,nrCEP,dsLogradouro,nrResidencial,dsBairro,dsComplemento,dsEmail,
                                                             nrCPF,nrRG, dataNasc, IdCidade,IdCondPagamento,dtCadastro, dtUltAlteracao)
                                                    VALUES(@nmCliente,@nmApelido,@flSexo,@nrTelefone,@nrCelular,@nrCEP,@dsLogradouro,@nrResidencial,@dsBairro,@dsComplemento,@dsEmail,
                                                             @nrCPF,@nrRG,@dataNasc,@IdCidade,@IdCondPagamento,@dtCadastro, @dtUltAlteracao)";
                SqlCommand sql = new SqlCommand(insertCliente, sqlconnection);
                sql.CommandType = CommandType.Text;

                sql.Parameters.AddWithValue("@nmCliente", cliente.nmCliente.ToUpper());
                sql.Parameters.AddWithValue("@nmApelido", cliente.nmApelido.ToUpper());
                sql.Parameters.AddWithValue("@flSexo", cliente.flSexo);
                sql.Parameters.AddWithValue("@nrTelefone", cliente.nrTelefone);
                sql.Parameters.AddWithValue("@nrCelular", cliente.nrCelular);
                sql.Parameters.AddWithValue("@nrCEP", cliente.nrCEP);
                sql.Parameters.AddWithValue("@dsLogradouro", cliente.dsLogradouro.ToUpper());
                sql.Parameters.AddWithValue("@nrResidencial", cliente.nrResidencial);
                sql.Parameters.AddWithValue("@dsBairro", cliente.dsBairro.ToUpper());
                sql.Parameters.AddWithValue("@dsComplemento", cliente.dsComplemento.ToUpper());
                sql.Parameters.AddWithValue("@dsEmail", cliente.dsEmail.ToUpper());
                sql.Parameters.AddWithValue("@dataNasc", cliente.dataNasc);
                sql.Parameters.AddWithValue("@nrCPF", cliente.nrCPF);
                sql.Parameters.AddWithValue("@nrRG", cliente.nrRG);
                sql.Parameters.AddWithValue("@IdCidade", cliente.idCidade);
                sql.Parameters.AddWithValue("@IdCondPagamento", cliente.IdCondPag);
                sql.Parameters.AddWithValue("@dtCadastro", cliente.dtCadastro = DateTime.Now);
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
                string updateCliente = @"UPDATE CLIENTE SET nmCliente = @nmCliente , nmApelido = @nmApelido, flSexo = @flSexo, nrTelefone = @nrTelefone, nrCelular = @nrCelular, nrCEP = @nrCEP, 
                                                          dsLogradouro = @dsLogradouro, nrResidencial = @nrResidencial, dsBairro = @dsBairro, dsComplemento = @dsComplemento,
                                                          dsEmail = @dsEmail, nrCPF = @nrCPF, dataNasc = @dataNasc, nrRG = @nrRG, IdCidade = @IdCidade, IdCondPagamento = @IdCondPagamento, 
                                                          dtUltAlteracao = @dtUltAlteracao  
                                                          WHERE IdCliente = @IdCliente";
                SqlCommand sql = new SqlCommand(updateCliente, sqlconnection);
                sql.CommandType = CommandType.Text;

                sql.Parameters.AddWithValue("@IdCliente", cliente.IdCliente);
                sql.Parameters.AddWithValue("@nmCliente", cliente.nmCliente.ToUpper());
                sql.Parameters.AddWithValue("@nmApelido", cliente.nmApelido.ToUpper());
                sql.Parameters.AddWithValue("@flSexo", cliente.flSexo);
                sql.Parameters.AddWithValue("@nrTelefone", cliente.nrTelefone);
                sql.Parameters.AddWithValue("@nrCelular", cliente.nrCelular);
                sql.Parameters.AddWithValue("@dsLogradouro", cliente.dsLogradouro);
                sql.Parameters.AddWithValue("@nrCEP", cliente.nrCEP);
                sql.Parameters.AddWithValue("@nrResidencial", cliente.nrResidencial);
                sql.Parameters.AddWithValue("@dsBairro", cliente.dsBairro);
                sql.Parameters.AddWithValue("@dsComplemento", cliente.dsComplemento);
                sql.Parameters.AddWithValue("@dsEmail", cliente.dsEmail);
                sql.Parameters.AddWithValue("@dataNasc", cliente.dataNasc);
                sql.Parameters.AddWithValue("@nrCPF", cliente.nrCPF);
                sql.Parameters.AddWithValue("@nrRG", cliente.nrRG);
                sql.Parameters.AddWithValue("@IdCidade", cliente.idCidade);
                sql.Parameters.AddWithValue("@IdCondPagamento", cliente.IdCondPag);
                //sql.Parameters.AddWithValue("@dsLogin", cliente.dsLogin);
                //sql.Parameters.AddWithValue("@senha", cliente.senha);

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
                string deleteCliente = "DELETE FROM Cliente WHERE IdCliente = @IdCliente";
                SqlCommand sql = new SqlCommand(deleteCliente, sqlconnection);
                sql.CommandType = CommandType.Text;

                sql.Parameters.AddWithValue("@IdCliente", Id);

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
                throw new Exception("Erro ao excluir Cliente: " + e.Message);
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
                        dsLogradouro = Convert.ToString(Dr["dsLogradouro"]),
                        nrResidencial = Convert.ToString(Dr["nrResidencial"]),
                        dsBairro = Convert.ToString(Dr["dsBairro"]),
                        dsComplemento = Convert.ToString(Dr["dsComplemento"]),
                        dsEmail = Convert.ToString(Dr["dsEmail"]),
                        nrCPF = Convert.ToString(Dr["nrCPF"]),
                        nrRG = Convert.ToString(Dr["nrRG"]),
                        idCidade = Convert.ToInt32(Dr["idCidade"]),
                        // IdCondPagamento = Convert.ToInt32(Dr[""]),
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

        public Cliente DAOGetCliente(int Id)
        {
            try
            {
                Open();
                var clienteVM = new Cliente();
                string selectEditCliente = @"SELECT* FROM CLIENTE WHERE IdCliente =" + Id;
                SQL = new SqlCommand(selectEditCliente, sqlconnection);


                Dr = SQL.ExecuteReader();
                while (Dr.Read())
                {
                    clienteVM.IdCliente = Convert.ToInt32(Dr["IdCliente"]);
                    clienteVM.nmCliente = Dr["nmCliente"].ToString();
                    clienteVM.nmApelido = Dr["nmApelido"].ToString();
                    clienteVM.flSexo = Dr["flSexo"].ToString();
                    clienteVM.nrTelefone = Dr["nrTelefone"].ToString();
                    clienteVM.nrCelular = Dr["nrCelular"].ToString();
                    clienteVM.nrCEP = Dr["nrCEP"].ToString();
                    clienteVM.dsLogradouro = Dr["dsLogradouro"].ToString();
                    clienteVM.nrResidencial = Dr["nrResidencial"].ToString();
                    clienteVM.dsBairro = Dr["dsBairro"].ToString();
                    clienteVM.dsComplemento = Dr["dsComplemento"].ToString();
                    clienteVM.dsEmail = Dr["dsEmail"].ToString();
                    clienteVM.nrCPF = Dr["nrCPF"].ToString();
                    clienteVM.nrRG = Dr["nrRG"].ToString();
                    clienteVM.idCidade = Convert.ToInt32(Dr["IdCidade"]);
                    clienteVM.IdCondPag = Convert.ToInt32(Dr["IdCondPagamento"]);
                    clienteVM.dataNasc = Dr["dataNasc"] == DBNull.Value ? DateTime.Now : Convert.ToDateTime(Dr["dataNasc"]);
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

        protected string BuscarCliente(int? id, string text)
        {
            var sqlSelectCliente = string.Empty;
            var where = string.Empty;

            if (id != null)
            {
                where = "WHERE IdCliente = " + id;
            }
            if (!string.IsNullOrEmpty(text))
            {
                var query = text.Split(' ');
                foreach (var item in query)
                {
                    where += "OR nmCLiente LIKE '%'" + item + "'%'";
                }
                where = "WHERE" + where.Remove(0, 3);
            }
            sqlSelectCliente = @"SELECT * FROM Cliente " + where;
            return sqlSelectCliente;
        }

        public List<SelectClienteVM> SelectCliente(int? idCliente, string nmCliete)
        {
            try
            {

                var sqlSelectCliente = this.BuscarCliente(idCliente, nmCliete);
                Open();
                SQL = new SqlCommand(sqlSelectCliente, sqlconnection);
                Dr = SQL.ExecuteReader();
                var list = new List<SelectClienteVM>();

                while (Dr.Read())
                {
                    var cliente = new SelectClienteVM
                    {
                        IdCliente = Convert.ToInt32(Dr["IdCliente"]),
                        nmCliente = Convert.ToString(Dr["nmCliente"]),

                    };

                    list.Add(cliente);
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