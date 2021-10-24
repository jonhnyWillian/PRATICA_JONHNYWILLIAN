using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SistemaBarbearia.ViewModels.Agendamentos
{
    public class FinalizarHorarioVW
    {



        [Display(Name = "Modelo")]
        [StringLength(50, MinimumLength = 3)]
        [Required(ErrorMessage = "Campo Modelo não Pode ser em Branco!")]
        public string nrModelo { get; set; }
        public string nrModeloAux { get; set; }

        [Display(Name = "Situação")]
        public string flSituacao { get; set; }

        [Display(Name = "Serie")]
        [StringLength(50, MinimumLength = 3)]
        [Required(ErrorMessage = "Campo Serie não Pode ser em Branco!")]
        public string nrSerie { get; set; }
        public string nrSerieAux { get; set; }
        
        [Display(Name = "Nº Nota")]
        [StringLength(50, MinimumLength = 3)]
        [Required(ErrorMessage = "Campo Apelido não Pode ser em Branco!")]
        public int? idAgenda { get; set; }
        public int? IdAgendaAux { get; set; }

        [Display(Name = "Dt. Nota")]
        [DataType(DataType.Date, ErrorMessage = "Data em formato inválido")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? dtNota { get; set; }
        public string dtNotaAux { get; set; }


        public ViewModels.CondPagamentos.SelectCondPagamentoVM CondicaoPagamento { get; set; }
        public ViewModels.Produtos.SelectProdutoVM Produto { get; set; }
        public ViewModels.Servicos.SelectServicoVM Servico { get; set; }

        public ViewModels.Clientes.SelectClienteVM Cliente { get; set; }
        public int IdCliente { get; set; }

        public string finalizar { get; set; }

        public decimal? vlTotal { get; set; }

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
            public int? idFormaPag { get; set; }
            public string dsFormaPagamento { get; set; }
            public DateTime? dtVencimento { get; set; }
            public decimal vlParcela { get; set; }
            public double? nrParcela { get; set; }
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
       


    }
}