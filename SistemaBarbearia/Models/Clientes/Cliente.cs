using SistemaBarbearia.Models.Cidades;
using System;
using System.ComponentModel.DataAnnotations;

namespace SistemaBarbearia.Models.Clientes
{
    public class Cliente
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string nmCliente { get; set; }

        [Required]
        public string nmApelido { get; set; }

        [Required]
        public string flTipoPessoa { get; set; }

        [Required]
        public string flSexo { get; set; }


        [Required]
        public string nrTelefone { get; set; }

        [Required]
        public string nrCelular { get; set; }

        [Required]
        public string dsLougradouro { get; set; }

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
        public string nrCNPJ { get; set; }

        [Required]
        public string nrInscEstadual { get; set; }

        [Required]
        public DateTime? dataNasc { get; set; }

        public int? idCidade { get; set; }

        public Cidade cidade { get; set; }

        [Required]
        public string nmLogin { get; set; }

        [Required]
        public string dsSenha { get; set; }


        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? dtCadastro { get; set; }

        public DateTime? dtUltAlteracao { get; set; }

    }
}