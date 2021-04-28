using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SistemaBarbearia.Models.Servicos
{
    public class Servico
    {

        [Key]
        public int Id { get; set; }
        [Required]
        public string dsServico { get; set; }
        [Required]
        public decimal? vlServico { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? dtCadastro { get; set; }

        public DateTime? dtUltAlteracao { get; set; }
    }
}