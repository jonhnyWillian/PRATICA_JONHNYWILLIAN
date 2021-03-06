using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SistemaBarbearia.Models.Compras
{
    public class Compra 
    {


        [Key]
        public int IdCompra { get; set; }

        public string nrModelo { get; set; }
        public string nrModeloAux { get; set; }

        public string flSituacao { get; set; }

        public string nrSerie { get; set; }
        public string nrSerieAux { get; set; }

        public int? nrNota { get; set; }
        public int? nrNotaAux { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? dtEmissao { get; set; }
        public string dtEmissaoAux { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? dtEntrega { get; set; }
        public string dtEntregaAux { get; set; }

        public ViewModels.Fornecedores.SelectFornecedorVM Fornecedor { get; set; }
        public int? IdFornecedor { get; set; }
        public ViewModels.CondPagamentos.SelectCondPagamentoVM CondicaoPagamento { get; set; }
        public int? IdCondPag { get; set; }
        public ViewModels.Produtos.SelectProdutoVM Produto { get; set; }
        public int? IdProduto { get; set; }

        public string finalizar { get; set; }
        public decimal? vlTotal { get; set; }
       
        public decimal? vlFrete { get; set; }
        public decimal? vlSeguro { get; set; }      
        public decimal? vlDespesa { get; set; }

        public DateTime? dtCadastro { get; set; }
        public DateTime? dtUltAlteracao { get; set; }

        public class ProdutosVM
        {
            public int? IdProduto { get; set; }
            public string dsProduto { get; set; }
            public decimal? nrQtd { get; set; }
            public decimal? vlVenda { get; set; }
            public decimal? vlCompra { get; set; }
            public decimal? txDesconto { get; set; }
            public decimal? vlTotal { get; set; }
        }
        public string jsProdutos { get; set; }

        public List<ProdutosVM> ProdutosCompra
        {
            get
            {
                if (string.IsNullOrEmpty(jsProdutos))
                    return new List<ProdutosVM>();
                return JsonConvert.DeserializeObject<List<ProdutosVM>>(jsProdutos);
            }
            set
            {
                jsProdutos = JsonConvert.SerializeObject(value);
            }
        }

        public class ParcelasVM
        {
            public int? IdFormaPagamento { get; set; }
            public string dsFormaPagamento { get; set; }
            public DateTime? dtVencimento { get; set; }
            public decimal vlParcela { get; set; }
            public double? nrParcela { get; set; }
            public string flSituacao { get; set; }
            public DateTime? dtPagamento { get; set; }
        }
        public string jsParcelas { get; set; }
        public List<ParcelasVM> ParcelasCompra
        {
            get
            {
                if (string.IsNullOrEmpty(jsParcelas))
                    return new List<ParcelasVM>();
                return JsonConvert.DeserializeObject<List<ParcelasVM>>(jsParcelas);
            }
            set
            {
                jsParcelas = JsonConvert.SerializeObject(value);
            }
        }
    }
}