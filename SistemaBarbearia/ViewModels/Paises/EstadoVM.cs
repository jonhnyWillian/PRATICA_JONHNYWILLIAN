﻿using SistemaBarbearia.Models.Paises;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SistemaBarbearia.ViewModels.Paises
{
    public class EstadoVM
    {
        [Display(Name = "ID")]
        public int Id { get; set; }

        [Display(Name = "Estado")]
        [Required(ErrorMessage = "Informe o nome do Estado. Nome do Estado não pode ser em branco")]
        public string nmEstado { get; set; }

        [Display(Name = "UF")]
        [Required(ErrorMessage = "Informe o a sigla do Estado. UF do Estado não pode ser em branco")]
        public string dsUF { get; set; }


        [Display(Name = "Pais")]
        [Required(ErrorMessage = "Informe o  Pais. Pais não pode ser em branco")]
        public int idPais { get; set; }

        public Models.Paises.Pais pais { get; set; }
        

        [Display(Name = "Data de Cadastro")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? dtCadastro { get; set; }

        [Display(Name = "Data de Ult. Alteracao")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? dtUltAlteracao { get; set; }

        public Models.Paises.Estado GetEstado(Models.Paises.Estado estado)
        {
            estado.nmEstado = this.nmEstado;
            estado.dsUF = this.dsUF;
            pais = new Models.Paises.Pais
            {
                Id = pais.Id,
                nmPais = pais.nmPais
            };

            estado.dtCadastro = this.dtCadastro;
            estado.dtUltAlteracao = this.dtUltAlteracao;

            return estado;
        }
    }
}