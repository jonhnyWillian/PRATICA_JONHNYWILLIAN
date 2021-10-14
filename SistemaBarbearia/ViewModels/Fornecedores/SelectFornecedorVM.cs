using SistemaBarbearia.Models.Cidades;
using SistemaBarbearia.Models.Paises;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SistemaBarbearia.ViewModels.Fornecedores
{
    public class SelectFornecedorVM
    {
        public int? IdFornecedor { get; set; }

        public string nmNome { get; set; }
     

    }
}