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
        public int Id { get; set; }

        public string nmNome { get; set; }

        public string dsNome { get; set; }

        public string flTipoPessoa { get; set; }

        public static SelectListItem[] TipoPessoa
        {
            get
            {
                return new[]
                {
                    new SelectListItem { Text = "Juridica", Value = "J" },
                    new SelectListItem { Text = "Fisica", Value = "F" },

                };
            }
        }
      
        public string nrCEP { get; set; }

        public Cidade cidade { get; set; }

        public string dsBairro { get; set; }

        public string dsComplemento { get; set; }

        public string dsLougradouro { get; set; }

        public string nrResidencial { get; set; }

        public string nrContato { get; set; }

        public string nrTelefone { get; set; }

        public string nrCelular { get; set; }

        public string dsSite { get; set; }

        public string dsEmail { get; set; }

        public string nrCPFCNPJ { get; set; }

        public string nrRGInscEst { get; set; }

        public DateTime? dtCadastro { get; set; }

        public DateTime? dtUltAlteracao { get; set; }

    }
}