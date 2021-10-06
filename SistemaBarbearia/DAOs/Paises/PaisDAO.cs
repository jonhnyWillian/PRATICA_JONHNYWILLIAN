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
        #region INSERT, UPDATE, DELETE 
        public bool InsertPais(Pais pais)
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

        public bool UpdatePais(Pais pais)
        {
            try
            {
                Open();
                string updatePais = @"UPDATE Pais SET nmPais = @nmPais, dsSigla = @dsSigla, dtUltAlteracao = @dtUltAlteracao  WHERE IdPais = "+ pais.IdPais;
                SqlCommand sql = new SqlCommand(updatePais, sqlconnection);
                sql.CommandType = CommandType.Text;

                sql.Parameters.AddWithValue("@IdPais", pais.IdPais);
                sql.Parameters.AddWithValue("@nmPais", pais.nmPais.ToUpper());
                sql.Parameters.AddWithValue("@dsSigla", pais.dsSigla.ToUpper());

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

        public bool DeletePais(int Id)
        {
            try
            {
                Open();
                string deletePais = "DELETE FROM Pais WHERE IdPais = @IdPais";
                SqlCommand sql = new SqlCommand(deletePais, sqlconnection);
                sql.CommandType = CommandType.Text;

                sql.Parameters.AddWithValue("@IdPais", Id);

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

        #endregion

        public IEnumerable<Pais> SelecionarPais()
        {
            try
            {
                Open();
                SQL = new SqlCommand(@"SELECT * FROM Pais", sqlconnection);
                SQL.CommandType = CommandType.Text;
                Dr = SQL.ExecuteReader();
                // Criando uma lista vazia
                var lista = new List<Pais>();
                while (Dr.Read())
                {
                    var pais = new Models.Paises.Pais()
                    {
                        IdPais = Convert.ToInt32(Dr["IdPais"]),
                        nmPais = Convert.ToString(Dr["nmPais"]),
                        dsSigla = Convert.ToString(Dr["dsSigla"]),
                        dtCadastro = Dr["dtCadastro"] == DBNull.Value ? DateTime.Now : Convert.ToDateTime(Dr["dtCadastro"]),
                        dtUltAlteracao = Dr["dtUltAlteracao"] == DBNull.Value ? DateTime.Now : Convert.ToDateTime(Dr["dtUltAlteracao"]),
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
                string selectEditPais = @"SELECT* FROM Pais WHERE IdPais =" + Id;
                SQL = new SqlCommand(selectEditPais, sqlconnection);


                Dr = SQL.ExecuteReader();
                while (Dr.Read())
                {
                    paisVM.IdPais = Convert.ToInt32(Dr["IdPais"]);
                    paisVM.nmPais = Dr["nmPais"].ToString();
                    paisVM.dsSigla = Dr["dsSigla"].ToString();
                    paisVM.dtCadastro = Dr["dtCadastro"] == DBNull.Value ? DateTime.Now : Convert.ToDateTime(Dr["dtCadastro"]);

                    paisVM.dtUltAlteracao = Dr["dtUltAlteracao"] == DBNull.Value ? DateTime.Now : Convert.ToDateTime(Dr["dtUltAlteracao"]);


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

        protected string BuscarPais(int? IdPais, string text)
        {
            var sqlSelectPais = string.Empty;
            var where = string.Empty;

            if (IdPais != null)
            {
                where = "WHERE IdPais = " + IdPais;
            }
            if (!string.IsNullOrEmpty(text))
            {
                var query = text.Split(' ');
                foreach (var item in query)
                {
                    where += "OR nmPais LIKE '%'" + item + "'%'";
                }
                where = "WHERE" + where.Remove(0, 3);
            }
            sqlSelectPais = @"SELECT * FROM PAIS " + where;
            return sqlSelectPais;
        }

        public List<SelectPaisVM> SelectPais(int? Id, string text)
        {
            try
            {

                var sqlSelectPais = this.BuscarPais(Id, text);
                Open();
                SQL = new SqlCommand(sqlSelectPais, sqlconnection);
                Dr = SQL.ExecuteReader();
                var list = new List<SelectPaisVM>();

                while (Dr.Read())
                {
                    var pais = new SelectPaisVM
                    {
                        Id = Convert.ToInt32(Dr["IdPais"]),
                        text = Convert.ToString(Dr["nmPais"]),                       
                        dsSigla = Convert.ToString(Dr["dsSigla"])
                        //dtCadastro = Dr["dtCadastro"] == DBNull.Value ? DateTime.Now : Convert.ToDateTime(Dr["dtCadastro"]),
                        //dtUltAlteracao = Dr["dtUltAlteracao"] == DBNull.Value ? DateTime.Now : Convert.ToDateTime(Dr["dtUltAlteracao"]),
                    };

                    list.Add(pais);
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