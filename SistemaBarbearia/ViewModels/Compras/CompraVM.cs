using Newtonsoft.Json;
using SistemaBarbearia.ViewModels.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SistemaBarbearia.ViewModels.Compras
{
    public class CompraVM  : ModelPaiVM
    {

        [Display(Name = "Modelo")]
        [StringLength(50, MinimumLength = 3)]
        [Required(ErrorMessage = "Campo Modelo não Pode ser em Branco!")]
        public short nrModelo { get; set; }

        [Display(Name = "Serie")]
        [StringLength(50, MinimumLength = 3)]
        [Required(ErrorMessage = "Campo Serie não Pode ser em Branco!")]
        public short nrSerie { get; set; }

        [Display(Name = "Nº Nota")]
        [StringLength(50, MinimumLength = 3)]
        [Required(ErrorMessage = "Campo Apelido não Pode ser em Branco!")]
        public int nrNota { get; set; }

        [Display(Name = "Dt. Emissão")]
        [DataType(DataType.Date, ErrorMessage = "Data em formato inválido")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? dtEmissao { get; set; }


        [Display(Name = "Dt. Entrega")]
        [DataType(DataType.Date, ErrorMessage = "Data em formato inválido")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? dtEntrega { get; set; }

        public ViewModels.Fornecedores.SelectFornecedorVM Fornecedor { get; set; }
        public ViewModels.CondPagamentos.SelectCondPagamentoVM CondicaoPagamento { get; set; }
        public ViewModels.Produtos.SelectProdutoVM Produto { get; set; }


        [Display(Name = "Unidade")]
        [StringLength(50, MinimumLength = 3)]
        [Required(ErrorMessage = "Campo Unidade não Pode ser em Branco!")]
        public string unidade { get; set; }

        public string finalizar { get; set; }
        public decimal? vlTotal { get; set; }

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