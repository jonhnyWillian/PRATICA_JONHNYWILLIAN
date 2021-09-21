using SistemaBarbearia.DataBase;
using SistemaBarbearia.Models.Produtos;
using SistemaBarbearia.ViewModels.Categorias;
using SistemaBarbearia.ViewModels.Produtos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace SistemaBarbearia.DAOs.Produtos
{
    public class ProdutoDAO : DataAccess
    {
        #region INSERT, UPDATE, DELETE 
        public bool InsertProduto(Produto produto)
        {

            try
            {
                Open();
                string insertProduto = @"INSERT INTO PRODUTO ( dsProduto,  nrUnidade,  nrQtd, codBarra,  vlCompra,  vlCusto,  vlVenda,  IdCategoria, idFornecedor,  dtCadastro)
                                                     VALUES(  @dsProduto, @nrUnidade, @nrQtd, @codBarra, @vlCompra, @vlCusto, @vlVenda, @IdCategoria, @idFornecedor,  @dtCadastro)";

                SQL = new SqlCommand(insertProduto, sqlconnection);
                SQL.CommandType = CommandType.Text;

                SQL.Parameters.AddWithValue("@dsProduto", produto.dsProduto.ToUpper());
                SQL.Parameters.AddWithValue("@nrUnidade", produto.nrUnidade);
                SQL.Parameters.AddWithValue("@nrQtd", produto.nrQtd);            
                SQL.Parameters.AddWithValue("@codBarra", produto.codBarra);
                SQL.Parameters.AddWithValue("@vlCompra", produto.vlCompra);
                SQL.Parameters.AddWithValue("@vlCusto", produto.vlCusto);
                SQL.Parameters.AddWithValue("@vlVenda", produto.vlVenda);
                SQL.Parameters.AddWithValue("@IdCategoria", produto.categoria.Id);
                SQL.Parameters.AddWithValue("@idFornecedor", produto.fornecedor.IdFornecedor);
                SQL.Parameters.AddWithValue("@dtCadastro", produto.dtCadastro = DateTime.Now);

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
                throw new Exception("Erro ao Adicionar Novo Produto: " + e.Message);
            }
            finally
            {
                Close();
            }
        }

        public bool UpdateProduto(Produto produto)
        {
            try
            {
                Open();
                string updateProduto = @"UPDATE PRODUTO SET dsProduto = @dsProduto, nrUnidade = @nrUnidade, nrQtd = @nrQtd,
                                                            codBarra = @codBarra, vlCompra = @vlCompra, vlCusto = @vlCusto, vlVenda = @vlVenda,
                                                            IdCategoria = @IdCategoria, idFornecedor = @idFornecedor dtUltAlteracao = @dtUltAlteracao  

                                                           WHERE IdProduto = " + produto.IdProduto;
                SqlCommand sql = new SqlCommand(updateProduto, sqlconnection);
                sql.CommandType = CommandType.Text;

                sql.Parameters.AddWithValue("@IdProduto", produto.IdProduto);
                sql.Parameters.AddWithValue("@dsProduto", produto.dsProduto.ToUpper());
                sql.Parameters.AddWithValue("@nrUnidade", produto.nrUnidade.ToUpper());                
                sql.Parameters.AddWithValue("@nrQtd", produto.nrQtd);               
                sql.Parameters.AddWithValue("@codBarra", produto.codBarra);
                sql.Parameters.AddWithValue("@vlCompra", produto.vlCompra);
                sql.Parameters.AddWithValue("@vlCusto", produto.vlCusto);
                sql.Parameters.AddWithValue("@vlVenda", produto.vlVenda);
                sql.Parameters.AddWithValue("@IdCategoria", produto.categoria.Id);
                sql.Parameters.AddWithValue("@idFornecedor", produto.fornecedor.IdFornecedor);


                sql.Parameters.AddWithValue("@dtUltAlteracao", produto.dtUltAlteracao = DateTime.Now);


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
                throw new Exception("Erro ao Atualizar Produto: " + e.Message);
            }
            finally
            {
                Close();
            }
        }

        public bool DeleteProduto(int Id)
        {
            try
            {
                Open();
                string deletePais = "DELETE FROM Produto WHERE IdProduto = @IdProduto";
                SqlCommand sql = new SqlCommand(deletePais, sqlconnection);
                sql.CommandType = CommandType.Text;

                sql.Parameters.AddWithValue("@IdProduto", Id);

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
                throw new Exception("Erro ao excluir Produto: " + e.Message);
            }
            finally
            {
                Close();
            }
        }

        #endregion

        public IEnumerable<Produto> SelecionarProduto()
        {
            try
            {
                Open();
                SQL = new SqlCommand(@"SELECT * FROM PRODUTO", sqlconnection);
                SQL.CommandType = CommandType.Text;
                Dr = SQL.ExecuteReader();
                // Criando uma lista vazia
                var lista = new List<Produto>();
                while (Dr.Read())
                {
                    var Produto = new Models.Produtos.Produto()
                    {
                        IdProduto = Convert.ToInt32(Dr["IdProduto"]),
                        dsProduto = Convert.ToString(Dr["dsProduto"]),
                        nrUnidade = Convert.ToString(Dr["nrUnidade"]),
                        nrQtd = Convert.ToInt32(Dr["nrQtd"]),                    
                        codBarra = Convert.ToString(Dr["codBarra"]),
                        vlCompra = Convert.ToDecimal(Dr["vlCompra"]),
                        vlCusto = Convert.ToDecimal(Dr["vlCusto"]),
                        vlVenda = Convert.ToDecimal(Dr["vlVenda"]),
                        idCategoria = Convert.ToInt32(Dr["IdCategoria"]),

                        dtCadastro = Dr["dtCadastro"] == DBNull.Value ? DateTime.Now : Convert.ToDateTime(Dr["dtCadastro"]),
                        dtUltAlteracao = Dr["dtUltAlteracao"] == DBNull.Value ? DateTime.Now : Convert.ToDateTime(Dr["dtUltAlteracao"]),

                    };
                    lista.Add(Produto);
                }
                return lista;
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao selecionar o Produto: " + e.Message);
            }
            finally
            {
                Close();
            }
        }

        public ProdutoVW GetProduto(int? Id)
        {
            try
            {
                Open();
                var produtoVM = new ProdutoVW();
                string selectEditPais = @"SELECT* FROM PRODUTO WHERE IdProduto =" + Id;
                SQL = new SqlCommand(selectEditPais, sqlconnection);


                Dr = SQL.ExecuteReader();
                while (Dr.Read())
                {
                    produtoVM.IdProduto = Convert.ToInt32(Dr["IdProduto"]);
                    produtoVM.dsProduto = Convert.ToString(Dr["dsProduto"]);
                    produtoVM.nrUnidade = Convert.ToString(Dr["nrUnidade"]);
                    produtoVM.nrQtd = Convert.ToInt32(Dr["nrQtd"]);                 
                    produtoVM.codBarra = Convert.ToString(Dr["codBarra"]);
                    produtoVM.vlCompra = Convert.ToDecimal(Dr["vlCompra"]);
                    produtoVM.vlCusto = Convert.ToDecimal(Dr["vlCusto"]);
                    produtoVM.vlVenda = Convert.ToDecimal(Dr["vlVenda"]);
                    produtoVM.IdCategoria = Convert.ToInt32(Dr["IdCategoria"]);
                    produtoVM.IdFornecedor = Convert.ToInt32(Dr["IdFornecedor"]);
                    produtoVM.dtCadastro = Dr["dtCadastro"] == DBNull.Value ? DateTime.Now : Convert.ToDateTime(Dr["dtCadastro"]);
                    produtoVM.dtUltAlteracao = Dr["dtUltAlteracao"] == DBNull.Value ? DateTime.Now : Convert.ToDateTime(Dr["dtUltAlteracao"]);


                }
                return produtoVM;
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao selecionar o Produto: " + e.Message);
            }
            finally
            {
                Close();
            }
        }



        protected string BuscarProduto(int? id, string text)
        {
            var sqlSelectProduto = string.Empty;
            var where = string.Empty;

            if (id != null)
            {
                where = "WHERE IdProduto = " + id;
            }
            if (!string.IsNullOrEmpty(text))
            {
                var query = text.Split(' ');
                foreach (var item in query)
                {
                    where += "OR dsProduto LIKE '%'" + item + "'%'";
                }
                where = "WHERE" + where.Remove(0, 3);
            }
            sqlSelectProduto = @"SELECT * FROM Produto " + where;
            return sqlSelectProduto;
        }

        public List<SelectProdutoVM> SelectProduto(int? id, string nmNome)
        {
            try
            {

                var sqlSelectProduto = this.BuscarProduto(id, nmNome);
                Open();
                SQL = new SqlCommand(sqlSelectProduto, sqlconnection);
                Dr = SQL.ExecuteReader();
                var list = new List<SelectProdutoVM>();

                while (Dr.Read())
                {
                    var produto = new SelectProdutoVM
                    {
                        IdProduto = Convert.ToInt32(Dr["idProduto"]),
                        dsProduto = Convert.ToString(Dr["dsProduto"]),
                    };

                    list.Add(produto);
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