using SistemaBarbearia.Models.Estados;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SistemaBarbearia.Models.Cidades
{
    public class Cidade
    {
        [Key]
        public int IdCidade { get; set; }
        [Required]
        public string nmCidade { get; set; }

        [Required]
        public string DDD { get; set; }

        [Required]
        public int IdEstado { get; set; }

        public SistemaBarbearia.ViewModels.Estados.SelectEstadoVM estado { get; set; }


        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? dtCadastro { get; set; }

        public DateTime? dtUltAlteracao { get; set; }
    }
}