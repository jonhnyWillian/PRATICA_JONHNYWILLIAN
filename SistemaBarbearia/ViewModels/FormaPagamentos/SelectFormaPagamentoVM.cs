﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SistemaBarbearia.ViewModels.FormaPagamentos
{
    public class SelectFormaPagamentoVM
    {
        public int id { get; set; }

        public string text { get; set; }

        public DateTime? dtCadastro { get; set; }

        public DateTime? dtUltAlteracao { get; set; }
    }
}