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

		public List<CondPagamentoParcela> CondicaoForma { get; set; }


		public DateTime? dtCadastro { get; set; }

		public DateTime? dtUltAlteracao { get; set; }



	}
}