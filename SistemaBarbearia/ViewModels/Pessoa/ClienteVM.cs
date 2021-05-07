﻿using System;
using System.ComponentModel.DataAnnotations;

namespace SistemaBarbearia.ViewModels.Pessoa
{
    public class ClienteVM
    {

        [Display(Name = "Codigo")]
        public int Id { get; set; }

        [Display(Name = "Cliente")]
        [StringLength(50, MinimumLength = 3)]
        [Required(ErrorMessage = "Campo Cliente não Pode ser em Branco!", AllowEmptyStrings = false)]
        public string nmCliente { get; set; }

        [Display(Name = "Apelido")]
        [StringLength(50, MinimumLength = 3)]
        [Required(ErrorMessage = "Campo Apelido não Pode ser em Branco!", AllowEmptyStrings = false)]
        public string nmApelido { get; set; }

        [Display(Name = "Tipo Pessoa")]
        [Required(ErrorMessage = "Campo Tipo Pessoa não Pode ser em Branco!", AllowEmptyStrings = false)]
        public string flTipoPessoa { get; set; }

        [Display(Name = "Sexo")]
        [Required(ErrorMessage = "Campo Sexo não Pode ser em Branco!", AllowEmptyStrings = false)]
        public string flSexo { get; set; }

        [Display(Name = "Telefone")]
        [StringLength(10, MinimumLength = 3)]
        [Required(ErrorMessage = "Campo Telefone não Pode ser em Branco!", AllowEmptyStrings = false)]
        public string nrTelefone { get; set; }

        [Display(Name = "Celular")]
        [StringLength(10, MinimumLength = 3)]
        [Required(ErrorMessage = "Campo Celular não Pode ser em Branco!", AllowEmptyStrings = false)]
        public string nrCelular { get; set; }

        [Display(Name = "Complemento")]
        [StringLength(50, MinimumLength = 3)]
        [Required(ErrorMessage = "Campo Complemento não Pode ser em Branco!", AllowEmptyStrings = false)]
        public string dsComplemento { get; set; }

        [Display(Name = "Lougradouro")]
        [StringLength(50, MinimumLength = 3)]
        [Required(ErrorMessage = "Campo Lougradouro não Pode ser em Branco!", AllowEmptyStrings = false)]
        public string dsLougradouro { get; set; }

        [Display(Name = "Residencial")]
        [StringLength(10, MinimumLength = 3)]
        [Required(ErrorMessage = "Campo Residencial não Pode ser em Branco!", AllowEmptyStrings = false)]
        public string nrResidencial { get; set; }

        [Display(Name = "E-mail")]
        [StringLength(50, MinimumLength = 3)]
        [Required(ErrorMessage = "Campo Email não Pode ser em Branco!", AllowEmptyStrings = false)]
        public string dsEmail { get; set; }

        [Display(Name = "CPF")]
        [StringLength(11, MinimumLength = 0)]
        [Required(ErrorMessage = "Campo CPF não Pode ser em Branco!", AllowEmptyStrings = false)]
        public string nrCPF { get; set; }

        [Display(Name = "RG")]
        [StringLength(11, MinimumLength = 0)]
        [Required(ErrorMessage = "Campo RG não Pode ser em Branco!", AllowEmptyStrings = false)]
        public string nrRG { get; set; }

        [Display(Name = "CNPJ")]
        [StringLength(11, MinimumLength = 0)]
        [Required(ErrorMessage = "Campo CNPJ não Pode ser em Branco!", AllowEmptyStrings = false)]
        public string nrCNPJ { get; set; }

        [Display(Name = "Insc. Estadual")]
        [StringLength(11, MinimumLength = 0)]
        [Required(ErrorMessage = "Campo Inscricao Estadual não Pode ser em Branco!", AllowEmptyStrings = false)]
        public string nrInscEstadual { get; set; }

        [Display(Name = "Data de Nascimento")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? dataNasc { get; set; }

        [Display(Name = "Data de Cadastro")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? dtCadastro { get; set; }

        [Display(Name = "Data de Ult. Alteracao")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? dtUltAlteracao { get; set; }
    }
}