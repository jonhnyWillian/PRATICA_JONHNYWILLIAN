using SistemaBarbearia.Models.FormaPagamentos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SistemaBarbearia.ViewModels.CondPagamentos
{
    public class CondPagamentoVM
    {
		[Display(Name = "Codigo")]
		public int idCondPag { get; set; }

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

		[Display(Name = "Forma Pagamento")]
		public SistemaBarbearia.ViewModels.FormaPagamentos.SelectFormaPagamentoVM formaPagamento { get; set; }

		[Display(Name = "Data de Cadastro")]
		[DataType(DataType.Date, ErrorMessage = "Data em formato inválido")]
		[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
		public DateTime? dtCadastro { get; set; }

		[Display(Name = "Data de Ult. Alteracao")]
		[DataType(DataType.Date, ErrorMessage = "Data em formato inválido")]
		[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
		public DateTime? dtUltAlteracao { get; set; }
	}
}