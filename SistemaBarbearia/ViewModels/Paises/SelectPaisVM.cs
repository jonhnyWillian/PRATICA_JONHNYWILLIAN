using SistemaBarbearia.Models.Paises;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SistemaBarbearia.ViewModels.Paises
{
    public class SelectPaisVM
    {
        public int? Id { get; set; }
        public string Text { get; set; }

        public string dsSigla { get; set; }

        public DateTime? dtCadastro { get; set; }
        public DateTime? dtUltAlteracao { get; set; }

    }
}