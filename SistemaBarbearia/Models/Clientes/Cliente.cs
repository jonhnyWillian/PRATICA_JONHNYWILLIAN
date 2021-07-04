using SistemaBarbearia.Models.Cidades;
using System;
using System.ComponentModel.DataAnnotations;

namespace SistemaBarbearia.Models.Clientes
{
    public class Cliente
    {
        [Key]
        public int IdCliente { get; set; }

        public string nmCliente { get; set; }

        public string nmApelido { get; set; }

        public string dsLogin { get; set; }

        public string senha { get; set; }

        public string flSexo { get; set; }
        
        public string flTipo { get; set; }

        public string nrTelefone { get; set; }

        public string nrCelular { get; set; }

        public string nrCEP { get; set; }

        public string dsComplemento { get; set; }

        public string dsBairro { get; set; }

        public string dsLogradouro { get; set; }

        public string nrResidencial { get; set; }
      
        public SistemaBarbearia.ViewModels.Cidades.SelectCidadeVM Cidade { get; set; }

        public int idCidade { get; set; }

        public SistemaBarbearia.ViewModels.CondPagamentos.SelectCondPagamentoVM CondPagamento { get; set; }

        public int IdCondPag { get; set; }


        public string dsEmail { get; set; }

        public string nrCPF { get; set; }

        public string nrRG { get; set; }

        public DateTime? dataNasc { get; set; }

        public DateTime? dtCadastro { get; set; }

        public DateTime? dtUltAlteracao { get; set; }

    }
}