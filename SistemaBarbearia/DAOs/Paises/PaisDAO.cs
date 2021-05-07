using SistemaBarbearia.DataBase;
using SistemaBarbearia.Models.Paises;
using SistemaBarbearia.ViewModels.Paises;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace SistemaBarbearia.DAOs.Paises
{
    public class PaisDAO : DataAccess
    {

        public bool AdicionarPais(Pais pais)
        {

            try
            {
                Open();
                string insertPais = @"INSERT INTO PAIS (nmPais, dsSigla, dtCadastro) VALUES(@nmPais, @dsSigla, @dtCadastro)";
                SQL = new SqlCommand(insertPais, sqlconnection);
                SQL.CommandType = CommandType.Text;

                SQL.Parameters.AddWithValue("@nmPais", pais.nmPais.ToUpper());
                SQL.Parameters.AddWithValue("@dsSigla", pais.dsSigla.ToUpper());
                SQL.Parameters.AddWithValue("@dtCadastro", pais.dtCadastro = DateTime.Now);

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
                throw new Exception("Erro ao Adicionar Novo Pais: " + e.Message);
            }
            finally
            {
                Close();
            }
        }

        public IEnumerable<Pais> SelecionarPais()
        {
            try
            {
                Open();
                SQL = new SqlCommand(@"SELECT * FROM Pais", sqlconnection);
                SQL.CommandType = CommandType.Text;
                Dr = SQL.ExecuteReader();
                // Criando uma lista vazia
                var lista = new List<Models.Paises.Pais>();
                while (Dr.Read())
                {
                    var pais = new Models.Paises.Pais()
                    {
                        Id = Convert.ToInt32(Dr["Id"]),
                        nmPais = Convert.ToString(Dr["nmPais"]),
                        dsSigla = Convert.ToString(Dr["dsSigla"]),
                        dtCadastro = Convert.ToDateTime(Dr["dtCadastro"]),
                       // dtUltAlteracao = Convert.ToDateTime(Dr["dtUltAlteracao"]),
                    };
                    lista.Add(pais);
                }
                return lista;
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao selecionar o Pais: " + e.Message);
            }
            finally
            {
                Close();
            }
        }

        public PaisVM GetPais(int? Id)
        {
            try
            {
                Open();
                var paisVM = new PaisVM();
                string selectEditPais = @"SELECT* FROM Pais WHERE id =" + Id;
                SQL = new SqlCommand(selectEditPais, sqlconnection);


                Dr = SQL.ExecuteReader();
                while (Dr.Read())
                {
                    paisVM.Id = Convert.ToInt32(Dr["id"]);
                    paisVM.nmPais = Dr["nmPais"].ToString();
                    paisVM.dsSigla = Dr["dsSigla"].ToString();
                    paisVM.dtCadastro = Convert.ToDateTime(Dr["dtCadastro"]);
                    
                    paisVM.dtUltAlteracao = Convert.ToDateTime(Dr["dtUltAlteracao"]);
                   
                    
                }
                return paisVM;
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao selecionar o Pais: " + e.Message);
            }
            finally
            {
                Close();
            }
        }

        public PaisVM filtraPais(string nmpais)
        {
            try
            {
                Open();
                var paisVM = new PaisVM();
                string selectEditPais = @"SELECT* FROM Pais WHERE nmPais =" + nmpais;
                SQL = new SqlCommand(selectEditPais, sqlconnection);


                Dr = SQL.ExecuteReader();
                while (Dr.Read())
                {
                    paisVM.Id = Convert.ToInt32(Dr["id"]);
                    paisVM.nmPais = Dr["nmPais"].ToString();
                    paisVM.dsSigla = Dr["dsSigla"].ToString();
                    paisVM.dtCadastro = Convert.ToDateTime(Dr["dtCadastro"]);
                    paisVM.dtUltAlteracao = Convert.ToDateTime(Dr["dtUltAlteracao"]);
                }
                return paisVM;
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao selecionar o Pais: " + e.Message);
            }
            finally
            {
                Close();
            }
        }

        public bool UpdatePais(Pais pais)
        {
            try
            {
                Open();
                string updatePais = @"UPDATE Pais SET nmPais = @nmPais, dsSigla = @dsSigla, dtUltAlteracao = @dtUltAlteracao  WHERE id = @id";
                SqlCommand sql = new SqlCommand(updatePais, sqlconnection);
                sql.CommandType = CommandType.Text;

                sql.Parameters.AddWithValue("@id", pais.Id);
                sql.Parameters.AddWithValue("@nmPais", pais.nmPais.ToUpper());
                sql.Parameters.AddWithValue("@dsSigla", pais.dsSigla.ToUpper());
                sql.Parameters.AddWithValue("@dtCadastro", pais.dtCadastro);
                sql.Parameters.AddWithValue("@dtUltAlteracao", pais.dtUltAlteracao = DateTime.Now);


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
                throw new Exception("Erro ao Atualizar Pais: " + e.Message);
            }
            finally
            {
                Close();
            }
        }

        public bool ExcluirPais(int Id)
        {
            try
            {
                Open();
                string deletePais = "DELETE FROM Pais WHERE Id = @Id";
                SqlCommand sql = new SqlCommand(deletePais, sqlconnection);
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