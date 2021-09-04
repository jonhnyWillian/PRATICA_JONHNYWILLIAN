using SistemaBarbearia.ViewModels.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using System.Web.Mvc;
using SistemaBarbearia.Models.CondPagamento;
using SistemaBarbearia.Helper;


namespace SistemaBarbearia.ViewModels.CondPagamentos
{
    public class CondPagamentoVM : ModelPaiVM
    {

		[Display(Name = "Condição Pagamento")]
		[StringLength(50, MinimumLength = 3)]
		[Required(ErrorMessage = "Campo Condição Pagamento não Pode ser em Branco!", AllowEmptyStrings = false)]
		public string dsCondPag { get; set; }

		[Display(Name = "Juro")]		
		[Required(ErrorMessage = "Campo Juro não Pode ser em Branco!", AllowEmptyStrings = false)]
		public decimal txJuro { get; set; }

		[Display(Name = "Multa")]
		[Required(ErrorMessage = "Campo Multa não Pode ser em Branco!", AllowEmptyStrings = false)]
		public decimal txMulta { get; set; }			

		[Display(Name = "Dias")]
		[Required(ErrorMessage = "Campo Qtd. Parcela não Pode ser em Branco!", AllowEmptyStrings = false)]
		public short? qtdDias { get; set; }

		[Display(Name = "Porcentagem (%)")]
		[Required(ErrorMessage = "Campo Qtd. Parcela não Pode ser em Branco!", AllowEmptyStrings = false)]
		public short? txPercentual { get; set; }

		[Display(Name = "Forma Pagamento")]
		public SistemaBarbearia.ViewModels.FormaPagamentos.SelectFormaPagamentoVM formaPagamento { get; set; }

		public CondPagamento GetCondPagamento (CondPagamento bean)
        {
			bean.dsCondPag = this.dsCondPag;
			bean.txJuro = this.txJuro;
			bean.txMulta = this.txMulta;
		

			foreach (var item in Itens.Get)
			{
				bean.CondPagamentoParcela.Add(new CondPagamentoParcela
				{
					idCondicaoPagParc = item.idCondicaoPagParc ?? 0,
					nrParcela = item.nrParcela ?? 0,
					qtdDias = item.qtdDias ?? 0,
					txPercentual = item.txPercentual ?? 0,
					idFormaPagto = item.idFormaPagto ?? 0,
					nmFormaPagto = item.nmFormaPagto
				});

            }

			return bean;
        }


		public SistemaBarbearia.Helper.DataTablesList<CondPagamentoParcelaVM> Itens { get; set; }
		

		public class CondPagamentoParcelaVM
		{
			public int? idCondicaoPagParc { get; set; }
			public short? nrParcela { get; set; }
			public short? qtdDias { get; set; }
			public decimal? txPercentual { get; set; }
			public int? idFormaPagto { get; set; }
			public string nmFormaPagto { get; set; }
		}
	}
}