using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SistemaBarbearia.Models.Vendas
{
    public class Venda
    {

        public string nrModelo { get; set; }
        public int nrNota { get; set; }
        public string nrSerie { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? dtNota { get; set; }
        public decimal? vlTotalProduto { get; set; }

        public int IdAgenda { get; set; }
        public string flSituacao { get; set; }

        public ViewModels.Clientes.SelectClienteVM Cliente { get; set; }
        public int IdCliente { get; set; }

        public ViewModels.Produtos.SelectProdutoVM Produto { get; set; }

        public ViewModels.CondPagamentos.SelectCondPagamentoVM CondicaoPagamento { get; set; }
        public int IdCondPagamento { get; set; }

        public class ProdutosVM
        {
            public int? IdProduto { get; set; }
            public string nmProduto { get; set; }
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
        public List<ParcelasVM> ParcelasVenda
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

        public DateTime? dtCadastro { get; set; }
        public DateTime? dtUltAlteracao { get; set; }
    }
}