using SistemaBarbearia.DataBase;
using SistemaBarbearia.Models.ContasPagar;
using SistemaBarbearia.ViewModels.ContasPagar;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace SistemaBarbearia.DAOs.ContasPagar
{
    public class ContaPagarDAO : DataAccess
    {
        public List<ContaPagar> SelecionarCompra()
        {
            try
            {
                Open();
                SQL = new SqlCommand(@"SELECT* FROM ContasPagar 
                                                INNER JOIN Fornecedor ON ContasPagar.fornecedor_id = Fornecedor.idFornecedor 
                                                INNER JOIN Compra ON Compra.IdFornecedor = Fornecedor.IdFornecedor ", sqlconnection);
                SQL.CommandType = CommandType.Text;
                Dr = SQL.ExecuteReader();
                // Criando uma lista vazia
                var lista = new List<ContaPagar>();
                while (Dr.Read())
                {
                    var c = new ContaPagar()
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

        public ContaPagarVM GetContaPagar(string filter, string nmModelo, string nrSerie, int nrNota, int? IdFornecedor)
        {
            try
            {
                Open();
                var contaVM = new ContaPagarVM();
                var sql = this.BuscarCompra(filter, nmModelo, nrSerie, nrNota, IdFornecedor);
          
                SQL = new SqlCommand(sql , sqlconnection);
                Dr = SQL.ExecuteReader();
                while (Dr.Read())
                {

                    contaVM.nrModelo = Convert.ToString(Dr["Compra_Modelo"]);
                    contaVM.nrNota = Convert.ToInt32(Dr["Compra_Nota"]);
                    contaVM.nrSerie = Convert.ToString(Dr["Compra_Serie"]);


                    contaVM.dtEmissao = Dr["Compra_dtEmissao"] == DBNull.Value ? DateTime.Now : Convert.ToDateTime(Dr["Compra_dtEmissao"]);

                    contaVM.Fornecedor = new ViewModels.Fornecedores.SelectFornecedorVM
                    {
                        IdFornecedor = Convert.ToInt32(Dr["Fornecedor_ID"]),
                        nmNome = Convert.ToString(Dr["Fornecedor_nmNome"])
                    };
                

                }
                return contaVM;
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
    }
}