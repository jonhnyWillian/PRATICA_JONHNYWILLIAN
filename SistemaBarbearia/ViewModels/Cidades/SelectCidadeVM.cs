using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SistemaBarbearia.ViewModels.Cidades
{
    public class SelectCidadeVM
    {
       
        public int Id { get; set; }

    
        public string nmCidade { get; set; }

    
        public string DDD { get; set; }

        public DateTime? dtCadastro { get; set; }

        public DateTime? dtUltAlteracao { get; set; }
    }
}