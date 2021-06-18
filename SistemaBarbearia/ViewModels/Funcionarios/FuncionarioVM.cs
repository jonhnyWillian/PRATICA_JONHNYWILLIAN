﻿using SistemaBarbearia.Models.Cargos;
using SistemaBarbearia.Models.Cidades;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SistemaBarbearia.ViewModels.Funcionarios
{
    public class FuncionarioVM
    {
        [Display(Name = "Codigo")]
        public int IdFuncionario { get; set; }

        [Display(Name = "Funcionario")]
        [StringLength(50, MinimumLength = 3)]
        [Required(ErrorMessage = "Campo Funcionario não Pode ser em Branco!", AllowEmptyStrings = false)]
        public string nmFuncionario { get; set; }

        [Display(Name = "Apelido")]
        [StringLength(50, MinimumLength = 3)]
        [Required(ErrorMessage = "Campo Apelido não Pode ser em Branco!", AllowEmptyStrings = false)]
        public string nmApelido { get; set; }

        [Display(Name = "Login")]
        [StringLength(50, MinimumLength = 3)]
        [Required(ErrorMessage = "Campo Login não Pode ser em Branco!", AllowEmptyStrings = false)]
        public string dsLogin { get; set; }

        [Display(Name = "Senha")]
        [StringLength(50, MinimumLength = 3)]
        [Required(ErrorMessage = "Campo Senha não Pode ser em Branco!", AllowEmptyStrings = false)]
        public string senha { get; set; }

        [Display(Name = "Sexo")]
        [Required(ErrorMessage = "Campo Sexo não Pode ser em Branco!", AllowEmptyStrings = false)]
        public string flSexo { get; set; }

        public static SelectListItem[] Sexo
        {
            get
            {
                return new[]
                {
                    new SelectListItem { Text = "Feminino", Value = "F" },
                     new SelectListItem { Text = "Masculino", Value = "M" }
                };
            }
        }


        [Display(Name = "Telefone")]
        [StringLength(10, MinimumLength = 3)]
        [Required(ErrorMessage = "Campo Telefone não Pode ser em Branco!", AllowEmptyStrings = false)]
        public string nrTelefone { get; set; }

        [Display(Name = "Celular")]
        [StringLength(10, MinimumLength = 3)]
        [Required(ErrorMessage = "Campo Celular não Pode ser em Branco!", AllowEmptyStrings = false)]
        public string nrCelular { get; set; }

        [Display(Name = "CEP")]
        [StringLength(50, MinimumLength = 3)]
        [Required(ErrorMessage = "Campo CEP não Pode ser em Branco!", AllowEmptyStrings = false)]
        public string nrCEP { get; set; }

        [Display(Name = "Complemento")]
        [StringLength(50, MinimumLength = 3)]
        [Required(ErrorMessage = "Campo Complemento não Pode ser em Branco!", AllowEmptyStrings = false)]
        public string dsComplemento { get; set; }

        [Display(Name = "Bairro")]
        [StringLength(50, MinimumLength = 3)]
        [Required(ErrorMessage = "Campo Bairro não Pode ser em Branco!", AllowEmptyStrings = false)]
        public string dsBairro { get; set; }

        [Display(Name = "Lougradouro")]
        [StringLength(50, MinimumLength = 3)]
        [Required(ErrorMessage = "Campo Lougradouro não Pode ser em Branco!", AllowEmptyStrings = false)]
        public string dsLougradouro { get; set; }

        [Display(Name = "Nº")]
        [StringLength(10, MinimumLength = 3)]
        [Required(ErrorMessage = "Campo Residencial não Pode ser em Branco!", AllowEmptyStrings = false)]
        public string nrResidencial { get; set; }

        [Display(Name = "Cidade")]
        [StringLength(10, MinimumLength = 3)]
        [Required(ErrorMessage = "Campo Cidade não Pode ser em Branco!", AllowEmptyStrings = false)]
        public SistemaBarbearia.ViewModels.Cidades.SelectCidadeVM cidade { get; set; }

        [Display(Name = "Cargo")]
        [StringLength(10, MinimumLength = 3)]
        [Required(ErrorMessage = "Campo Cargo não Pode ser em Branco!", AllowEmptyStrings = false)]
        public SistemaBarbearia.ViewModels.Cargos.SelectCargoVM cargos { get; set; }


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

        [Display(Name = "Salário")]
        [StringLength(11, MinimumLength = 0)]
        [Required(ErrorMessage = "Campo Salario não Pode ser em Branco!", AllowEmptyStrings = false)]
        public Decimal? vlSalario { get; set; }

        [Display(Name = "Data de Nacimento")]
        [DataType(DataType.Date, ErrorMessage = "Data em formato inválido")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? dtNasc { get; set; }

        [Display(Name = "Data de Adimissao")]
        [DataType(DataType.Date, ErrorMessage = "Data em formato inválido")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? dtAdimissao { get; set; }

        [Display(Name = "Data de Demissao")]
        [DataType(DataType.Date, ErrorMessage = "Data em formato inválido")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? dtDemissao { get; set; }

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