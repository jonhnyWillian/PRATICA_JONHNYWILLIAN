using SistemaBarbearia.Models.Cidades;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SistemaBarbearia.Models.Fornecedor
{
    public class Fornecedor
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string nmRazaoSocial { get; set; }

        [Required]
        public string nmFantasia { get; set; }

        [Required]
        public string nmFornecedor { get; set; }

        [Required]
        public string nmApelido { get; set; }

        [Required]
        public string nrContato { get; set; }

        [Required]
        public string nrTelefone { get; set; }

        [Required]
        public string nrCelular { get; set; }

        [Required]
        public string nrCEP { get; set; }

        [Required]
        public string dsLougradouro { get; set; }

        [Required]
        public string nrResidencial { get; set; }

        [Required]
        public string dsBairro { get; set; }

        [Required]
        public string dsComplemento { get; set; }

        [Required]
        public string dsEmail { get; set; }

        [Required]
        public string nrCNPJ { get; set; }

        [Required]
        public string nrInscEstadual { get; set; }

        [Required]
        public string nrCPF{ get; set; }

        [Required]
        public string nrRG { get; set; }

       

        public int? idCidade { get; set; }

        public Cidade cidade { get; set; }

        //public CondPagamento condPg {get; set;}

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? dtCadastro { get; set; }

        public DateTime? dtUltAlteracao { get; set; }
    }
}