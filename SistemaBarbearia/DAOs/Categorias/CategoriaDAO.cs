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

        public bool UpdateCategoria(Categoria servico)
        {
            try
            {
                Open();
                string updateCategoria = @"UPDATE CATEGORIA SET dsCategoria = @dsCategoria, dtUltAlteracao = @dtUltAlteracao  WHERE id = @id";
                SqlCommand sql = new SqlCommand(updateCategoria, sqlconnection);
                sql.CommandType = CommandType.Text;

                sql.Parameters.AddWithValue("@id", servico.Id);
                sql.Parameters.AddWithValue("@dsCategoria", servico.dsCategoria.ToUpper());
                sql.Parameters.AddWithValue("@dtUltAlteracao", servico.dtUltAlteracao = DateTime.Now);


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
                string deleteCategoria = "DELETE FROM CATEGORIA WHERE Id = @Id";
                SqlCommand sql = new SqlCommand(deleteCategoria, sqlconnection);
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
                        Id = Convert.ToInt32(Dr["Id"]),
                        dsCategoria = Convert.ToString(Dr["dsCategoria"]),
                        dtCadastro = Convert.ToDateTime(Dr["dtCadastro"]),
                        // dtUltAlteracao = Convert.ToDateTime(Dr["dtUltAlteracao"]),
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
                string selectEditCategoria = @"SELECT* FROM CATEGORIA WHERE id =" + Id;
                SQL = new SqlCommand(selectEditCategoria, sqlconnection);


                Dr = SQL.ExecuteReader();
                while (Dr.Read())
                {
                    servicoVM.Id = Convert.ToInt32(Dr["id"]);
                    servicoVM.dsCategoria = Dr["dsCategoria"].ToString();
                    servicoVM.dtCadastro = Convert.ToDateTime(Dr["dtCadastro"]);
                    // paisVM.dtUltAlteracao = Convert.ToDateTime(Dr["dtUltAlteracao"]);
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


       
    }
}