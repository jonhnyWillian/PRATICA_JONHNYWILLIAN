using SistemaBarbearia.Models.Cidades;
using SistemaBarbearia.Models.Fornecedores;
using SistemaBarbearia.ViewModels.Model;
using SistemaBarbearia.ViewModels.Pessoas;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SistemaBarbearia.ViewModels.Fornecedores
{
    public class FornecedorVM : PessoaVM
    {      

        public Fornecedor GetFornecedor(Fornecedor bean)
        {
            bean.nmNome = this.nmPessoa;
            bean.nrTelefone = !string.IsNullOrEmpty(this.nrTelefone) ? this.nrTelefone.Replace("(", "").Replace(")", "").Replace("-", "") : this.nrTelefone;
            bean.nrCelular = !string.IsNullOrEmpty(this.nrCelular) ? this.nrCelular.Replace("(", "").Replace(")", "").Replace("-", "") : this.nrCelular;
            bean.dsEmail = this.dsEmail;          
            bean.flTipo = this.flTipo;            
            bean.nrCEP = !string.IsNullOrEmpty(this.nrCEP) ? this.nrCEP.Replace("-", "") : this.nrCEP;
            bean.dsLogradouro = this.dsLogradouro;
            bean.nrResidencial = this.nrResidencial;
            bean.dsBairro = this.dsBairro;
            bean.dsComplemento = this.dsComplemento;
            bean.dtUltAlteracao = this.dtUltAlteracao;
            bean.dtCadastro = this.dtCadastro;
            bean.idCidade = this.Cidade.Id;
            if (bean.flTipo == Fornecedor.TIPO_JURIDICA)
                bean = this.Juridica.GetFornecedorJuridico(bean);
            if (bean.flTipo == Fornecedor.TIPO_FISICA)
                bean = this.Fisica.GetFornecedorFisica(bean);

            return bean;
        }

        public PessoaFisicaVM Fisica { get; set; }
        public PessoaJuridicaVM Juridica { get; set; }

        public class PessoaFisicaVM
        {
            [Display(Name = "Apelido")]
            //[Required(ErrorMessage = "Por favor, informe o Apelido!")]
            public string nmApelido { get; set; }

            [Display(Name = "CPF")]
            //[Required(ErrorMessage = "Por favor, informe o CPF!")]
            public string nrCPF { get; set; }

            [Display(Name = "RG")]
            //[Required(ErrorMessage = "Por favor, informe o RG!")]
            public string nrRG { get; set; }

            [Display(Name = "Data de Nascimento")]
            //[Required(ErrorMessage = "Por favor, informe o Data Nascimento!")]
            [DataType(DataType.Date, ErrorMessage = "Data em formato inválido")]
            [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
            public DateTime? dtNascimento { get; set; }

            [Display(Name = "Sexo")]
            //[Required(ErrorMessage = "Por favor, informe o cliente!")]
            public string flSexo { get; set; }

            public Fornecedor GetFornecedorFisica(Fornecedor bean)
            {
                bean.dsNome = this.nmApelido;
                bean.nrCPFCNPJ = !string.IsNullOrEmpty(this.nrCPF) ? this.nrCPF.Replace(".", "").Replace("-", "") : this.nrCPF;
                bean.nrRGIE = this.nrRG;
                bean.flSexo = this.flSexo;
                bean.dtNasc = this.dtNascimento;

                return bean;
            }
        }

        public class PessoaJuridicaVM
        {

            [Display(Name = "Nome fantasia")]
            //[Required(ErrorMessage = "Por favor, informe o Nome Fantasia!")]
            public string nmFantasia { get; set; }

            [Display(Name = "CNPJ")]
            //[Required(ErrorMessage = "Por favor, informe o CNPJ!")]
            public string nrCNPJ { get; set; }

            [Display(Name = "Nº IE")]
            //[Required(ErrorMessage = "Por favor, informe o EI!")]
            public string nrIE { get; set; }

            [Display(Name = "Site")]
            public string dsSite { get; set; }

            [Display(Name = "Contato")]
            public string nrContato { get; set; }

            public Fornecedor GetFornecedorJuridico(Fornecedor bean)
            {
                bean.nrCPFCNPJ = !string.IsNullOrEmpty(this.nrCNPJ) ? this.nrCNPJ.Replace(".", "").Replace(",", "").Replace("/", "").Replace("-", "") : this.nrCNPJ;
                bean.dsNome = this.nmFantasia;
                bean.nrRGIE = this.nrIE;
                bean.dsSite = this.dsSite;
                bean.nrContato = this.nrContato;
                


                return bean;
            }

        }
    }
}