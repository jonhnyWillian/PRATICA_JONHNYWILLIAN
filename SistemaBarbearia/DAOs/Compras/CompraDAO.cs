using SistemaBarbearia.DataBase;
using SistemaBarbearia.Models.Compras;
using SistemaBarbearia.ViewModels.Compras;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace SistemaBarbearia.DAOs.Compras
{
    public class CompraDAO : DataAccess
    {
        #region INSERT UPDATE DELETE

        public bool InsertCompra(Compra compra)
        {
            int i = 0;
            Open();
            SqlTransaction sqlTransaction = sqlconnection.BeginTransaction();
            SqlCommand command;
            try
            {
                command = sqlconnection.CreateCommand();
                command.Transaction = sqlTransaction; 
                command.CommandText = "INSERT INTO Compra ( nrModelo,  nrSerie,  nrNota,  IdFornecedor,  IdCondPag,  dtEmissao,  dtEntrega,  vlSeguro,  vlFrete,  vlDespesas, vlTotal,  dtCadastro,  flSituacao,  dtUltAlteracao  )" +
                                                   "VALUES(@nrModelo, @nrSerie, @nrNota, @IdFornecedor, @IdCondPag, @dtEmissao, @dtEntrega, @vlSeguro, @vlFrete, @vlDespesas,  @vlTotal, @dtCadastro, @flSituacao, @dtUltAlteracao );"; //SELECT CAST(SCOPE_IDENTITY() AS int)

                command.Parameters.AddWithValue("@nrModelo", compra.nrModelo );
                command.Parameters.AddWithValue("@nrSerie", compra.nrSerie );
                command.Parameters.AddWithValue("@nrNota", compra.nrNota );
                command.Parameters.AddWithValue("@IdFornecedor", compra.Fornecedor.IdFornecedor);
                command.Parameters.AddWithValue("@IdCondPag", compra.CondicaoPagamento.Id);
                command.Parameters.AddWithValue("@dtEmissao", compra.dtEmissao);
                command.Parameters.AddWithValue("@dtEntrega", compra.dtEntrega);                
                command.Parameters.AddWithValue("@flSituacao", compra.flSituacao);
                command.Parameters.AddWithValue("@vlSeguro", compra.vlSeguro);
                command.Parameters.AddWithValue("@vlFrete", compra.vlFrete);
                command.Parameters.AddWithValue("@vlDespesas", compra.vlDespesa);
                command.Parameters.AddWithValue("@vlTotal", compra.vlTotal);
                command.Parameters.AddWithValue("@dtcadastro", compra.dtCadastro = DateTime.Now);
                command.Parameters.AddWithValue("@dtUltAlteracao", compra.dtUltAlteracao = DateTime.Now);


                i = command.ExecuteNonQuery();


                foreach (var item in compra.ProdutosCompra)
                {
                   
                    command.CommandText = "INSERT INTO ProdutoCompras ( nrModelo, nrSerie, nrNota, IdProduto, nrQtd, vlCompra, vlVenda, txDesconto)" +
                                                       " VALUES( @nrModelo, @nrSerie, @nrNota, @IdProduto, @nrQtd, @vlCompra, @vlVenda, @txDesconto)";

                    command.Parameters.Clear();
                    command.Parameters.AddWithValue("@nrModelo", compra.nrModelo);
                    command.Parameters.AddWithValue("@nrSerie", compra.nrSerie);
                    command.Parameters.AddWithValue("@nrNota", compra.nrNota);
                    command.Parameters.AddWithValue("@IdProduto", item.IdProduto);
                    command.Parameters.AddWithValue("@nrQtd", item.nrQtd);
                    command.Parameters.AddWithValue("@vlCompra", item.vlCompra);
                    command.Parameters.AddWithValue("@vlVenda",item.vlVenda);
                    command.Parameters.AddWithValue("@txDesconto", item.txDesconto);

                    i = command.ExecuteNonQuery();
                }
                
                //command.CommandText = "UPDATE Produto SET nrQtd += nrQtd@, vlUltCompra += @vlUltCompra WHERE idProduto = @IdProduto";
                //foreach (var item in compra.ProdutosCompra)
                //{
                //    command.Parameters.Clear();
                //    command.Parameters.AddWithValue("@nrQtd", ((object)item.nrQtd) != DBNull.Value ? ((object)item.nrQtd) : 0);
                //    command.Parameters.AddWithValue("@vlUltCompra", ((object)item.vlCompra) != DBNull.Value ? ((object)item.vlCompra) : 0);
                //    command.Parameters.AddWithValue("@IdProduto", item.IdProduto);
                //    command.ExecuteNonQuery();
                //}

                
                foreach (var item in compra.ParcelasCompra)
                {
                    command = sqlconnection.CreateCommand();
                    command.Transaction = sqlTransaction;
                    command.CommandText = "INSERT INTO ContasPagar  ( nrModelo,  nrSerie,  nrNota, IdFornecedor, IdFormaPagamento, vlParcela,  flSituacao, dtVencimento, nrparcela  )" +
                                                         "VALUES(@nrModelo, @nrSerie, @nrNota, @IdFornecedor, @IdFormaPagamento, @vlParcela,  @flSituacao, @dtVencimento, @nrparcela )";

                    command.Parameters.AddWithValue("@nrModelo", compra.nrModelo);
                    command.Parameters.AddWithValue("@nrSerie", compra.nrSerie);
                    command.Parameters.AddWithValue("@nrNota", compra.nrNota);
                    command.Parameters.AddWithValue("@IdFornecedor", compra.Fornecedor.IdFornecedor);
                    command.Parameters.AddWithValue("@IdFormaPagamento", item.IdFormaPagamento);                 
                    command.Parameters.AddWithValue("@vlParcela", item.vlParcela );
                    command.Parameters.AddWithValue("@flSituacao", item.flSituacao = "A");
                    command.Parameters.AddWithValue("@dtVencimento",item.dtVencimento);
                    command.Parameters.AddWithValue("@nrparcela", item.nrParcela);
                    i = command.ExecuteNonQuery();


                }
                sqlTransaction.Commit();


                if (i >= 1)
                {
                    return true;

                }
                else
                {
                    return false;
                }

            }
            catch (Exception ex)
            {
                sqlTransaction.Rollback();
                throw new Exception("Erro ao inserir o Compra: " + ex.Message);
            }
            finally
            {
                Close();
            }
        }
        public bool UpdateCompra(Compra compra)
        {
            return true;
        }
        public bool DeleteCompra(Compra compra)
        {
            return true;
        }
        #endregion


        public List<Compra> SelecionarCompra()
        {
            try
            {
                Open();
                SQL = new SqlCommand(@"SELECT* FROM Compra INNER JOIN Fornecedor ON Compra.idFornecedor = Fornecedor.idFornecedor ", sqlconnection);
                SQL.CommandType = CommandType.Text;
                Dr = SQL.ExecuteReader();
                // Criando uma lista vazia
                var lista = new List<Compra>();
                while (Dr.Read())
                {
                    var c = new Compra()
                    {                       
                        Fornecedor = new ViewModels.Fornecedores.SelectFornecedorVM
                        {
                            IdFornecedor = Convert.ToInt32(Dr["IdFornecedor"]),
                            nmNome = Convert.ToString(Dr["nmNome"]),
                        },
                        nrModelo = Convert.ToString(Dr["nrModelo"]),
                        nrSerie = Convert.ToString(Dr["nrSerie"]),
                        nrNota = Convert.ToInt32(Dr["nrNota"]),
                        dtEmissao = Dr["dtEmissao"] == DBNull.Value ? DateTime.Now : Convert.ToDateTime(Dr["dtEmissao"]),
                       
                    };
                    lista.Add(c);
                }
                return lista;
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao selecionar o Compra: " + e.Message);
            }
            finally
            {
                Close();
            }
        }

        public CompraVM GetCompra(string filter, string nmModelo, string nrSerie, int nrNota, int? IdFornecedor)
        {
            try
            {
                Open();
                var compraVM = new CompraVM();
                var sql = this.BuscarCompra(filter, nmModelo, nrSerie, nrNota, IdFornecedor);
                var sqlProduto = this.BuscarProdutos(nmModelo, nrSerie, nrNota);
                var sqlParcela = this.BuscarParcelas(nmModelo, nrSerie, nrNota, IdFornecedor);

                var listProdutos = new List<CompraVM.ProdutosVM>();
                var listParcelas = new List<CompraVM.ParcelasVM>();

                SQL = new SqlCommand(sql + sqlProduto + sqlParcela, sqlconnection);
                Dr = SQL.ExecuteReader();
                while (Dr.Read())
                {
              
                    compraVM.nrModelo = Convert.ToString(Dr["Compra_Modelo"]);
                    compraVM.nrNota = Convert.ToInt32(Dr["Compra_Nota"]);
                    compraVM.nrSerie = Convert.ToString(Dr["Compra_Serie"]);
                    compraVM.vlDespesa = Convert.ToDecimal(Dr["Compra_vlDespesas"]);
                    compraVM.vlSeguro = Convert.ToDecimal(Dr["Compra_vlSeguro"]);
                    compraVM.vlFrete = Convert.ToDecimal(Dr["Compra_vlFrete"]);
                    compraVM.vlTotal = Convert.ToDecimal(Dr["Compra_vlTotal"]);

                    compraVM.dtEmissao = Dr["Compra_dtEmissao"] == DBNull.Value ? DateTime.Now : Convert.ToDateTime(Dr["Compra_dtEmissao"]);
                    compraVM.dtEntrega = Dr["Compra_dtentrega"] == DBNull.Value ? DateTime.Now : Convert.ToDateTime(Dr["Compra_dtentrega"]);
                                 
                    compraVM.Fornecedor = new ViewModels.Fornecedores.SelectFornecedorVM
                    {
                        IdFornecedor = Convert.ToInt32(Dr["Fornecedor_ID"]),
                        nmNome = Convert.ToString(Dr["Fornecedor_nmNome"])
                    };
                    compraVM.CondicaoPagamento = new ViewModels.CondPagamentos.SelectCondPagamentoVM
                    {
                        Id = Convert.ToInt32(Dr["CondicaoPagamento_ID"]),
                        Text = Convert.ToString(Dr["CondicaoPagamento_Nome"]),
                    };
                  
 
                }
                if (Dr.NextResult())
                {
                    while (Dr.Read())
                    {
                        var produto = new CompraVM.ProdutosVM
                        {
                            IdProduto = Convert.ToInt32(Dr["ProdutoCompra_ID"]),
                            dsProduto = Convert.ToString(Dr["ProdutoCompra_dsProduto"]),                         
                            nrQtd = Convert.ToDecimal(Dr["ProdutoCompra_nrQtd"]),
                            vlCompra = Convert.ToDecimal(Dr["ProdutoCompra_vlCompra"]),
                            txDesconto = Convert.ToDecimal(Dr["ProdutoCompra_txDesconto"]),
                            vlVenda = Convert.ToDecimal(Dr["ProdutoCompra_vlVenda"]),
                        };
                        var txDesc = (produto.vlCompra * produto.txDesconto) / 100;
                        var vlTotal = produto.vlCompra - txDesc;
                        produto.vlTotal = vlTotal;
                        listProdutos.Add(produto);
                    }
                }

                if (Dr.NextResult())
                {
                    while (Dr.Read())
                    {
                        var parcela = new CompraVM.ParcelasVM
                        {
                            IdFormaPagamento = Convert.ToInt32(Dr["FormaPagamento_ID"]),
                            dsFormaPagamento = Convert.ToString(Dr["FormaPagamento_dsForma"]),
                            nrParcela = Convert.ToDouble(Dr["ContaPagar_NrParcela"]),
                            vlParcela = Convert.ToDecimal(Dr["ContaPagar_VlParcela"]),
                            dtVencimento = Convert.ToDateTime(Dr["ContaPagar_DtVencimento"]),
                            //flSituacao =Convert.ToString(Dr["ContaPagar_flSituacao"])
                        };
                        listParcelas.Add(parcela);
                    }
                }
                compraVM.ProdutosCompra = listProdutos;
                compraVM.ParcelasCompra = listParcelas;
                return compraVM;
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao selecionar o Agenda: " + e.Message);
            }
            finally
            {
                Close();
            }
        }

        protected string BuscarCompra(string filter, string nrModelo, string nrSerie, int? nrNota, int? IdFornecedor)
        {
            var sql = string.Empty;
            var swhere = string.Empty;
            if (!string.IsNullOrEmpty(nrModelo))
            {
                swhere += " AND Compra.nrModelo = '" + nrModelo + "'";
            }
            if (!string.IsNullOrEmpty(nrSerie))
            {
                swhere += " AND Compra.nrSerie = '" + nrSerie + "'";
            }
            if (nrNota != null)
            {
                swhere += " AND Compra.nrNota = " + nrNota;
            }
            if (nrNota != null)
            {
                swhere += " AND Compra.IdFornecedor = " + IdFornecedor;
            }

            if (!string.IsNullOrEmpty(filter))
            {
                var filterQ = filter.Split(' ');
                foreach (var word in filterQ)
                {
                    swhere += " OR Fornecedor.nmNome LIKE'%" + word + "%'";
                }
            }

            if (!string.IsNullOrEmpty(swhere))
                swhere = " WHERE " + swhere.Remove(0, 4);
            sql = @"
                 SELECT
	                    Compra.flSituacao  AS Compra_flSituacao,

	                    Compra.nrModelo AS Compra_Modelo,
	                    Compra.nrSerie AS Compra_Serie,
	                    Compra.nrNota AS Compra_Nota,

	                    Compra.dtEmissao AS Compra_dtEmissao,
	                    Compra.dtentrega AS Compra_dtentrega,	    
	                    Compra.dtCadastro AS Compra_dtCadastro,

	                    Compra.vlFrete AS Compra_vlFrete,
	                    Compra.vlSeguro AS Compra_vlSeguro,
	                    Compra.vlDespesas AS Compra_vlDespesas,
                        Compra.vlTotal AS Compra_vlTotal,

	                    Compra.IdFornecedor AS Fornecedor_ID,
	                    Fornecedor.nmNome AS Fornecedor_nmNome,

	                    Compra.IdCondPag AS CondicaoPagamento_ID,
	                    CondPagamento.dsCondPag AS CondicaoPagamento_Nome

	            FROM Compra
	            INNER JOIN Fornecedor on Compra.IdFornecedor = Fornecedor.IdFornecedor
	            INNER JOIN CondPagamento on Compra.IdCondPag = CondPagamento.IdCondPag
                " + swhere + ";";
            return sql;
        }

        protected string BuscarProdutos(string nrModelo, string nrSerie, int? nrNota)
        {
            var sql = string.Empty;

            sql = @"SELECT
	                    ProdutoCompras.nrModelo AS Compra_Modelo,
	                    ProdutoCompras.nrSerie AS Compra_Modelo,
	                    ProdutoCompras.nrNota AS Compra_Modelo,	 

	                    ProdutoCompras.IdProduto AS ProdutoCompra_ID,
	                    Produto.dsProduto AS ProdutoCompra_dsProduto,

	                    ProdutoCompras.nrQtd AS ProdutoCompra_nrQtd,
	                    ProdutoCompras.vlCompra AS ProdutoCompra_vlCompra,
	                    ProdutoCompras.txDesconto AS ProdutoCompra_txDesconto,
	                    ProdutoCompras.vlVenda AS ProdutoCompra_vlVenda
	                FROM ProdutoCompras
	                    INNER JOIN Produto on ProdutoCompras.IdProduto = Produto.IdProduto
                    WHERE ProdutoCompras.nrModelo = '" + nrModelo + "' AND ProdutoCompras.nrSerie = '" + nrSerie + "' AND ProdutoCompras.nrNota = " + nrNota ;

            ;
            return sql;
        }

        protected string BuscarParcelas(string nrModelo, string nrSerie, int? nrNota, int? IdFornecedor)
        {
            var sql = string.Empty;
            sql = @"
                    SELECT  ContasPagar.IdFornecedor AS ContaPagar_Fornecedor_ID ,
	                        ContasPagar.IdFormaPagamento AS FormaPagamento_ID,
	                        Formapagamento.dsFormaPagamento AS FormaPagamento_dsForma,
	                        ContasPagar.nrparcela AS ContaPagar_NrParcela,
	                        ContasPagar.vlparcela AS ContaPagar_VlParcela,
	                        ContasPagar.dtvencimento AS ContaPagar_DtVencimento,
	                      
	                        ContasPagar.nrModelo AS ContaPagar_nrModelo,
	                        ContasPagar.nrSerie AS ContaPagar_nrSerie,
	                        ContasPagar.nrNota AS ContaPagar_nrNota
                    FROM ContasPagar
	                    INNER JOIN Formapagamento on ContasPagar.IdFormaPagamento = FormaPagamento.IdFormaPagamento
                        WHERE ContasPagar.nrModelo = '" + nrModelo + "' AND ContasPagar.nrSerie = '" + nrSerie + "' AND ContasPagar.nrNota = " + nrNota + " AND ContasPagar.IdFornecedor = " + IdFornecedor;
            return sql;
        }

        public bool validNota(string nrModelo, string nrSerie, int nrNota, int idFornecedor)
        {
            string sql = "SELECT * FROM Compra WHERE nrModelo = '" + nrModelo + "' AND nrSerie = '" + nrSerie + "' AND nrNota = " + nrNota + " AND idFornecedor = " + idFornecedor;
            Open();
            SqlCommand query = new SqlCommand(sql, sqlconnection);
            var exist = query.ExecuteScalar();
            Close();
            if (exist == null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}