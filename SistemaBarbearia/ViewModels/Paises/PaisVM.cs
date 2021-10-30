using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SistemaBarbearia.ViewModels.Paises
{
    public class PaisVM
    {
        [Display(Name = "Codigo")]
        public int IdPais { get; set; }

        [Display(Name = "Pais")]
        [StringLength(50, MinimumLength = 3)]
        //[RegularExpression("([A-Z])", ErrorMessage = "Somente Letras")]
        [Required(ErrorMessage = "Campo Pais não Pode ser em Branco!", AllowEmptyStrings = false)]
        public string nmPais { get; set; }

        [Display(Name = "Sigla")]
        [StringLength(4, MinimumLength = 2)]
        //[Required(ErrorMessage = "Campo Sigla não Pode ser em Branco!", AllowEmptyStrings = false)]
        public string dsSigla { get; set; }

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