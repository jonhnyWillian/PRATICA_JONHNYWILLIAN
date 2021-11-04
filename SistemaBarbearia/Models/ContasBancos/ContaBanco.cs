using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SistemaBarbearia.Models.ContasBancos
{
    public class ContaBanco
    {
        [Key]
        public int IdConta{ get; set; }

        public string dsConta { get; set; }

        public string dsClassificacao { get; set; }

        public decimal vlSaldo { get; set; }

        public string flSituacao { get; set; }


        public DateTime? dtCadastro { get; set; }

        public DateTime? dtUltAlteracao { get; set; }

    }
}