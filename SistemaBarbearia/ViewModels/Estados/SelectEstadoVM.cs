﻿using SistemaBarbearia.Models.Paises;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SistemaBarbearia.ViewModels.Estados
{
    public class SelectEstadoVM
    {
        public int? IdEstado { get; set; }
        public string nmEstado { get; set; }

        public int IdPais { get; set; }

        public string dsUF { get; set; }

        public DateTime? dtCadastro { get; set; }
        public DateTime? dtUltAlteracao { get; set; }

    }
}