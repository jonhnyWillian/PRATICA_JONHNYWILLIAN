using SistemaBarbearia.Models.Paises;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SistemaBarbearia.Models.Estados
{
    public class Estado
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string nmEstado { get; set; }
        [Required]
        public string dsUF { get; set; }
        [Required]
        public int idPais { get; set; }

        public Pais pais { get; set; }
       

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? dtCadastro { get; set; }

        public DateTime? dtUltAlteracao { get; set; }
    }
}