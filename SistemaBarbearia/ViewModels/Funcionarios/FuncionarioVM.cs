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
            funcionario.nmFuncionario = this.nmFuncionario;
            funcionario.nmApelido = this.nmApelido;
            funcionario.nrCEP = this.nrCEP;
            funcionario.dsLogradouro = this.dsLogradouro;
            funcionario.nrResidencial = this.nrResidencial;
            funcionario.dsComplemento = this.dsComplemento;
            funcionario.dsBairro = this.dsBairro;
            funcionario.IdCidade = this.cidade.Id;
            funcionario.nrTelefone = this.nrTelefone;
            funcionario.nrCelular = this.nrCelular;
            funcionario.dsEmail = this.dsEmail;
            funcionario.IdCargo = this.cargo.Id;
            funcionario.nrCPF = this.nrCPF;
            funcionario.nrRG = this.nrRG;
            funcionario.dtNasc = this.dtNasc;
            funcionario.dsLogin = this.dsLogin;
            funcionario.senha = this.senha;
            funcionario.vlSalario = this.vlSalario;
            funcionario.dtAdimissao = this.dtAdimissao;
            funcionario.dtDemissao = this.dtDemissao;
            funcionario.dtCadastro = this.dtCadastro;
            funcionario.dtUltAlteracao = this.dtUltAlteracao;
            return funcionario;
        }


        [Display(Name = "Funcionario")]
        [StringLength(50, MinimumLength = 3)]
        [Required(ErrorMessage = "Campo Funcionario não Pode ser em Branco!", AllowEmptyStrings = false)]
        public string nmFuncionario { get; set; }

        [Display(Name = "Apelido")]
        [StringLength(50, MinimumLength = 3)]
        [Required(ErrorMessage = "Campo Apelido não Pode ser em Branco!", AllowEmptyStrings = false)]
        public string nmApelido { get; set; }

        [Display(Name = "Login")]
        [StringLength(50, MinimumLength = 3)]
        [Required(ErrorMessage = "Campo Login não Pode ser em Branco!", AllowEmptyStrings = false)]
        public string dsLogin { get; set; }

        [Display(Name = "Senha")]
        [StringLength(50, MinimumLength = 3)]
        [Required(ErrorMessage = "Campo Senha não Pode ser em Branco!", AllowEmptyStrings = false)]
        public string senha { get; set; }

        [Display(Name = "Sexo")]
        [Required(ErrorMessage = "Campo Sexo não Pode ser em Branco!", AllowEmptyStrings = false)]
        public string flSexo { get; set; }

        [Display(Name = "Cidade")]
        [StringLength(10, MinimumLength = 3)]
        [Required(ErrorMessage = "Campo Cidade não Pode ser em Branco!", AllowEmptyStrings = false)]
        public SistemaBarbearia.ViewModels.Cidades.SelectCidadeVM cidade { get; set; }

        public int IdCargo { get; set; }

        [Display(Name = "Cargo")]
        [StringLength(10, MinimumLength = 3)]
        [Required(ErrorMessage = "Campo Cargo não Pode ser em Branco!", AllowEmptyStrings = false)]
        public SistemaBarbearia.ViewModels.Cargos.SelectCargoVM cargo { get; set; }
     

        [Display(Name = "CPF")]
        [StringLength(11, MinimumLength = 0)]
        [Required(ErrorMessage = "Campo CPF não Pode ser em Branco!", AllowEmptyStrings = false)]
        public string nrCPF { get; set; }

        [Display(Name = "RG")]
        [StringLength(11, MinimumLength = 0)]
        [Required(ErrorMessage = "Campo RG não Pode ser em Branco!", AllowEmptyStrings = false)]
        public string nrRG { get; set; }

        [Display(Name = "Comissão")]
        [StringLength(11, MinimumLength = 0)]
        [Required(ErrorMessage = "Campo Salario não Pode ser em Branco!", AllowEmptyStrings = false)]
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

    
    }
}