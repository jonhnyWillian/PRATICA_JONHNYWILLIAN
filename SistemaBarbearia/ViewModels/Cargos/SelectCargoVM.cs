using SistemaBarbearia.Models.Paises;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SistemaBarbearia.ViewModels.Cargos
{
    public class SelectCargoVM
    {
        public int IdCargo { get; set; }

        public string dsCargo { get; set; }

        public string flSituacao { get; set; }

        public DateTime? dtCadastro { get; set; }

       
        public DateTime? dtUltAlteracao { get; set; }

    }
}