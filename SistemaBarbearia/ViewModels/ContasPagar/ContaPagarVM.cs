using SistemaBarbearia.ViewModels.Model;
using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace SistemaBarbearia.ViewModels.ContasPagar
{
    public class ContaPagarVM : ModelPaiVM
    {

        [Display(Name = "Situação")]
        public string flSituacao { get; set; }




        public static SelectListItem[] Situacao
        {
            get
            {
                return new[]
                {
                    new SelectListItem { Value = "A", Text = "ABERTA" },
                    new SelectListItem { Value = "P", Text = "PAGO" },
                    
                };
            }
        }


        [Display(Name = "Modelo")]
        [StringLength(50, MinimumLength = 3)]
        [Required(ErrorMessage = "Campo Modelo não Pode ser em Branco!")]
        public string nrModelo { get; set; }

        [Display(Name = "Nº Serie")]
        [StringLength(50, MinimumLength = 3)]
        [Required(ErrorMessage = "Campo Modelo não Pode ser em Branco!")]
        public string nrSerie { get; set; }

        [Display(Name = "Nº Nota")]
        [StringLength(50, MinimumLength = 3)]
        [Required(ErrorMessage = "Campo Modelo não Pode ser em Branco!")]
        public int nrNota { get; set; }

        [Display(Name = "Nº Parcela")]
        [StringLength(50, MinimumLength = 3)]
        [Required(ErrorMessage = "Campo Modelo não Pode ser em Branco!")]
        public int nrParcela { get; set; }

        [Display(Name = "Valor Parcelas")]
        [StringLength(50, MinimumLength = 3)]
        [Required(ErrorMessage = "Campo Modelo não Pode ser em Branco!")]
        public decimal? vlParcela { get; set; }

        [Display(Name = "Dt. Vencimento")]
        [DataType(DataType.Date, ErrorMessage = "Data em formato inválido")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? dtVencimento { get; set; }


        [Display(Name = "Dt. Pagamento")]
        [DataType(DataType.Date, ErrorMessage = "Data em formato inválido")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? dtPagamento { get; set; }


        [Display(Name = "Taxa de juro")]
        [StringLength(50, MinimumLength = 3)]
        [Required(ErrorMessage = "Campo Modelo não Pode ser em Branco!")]
        public decimal? txJuro { get; set; }

        [Display(Name = "Taxa de Multa")]
        [StringLength(50, MinimumLength = 3)]
        [Required(ErrorMessage = "Campo Modelo não Pode ser em Branco!")]
        public decimal? txMulta { get; set; }

        [Display(Name = "Taxa de Desconto")]
        [StringLength(50, MinimumLength = 3)]
        [Required(ErrorMessage = "Campo Modelo não Pode ser em Branco!")]
        public decimal? txDesconto { get; set; }

        [Display(Name = "Valor Pago")]
        [StringLength(50, MinimumLength = 3)]
        [Required(ErrorMessage = "Campo Modelo não Pode ser em Branco!")]
        public decimal? vlPago { get; set; }


        [Display(Name = "Dt. Emissão")]
        [DataType(DataType.Date, ErrorMessage = "Data em formato inválido")]
        //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? dtEmissao { get; set; }


        public ViewModels.Fornecedores.SelectFornecedorVM Fornecedor { get; set; }

        public ViewModels.FormaPagamentos.SelectFormaPagamentoVM formaPag { get; set; }

        public ViewModels.ContasBancos.SelectContaBancoVM ContaBancaria { get; set; }
    }
}