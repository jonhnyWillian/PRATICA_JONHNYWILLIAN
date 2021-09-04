using SistemaBarbearia.DataBase;
using SistemaBarbearia.Models.Cidades;
using SistemaBarbearia.ViewModels.Cidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace SistemaBarbearia.DAOs.Cidades
{
    public class CidadeDAO : DataAccess
    {
        #region INSERT UPDATE DELETE
        public bool InsertCidade(Cidade cidade)
        {

            try
            {
                Open();
                string insertCidade = @"INSERT INTO CIDADE (nmCidade, ddd, IdEstado, dtCadastro) VALUES(@nmCidade, @ddd, @IdEstado, @dtCadastro)";
                SQL = new SqlCommand(insertCidade, sqlconnection);
                SQL.CommandType = CommandType.Text;

                SQL.Parameters.AddWithValue("@nmCidade", cidade.nmCidade.ToUpper());
                SQL.Parameters.AddWithValue("@ddd", cidade.DDD.ToUpper());
                SQL.Parameters.AddWithValue("@IdEstado", cidade.estado.IdEstado);
                SQL.Parameters.AddWithValue("@dtCadastro", cidade.dtCadastro = DateTime.Now);

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
                throw new Exception("Erro ao Adicionar Novo Cidade: " + e.Message);
            }
            finally
            {
                Close();
            }
        }

        public bool UpdateCidade(Cidade cidade)
        {
            try
            {
                Open();
                string updateCidade = @"UPDATE CIDADE SET nmCidade = @nmCidade, ddd = @ddd, IdEstado = @IdEstado, dtUltAlteracao = @dtUltAlteracao  WHERE IdCidade =" + cidade.IdCidade;
                SqlCommand sql = new SqlCommand(updateCidade, sqlconnection);
                sql.CommandType = CommandType.Text;

                sql.Parameters.AddWithValue("@IdCidade", cidade.IdCidade);
                sql.Parameters.AddWithValue("@nmCidade", cidade.nmCidade.ToUpper());
                sql.Parameters.AddWithValue("@ddd", cidade.DDD.ToUpper());
                sql.Parameters.AddWithValue("@IdEstado", cidade.estado.IdEstado);                
                sql.Parameters.AddWithValue("@dtUltAlteracao", cidade.dtUltAlteracao = DateTime.Now);


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
                throw new Exception("Erro ao Atualizar Cidade: " + e.Message);
            }
            finally
            {
                Close();
            }
        }

        public bool DeleteCidade(int Id)
        {
            try
            {
                Open();
                string deletePais = "DELETE FROM CIDADE WHERE IdCidade = @IdCidade";
                SqlCommand sql = new SqlCommand(deletePais, sqlconnection);
                sql.CommandType = CommandType.Text;

                sql.Parameters.AddWithValue("@IdCidade", Id);

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
                throw new Exception("Erro ao excluir Cidade: " + e.Message);
            }
            finally
            {
                Close();
            }
        }
        #endregion

        public IEnumerable<Cidade> SelecionarCidade()
        {
            try
            {
                Open();
                SQL = new SqlCommand(@"SELECT * FROM CIDADE", sqlconnection);
                SQL.CommandType = CommandType.Text;
                Dr = SQL.ExecuteReader();
                // Criando uma lista vazia
                var lista = new List<Cidade>();
                while (Dr.Read())
                {
                    var cidade = new Cidade()
                    {
                        IdCidade = Convert.ToInt32(Dr["IdCidade"]),
                        nmCidade = Convert.ToString(Dr["nmCidade"]),
                        DDD = Convert.ToString(Dr["ddd"]),
                        IdEstado = Convert.ToInt32(Dr["IdEstado"]),
                        dtCadastro = Dr["dtCadastro"] == DBNull.Value ? DateTime.Now : Convert.ToDateTime(Dr["dtCadastro"]),
                        dtUltAlteracao = Dr["dtUltAlteracao"] == DBNull.Value ? DateTime.Now : Convert.ToDateTime(Dr["dtUltAlteracao"]),
                    };
                    lista.Add(cidade);
                }
                return lista;
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao selecionar o Cidade: " + e.Message);
            }
            finally
            {
                Close();
            }
        }

        public CidadeVM GetCidade(int? Id)
        {
            try
            {
                Open();
                var cidadeVM = new CidadeVM();
                string selectEditCidade = @"SELECT* FROM CIDADE WHERE IdCidade =" + Id;
                SQL = new SqlCommand(selectEditCidade, sqlconnection);


                Dr = SQL.ExecuteReader();
                while (Dr.Read())
                {
                    cidadeVM.IdCidade = Convert.ToInt32(Dr["IdCidade"]);
                    cidadeVM.nmCidade = Dr["nmCidade"].ToString();
                    cidadeVM.DDD = Dr["ddd"].ToString();
                    cidadeVM.IdEstado = Convert.ToInt32(Dr["IdEstado"]);                     

                    cidadeVM.dtCadastro = Dr["dtCadastro"] == DBNull.Value ? DateTime.Now : Convert.ToDateTime(Dr["dtCadastro"]);
                    cidadeVM.dtUltAlteracao = Dr["dtUltAlteracao"] == DBNull.Value ? DateTime.Now : Convert.ToDateTime(Dr["dtUltAlteracao"]);


                }
                return cidadeVM;
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao selecionar o Cidade: " + e.Message);
            }
            finally
            {
                Close();
            }
        }

        protected string BuscarCidade(int? id, string text)
        {
            var sqlSelectPais = string.Empty;
            var where = string.Empty;

            if (id != null)
            {
                where = "WHERE IdCidade = " + id;
            }
            if (!string.IsNullOrEmpty(text))
            {
                var query = text.Split(' ');
                foreach (var item in query)
                {
                    where += "OR nmCidade LIKE '%'" + item + "'%'";
                }
                where = "WHERE" + where.Remove(0, 3);
            }
            sqlSelectPais = @"SELECT * FROM Cidade " + where;
            return sqlSelectPais;
        }

        public List<SelectCidadeVM> SelectCidade(int? id, string nmCidade)
        {
            try
            {

                var sqlSelectCidade = this.BuscarCidade(id, nmCidade);
                Open();
                SQL = new SqlCommand(sqlSelectCidade, sqlconnection);
                Dr = SQL.ExecuteReader();
                var list = new List<SelectCidadeVM>();

                while (Dr.Read())
                {
                    var pais = new SelectCidadeVM
                    {
                        Id = Convert.ToInt32(Dr["IdCidade"]),
                        Text = Convert.ToString(Dr["nmCidade"]),
                        DDD = Convert.ToString(Dr["DDD"]),
                        dtCadastro = Dr["dtCadastro"] == DBNull.Value ? DateTime.Now : Convert.ToDateTime(Dr["dtCadastro"]),
                        dtUltAlteracao = Dr["dtUltAlteracao"] == DBNull.Value ? DateTime.Now : Convert.ToDateTime(Dr["dtUltAlteracao"]),
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