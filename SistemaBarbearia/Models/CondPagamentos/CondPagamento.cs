using Newtonsoft.Json;
using SistemaBarbearia.Models.FormaPagamentos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SistemaBarbearia.Models.CondPagamento
{
	public class CondPagamento
	{

		
		public int IdCondPag { get; set; }

		public string dsCondPag { get; set; }

		public decimal txJuro { get; set; }

		public decimal txMulta { get; set; }

		public CondPagamento()
		{
			this.CondicaoForma = new List<CondPagamentoParcela>();
		}

		public ICollection<CondPagamentoParcela> CondicaoForma { get; set; }

		//public class CondPagamentoParcelaVM
		//{
		//	public int IdCondPag { get; set; }
		//	public short? nrParcela { get; set; }
		//	public short? qtdDias { get; set; }
		//	public decimal? txPercentual { get; set; }
		//	public int? IdFormaPagamento { get; set; }
		//	public string dsFormaPagamento { get; set; }
		//}


		//public string jsItens { get; set; }
		//public List<CondPagamentoParcelaVM> ListCondicao
		//{
		//	get
		//	{
		//		if (string.IsNullOrEmpty(jsItens))
		//			return new List<CondPagamentoParcelaVM>();
		//		return JsonConvert.DeserializeObject<List<CondPagamentoParcelaVM>>(jsItens);
		//	}
		//	set
		//	{
		//		jsItens = JsonConvert.SerializeObject(value);
		//	}
		//}


		public DateTime? dtCadastro { get; set; }

		public DateTime? dtUltAlteracao { get; set; }



	}
}