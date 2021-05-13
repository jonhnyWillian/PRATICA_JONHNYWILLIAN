using SistemaBarbearia.Models.Estados;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SistemaBarbearia.ViewModels.Paises
{
    public class CidadeVM
    {
        [Display(Name = "Codigo")]
        public int Id { get; set; }

        [Display(Name = "Cidade")]
        [Required(ErrorMessage = "Campo Cidade não Pode ser em Branco!", AllowEmptyStrings = false)]
        public string nmCidade { get; set; }


        [Display(Name = "Estado")]
        [Required(ErrorMessage = "Campo Estado não Pode ser em Branco!", AllowEmptyStrings = false)]
        public Estado estado { get; set; }

        [Display(Name = "Data de Cadastro")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? dtCadastro { get; set; }

        [Display(Name = "Data de Ult. Alteracao")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? dtUltAlteracao { get; set; }
    }
}