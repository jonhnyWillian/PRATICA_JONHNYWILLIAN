using SistemaBarbearia.ViewModels.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SistemaBarbearia.ViewModels.ContasReceber
{
    public class ContaReceberVW : ModelPaiVM
    {
        public ViewModels.FormaPagamentos.SelectFormaPagamentoVM FormaPag { get; set; }
        public ViewModels.Clientes.SelectClienteVM Cliente { get; set; }
        public ViewModels.ContasBancos.SelectContaBancoVM ContaBancaria { get; set; }


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

        [Display(Name = "Situação")]
        public string flSituacao { get; set; }

        [Display(Name = "Dt. Pagamento")]
        [DataType(DataType.Date, ErrorMessage = "Data em formato inválido")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? dtPagamento { get; set; }
    }
}