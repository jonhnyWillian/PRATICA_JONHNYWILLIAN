using SistemaBarbearia.ViewModels.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SistemaBarbearia.ViewModels.Pessoas
{
    public class PessoaVM : ModelPaiVM
    {
        [Display(Name = "Pessoa")]
        [Required(ErrorMessage = "Por favor, informe o cliente!")]
        public string nmPessoa { get; set; }

        [Display(Name = "Telefone")]
        public string nrTelefone { get; set; }

        [Display(Name = "Celular")]
        public string nrCelular { get; set; }

        [Display(Name = "E-mail")]
        public string dsEmail { get; set; }

        [Display(Name = "Tipo")]
        public string flTipo { get; set; }

        [Display(Name = "Situação")]
        public string flSituacao { get; set; }

        [Display(Name = "CEP")]
        public string nrCEP { get; set; }

        [Display(Name = "Logradouro")]
        public string dsLogradouro { get; set; }

        [Display(Name = "Número")]
        public string nrResidencial { get; set; }

        [Display(Name = "Bairro")]
        public string dsBairro { get; set; }

        [Display(Name = "Complemento")]
        public string dsComplemento { get; set; }

        [Display(Name = "Cidade")]
        public int idCidade { get; set; }

        [Display(Name = "Condição Pagamento")]
        public int IdCondPag { get; set; }

        public SistemaBarbearia.ViewModels.Cidades.SelectCidadeVM Cidade { get; set; }

        public SistemaBarbearia.ViewModels.CondPagamentos.SelectCondPagamentoVM condPagamento { get; set;  }

        public static SelectListItem[] Tipo
        {
            get
            {
                return new[]
                {
                    new SelectListItem { Value = "F", Text = "FÍSICA" },
                    new SelectListItem { Value = "J", Text = "JURÍDICA" }
                };
            }
        }

        public static SelectListItem[] Sexo
        {
            get
            {
                return new[]
                {
                    new SelectListItem { Value = "F", Text = "Feminino" },
                    new SelectListItem { Value = "M", Text = "Masculino" }
                };
            }
        }

    }
}