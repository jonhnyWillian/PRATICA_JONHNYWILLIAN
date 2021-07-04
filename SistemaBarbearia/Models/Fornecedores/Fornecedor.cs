using SistemaBarbearia.Models.Cidades;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SistemaBarbearia.Models.Fornecedores
{
    public class Fornecedor
    {
        [Key]
        public int IdFornecedor { get; set; }

        
        public string nmNome { get; set; }

        public string dsNome { get; set; }


        public string nrContato { get; set; }

        public string nrTelefone { get; set; }

        
        public string nrCelular { get; set; }

        
        public string nrCEP { get; set; }

        
        public string dsLogradouro { get; set; }

        
        public string nrResidencial { get; set; }

        
        public string dsBairro { get; set; }

        
        public string dsComplemento { get; set; }

        
        public string dsEmail { get; set; }

        
        public string nrCPFCNPJ { get; set; }

        
        public string nrRGIE { get; set; }

        
        public string dsSite { get; set; }

        public int idCidade { get; set; }


        public int idCondPagamento { get; set; }

        public SistemaBarbearia.ViewModels.Cidades.SelectCidadeVM cidade { get; set; }

        public SistemaBarbearia.ViewModels.CondPagamentos.SelectCondPagamentoVM CondPagamento { get; set;}

        public string flSexo { get; set; }

        public string flTipo { get; set; }

        public const string TIPO_FISICA = "F";
        public const string TIPO_JURIDICA = "J";

        public DateTime? dtNasc { get; set; }


        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? dtCadastro { get; set; }



        public DateTime? dtUltAlteracao { get; set; }
    }
}