using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SistemaBarbearia.ViewModels.Pessoa
{
    public class FuncionarioVM
    {
        [Display(Name = "Codigo")]
        public int Id { get; set; }

        [Display(Name = "Funcionario")]
        [StringLength(50, MinimumLength = 3)]
        [Required(ErrorMessage = "Campo Funcionario não Pode ser em Branco!", AllowEmptyStrings = false)]
        public string nmCliente { get; set; }

        [Display(Name = "Apelido")]
        [StringLength(50, MinimumLength = 3)]
        [Required(ErrorMessage = "Campo Apelido não Pode ser em Branco!", AllowEmptyStrings = false)]
        public string nmApelido { get; set; }

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

        [Display(Name = "Data de Adimissao")]
        [Required(ErrorMessage = "Campo Data de Adimissao não Pode ser em Branco!", AllowEmptyStrings = false)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? dataAdimissao { get; set; }

        [Display(Name = "Data de Demissao")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? dtDemissao { get; set; }

        [Display(Name = "Data de Nascimento")]
        [Required(ErrorMessage = "Campo Data de Nascimento não Pode ser em Branco!", AllowEmptyStrings = false)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? dataNasc { get; set; }

        [Display(Name = "Data de Cadastro")]
        [Required(ErrorMessage = "Campo Data de Cadastro não Pode ser em Branco!", AllowEmptyStrings = false)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? dtCadastro { get; set; }

        [Display(Name = "Data de Ult. Alteracao")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? dtUltAlteracao { get; set; }
    }
}