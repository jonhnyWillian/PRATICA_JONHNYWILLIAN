using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SistemaBarbearia.Models.FormaPagamentos
{
    public class FormaPagamento
    {

        [Key]
        public int IdFormaPag { get; set; }

        [Required]
        public string dsFormaPagamento { get; set; }

        public string flSituacao { get; set; }

        public DateTime? dtCadastro { get; set; }

        public DateTime? dtUltAlteracao { get; set; }
    }
}