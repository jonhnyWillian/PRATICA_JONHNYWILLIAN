using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SistemaBarbearia.ViewModels.Paises
{
    public class PaisVM
    {
        [Display(Name = "ID")]
        public int Id { get; set; }

        [Display(Name = "Pais")]
        [Required(ErrorMessage = "Por favor, informe o nome do pais")]
        public string nmPais { get; set; }

        [Display(Name = "Sigla")]
        [Required(ErrorMessage = "Por favor, informe o a sigla do pais")]
        public string dsSigla { get; set; }

        [Display(Name = "Data de Cadastro")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? dtCadastro { get; set; }

        [Display(Name = "Data de Ult. Alteracao")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? dtUltAlteracao { get; set; }

        public Models.Paises.Pais GetPais(Models.Paises.Pais pais)
        {
            pais.nmPais = this.nmPais;
            pais.dsSigla = this.dsSigla;
            pais.dtCadastro = this.dtCadastro;
            pais.dtUltAlteracao = this.dtUltAlteracao;

            return pais;
        }
    }
}