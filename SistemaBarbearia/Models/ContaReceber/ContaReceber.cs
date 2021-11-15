using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SistemaBarbearia.Models.ContaReceber
{
    public class ContaReceber
    {

        public ViewModels.FormaPagamentos.SelectFormaPagamentoVM FormaPag { get; set; }
        public ViewModels.Clientes.SelectClienteVM Cliente { get; set; }

        public string nrModelo { get; set; }

        public string nrSerie { get; set; }

        public int nrNota { get; set; }


        [Display(Name = "Nº parcela")]
        public int nrParcela { get; set; }

        [Display(Name = "Valor da parcela")]
        public decimal vlParcela { get; set; }

        [Display(Name = "Data de vencimento")]
        public DateTime dtVencimento { get; set; }

        [Display(Name = "Situação")]
        public string flSituacao { get; set; }

        [Display(Name = "Data de pagamento")]
        public DateTime? dtPagamento { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? dtEmissao { get; set; }

        public ViewModels.ContasBancos.SelectContaBancoVM ContaBancaria { get; set; }
    }
}