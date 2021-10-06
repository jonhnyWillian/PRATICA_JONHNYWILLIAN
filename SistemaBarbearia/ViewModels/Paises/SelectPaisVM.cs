using System;

namespace SistemaBarbearia.ViewModels.Paises
{
    public class SelectPaisVM
    {
        public int? Id { get; set; }
        public string text { get; set; }

        public string dsSigla { get; set; }

        public DateTime? dtCadastro { get; set; }
        public DateTime? dtUltAlteracao { get; set; }

    }
}