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
		//[Required(ErrorMessage = "Campo Condição Pagamento não Pode ser em Branco!", AllowEmptyStrings = false)]
		public string dsCondPag { get; set; }

		[Display(Name = "Juro")]		
		//[Required(ErrorMessage = "Campo Juro não Pode ser em Branco!", AllowEmptyStrings = false)]
		public decimal txJuro { get; set; }

		[Display(Name = "Multa")]
		//[Required(ErrorMessage = "Campo Multa não Pode ser em Branco!", AllowEmptyStrings = false)]
		public decimal? txMulta { get; set; }

		[Display(Name = "Desconto")]
		//[Required(ErrorMessage = "Campo Desconto não Pode ser em Branco!", AllowEmptyStrings = false)]
		public decimal txDesconto { get; set; }

		[Display(Name = "Dias")]
		//[Required(ErrorMessage = "Campo Qtd. Parcela não Pode ser em Branco!", AllowEmptyStrings = false)]
		public short? qtdDias { get; set; }

		[Display(Name = "Porcentagem (%)")]
		//[Required(ErrorMessage = "Campo Qtd. Parcela não Pode ser em Branco!", AllowEmptyStrings = false)]
		public decimal? txPercentual { get; set; }


		[Display(Name = "Total (%)")]
		public decimal? txPercentualTotal { get; set; }
		public decimal? txPercentualTotalAux { get; set; }

		[Display(Name = "Forma Pagamento")]
		public SistemaBarbearia.ViewModels.FormaPagamentos.SelectFormaPagamentoVM formaPagamento { get; set; }

		public DataTablesList<CodicaoFormaVM> Itens { get; set; }

		public class CodicaoFormaVM
		{
			public int? IdCondPag { get; set; }
			public short? nrParcela { get; set; }
			public short? qtdDias { get; set; }
			public decimal? txPercentual { get; set; }
			public int? idFormaPagamento { get; set; }
			public string dsFormaPagamento { get; set; }
		}
		public CondPagamento GetPagamento(CondPagamento bean)
		{

			bean.dsCondPag = this.dsCondPag;		
			bean.txJuro = this.txJuro ;
			bean.txMulta = this.txMulta ?? 0;
			bean.dtCadastro = Convert.ToDateTime(this.dtCadastro);
			bean.dtUltAlteracao = Convert.ToDateTime(this.dtUltAlteracao);
			bean.CondicaoForma = this.ListCondicao;


			return bean;
		}
      

        public string jsItens { get; set; }
        public List<CondPagamentoParcela> ListCondicao
        {
            get
            {
                if (string.IsNullOrEmpty(jsItens))
                    return new List<CondPagamentoParcela>();
                return JsonConvert.DeserializeObject<List<CondPagamentoParcela>>(jsItens);
            }
            set
            {
                jsItens = JsonConvert.SerializeObject(value);
            }
        }

		public class ParcelasVM
		{
			public int? idFormaPag { get; set; }
			public string dsFormaPagamento { get; set; }
			public DateTime? dtVencimento { get; set; }
			public decimal vlParcela { get; set; }
			public double? nrParcela { get; set; }
			public DateTime? dtPagamento { get; set; }
		}
		public string jsParcelas { get; set; }
		public List<ParcelasVM> ParcelasCompra
		{
			get
			{
				if (string.IsNullOrEmpty(jsParcelas))
					return new List<ParcelasVM>();
				return JsonConvert.DeserializeObject<List<ParcelasVM>>(jsParcelas);
			}
			set
			{
				jsParcelas = JsonConvert.SerializeObject(value);
			}
		}

	}
}