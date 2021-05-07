using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SistemaBarbearia.ViewModels.Fornecedor
{
    public class FornecedorVM
    {
        [Display(Name = "Codigo")]
        public int Id { get; set; }

        [Display(Name = "Razao Social")]
        [StringLength(50, MinimumLength = 3)]
        [Required(ErrorMessage = "Campo Razao Social não Pode ser em Branco!", AllowEmptyStrings = false)]
        public string nmRazaoSocial { get; set; }

        [Display(Name = "Nome Fantasia")]
        [StringLength(50, MinimumLength = 3)]
        [Required(ErrorMessage = "Campo Nome Fantasia não Pode ser em Branco!", AllowEmptyStrings = false)]
        public string nmFantasia { get; set; }

        [Display(Name = "Tipo Pessoa")]
        [Required(ErrorMessage = "Campo Tipo Pessoa não Pode ser em Branco!", AllowEmptyStrings = false)]
        public string flTipoPessoa { get; set; }


        [Display(Name = "Contatos")]
        [StringLength(10, MinimumLength = 3)]
        [Required(ErrorMessage = "Campo Contatos não Pode ser em Branco!", AllowEmptyStrings = false)]
        public string nrContatos { get; set; }


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


        [Display(Name = "CNPJ")]
        [StringLength(11, MinimumLength = 0)]
        [Required(ErrorMessage = "Campo CNPJ não Pode ser em Branco!", AllowEmptyStrings = false)]
        public string nrCNPJ { get; set; }

        [Display(Name = "Insc. Estadual")]
        [StringLength(11, MinimumLength = 0)]
        [Required(ErrorMessage = "Campo Inscricao Estadual não Pode ser em Branco!", AllowEmptyStrings = false)]
        public string nrInscEstadual { get; set; }

        [Display(Name = "Data de Cadastro")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? dtCadastro { get; set; }

        [Display(Name = "Data de Ult. Alteracao")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? dtUltAlteracao { get; set; }
    }
}