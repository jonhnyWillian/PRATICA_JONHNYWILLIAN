using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SistemaBarbearia.ViewModels.Servicos
{
    public class ServicoVM
    {
        [Display(Name = "Codigo")]
        public int IdServico { get; set; }

        [Display(Name = "Serviço")]
        [Required(ErrorMessage = "Campo nome do Serviço não pode ser em branco")]
        public string dsServico { get; set; }

        [Display(Name = "Valor Serviço")]
        [Required(ErrorMessage = "Campo de Valor Serviço não pode ser em branco")]
        public decimal? vlServico { get; set; }

        [Display(Name = "Situação")]
        [Required(ErrorMessage = "Campo nome do Cargo não pode ser em branco")]
        public string flSituacao { get; set; }

        public static SelectListItem[] Situacao
        {
            get
            {
                return new[]
                {
                    new SelectListItem { Value = "A", Text = "ATIVA" },
                    new SelectListItem { Value = "I", Text = "INATIVA" }
                };
            }
        }

        [Display(Name = "Data de Cadastro")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? dtCadastro { get; set; }

        [Display(Name = "Data de Ult. Alteracao")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? dtUltAlteracao { get; set; }

       
    }
}