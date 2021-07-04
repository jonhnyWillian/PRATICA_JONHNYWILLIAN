using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SistemaBarbearia.ViewModels.FormaPagamentos
{
    public class SelectFormaPagamentoVM
    {
        public int Id { get; set; }

        public string Text { get; set; }

        public DateTime? dtCadastro { get; set; }

        public DateTime? dtUltAlteracao { get; set; }
    }
}