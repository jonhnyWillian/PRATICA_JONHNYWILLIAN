using SistemaBarbearia.Models.Paises;
using SistemaBarbearia.ViewModels.Paises;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SistemaBarbearia.ViewModels.Estados
{
    public class EstadoVM
    {
        [Display(Name = "Codigo")]
        public int IdEstado { get; set; }

        [Display(Name = "Estado")]
        [Required(ErrorMessage = "Informe o nome do Estado. Nome do Estado não pode ser em branco")]
        public string nmEstado { get; set; }

        [Display(Name = "UF")]
        [Required(ErrorMessage = "Campo UF não Pode ser em Branco!", AllowEmptyStrings = false)]
        public string dsUF { get; set; }

        public int IdPais { get; set; }
        public SistemaBarbearia.ViewModels.Paises.SelectPaisVM Pais { get; set; }

        [Display(Name = "Data de Cadastro")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? dtCadastro { get; set; }

        [Display(Name = "Data de Ult. Alteracao")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? dtUltAlteracao { get; set; }

        
    }
}