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

		[Display(Name = "Desconto")]
		[Required(ErrorMessage = "Campo Desconto não Pode ser em Branco!", AllowEmptyStrings = false)]
		public decimal txDesconto { get; set; }

		[Display(Name = "Dias")]
		[Required(ErrorMessage = "Campo Qtd. Parcela não Pode ser em Branco!", AllowEmptyStrings = false)]
		public short? qtdDias { get; set; }

		[Display(Name = "Porcentagem (%)")]
		[Required(ErrorMessage = "Campo Qtd. Parcela não Pode ser em Branco!", AllowEmptyStrings = false)]
		public short? txPercentual { get; set; }


		[Display(Name = "Total (%)")]
		public decimal? txPercentualTotal { get; set; }
		public decimal? txPercentualTotalAux { get; set; }

		[Display(Name = "Forma Pagamento")]
		public SistemaBarbearia.ViewModels.FormaPagamentos.SelectFormaPagamentoVM formaPagamento { get; set; }
		public static SelectListItem[] Juros
		{
			get
			{
				return new[]
				{
					new SelectListItem { Value = "N", Text = "NÃO" },
					new SelectListItem { Value = "S", Text = "SIM" },

				};
			}
		}
		public static SelectListItem[] Parcela
		{
			get
			{
				return new[]
				{
					new SelectListItem { Value = "N", Text = "NÃO" },
					new SelectListItem { Value = "S", Text = "SIM" },
				};
			}
		}
		public class CondPagamentoParcelaVM
		{
		
			public short? nrParcela { get; set; }
			public short? qtdDias { get; set; }
			public decimal? txPercentual { get; set; }
			public int? IdFormaPagto { get; set; }
			public string nmFormaPagto { get; set; }
		}


		public string jsItens { get; set; }
		public List<CondPagamentoParcelaVM> ListCondicao
		{
			get
			{
				if (string.IsNullOrEmpty(jsItens))
					return new List<CondPagamentoParcelaVM>();
				return JsonConvert.DeserializeObject<List<CondPagamentoParcelaVM>>(jsItens);
			}
			set
			{
				jsItens = JsonConvert.SerializeObject(value);
			}
		}

	}
}