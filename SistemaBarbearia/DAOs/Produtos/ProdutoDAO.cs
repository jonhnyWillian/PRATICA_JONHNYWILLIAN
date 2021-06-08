using SistemaBarbearia.DataBase;
using SistemaBarbearia.Models.Produtos;
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
                string insertProduto = @"INSERT INTO PRODUTO (dsProduto,nrUnidade,nrQtd,qtdEstoque,codBarra,vlCompra,vlCusto,idCategoria,idFornecedor,dtCadastro)
                                                    VALUES(@dsProduto,@nrUnidade,@nrQtd,@qtdEstoque,@codBarra,@vlCompra,@vlCusto,@idCategoria,@idFornecedor,@dtCadastro)";

                SQL = new SqlCommand(insertProduto, sqlconnection);
                SQL.CommandType = CommandType.Text;

                SQL.Parameters.AddWithValue("@dsProduto", produto.dsProduto.ToUpper());
                SQL.Parameters.AddWithValue("@nrUnidade", produto.nrUnidade.ToUpper());

                SQL.Parameters.AddWithValue("@nrQtd", produto.nrQtd);
                SQL.Parameters.AddWithValue("@qtdEstoque", produto.qtdEstoque);
                SQL.Parameters.AddWithValue("@codBarra", produto.codBarra);
                SQL.Parameters.AddWithValue("@vlCompra", produto.vlCompra);
                SQL.Parameters.AddWithValue("@vlCusto", produto.vlCusto);
                SQL.Parameters.AddWithValue("@idCategoria", produto.categoria.Id);
                SQL.Parameters.AddWithValue("@idFornecedor", produto.fornecedor.Id);

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
                string updateProduto = @"UPDATE PRODUTO SET dsProduto = @dsProduto, nrUnidade = @nrUnidade, nrQtd = @nrQtd, qtdEstoque = @qtdEstoque,
                                                            codBarra = @codBarra, vlCompra = @vlCompra, vlCusto = @vlCusto,
                                                            idCategoria = @idCategoria, idFornecedor = @idFornecedor, dtUltAlteracao = @dtUltAlteracao  

                                                           WHERE id = @id";
                SqlCommand sql = new SqlCommand(updateProduto, sqlconnection);
                sql.CommandType = CommandType.Text;

                sql.Parameters.AddWithValue("@id", produto.Id);
                SQL.Parameters.AddWithValue("@dsProduto", produto.dsProduto.ToUpper());
                SQL.Parameters.AddWithValue("@nrUnidade", produto.nrUnidade.ToUpper());

                SQL.Parameters.AddWithValue("@nrQtd", produto.nrQtd);
                SQL.Parameters.AddWithValue("@qtdEstoque", produto.qtdEstoque);
                SQL.Parameters.AddWithValue("@codBarra", produto.codBarra);
                SQL.Parameters.AddWithValue("@vlCompra", produto.vlCompra);
                SQL.Parameters.AddWithValue("@vlCusto", produto.vlCusto);
                SQL.Parameters.AddWithValue("@idCategoria", produto.categoria.Id);
                SQL.Parameters.AddWithValue("@idFornecedor", produto.fornecedor.Id);

                SQL.Parameters.AddWithValue("@dtUltAlteracao", produto.dtUltAlteracao = DateTime.Now);


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
                string deletePais = "DELETE FROM Produto WHERE Id = @Id";
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
                        Id = Convert.ToInt32(Dr["Id"]),
                        dsProduto = Convert.ToString(Dr["dsProduto"]),
                        nrUnidade = Convert.ToString(Dr["nrUnidade"]),
                        nrQtd = Convert.ToInt32(Dr["nrQtd"]),
                        qtdEstoque = Convert.ToInt32(Dr["qtdEstoque"]),
                        codBarra = Convert.ToString(Dr["codBarra"]),
                        vlCompra = Convert.ToDecimal(Dr["vlCompra"]),
                        vlCusto = Convert.ToDecimal(Dr["vlCusto"]),
                        idCategoria = Convert.ToInt32(Dr["idCategoria"]),
                        idFornecedor = Convert.ToInt32(Dr["idFornecedor"]),
                        dtCadastro = Convert.ToDateTime(Dr["dtCadastro"]),

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
                string selectEditPais = @"SELECT* FROM PRODUTO WHERE id =" + Id;
                SQL = new SqlCommand(selectEditPais, sqlconnection);


                Dr = SQL.ExecuteReader();
                while (Dr.Read())
                {
                    produtoVM.Id = Convert.ToInt32(Dr["id"]);
                    produtoVM.dsProduto = Convert.ToString(Dr["dsProduto"]);
                    produtoVM.flUnidade = Convert.ToString(Dr["nrUnidade"]);
                    produtoVM.nrQtd = Convert.ToInt32(Dr["nrQtd"]);
                    produtoVM.qtdEstoque = Convert.ToInt32(Dr["qtdEstoque"]);
                    produtoVM.codBarra = Convert.ToString(Dr["codBarra"]);
                    produtoVM.vlCompra = Convert.ToDecimal(Dr["vlCompra"]);
                    produtoVM.vlCusto = Convert.ToDecimal(Dr["vlCusto"]);
                    produtoVM.categoria.IdCategoria = Convert.ToInt32(Dr["idCategoria"]);
                    produtoVM.fornecedor.Id = Convert.ToInt32(Dr["idFornecedor"]);
                    produtoVM.dtCadastro = Convert.ToDateTime(Dr["dtCadastro"]);
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
    }
}