using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SistemaBarbearia.Models.CondPagamento
{
    public class CondPagamentoParcela
    {
        public int? idCondicaoPagParc { get; set; }
        public short? nrParcela { get; set; }
        public short? qtdDias { get; set; }
        public decimal? txPercentual { get; set; }
        public int? idFormaPagto { get; set; }
        public string nmFormaPagto { get; set; }

        public SistemaBarbearia.ViewModels.FormaPagamentos.SelectFormaPagamentoVM FormaPagatento { get; set; }

        public CondPagamento CondicaoPagamento { get; set; }
    }
}