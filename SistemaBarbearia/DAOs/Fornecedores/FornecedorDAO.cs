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
                Open();
                string insertFornecedor = @"INSERT INTO FORNECEDOR  (  nmNome, dsNome, nrContato, nrTelefone, nrCelular, nrCEP, dsLogradouro
                                                                      ,nrResidencial, dsBairro, dsComplemento, dsEmail, nrCPFCNPJ, nrRGIE, dsSite
                                                                      ,idCidade, IdCondPag, dtCadastro,  dtNasc, flTipo, flSexo )
                                                           VALUES(  @nmNome, @dsNome, @nrContato, @nrTelefone, @nrCelular, @nrCEP, @dsLogradouro
                                                                    ,@nrResidencial, @dsBairro, @dsComplemento, @dsEmail, @nrCPFCNPJ, @nrRGIE, @dsSite
                                                                    ,@idCidade, @IdCondPag, @dtCadastro, @dtNasc, @flTipo, @flSexo )";
                SqlCommand sql = new SqlCommand(insertFornecedor, sqlconnection);
                sql.CommandType = CommandType.Text;

                sql.Parameters.AddWithValue("@nmNome", fornecedor.nmNome ?? (object)DBNull.Value);
                sql.Parameters.AddWithValue("@dsNome", fornecedor.dsNome ?? (object)DBNull.Value);                
                sql.Parameters.AddWithValue("@nrContato", fornecedor.nrContato ?? (object)DBNull.Value);
                sql.Parameters.AddWithValue("@nrTelefone", fornecedor.nrTelefone ?? (object)DBNull.Value);
                sql.Parameters.AddWithValue("@nrCelular", fornecedor.nrCelular ?? (object)DBNull.Value);
                sql.Parameters.AddWithValue("@nrCEP", fornecedor.nrCEP ?? (object)DBNull.Value);
                sql.Parameters.AddWithValue("@dsLogradouro", fornecedor.dsLogradouro ?? (object)DBNull.Value);
                sql.Parameters.AddWithValue("@nrResidencial", fornecedor.nrResidencial ?? (object)DBNull.Value);
                sql.Parameters.AddWithValue("@dsBairro", fornecedor.dsBairro ?? (object)DBNull.Value);
                sql.Parameters.AddWithValue("@dsComplemento", fornecedor.dsComplemento ?? (object)DBNull.Value);
                sql.Parameters.AddWithValue("@dsEmail", fornecedor.dsEmail ?? (object)DBNull.Value);
                sql.Parameters.AddWithValue("@nrCPFCNPJ", fornecedor.nrCPFCNPJ ?? (object)DBNull.Value);
                sql.Parameters.AddWithValue("@nrRGIE", fornecedor.nrRGIE ?? (object)DBNull.Value);                
                sql.Parameters.AddWithValue("@dsSite", fornecedor.dsSite ?? (object)DBNull.Value);
                sql.Parameters.AddWithValue("@idCidade", ((object)fornecedor.idCidade) ?? DBNull.Value);
                sql.Parameters.AddWithValue("@IdCondPag", ((object)fornecedor.idCondPagamento) ?? DBNull.Value);
                sql.Parameters.AddWithValue("@dtNasc", ((object)fornecedor.dtNasc) ?? DBNull.Value);
                sql.Parameters.AddWithValue("@flTipo", fornecedor.flTipo ?? (object)DBNull.Value);
                sql.Parameters.AddWithValue("@flSexo", fornecedor.flSexo ?? (object)DBNull.Value);
                sql.Parameters.AddWithValue("@dtCadastro", fornecedor.dtCadastro = DateTime.Now);



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
                string updateCliente = @"UPDATE FORNECEDOR SET nmNome = @nmNome, dsNome = @dsNome, nrContato = @nrContato, nrTelefone = @nrTelefone, nrCelular = @nrCelular, 
                                                               nrCEP = @nrCEP, dsLogradouro = @dsLogradouro, nrResidencial = @nrResidencial, dsBairro = @dsBairro, dsComplemento = @dsComplemento,
                                                               dsEmail = @dsEmail, nrCPFCNPJ = @nrCPFCNPJ, nrRGIE = @nrRGIE, dsSite = @dsSite, idCidade = @idCidade, 
                                                               idCondPagamento = @idCondPagamento,  dtNasc = @dtNasc, flTipo = @flTipo, dtUltAlteracao = @dtUltAlteracao  

                                                                WHERE idFornecedor = @idFornecedor";
                SqlCommand sql = new SqlCommand(updateCliente, sqlconnection);
                sql.CommandType = CommandType.Text;

                sql.Parameters.AddWithValue("@nmNome", fornecedor.nmNome.ToUpper());
                sql.Parameters.AddWithValue("@dsNome", fornecedor.dsNome.ToUpper());
                sql.Parameters.AddWithValue("@flTipo", fornecedor.flTipo);
                sql.Parameters.AddWithValue("@nrContato", fornecedor.nrContato);
                sql.Parameters.AddWithValue("@nrTelefone", fornecedor.nrTelefone);
                sql.Parameters.AddWithValue("@nrCelular", fornecedor.nrCelular);
                sql.Parameters.AddWithValue("@nrCEP", fornecedor.nrCEP);
                sql.Parameters.AddWithValue("@dsLogradouro", fornecedor.dsLogradouro.ToUpper());
                sql.Parameters.AddWithValue("@nrResidencial", fornecedor.nrResidencial);
                sql.Parameters.AddWithValue("@dsBairro", fornecedor.dsBairro.ToUpper());
                sql.Parameters.AddWithValue("@dsComplemento", fornecedor.dsComplemento.ToUpper());
                sql.Parameters.AddWithValue("@dsEmail", fornecedor.dsEmail.ToUpper());
                sql.Parameters.AddWithValue("@nrCPFCNPJ", fornecedor.nrCPFCNPJ);
                sql.Parameters.AddWithValue("@nrRGIE", fornecedor.nrRGIE);
                sql.Parameters.AddWithValue("@dtNasc", fornecedor.dtNasc);
                sql.Parameters.AddWithValue("@dsSite", fornecedor.dsSite);
                sql.Parameters.AddWithValue("@idCidade", fornecedor.cidade.Id);
                sql.Parameters.AddWithValue("@idCondPagamento", fornecedor.CondPagamento.Id);
                
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
                string deleteCliente = "DELETE FROM Fornecedor WHERE idFornecedor = @idFornecedor";
                SqlCommand sql = new SqlCommand(deleteCliente, sqlconnection);
                sql.CommandType = CommandType.Text;

                sql.Parameters.AddWithValue("@idFornecedor", Id);

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
                        IdFornecedor = Convert.ToInt32(Dr["IdFornecedor"]),
                        nmNome = Convert.ToString(Dr["nmNome"]),
                        dsNome = Convert.ToString(Dr["dsNome"]),
                        nrContato = Convert.ToString(Dr["nrContato"]),
                        nrTelefone = Convert.ToString(Dr["nrTelefone"]),
                        nrCelular = Convert.ToString(Dr["nrCelular"]),
                        dsLogradouro = Convert.ToString(Dr["dsLogradouro"]),
                        nrResidencial = Convert.ToString(Dr["nrResidencial"]),
                        dsBairro = Convert.ToString(Dr["dsBairro"]),
                        dsComplemento = Convert.ToString(Dr["dsComplemento"]),
                        dsEmail = Convert.ToString(Dr["dsEmail"]),
                        nrCPFCNPJ = Convert.ToString(Dr["nrCPFCNPJ"]),
                        nrRGIE = Convert.ToString(Dr["nrRGIE"]),
                        idCidade = Convert.ToInt32(Dr["idCidade"]),
                        idCondPagamento = Convert.ToInt32(Dr["idCondPagamento"]),
                     
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

        public Fornecedor GetFornecedor(int? Id)
        {
            try
            {
                Open();
                var fornecedor = new Fornecedor();
                string selectEditFornecedor = @"SELECT* FROM FORNECEDOR WHERE IdFornecedor =" + Id;
                SQL = new SqlCommand(selectEditFornecedor, sqlconnection);

                //nmNome, dsNome, nrContato, nrTelefone, nrCelular, nrCEP, dsLogradouro, 
                //nrResidencial, dsBairro, dsComplemento, dsEmail,
                //nrCPFCNPJ, nrRGIE, dsSite, idCidade, idCondPagamento, dtCadastro, dtUltAlteracao, dtNasc, flTipo, flSexo


                Dr = SQL.ExecuteReader();
                while (Dr.Read())
                {
                    fornecedor.IdFornecedor = Convert.ToInt32(Dr["IdFornecedor"]);
                    fornecedor.nmNome = Dr["nmNome"].ToString();
                    fornecedor.dsNome = Dr["dsNome"].ToString();
                    fornecedor.nrContato = Dr["nrContato"].ToString();
                    fornecedor.nrTelefone = Dr["nrTelefone"].ToString();
                    fornecedor.nrCelular = Dr["nrCelular"].ToString();
                    fornecedor.nrCEP = Dr["nrCEP"].ToString();
                    fornecedor.dsLogradouro = Dr["dsLogradouro"].ToString();
                    fornecedor.nrResidencial = Dr["nrResidencial"].ToString();
                    fornecedor.dsBairro = Dr["dsBairro"].ToString();
                    fornecedor.dsComplemento = Dr["dsComplemento"].ToString();
                    fornecedor.dsEmail = Dr["dsEmail"].ToString();
                    fornecedor.nrCPFCNPJ = Dr["nrCPFCNPJ"].ToString();
                    fornecedor.nrRGIE = Dr["nrRGIE"].ToString();
                    fornecedor.dsSite = Dr["dsSite"].ToString();
                    fornecedor.idCidade = Convert.ToInt32(Dr["IdCidade"]);
                    fornecedor.idCondPagamento = Convert.ToInt32(Dr["IdCondPagamento"]);
                    fornecedor.dtNasc = Dr["dtNasc"] == DBNull.Value ? DateTime.Now : Convert.ToDateTime(Dr["dtNasc"]);
                    fornecedor.flTipo = Dr["flTipo"].ToString();
                    fornecedor.flSexo = Dr["flSexo"].ToString();
                    fornecedor.dtCadastro = Dr["dtCadastro"] == DBNull.Value ? DateTime.Now : Convert.ToDateTime(Dr["dtCadastro"]);
                    fornecedor.dtUltAlteracao = Dr["dtUltAlteracao"] == DBNull.Value ? DateTime.Now : Convert.ToDateTime(Dr["dtUltAlteracao"]);
                }
                return fornecedor;
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