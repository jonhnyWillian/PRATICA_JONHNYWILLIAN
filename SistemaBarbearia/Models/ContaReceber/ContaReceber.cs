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

        [Display(Name = "Nº parcela")]
        public short nrParcela { get; set; }

        [Display(Name = "Valor da parcela")]
        public decimal vlParcela { get; set; }

        [Display(Name = "Data de vencimento")]
        public DateTime dtVencimento { get; set; }

        [Display(Name = "Situação")]
        public string flSituacao { get; set; }

        [Display(Name = "Data de pagamento")]
        public DateTime? dtPagamento { get; set; }
    }
}