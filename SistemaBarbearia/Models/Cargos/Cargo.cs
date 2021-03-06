using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SistemaBarbearia.Models.Cargos
{
    public class Cargo
    {
        [Key]
        public int IdCargo { get; set; }
        [Required]
        public string dsCargo { get; set; }

        public string flSituacao { get; set; }

        
        public DateTime? dtCadastro { get; set; }

        public DateTime? dtUltAlteracao { get; set; }
    }
}