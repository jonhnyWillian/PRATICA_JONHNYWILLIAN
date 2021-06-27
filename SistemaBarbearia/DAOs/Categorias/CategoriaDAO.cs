using SistemaBarbearia.DataBase;
using SistemaBarbearia.Models.Categorias;
using SistemaBarbearia.ViewModels.Categorias;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;


namespace SistemaBarbearia.DAOs.Categorias
{
    public class CategoriaDAO : DataAccess
    {
        #region INSERT UPDATE DELETE
        public bool InsertCategoria(Categoria categoria)
        {

            try
            {
                Open();
                string insertCategoria = @"INSERT INTO CATEGORIA (dsCategoria, dtCadastro) VALUES(@dsCategoria, @dtCadastro)";
                SQL = new SqlCommand(insertCategoria, sqlconnection);
                SQL.CommandType = CommandType.Text;

                SQL.Parameters.AddWithValue("@dsCategoria", categoria.dsCategoria.ToUpper());
                SQL.Parameters.AddWithValue("@dtCadastro", categoria.dtCadastro = DateTime.Now);

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
                throw new Exception("Erro ao Adicionar Novo Categoria: " + e.Message);
            }
            finally
            {
                Close();
            }
        }

        public bool UpdateCategoria(Categoria categoria)
        {
            try
            {
                Open();
                string updateCategoria = @"UPDATE CATEGORIA SET dsCategoria = @dsCategoria, dtUltAlteracao = @dtUltAlteracao  WHERE IdCategoria = @IdCategoria";
                SqlCommand sql = new SqlCommand(updateCategoria, sqlconnection);
                sql.CommandType = CommandType.Text;

                sql.Parameters.AddWithValue("@IdCategoria", categoria.IdCategoria);
                sql.Parameters.AddWithValue("@dsCategoria", categoria.dsCategoria.ToUpper());
                sql.Parameters.AddWithValue("@dtUltAlteracao", categoria.dtUltAlteracao = DateTime.Now);


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
                throw new Exception("Erro ao Atualizar Categoria: " + e.Message);
            }
            finally
            {
                Close();
            }
        }

        public bool DeleteCategoria(int Id)
        {
            try
            {
                Open();
                string deleteCategoria = "DELETE FROM CATEGORIA WHERE IdCategoria = @IdCategoria";
                SqlCommand sql = new SqlCommand(deleteCategoria, sqlconnection);
                sql.CommandType = CommandType.Text;

                sql.Parameters.AddWithValue("@IdCategoria", Id);

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
                throw new Exception("Erro ao excluir Categoria: " + e.Message);
            }
            finally
            {
                Close();
            }
        }
        #endregion

        public IEnumerable<Categoria> SelecionarCategoria()
        {
            try
            {
                Open();
                SQL = new SqlCommand(@"SELECT * FROM CATEGORIA", sqlconnection);
                SQL.CommandType = CommandType.Text;
                Dr = SQL.ExecuteReader();
                // Criando uma lista vazia
                var lista = new List<Categoria>();
                while (Dr.Read())
                {
                    var servico = new Categoria()
                    {
                        IdCategoria = Convert.ToInt32(Dr["IdCategoria"]),
                        dsCategoria = Convert.ToString(Dr["dsCategoria"]),
                        dtCadastro = Dr["dtCadastro"] == DBNull.Value ? DateTime.Now : Convert.ToDateTime(Dr["dtCadastro"]),
                        dtUltAlteracao = Dr["dtUltAlteracao"] == DBNull.Value ? DateTime.Now : Convert.ToDateTime(Dr["dtUltAlteracao"]),
                    };
                    lista.Add(servico);
                }
                return lista;
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao selecionar o Categoria: " + e.Message);
            }
            finally
            {
                Close();
            }
        }

        public CategoriaVM GetCategoria(int? Id)
        {
            try
            {
                Open();
                var servicoVM = new CategoriaVM();
                string selectEditCategoria = @"SELECT* FROM CATEGORIA WHERE IdCategoria =" + Id;
                SQL = new SqlCommand(selectEditCategoria, sqlconnection);


                Dr = SQL.ExecuteReader();
                while (Dr.Read())
                {
                    servicoVM.IdCategoria = Convert.ToInt32(Dr["IdCategoria"]);
                    servicoVM.dsCategoria = Dr["dsCategoria"].ToString();
                    servicoVM.dtCadastro = Dr["dtCadastro"] == DBNull.Value ? DateTime.Now : Convert.ToDateTime(Dr["dtCadastro"]);
                    servicoVM.dtUltAlteracao = Dr["dtUltAlteracao"] == DBNull.Value ? DateTime.Now : Convert.ToDateTime(Dr["dtUltAlteracao"]);
                }
                return servicoVM;
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao selecionar o Categoria: " + e.Message);
            }
            finally
            {
                Close();
            }
        }

        protected string BuscarCategoria(int? id, string text)
        {
            var sqlSelectPais = string.Empty;
            var where = string.Empty;

            if (id != null)
            {
                where = "WHERE IdCategoria = " + id;
            }
            if (!string.IsNullOrEmpty(text))
            {
                var query = text.Split(' ');
                foreach (var item in query)
                {
                    where += "OR dsCategoria LIKE '%'" + item + "'%'";
                }
                where = "WHERE" + where.Remove(0, 3);
            }
            sqlSelectPais = @"SELECT * FROM Categoria " + where;
            return sqlSelectPais;
        }

        public List<SelectCategoriaVM> SelectCategoria(int? IdCategoria, string dsCategoria)
        {
            try
            {

                var sqlSelectCargo = this.BuscarCategoria(IdCategoria, dsCategoria);
                Open();
                SQL = new SqlCommand(sqlSelectCargo, sqlconnection);
                Dr = SQL.ExecuteReader();
                var list = new List<SelectCategoriaVM>();

                while (Dr.Read())
                {
                    var categoria = new SelectCategoriaVM
                    {
                        Id = Convert.ToInt32(Dr["IdCategoria"]),
                        Text = Convert.ToString(Dr["dsCategoria"]),
                        flSituacao = Convert.ToString(Dr["flSituacao"]),
                        dtCadastro = Dr["dtCadastro"] == DBNull.Value ? DateTime.Now : Convert.ToDateTime(Dr["dtCadastro"]),
                        dtUltAlteracao = Dr["dtUltAlteracao"] == DBNull.Value ? DateTime.Now : Convert.ToDateTime(Dr["dtUltAlteracao"]),
                    };

                    list.Add(categoria);
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