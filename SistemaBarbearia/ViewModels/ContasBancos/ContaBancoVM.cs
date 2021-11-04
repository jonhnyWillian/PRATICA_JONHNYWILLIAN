using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SistemaBarbearia.ViewModels.ContasBancos
{
    public class ContaBancoVM : Model.ModelPaiVM
    {

        [Display(Name = "Conta")]
        [Required(ErrorMessage = "Por favor, informe o Nome!")]
        public string dsConta { get; set; }


        [Display(Name = "Classificação")]
        [Required(ErrorMessage = "Por favor, informe o Nome!")]
        public string dsClassificacao { get; set; }

        [Display(Name = "Valor de Saldo")]
        [Required(ErrorMessage = "Por favor, informe o Nome!")]
        public decimal vlSaldo { get; set; }

        [Display(Name = "Situação")]
        [Required(ErrorMessage = "Por favor, informe o Nome!")]
        public string flSituacao { get; set; }

        public static SelectListItem[] Situacao
        {
            get
            {
                return new[]
                {
                    new SelectListItem { Value = "A", Text = "ATIVA" },
                    new SelectListItem { Value = "I", Text = "INATIVA" }
                };
            }
        }

    }
}