using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SistemaBarbearia.Models.Paises
{
    public class Pais
    {
        [Key]
        public int IdPais { get; set; }
        [Required]
        public string nmPais { get; set; }
        [Required]
        public string dsSigla { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? dtCadastro { get; set; }

        public DateTime? dtUltAlteracao { get; set; }

    }
}