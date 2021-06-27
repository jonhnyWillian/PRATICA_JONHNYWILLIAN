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

		[Key]
		public int idCondPag { get; set; }

		public string dsCondPag { get; set; }

		public decimal txJuro { get; set; }

		public decimal txMulta { get; set; }

		public decimal txDesconto { get; set; }

		public FormaPagamento formaPagamento { get; set; }
		public int idFormaPagamento { get; set; }

		public DateTime? dtCadastro { get; set; }

		public DateTime? dtUltAlteracao { get; set; }

	}
}