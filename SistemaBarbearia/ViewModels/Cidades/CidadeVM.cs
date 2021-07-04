using SistemaBarbearia.Models.Estados;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SistemaBarbearia.ViewModels.Cidades
{
    public class CidadeVM
    {
        [Display(Name = "Codigo")]
        public int IdCidade { get; set; }

        [Display(Name = "Cidade")]
        [Required(ErrorMessage = "Campo Cidade não Pode ser em Branco!")]
        public string nmCidade { get; set; }

        [Display(Name = "DDD")]
        [Required(ErrorMessage = "Campo DDD não Pode ser em Branco!")]
        public string DDD { get; set; }

        public int IdEstado { get; set; }

        public SistemaBarbearia.ViewModels.Estados.SelectEstadoVM Estado { get; set; }

        [Display(Name = "Data de Cadastro")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? dtCadastro { get; set; }

        [Display(Name = "Data de Ult. Alteracao")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? dtUltAlteracao { get; set; }
    }
}