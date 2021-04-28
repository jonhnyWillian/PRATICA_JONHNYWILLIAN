using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SistemaBarbearia.ViewModels.Servico
{
    public class ServicoVM
    {
        [Display(Name = "ID")]
        public int Id { get; set; }

        [Display(Name = "Serviço")]
        [Required(ErrorMessage = "Informe o nome do Serviço. Nome do Serviço não pode ser em branco")]
        public string dsServico { get; set; }

        [Display(Name = "Valor Serviço")]
        [Required(ErrorMessage = "Informe o a Valor Serviço . Valor Serviço não pode ser em branco")]
        public decimal? vlServico { get; set; }

        [Display(Name = "Data de Cadastro")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? dtCadastro { get; set; }

        [Display(Name = "Data de Ult. Alteracao")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? dtUltAlteracao { get; set; }

       
    }
}