using SistemaBarbearia.DataBase;
using SistemaBarbearia.Models.Fornecedores;
using SistemaBarbearia.ViewModels.Fornecedores;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace SistemaBarbearia.DAOs.Fornecedores
{

    public class FornecedorDAO : DataAccess
    {
        #region INSERT UPDATE DELETE
        public bool InsertFornecedor(Fornecedor fornecedor)
        {

            try
            {
                string insertCliente = @"INSERT INTO FORNECEDOR  ( nmNome, dsNome, nrContato, nrTelefone, nrCelular, nrCEP, dsLougradouro, nrResidencial,dsBairro, 
                                                                   dsComplemento, dsEmail, nrCPFCNPJ, nrRGIE, dsSite, idCidade, idCondPagamento, dtCadastro)
                                                     VALUES( @nmNome, @dsNome, @nrContato, @nrTelefone, @nrCelular, @nrCEP, @dsLougradouro, @nrResidencial, @dsBairro, 
                                                                   @dsComplemento, @dsEmail, @nrCPFCNPJ, @nrRGIE, @dsSite, @idCidade, @idCondPagamento, @dtCadastro)";
                SQL = new SqlCommand(insertCliente, sqlconnection);
                SQL.CommandType = CommandType.Text;

                SQL.Parameters.AddWithValue("@nmNome", fornecedor.nmNome.ToUpper());
                SQL.Parameters.AddWithValue("@dsNome", fornecedor.dsNome.ToUpper());
                SQL.Parameters.AddWithValue("@nrContato", fornecedor.nrContato);
                SQL.Parameters.AddWithValue("@nrTelefone", fornecedor.nrTelefone);
                SQL.Parameters.AddWithValue("@nrCelular", fornecedor.nrCelular);
                SQL.Parameters.AddWithValue("@nrCEP", fornecedor.nrCEP);
                SQL.Parameters.AddWithValue("@dsLougradouro", fornecedor.dsLougradouro.ToUpper());
                SQL.Parameters.AddWithValue("@nrResidencial", fornecedor.nrResidencial);
                SQL.Parameters.AddWithValue("@dsBairro", fornecedor.dsBairro.ToUpper());
                SQL.Parameters.AddWithValue("@dsComplemento", fornecedor.dsComplemento.ToUpper());
                SQL.Parameters.AddWithValue("@dsEmail", fornecedor.dsEmail.ToUpper());
                SQL.Parameters.AddWithValue("@nrCPFCNPJ", fornecedor.nrCPFCNPJ);
                SQL.Parameters.AddWithValue("@nrRGIE", fornecedor.nrRGIE);
                SQL.Parameters.AddWithValue("@dsSite", fornecedor.dsSite);
                SQL.Parameters.AddWithValue("@idCidade", fornecedor.cidade.IdCidade);
                //SQL.Parameters.AddWithValue("@idCondPagamento", fornecedor.condPagamento.Id);                               
                SQL.Parameters.AddWithValue("@dtCadastro", fornecedor.dtCadastro = DateTime.Now);

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
                throw new Exception("Erro ao Adicionar Novo Fornecedor: " + e.Message);
            }
            finally
            {
                Close();
            }
        }

        public bool UpdateFornecedor(Fornecedor fornecedor)
        {
            try
            {
                Open();
                string updateCliente = @"UPDATE FORNECEDOR SET nmNome = @nmNome, dsNome = @dsNome, nrContato = @nrContato, nrTelefone = @nrTelefone, nrCelular = @nrCelular, nrCEP = @nrCEP
                                                                ,dsLougradouro = @dsLougradouro, nrResidencial = @nrResidencial, dsBairro = @dsBairro, dsComplemento = @dsComplemento, 
                                                                dsEmail = @dsEmail, nrCPFCNPJ = @nrCPFCNPJ, nrRGIE = @nrRGIE, dsSite = @dsSite, idCidade = @idCidade, 
                                                                idCondPagamento = @idCondPagamento,  dtUltAlteracao = @dtUltAlteracao  
                                                          WHERE id = @id";
                SqlCommand sql = new SqlCommand(updateCliente, sqlconnection);
                sql.CommandType = CommandType.Text;

                SQL.Parameters.AddWithValue("@nmNome", fornecedor.nmNome.ToUpper());
                SQL.Parameters.AddWithValue("@dsNome", fornecedor.dsNome.ToUpper());
                SQL.Parameters.AddWithValue("@nrContato", fornecedor.nrContato);
                SQL.Parameters.AddWithValue("@nrTelefone", fornecedor.nrTelefone);
                SQL.Parameters.AddWithValue("@nrCelular", fornecedor.nrCelular);
                SQL.Parameters.AddWithValue("@nrCEP", fornecedor.nrCEP);
                SQL.Parameters.AddWithValue("@dsLougradouro", fornecedor.dsLougradouro.ToUpper());
                SQL.Parameters.AddWithValue("@nrResidencial", fornecedor.nrResidencial);
                SQL.Parameters.AddWithValue("@dsBairro", fornecedor.dsBairro.ToUpper());
                SQL.Parameters.AddWithValue("@dsComplemento", fornecedor.dsComplemento.ToUpper());
                SQL.Parameters.AddWithValue("@dsEmail", fornecedor.dsEmail.ToUpper());
                SQL.Parameters.AddWithValue("@nrCPFCNPJ", fornecedor.nrCPFCNPJ);
                SQL.Parameters.AddWithValue("@nrRGIE", fornecedor.nrRGIE);
                SQL.Parameters.AddWithValue("@dsSite", fornecedor.dsSite);
                SQL.Parameters.AddWithValue("@idCidade", fornecedor.cidade.IdCidade);
                //SQL.Parameters.AddWithValue("@idCondPagamento", fornecedor.condPagamento.Id);  

                sql.Parameters.AddWithValue("@dtUltAlteracao", fornecedor.dtUltAlteracao = DateTime.Now);

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
                throw new Exception("Erro ao Atualizar Fornecedor: " + e.Message);
            }
            finally
            {
                Close();
            }
        }

        public bool DeleteFornecedor(int Id)
        {
            try
            {
                Open();
                string deleteCliente = "DELETE FROM Fornecedor WHERE Id = @Id";
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
                throw new Exception("Erro ao excluir Fornecedor: " + e.Message);
            }
            finally
            {
                Close();
            }
        }
        #endregion

        public IEnumerable<Fornecedor> SelecionarFornecedor()
        {
            try
            {
                Open();
                SQL = new SqlCommand(@"SELECT* FROM Fornecedor", sqlconnection);
                SQL.CommandType = CommandType.Text;
                Dr = SQL.ExecuteReader();
                // Criando uma lista vazia
                var lista = new List<Fornecedor>();
                while (Dr.Read())
                {
                    var fornecedor = new Fornecedor()
                    {
                        Id = Convert.ToInt32(Dr["Id"]),
                        nmNome = Convert.ToString(Dr["nmCliente"]),
                        dsNome = Convert.ToString(Dr["nmApelido"]),
                        nrContato = Convert.ToString(Dr["flSexo"]),
                        nrTelefone = Convert.ToString(Dr["nrTelefone"]),
                        nrCelular = Convert.ToString(Dr["nrCelular"]),
                        dsLougradouro = Convert.ToString(Dr["dsLougradouro"]),
                        nrResidencial = Convert.ToString(Dr["nrResidencial"]),
                        dsBairro = Convert.ToString(Dr["dsBairro"]),
                        dsComplemento = Convert.ToString(Dr["dsComplemento"]),
                        dsEmail = Convert.ToString(Dr["dsEmail"]),
                        nrCPFCNPJ = Convert.ToString(Dr["nrCPF"]),
                        nrRGIE = Convert.ToString(Dr["nrRG"]),
                        //idCidade = Convert.ToInt32(Dr[""]),
                        // idCondPagamento = Convert.ToInt32(Dr[""]),
                     
                        dtCadastro = Convert.ToDateTime(Dr["dtCadastro"]),

                    };
                    lista.Add(fornecedor);
                }
                return lista;
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao selecionar o Fornecedor: " + e.Message);
            }
            finally
            {
                Close();
            }
        }

        public FornecedorVM GetFornecedor(int? Id)
        {
            try
            {
                Open();
                var fornecedorVM = new FornecedorVM();
                string selectEditFornecedor = @"SELECT* FROM FORNECEDOR WHERE id =" + Id;
                SQL = new SqlCommand(selectEditFornecedor, sqlconnection);


                Dr = SQL.ExecuteReader();
                while (Dr.Read())
                {
                    fornecedorVM.Id = Convert.ToInt32(Dr["id"]);
                    fornecedorVM.nmNome = Dr["nmNome"].ToString();
                    fornecedorVM.dsNome = Dr["dsNome"].ToString();
                    fornecedorVM.nrContato = Dr["nrContato"].ToString();
                    fornecedorVM.nrTelefone = Dr["nrTelefone"].ToString();
                    fornecedorVM.nrCelular = Dr["nrCelular"].ToString();
                    fornecedorVM.nrCEP = Dr["nrCEP"].ToString();
                    fornecedorVM.dsBairro = Dr["dsBairro"].ToString();
                    fornecedorVM.dsComplemento = Dr["dsComplemento"].ToString();
                    fornecedorVM.dsLougradouro = Dr["dsLougradouro"].ToString();
                    fornecedorVM.nrResidencial = Dr["nrResidencial"].ToString();
                    fornecedorVM.cidade.IdCidade = Convert.ToInt32(Dr["id"]);                  
                    fornecedorVM.dsEmail = Dr["dsEmail"].ToString();
                    fornecedorVM.nrCPFCNPJ = Dr["nrCPF"].ToString();
                    fornecedorVM.nrRGIE = Dr["nrRG"].ToString();                    
                    fornecedorVM.dtCadastro = Dr["dtCadastro"] == DBNull.Value ? DateTime.Now : Convert.ToDateTime(Dr["dtCadastro"]);
                    fornecedorVM.dtUltAlteracao = Dr["dtUltAlteracao"] == DBNull.Value ? DateTime.Now : Convert.ToDateTime(Dr["dtUltAlteracao"]);
                }
                return fornecedorVM;
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao selecionar o Fornecedor: " + e.Message);
            }
            finally
            {
                Close();
            }
        }

    }
}