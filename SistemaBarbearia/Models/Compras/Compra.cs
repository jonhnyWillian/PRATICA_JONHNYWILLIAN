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
        public short modelo { get; set; }

        public short nrSerie { get; set; }
        
        public int nrNota { get; set; }
        
        public DateTime? dtEmissao { get; set; }

        public DateTime? dtEntrega { get; set; }

        public ViewModels.Fornecedores.SelectFornecedorVM Fornecedor { get; set; }
        public ViewModels.CondPagamentos.SelectCondPagamentoVM CondicaoPagamento { get; set; }
       // public ViewModels.Produtos.SelectProdutoVW Produto { get; set; }


        public class ProdutosVM
        {
            public int? idProduto { get; set; }
            public string nmProduto { get; set; }
            public string unidade { get; set; }
            public decimal? qtProduto { get; set; }
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
        public string jsParcelas { get; set; }
        //public List<Shared.ParcelasVM> ParcelasCompra
        //{
        //    get
        //    {
        //        if (string.IsNullOrEmpty(jsParcelas))
        //            return new List<Shared.ParcelasVM>();
        //        return JsonConvert.DeserializeObject<List<Shared.ParcelasVM>>(jsParcelas);
        //    }
        //    set
        //    {
        //        jsParcelas = JsonConvert.SerializeObject(value);
        //    }
        //}
    }
}