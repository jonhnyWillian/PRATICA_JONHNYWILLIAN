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
                string insertEstado = @"INSERT INTO ";
                SQL = new SqlCommand(insertEstado, sqlconnection);
                SQL.CommandType = CommandType.Text;

                //SQL.Parameters.AddWithValue("@nmEstado", estado.nmEstado.ToUpper());
                //SQL.Parameters.AddWithValue("@dsUF", estado.dsUF.ToUpper());
                //SQL.Parameters.AddWithValue("idPais", estado.Pais.Id);
                //SQL.Parameters.AddWithValue("@dtCadastro", estado.dtCadastro = DateTime.Now);

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
                string updateEstado = @"UPDATE ESTADO SET nmEstado = @nmEstado, dsUF = @dsSigla, idPais = @idPais ,dtUltAlteracao = @dtUltAlteracao  WHERE id = @id";
                SqlCommand sql = new SqlCommand(updateEstado, sqlconnection);
                sql.CommandType = CommandType.Text;

                //sql.Parameters.AddWithValue("@id", estado.Id);
                //sql.Parameters.AddWithValue("@nmEstado", estado.nmEstado.ToUpper());
                //sql.Parameters.AddWithValue("@dsUF", estado.dsUF.ToUpper());
                //sql.Parameters.AddWithValue("@idPais", estado.idPais);
                //sql.Parameters.AddWithValue("@dtUltAlteracao", estado.dtUltAlteracao = DateTime.Now);

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
                throw new Exception("Erro ao Atualizar Esatdo: " + e.Message);
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
                string deleteEstado = "DELETE FROM Estado WHERE Id = @Id";
                SqlCommand sql = new SqlCommand(deleteEstado, sqlconnection);
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
                throw new Exception("Erro ao excluir Esatdo: " + e.Message);
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
                SQL = new SqlCommand(@"", sqlconnection);
                SQL.CommandType = CommandType.Text;
                Dr = SQL.ExecuteReader();
                // Criando uma lista vazia
                var lista = new List<Cliente>();
                while (Dr.Read())
                {
                    var cliente = new Cliente()
                    {
                        


                       
                    };
                    lista.Add(cliente);
                }
                return lista;
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao selecionar o Estado: " + e.Message);
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
                string selectEditCliente = @"SELECT* FROM ESTADO WHERE id =" + Id;
                SQL = new SqlCommand(selectEditCliente, sqlconnection);


                Dr = SQL.ExecuteReader();
                while (Dr.Read())
                {
                    //estadoVM.Id = Convert.ToInt32(Dr["id"]);
                    //estadoVM.nmEstado = Dr["nmEstado"].ToString();
                    //estadoVM.dsUF = Dr["dsUFa"].ToString();
                    ////estadoVM.Pais = Convert.ToInt32(Dr["idPais"]);
                    //estadoVM.dtCadastro = Convert.ToDateTime(Dr["dtCadastro"]);
                    //estadoVM.dtUltAlteracao = Convert.ToDateTime(Dr["dtUltAlteracao"]);
                }
                return clienteVM;
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao selecionar o Estado: " + e.Message);
            }
            finally
            {
                Close();
            }
        }

       
    }
}