using SistemaBarbearia.Models.Cidades;
using SistemaBarbearia.Models.Clientes;
using SistemaBarbearia.ViewModels.Model;
using SistemaBarbearia.ViewModels.Pessoas;
using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace SistemaBarbearia.ViewModels.Clientes
{
    public class ClienteVM : PessoaVM
    {
        public Cliente GetCliente(Cliente cliente)
        {
            cliente.nmCliente = this.nmPessoa;
            cliente.nrTelefone = !string.IsNullOrEmpty(this.nrTelefone) ? this.nrTelefone.Replace("(", "").Replace(")", "").Replace("-", "") : this.nrTelefone;
            cliente.nrCelular = !string.IsNullOrEmpty(this.nrCelular) ? this.nrCelular.Replace("(", "").Replace(")", "").Replace("-", "") : this.nrCelular;
            cliente.dsEmail = this.dsEmail;
            cliente.flTipo = this.flTipo;
            cliente.nrCEP = !string.IsNullOrEmpty(this.nrCEP) ? this.nrCEP.Replace("-", "") : this.nrCEP;
            cliente.dsLogradouro = this.dsLogradouro;
            cliente.nrResidencial = this.nrResidencial;
            cliente.dsBairro = this.dsBairro;
            cliente.dsComplemento = this.dsComplemento;
            cliente.idCidade = this.Cidade.Id;
            cliente.IdCondPag = this.condPagamento.Id;
            cliente = this.Fisica.GetCliente(cliente);
            cliente.dtCadastro = this.dtCadastro;
            cliente.dtUltAlteracao = this.dtUltAlteracao;
            return cliente;
        }


        public PessoaFisicaVM Fisica { get; set; }

        public class PessoaFisicaVM
        {

            [Display(Name = "Apelido")]
            [StringLength(50, MinimumLength = 3)]
            [Required(ErrorMessage = "Campo Apelido não Pode ser em Branco!", AllowEmptyStrings = false)]
            public string nmApelido { get; set; }

            [Display(Name = "CPF")]
            [StringLength(14, MinimumLength = 0)]
            [Required(ErrorMessage = "Campo CPF não Pode ser em Branco!", AllowEmptyStrings = false)]
            public string nrCPF { get; set; }

            [Display(Name = "RG")]
            [StringLength(11, MinimumLength = 0)]
            [Required(ErrorMessage = "Campo RG não Pode ser em Branco!", AllowEmptyStrings = false)]
            public string nrRG { get; set; }

            [Display(Name = "Sexo")]
            [Required(ErrorMessage = "Campo Sexo não Pode ser em Branco!", AllowEmptyStrings = false)]
            public string flSexo { get; set; }

            public SistemaBarbearia.ViewModels.CondPagamentos.CondPagamentoVM condPagamento { get; set; }

            [Display(Name = "Data de Nascimento")]
            [DataType(DataType.Date, ErrorMessage = "Data em formato inválido")]
            [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
            public DateTime? dataNasc { get; set; }



            public Cliente GetCliente(Cliente cliente)
            {
                
                cliente.nmApelido = this.nmApelido;
                cliente.nrCPF = !string.IsNullOrEmpty(this.nrCPF) ? this.nrCPF.Replace(".", "").Replace("-", "") : this.nrCPF;
                cliente.nrRG = this.nrRG;
                cliente.flSexo = this.flSexo;
                cliente.dataNasc = this.dataNasc;

                return cliente;
            }
        }

        internal object GetCliente()
        {
            throw new NotImplementedException();
        }
    }
}