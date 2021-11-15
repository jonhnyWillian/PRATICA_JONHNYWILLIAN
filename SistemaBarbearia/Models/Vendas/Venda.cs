using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SistemaBarbearia.Models.Vendas
{
    public class Venda
    {
       
        public string nrModelo { get; set; }
        public string nrNota { get; set; }
        public string nrSerie { get; set; }
        public DateTime? dtNota { get; set; }

        public ViewModels.CondPagamentos.SelectCondPagamentoVM CondicaoPagamento { get; set; }
        public int IdCondPagamento { get; set; }

        public class ParcelasVM
        {
            public int? IdFormaPagamento { get; set; }
            public string dsFormaPagamento { get; set; }
            public DateTime? dtVencimento { get; set; }
            public decimal vlParcela { get; set; }
            public double? nrParcela { get; set; }
            public DateTime? dtPagamento { get; set; }
        }
        public string jsParcelas { get; set; }
        public List<ParcelasVM> ParcelasVenda
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