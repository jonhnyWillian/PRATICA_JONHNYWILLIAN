using SistemaBarbearia.DataBase;
using SistemaBarbearia.Models.Estados;
using SistemaBarbearia.ViewModels.Estados;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;


namespace SistemaBarbearia.DAOs.Estados
{
    public class EstadoDAO : DataAccess
    {

        #region INSERT UPDATE DELETE
        public bool InsertEstado(Estado estado)
        {

            try
            {
                Open();
                string insertEstado = @"INSERT INTO ESTADO (nmEstado, dsUF, dtCadastro, IdPais) VALUES(@nmEstado, @dsUF, @dtCadastro, @IdPais)";
                SQL = new SqlCommand(insertEstado, sqlconnection);
                SQL.CommandType = CommandType.Text;

                SQL.Parameters.AddWithValue("@nmEstado", estado.nmEstado.ToUpper());
                SQL.Parameters.AddWithValue("@dsUF", estado.dsUF.ToUpper());
                SQL.Parameters.AddWithValue("@IdPais", estado.pais.IdPais);
                SQL.Parameters.AddWithValue("@dtCadastro", estado.dtCadastro = DateTime.Now);

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
                throw new Exception("Erro ao Adicionar Novo Estado: " + e.Message);
            }
            finally
            {
                Close();
            }
        }

        public bool UpdateEstado(Estado estado)
        {
            try
            {
                Open();
                string updateEstado = @"UPDATE ESTADO SET nmEstado = @nmEstado, dsUF = @dsUF, IdPais = @IdPais ,dtUltAlteracao = @dtUltAlteracao  WHERE IdEstado =" + estado.IdEstado;
                SqlCommand sql = new SqlCommand(updateEstado, sqlconnection);
                sql.CommandType = CommandType.Text;

                sql.Parameters.AddWithValue("@IdEstado", estado.IdEstado);
                sql.Parameters.AddWithValue("@nmEstado", estado.nmEstado.ToUpper());
                sql.Parameters.AddWithValue("@dsUF", estado.dsUF.ToUpper());
                sql.Parameters.AddWithValue("@IdPais", estado.pais.IdPais);
                sql.Parameters.AddWithValue("@dtUltAlteracao", estado.dtUltAlteracao = DateTime.Now);

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
                throw new Exception("Erro ao Atualizar Estado: " + e.Message);
            }
            finally
            {
                Close();
            }
        }

        public bool DeleteEstado(int Id)
        {
            try
            {
                Open();
                string deleteEstado = "DELETE FROM Estado WHERE IdEstado = @IdEstado";
                SqlCommand sql = new SqlCommand(deleteEstado, sqlconnection);
                sql.CommandType = CommandType.Text;

                sql.Parameters.AddWithValue("@IdEstado", Id);

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
                throw new Exception("Erro ao excluir Estado: " + e.Message);
            }
            finally
            {
                Close();
            }
        }

        #endregion

        public IEnumerable<Estado> SelecionarEstado()
        {
            try
            {
                Open();
                SQL = new SqlCommand(@"SELECT * FROM ESTADO ", sqlconnection);
                SQL.CommandType = CommandType.Text;
                Dr = SQL.ExecuteReader();
                // Criando uma lista vazia
                var lista = new List<Estado>();
                while (Dr.Read())
                {
                    var estado = new Estado()
                    {
                        IdEstado = Convert.ToInt32(Dr["IdEstado"]),
                        nmEstado = Convert.ToString(Dr["nmEstado"]),
                        dsUF = Convert.ToString(Dr["dsUF"]),
                        IdPais = Convert.ToInt32(Dr["IdPais"]),
                       


                        dtCadastro = Dr["dtCadastro"] == DBNull.Value ? DateTime.Now : Convert.ToDateTime(Dr["dtCadastro"]),
                        dtUltAlteracao = Dr["dtUltAlteracao"] == DBNull.Value ? DateTime.Now : Convert.ToDateTime(Dr["dtUltAlteracao"]),
                    };
                    lista.Add(estado);
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

        public EstadoVM GetEstado(int? Id)
        {
            try
            {
                Open();
                var estadoVM = new EstadoVM();
                string selectEditEstado = @"SELECT* FROM ESTADO WHERE IdEstado =" + Id;
                SQL = new SqlCommand(selectEditEstado, sqlconnection);
                Dr = SQL.ExecuteReader();
                while (Dr.Read())
                {
                    estadoVM.IdEstado = Convert.ToInt32(Dr["IdEstado"]);
                    estadoVM.nmEstado = Dr["nmEstado"].ToString();
                    estadoVM.dsUF = Dr["dsUF"].ToString();
                   
                    estadoVM.dtCadastro = Dr["dtCadastro"] == DBNull.Value ? DateTime.Now : Convert.ToDateTime(Dr["dtCadastro"]);
                    estadoVM.dtUltAlteracao = Dr["dtUltAlteracao"] == DBNull.Value ? DateTime.Now : Convert.ToDateTime(Dr["dtUltAlteracao"]);

                    estadoVM.IdPais = Convert.ToInt32(Dr["IdPais"]);
                                           


                }
                return estadoVM;
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

        protected string BuscarPais(int? id, string text)
        {
            var sqlSelectPais = string.Empty;
            var where = string.Empty;

            if (id != null)
            {
                where = "WHERE IdEstado = " + id;
            }
            if (!string.IsNullOrEmpty(text))
            {
                var query = text.Split(' ');
                foreach (var item in query)
                {
                    where += "OR nmEstado LIKE '%'" + item + "'%'";
                }
                where = "WHERE" + where.Remove(0, 3);
            }
            sqlSelectPais = @"SELECT * FROM Estado " + where;
            return sqlSelectPais;
        }

        public List<SelectEstadoVM> SelectEstado(int? id, string nmEstado)
        {
            try
            {

                var sqlSelectEstado = this.BuscarPais(id, nmEstado);
                Open();
                SQL = new SqlCommand(sqlSelectEstado, sqlconnection);
                Dr = SQL.ExecuteReader();
                var list = new List<SelectEstadoVM>();

                while (Dr.Read())
                {
                    var estado = new SelectEstadoVM
                    {
                        IdEstado = Convert.ToInt32(Dr["IdEstado"]),
                        Text = Convert.ToString(Dr["nmEstado"]),
                        dsUF = Convert.ToString(Dr["dsUF"]),
                        IdPais = Convert.ToInt32(Dr["IdPais"]),
                      
                        dtCadastro = Dr["dtCadastro"] == DBNull.Value ? DateTime.Now : Convert.ToDateTime(Dr["dtCadastro"]),
                        dtUltAlteracao = Dr["dtUltAlteracao"] == DBNull.Value ? DateTime.Now : Convert.ToDateTime(Dr["dtUltAlteracao"]),
                    };

                    list.Add(estado);
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