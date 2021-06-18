using SistemaBarbearia.DataBase;
using SistemaBarbearia.Models.Funcionarios;
using SistemaBarbearia.ViewModels.Funcionarios;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace SistemaBarbearia.DAOs.Funcionarios
{
    public class FuncionarioDAO : DataAccess
    {
        #region INSERT UPDATE DELETE
        public bool InsertFuncionario(Funcionario funcionario)
        {

            try
            {
                string insertCliente = @"INSERT INTO FUNCIONARIO (nmFuncionario,nmApelido,flSexo nrTelefone,nrCelular, nrCEP, dsLougradouro, nrResidencial, dsBairro, dsComplemento,dsEmail, 
                                                              nrCPF,nrRG,vlSalario,idCidade,idCargo,dsLogin,senha,dtAdimissao,dtDemissao,dtCadastro)
                                                    VALUES(@nmFuncionario,@nmApelido,@flSexo,@nrTelefone,@nrCelular,@dsLougradouro,@nrResidencial,@dsBairro,@dsComplemento,@dsEmail, 
                                                              @nrCPF,@nrRG,@vlSalario,@idCidade,@idCargo,@dsLogin,@senha,@dtAdimissao,@dtCadastro)";
                SQL = new SqlCommand(insertCliente, sqlconnection);
                SQL.CommandType = CommandType.Text;

                SQL.Parameters.AddWithValue("@nmFuncionario", funcionario.nmFuncionario.ToUpper());
                SQL.Parameters.AddWithValue("@nmApelido", funcionario.nmApelido.ToUpper());
                SQL.Parameters.AddWithValue("@flSexo", funcionario.flSexo);
                SQL.Parameters.AddWithValue("@nrTelefone", funcionario.nrTelefone);
                SQL.Parameters.AddWithValue("@nrCelular", funcionario.nrCelular);
                SQL.Parameters.AddWithValue("@nrCEP", funcionario.nrCEP);
                SQL.Parameters.AddWithValue("@dsLougradouro", funcionario.dsLougradouro.ToUpper());
                SQL.Parameters.AddWithValue("@nrResidencial", funcionario.nrResidencial);
                SQL.Parameters.AddWithValue("@dsBairro", funcionario.dsBairro.ToUpper());
                SQL.Parameters.AddWithValue("@dsComplemento", funcionario.dsComplemento.ToUpper());
                SQL.Parameters.AddWithValue("@nrCPF", funcionario.nrCPF);
                SQL.Parameters.AddWithValue("@nrRG", funcionario.nrRG);
                SQL.Parameters.AddWithValue("@vlSalario", funcionario.vlSalario);
                SQL.Parameters.AddWithValue("@idCidade", funcionario.Cidade.IdCidade);
                //SQL.Parameters.AddWithValue("@idCondPagamento", funcionario.condPagamento.Id);
                SQL.Parameters.AddWithValue("@dsLogin", funcionario.dsLogin);
                SQL.Parameters.AddWithValue("@senha", funcionario.senha);
                SQL.Parameters.AddWithValue("@dtAdimissao", funcionario.dtAdimissao = DateTime.Now);
                SQL.Parameters.AddWithValue("@dtCadastro", funcionario.dtCadastro = DateTime.Now);

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
                throw new Exception("Erro ao Adicionar Novo Funcinario: " + e.Message);
            }
            finally
            {
                Close();
            }
        }

        public bool UpdateFuncionario(Funcionario funcionario)
        {
            try
            {
                Open();
                string updateCliente = @"UPDATE FUNCIONARIO SET nmFuncionario = @nmFuncionario,nmApelido = @nmApelido,flSexo = @flSexo, nrTelefone = @nrTelefone, nrCelular = @nrCelular, 
                                                                dsLougradouro = @dsLougradouro, nrResidencial = @nrResidencial, dsBairro = @dsBairro, dsComplemento = @dsComplemento,
                                                                dsEmail = @dsEmail, nrCPF = @nrCPF, nrRG = @nrRG, vlSalario = @vlSalario, idCidade = @idCidade, idCargo = @idCargo, dsLogin = @dsLogin,
                                                                senha = @senha, dtAdimissao = @dtAdimissao, dtDemissao = @dtDemissao ,dtCadastro  = @dtCadastro, dtUltAlteracao = @dtUltAlteracao  
                                                          WHERE IdFuncionario = @IdFuncionario";
                SqlCommand sql = new SqlCommand(updateCliente, sqlconnection);
                sql.CommandType = CommandType.Text;

                sql.Parameters.AddWithValue("@IdFuncionario", funcionario.IdFuncionario);
                SQL.Parameters.AddWithValue("@nmFuncionario", funcionario.nmFuncionario.ToUpper());
                SQL.Parameters.AddWithValue("@nmApelido", funcionario.nmApelido.ToUpper());
                SQL.Parameters.AddWithValue("@flSexo", funcionario.flSexo);
                SQL.Parameters.AddWithValue("@nrTelefone", funcionario.nrTelefone);
                SQL.Parameters.AddWithValue("@nrCelular", funcionario.nrCelular);
                SQL.Parameters.AddWithValue("@dsLougradouro", funcionario.dsLougradouro.ToUpper());
                SQL.Parameters.AddWithValue("@nrResidencial", funcionario.nrResidencial);
                SQL.Parameters.AddWithValue("@dsBairro", funcionario.dsBairro.ToUpper());
                SQL.Parameters.AddWithValue("@dsComplemento", funcionario.dsComplemento.ToUpper());
                SQL.Parameters.AddWithValue("@nrCPF", funcionario.nrCPF);
                SQL.Parameters.AddWithValue("@nrRG", funcionario.nrRG);
                SQL.Parameters.AddWithValue("@vlSalario", funcionario.vlSalario);
                SQL.Parameters.AddWithValue("@idCidade", funcionario.Cidade.IdCidade);
                //SQL.Parameters.AddWithValue("@idCondPagamento", funcionario.condPagamento.Id);
                SQL.Parameters.AddWithValue("@dsLogin", funcionario.dsLogin);
                SQL.Parameters.AddWithValue("@senha", funcionario.senha);

                sql.Parameters.AddWithValue("@dtUltAlteracao", funcionario.dtUltAlteracao = DateTime.Now);

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

        public bool DeleteFuncionario(int Id)
        {
            try
            {
                Open();
                string deleteCliente = "DELETE FROM Funcionario WHERE IdFuncionario = @IdFuncionario";
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
                throw new Exception("Erro ao excluir Funcionario: " + e.Message);
            }
            finally
            {
                Close();
            }
        }
        #endregion

        public IEnumerable<Funcionario> SelecionarFuncionario()
        {
            try
            {
                Open();
                SQL = new SqlCommand(@"SELECT* FROM FUNCIONARIO", sqlconnection);
                SQL.CommandType = CommandType.Text;
                Dr = SQL.ExecuteReader();
                // Criando uma lista vazia
                var lista = new List<Funcionario>();
                while (Dr.Read())
                {
                    var funcionario = new Funcionario()
                    {
                        IdFuncionario = Convert.ToInt32(Dr["IdFuncionario"]),
                        nmFuncionario = Convert.ToString(Dr["nmFuncionario"]),
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
                        //idCidade = Convert.ToInt32(Dr[""]),
                        // idCondPagamento = Convert.ToInt32(Dr[""]),
                        dsLogin = Convert.ToString(Dr["dsLogin"]),
                        senha = Convert.ToString(Dr["senha"]),
                        dtCadastro = Convert.ToDateTime(Dr["dtCadastro"]),

                    };
                    lista.Add(funcionario);
                }
                return lista;
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao selecionar o Funcionario: " + e.Message);
            }
            finally
            {
                Close();
            }
        }

        public FuncionarioVM GetFuncionario(int? Id)
        {
            try
            {
                Open();
                var funcionarioVM = new FuncionarioVM();
                string selectEditCliente = @"SELECT* FROM FUNCIONARIO WHERE IdFuncionario =" + Id;
                SQL = new SqlCommand(selectEditCliente, sqlconnection);


                Dr = SQL.ExecuteReader();
                while (Dr.Read())
                {
                    funcionarioVM.IdFuncionario = Convert.ToInt32(Dr["IdFuncionario"]);
                    funcionarioVM.nmFuncionario = Dr["nmEstado"].ToString();
                    funcionarioVM.nmApelido = Dr["nmApelido"].ToString();
                    funcionarioVM.flSexo = Dr["flSexo"].ToString();
                    funcionarioVM.nrTelefone = Dr["nrTelefone"].ToString();
                    funcionarioVM.nrCelular = Dr["nrCelular"].ToString();
                    funcionarioVM.nrCEP = Dr["nrCEP"].ToString();
                    funcionarioVM.cidade.IdCidade = Convert.ToInt32(Dr["idCidade"]);
                    funcionarioVM.dsLogin = Dr["dsLogin"].ToString();
                    funcionarioVM.senha = Dr["senha"].ToString();
                    funcionarioVM.dsBairro = Dr["dsBairro"].ToString();
                    funcionarioVM.dsComplemento = Dr["dsComplemento"].ToString();
                    funcionarioVM.dsLougradouro = Dr["dsLougradouro"].ToString();
                    funcionarioVM.nrResidencial = Dr["nrResidencial"].ToString();                    
                    funcionarioVM.cargos.IdCargo = Convert.ToInt32(Dr["id"]);
                    funcionarioVM.dsEmail = Dr["dsEmail"].ToString();
                    funcionarioVM.nrCPF = Dr["nrCPF"].ToString();
                    funcionarioVM.nrRG = Dr["nrRG"].ToString();
                    funcionarioVM.vlSalario = Convert.ToDecimal( Dr["vlSalario"]);
                    funcionarioVM.dtNasc = Dr["dtNasc"] == DBNull.Value ? DateTime.Now : Convert.ToDateTime(Dr["dtNasc"]);
                    funcionarioVM.dtAdimissao = Dr["dtAdimissao"] == DBNull.Value ? DateTime.Now : Convert.ToDateTime(Dr["dtAdimissao"]);
                    funcionarioVM.dtDemissao = Dr["dtDemissao"] == DBNull.Value ? DateTime.Now : Convert.ToDateTime(Dr["dtDemissao"]);
                    funcionarioVM.dtCadastro = Dr["dtCadastro"] == DBNull.Value ? DateTime.Now : Convert.ToDateTime(Dr["dtCadastro"]);
                    funcionarioVM.dtUltAlteracao = Dr["dtUltAlteracao"] == DBNull.Value ? DateTime.Now : Convert.ToDateTime(Dr["dtUltAlteracao"]);
                }
                return funcionarioVM;
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao selecionar o Funcionario: " + e.Message);
            }
            finally
            {
                Close();
            }
        }


    }
}