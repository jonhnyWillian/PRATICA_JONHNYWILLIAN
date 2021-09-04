using System;

namespace SistemaBarbearia.ViewModels.Paises
{
    public class SelectPaisVM
    {
        public int? IdPais { get; set; }
        public string nmPais { get; set; }

        public string dsSigla { get; set; }

        public DateTime? dtCadastro { get; set; }
        public DateTime? dtUltAlteracao { get; set; }

    }
}