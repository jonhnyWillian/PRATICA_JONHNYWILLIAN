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
        //[Required(ErrorMessage = "Por favor, informe o Nome!")]
        public string nmPessoa { get; set; }

        [Display(Name = "Telefone")]
        //[Required(ErrorMessage = "Por favor, informe o Telefone!")]
        public string nrTelefone { get; set; }

        [Display(Name = "Celular")]
        //[Required(ErrorMessage = "Por favor, informe o Celular!")]
        public string nrCelular { get; set; }

        [Display(Name = "E-mail")]
        //[Required(ErrorMessage = "Por favor, informe o E-mail!")]
        public string dsEmail { get; set; }

        [Display(Name = "Tipo")]
        //[Required(ErrorMessage = "Por favor, informe o Tipo!")]
        public string flTipo { get; set; }

        [Display(Name = "Situação")]
        //[Required(ErrorMessage = "Por favor, informe o Situação!")]
        public string flSituacao { get; set; }

        [Display(Name = "CEP")]
        //[Required(ErrorMessage = "Por favor, informe o CEP!")]
        public string nrCEP { get; set; }

        [Display(Name = "Logradouro")]
        //[Required(ErrorMessage = "Por favor, informe o Logradouro!")]
        public string dsLogradouro { get; set; }

        [Display(Name = "Número")]
        //[Required(ErrorMessage = "Por favor, informe o Nº!")]
        public string nrResidencial { get; set; }

        [Display(Name = "Bairro")]
        //[Required(ErrorMessage = "Por favor, informe o Bairro!")]
        public string dsBairro { get; set; }

        [Display(Name = "Complemento")]
        //[Required(ErrorMessage = "Por favor, informe o Complemento!")]
        public string dsComplemento { get; set; }

        [Display(Name = "Cidade")]
        //[Required(ErrorMessage = "Por favor, informe o Cidade!")]
        public int idCidade { get; set; }

        [Display(Name = "Condição Pagamento")]
        //[Required(ErrorMessage = "Por favor, informe o Condição Pagamento!")]
        public int idCondPagamento { get; set; }

        [Display(Name = "Cargo")]
        //[Required(ErrorMessage = "Por favor, informe o Cargo!")]
        public int idCargo { get; set; }

        public SistemaBarbearia.ViewModels.Cargos.SelectCargoVM Cargo { get; set; }

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