using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SistemaBarbearia.ViewModels.Model
{
    public class ModelPaiVM
    {
        [Key]
        [Display(Name = "Código")]
        public int IdModelPai { get; set; }


        [Display(Name = "Data de Cadastro")]
        [DataType(DataType.Date, ErrorMessage = "Data em formato inválido")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? dtCadastro { get; set; }

        [Display(Name = "Data de Ult. Alteração")]
        [DataType(DataType.Date, ErrorMessage = "Data em formato inválido")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? dtUltAlteracao { get; set; }
    }
}