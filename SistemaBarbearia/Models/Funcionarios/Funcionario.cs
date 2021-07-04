using SistemaBarbearia.Models.Cargos;
using SistemaBarbearia.Models.Cidades;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SistemaBarbearia.Models.Funcionarios
{
    public class Funcionario
    {
        [Key]
        public int IdFuncionario { get; set; }
        [Required]
        public string nmFuncionario { get; set; }

        [Required]
        public string nmApelido { get; set; }

        [Required]
        public string flSexo { get; set; }

        [Required]
        public string nrTelefone { get; set; }

        [Required]
        public string nrCelular { get; set; }


        [Required]
        public string nrCEP { get; set; }

        [Required]
        public string dsLogradouro { get; set; }

        [Required]
        public string nrResidencial { get; set; }

        [Required]
        public string dsBairro { get; set; }

        [Required]
        public string dsComplemento { get; set; }

        [Required]
        public string dsEmail { get; set; }

        [Required]
        public string nrCPF { get; set; }

        [Required]
        public string nrRG { get; set; }


        [Required]
        public Decimal? vlSalario { get; set; }

        public int IdCidade { get; set; }

        public SistemaBarbearia.ViewModels.Cidades.SelectCidadeVM cidade { get; set; }


        public int IdCargo { get; set; }

        public SistemaBarbearia.ViewModels.Cargos.SelectCargoVM cargo { get; set; }

        [Required]
        public string dsLogin { get; set; }

        [Required]
        public string senha { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? dtNasc { get; set; }


        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? dtAdimissao { get; set; }

        public DateTime? dtDemissao { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? dtCadastro { get; set; }

        public DateTime? dtUltAlteracao { get; set; }
    }
}