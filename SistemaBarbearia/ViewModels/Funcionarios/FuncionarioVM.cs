using SistemaBarbearia.Models.Cargos;
using SistemaBarbearia.Models.Cidades;
using SistemaBarbearia.Models.Funcionarios;
using SistemaBarbearia.ViewModels.Pessoas;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SistemaBarbearia.ViewModels.Funcionarios
{
    public class FuncionarioVM : PessoaVM
    {
       
        public Funcionario GetFuncionario (Funcionario funcionario)
        {
            funcionario.nmFuncionario = this.nmPessoa;
            funcionario.nrCEP = !string.IsNullOrEmpty(this.nrCEP) ? this.nrCEP.Replace("-", "") : this.nrCEP;
            funcionario.dsLogradouro = this.dsLogradouro;
            funcionario.nrResidencial = this.nrResidencial;
            funcionario.dsComplemento = this.dsComplemento;
            funcionario.dsBairro = this.dsBairro;
            funcionario.nrTelefone = !string.IsNullOrEmpty(this.nrTelefone) ? this.nrTelefone.Replace("(", "").Replace(")", "").Replace("-", "") : this.nrTelefone; ;
            funcionario.nrCelular = !string.IsNullOrEmpty(this.nrCelular) ? this.nrCelular.Replace("(", "").Replace(")", "").Replace("-", "") : this.nrCelular; ;
            funcionario.dsEmail = this.dsEmail;
            funcionario.IdCidade = this.Cidade.Id;
            funcionario.IdCargo = this.Cargo.Id;
          
            funcionario = this.Fisica.GetFuncionarioFisica(funcionario);
            funcionario.dtCadastro = this.dtCadastro;
            funcionario.dtUltAlteracao = this.dtUltAlteracao;
            return funcionario;
        }

        public PessoaFisicaVM Fisica { get; set; }

        public class PessoaFisicaVM
        {
            [Display(Name = "Apelido")]
            [Required(ErrorMessage = "Campo Apelido não Pode ser em Branco!")]
            public string nmApelido { get; set; }

            [Display(Name = "Login")]            
            [Required(ErrorMessage = "Campo Login não Pode ser em Branco!")]
            public string dsLogin { get; set; }

            [Display(Name = "Senha")]
            [Required(ErrorMessage = "Campo Senha não Pode ser em Branco!")]
            public string senha { get; set; }

            [Display(Name = "Sexo")]
            [Required(ErrorMessage = "Campo Sexo não Pode ser em Branco!")]
            public string flSexo { get; set; }          

            [Display(Name = "Cargo")]
            [Required(ErrorMessage = "Campo Cargo não Pode ser em Branco!")]
            public SistemaBarbearia.ViewModels.Cargos.SelectCargoVM Cargo { get; set; }


            [Display(Name = "CPF")]           
            [Required(ErrorMessage = "Campo CPF não Pode ser em Branco!")]
            public string nrCPF { get; set; }

            [Display(Name = "RG")]
            [Required(ErrorMessage = "Campo RG não Pode ser em Branco!")]
            public string nrRG { get; set; }

            [Display(Name = "Comissão")]          
            [Required(ErrorMessage = "Campo Salario não Pode ser em Branco!")]
            public Decimal? vlSalario { get; set; }

            [Display(Name = "Data de Nacimento")]
            [DataType(DataType.Date, ErrorMessage = "Data em formato inválido")]
            [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
            public DateTime? dtNasc { get; set; }

            [Display(Name = "Data de Adimissao")]
            [DataType(DataType.Date, ErrorMessage = "Data em formato inválido")]
            [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
            public DateTime? dtAdimissao { get; set; }

            [Display(Name = "Data de Demissao")]
            [DataType(DataType.Date, ErrorMessage = "Data em formato inválido")]
            [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
            public DateTime? dtDemissao { get; set; }

            public Funcionario GetFuncionarioFisica(Funcionario bean)
            {
                bean.nmApelido = this.nmApelido;
                bean.flSexo = this.flSexo;
                bean.nrCPF = this.nrCPF;
                bean.nrRG = this.nrRG;
                bean.dtNasc = this.dtNasc;
                bean.dsLogin = this.dsLogin;
                bean.senha = this.senha;
                bean.vlSalario = this.vlSalario;
                bean.dtAdimissao = this.dtAdimissao;
                bean.dtDemissao = this.dtDemissao;

                return bean;
            }
        }

      
    }
}