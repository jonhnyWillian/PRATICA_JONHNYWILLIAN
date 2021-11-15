using Newtonsoft.Json;
using SistemaBarbearia.Models.Agendamentos;
using SistemaBarbearia.ViewModels.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace SistemaBarbearia.ViewModels.Agendamentos
{
    public class AgendamentoVW : ModelPaiVM
    {

        public Agenda GetAgenda(Agenda agenda)
        {
            agenda.IdAgenda = this.idAgenda;
            agenda.dtAgendamento = (DateTime)this.dtAgendamento;
            agenda.flhoraAgendamento = this.flhoraAgendamento;
            agenda.flSituacao = this.flSituação;
            agenda.IdCliente = this.Cliente.IdCliente;
            agenda.IdServico = this.Servico.IdServico;
            agenda.IdFuncionario = this.Funcionario.IdFuncionario;
            agenda.dtCadastro = this.dtCadastro;
            agenda.dtUltAlteracao = this.dtUltAlteracao;

            return agenda;
        }


        public int idAgenda { get; set; }

        [Display(Name = "Data - Agendamento")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? dtAgendamento { get; set; }
        public string dtAgendamentoAux { get; set; }

        [Display(Name = "Horário")]
        [Required(ErrorMessage = "Campo Hora do Agendamento não Pode ser em Branco!")]
        public string flhoraAgendamento { get; set; }

        public static SelectListItem[] Horario
        {
            get
            {
                return new[]
                {
                    new SelectListItem { Value = "", Text = "" },
                    new SelectListItem { Value = "8:00", Text = "8:00" },
                    new SelectListItem { Value = "8:30", Text = "8:30" },
                    new SelectListItem { Value = "9:00", Text = "9:00" },
                    new SelectListItem { Value = "9:30", Text = "9:30" },
                    new SelectListItem { Value = "10:00", Text = "10:00" },
                    new SelectListItem { Value = "10:30", Text = "10:30" },
                    new SelectListItem { Value = "11:00", Text = "11:00" },
                    new SelectListItem { Value = "11:30", Text = "11:30" },
                    new SelectListItem { Value = "13:30", Text = "13:30" },
                    new SelectListItem { Value = "14:00", Text = "14:00" },
                    new SelectListItem { Value = "14:30", Text = "14:30" },
                    new SelectListItem { Value = "15:00", Text = "15:00" },
                    new SelectListItem { Value = "15:30", Text = "15:30" },
                    new SelectListItem { Value = "16:00", Text = "16:00" },
                    new SelectListItem { Value = "16:30", Text = "16:30" },
                    new SelectListItem { Value = "17:00", Text = "17:00" },
                    new SelectListItem { Value = "18:00", Text = "18:00" },
                    new SelectListItem { Value = "18:30", Text = "18:30" },
                    new SelectListItem { Value = "19:00", Text = "19:00" },
                    new SelectListItem { Value = "19:30", Text = "19:30" },
                };
            }
        }


        [Display(Name = "Situação")]
        [Required(ErrorMessage = "Campo Situação do Agendamento não Pode ser em Branco!")]
        public string flSituação { get; set; }

        public static SelectListItem[] Situação
        {
            get
            {
                return new[]
                {
                    new SelectListItem { Value = "A", Text = "Aberto" },
                    new SelectListItem { Value = "R", Text = "Realizado" },
                    new SelectListItem { Value = "C", Text = "Cancelado" },

                };
            }
        }

        public ViewModels.Clientes.SelectClienteVM Cliente { get; set; }
        public int IdCliente { get; set; }
        public ViewModels.Funcionarios.SelectFuncionarioVM Funcionario { get; set; }
        public int IdFuncionario { get; set; }
        public ViewModels.Servicos.SelectServicoVM Servico { get; set; }
        public int IdServico { get; set; }


        [Display(Name = "Valor Serviço")]
        [Required(ErrorMessage = "Campo Hora do Agendamento não Pode ser em Branco!")]
        public decimal? vlServico { get; set; }


        [Display(Name = "Situação")]
        public string flSituacao { get; set; }


        public VendaVM VendaServico { get; set; }
        public ProdutoVendaVM VendaProduto { get; set; }

        public class VendaVM
        {
            [Display(Name = "Modelo")]
            [StringLength(50, MinimumLength = 3)]
            [Required(ErrorMessage = "Campo Modelo não Pode ser em Branco!")]
            public string nrModelo { get; set; }

            [Display(Name = "Nota")]
            [StringLength(50, MinimumLength = 3)]
            [Required(ErrorMessage = "Campo Serie não Pode ser em Branco!")]
            public string nrNota { get; set; }

            [Display(Name = "Serie")]
            [StringLength(50, MinimumLength = 3)]
            [Required(ErrorMessage = "Campo Serie não Pode ser em Branco!")]
            public string nrSerie { get; set; }

            
            public decimal? nrQtd { get; set; }

            [Display(Name = "Dt. Nota")]
            [DataType(DataType.Date, ErrorMessage = "Data em formato inválido")]
            [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
            public DateTime? dtNota { get; set; }
        }

        public class ProdutoVendaVM
        {
            [Display(Name = "Modelo")]
            [StringLength(50, MinimumLength = 3)]
            [Required(ErrorMessage = "Campo Modelo não Pode ser em Branco!")]
            public string nrModelo { get; set; }

            [Display(Name = "Situação")]
            public string flSituacao { get; set; }

            [Display(Name = "Nota")]
            [StringLength(50, MinimumLength = 3)]
            [Required(ErrorMessage = "Campo Serie não Pode ser em Branco!")]
            public string nrNota { get; set; }

            [Display(Name = "Serie")]
            [StringLength(50, MinimumLength = 3)]
            [Required(ErrorMessage = "Campo Serie não Pode ser em Branco!")]
            public string nrSerie { get; set; }

            [Display(Name = "Dt. Nota")]
            [DataType(DataType.Date, ErrorMessage = "Data em formato inválido")]
            [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
            public DateTime? dtNota { get; set; }
        }

        public ViewModels.CondPagamentos.SelectCondPagamentoVM CondicaoPagamento { get; set; }
        public int IdCondPagamento { get; set; }
        public ViewModels.Produtos.SelectProdutoVM Produto { get; set; }

        public string finalizar { get; set; }

        public decimal? vlTotalServico { get; set; }

        public decimal? vlTotalProduto { get; set; }


        public decimal? TotalServico { get; set; }

        public decimal? TotalProduto { get; set; }

        public class ProdutosVM
        {
            public int? IdProduto { get; set; }
            public string nmProduto { get; set; }
            public decimal? nrQtd { get; set; }
            public decimal? vlVenda { get; set; }
            public decimal? vlTotal { get; set; }
        }
        public string jsProdutos { get; set; }

        public List<ProdutosVM> ProdutosCompra
        {
            get
            {
                if (string.IsNullOrEmpty(jsProdutos))
                    return new List<ProdutosVM>();
                return JsonConvert.DeserializeObject<List<ProdutosVM>>(jsProdutos);
            }
            set
            {
                jsProdutos = JsonConvert.SerializeObject(value);
            }
        }


        public class ParcelasVM
        {
            public int? IdFormaPagamento { get; set; }
            public string dsFormaPagamento { get; set; }
            public DateTime? dtVencimento { get; set; }
            public decimal vlParcela { get; set; }
            public string flSituacao { get; set; }
            public double? nrParcela { get; set; }
            public DateTime? dtPagamento { get; set; }
        }
        public string jsParcelas { get; set; }
        public List<ParcelasVM> ParcelasVenda
        {
            get
            {
                if (string.IsNullOrEmpty(jsParcelas))
                    return new List<ParcelasVM>();
                return JsonConvert.DeserializeObject<List<ParcelasVM>>(jsParcelas);
            }
            set
            {
                jsParcelas = JsonConvert.SerializeObject(value);
            }
        }

        public class ParcelasProdutoVM
        {
            public int? IdFormaPagamento { get; set; }
            public string dsFormaPagamento { get; set; }
            public DateTime? dtVencimento { get; set; }
            public decimal vlParcela { get; set; }
            public double? nrParcela { get; set; }

            public string flSituacao { get; set; }
            public DateTime? dtPagamento { get; set; }
        }
        public string jsParcelasProduto { get; set; }
        public List<ParcelasProdutoVM> ParcelasVendaProduto
        {
            get
            {
                if (string.IsNullOrEmpty(jsParcelasProduto))
                    return new List<ParcelasProdutoVM>();
                return JsonConvert.DeserializeObject<List<ParcelasProdutoVM>>(jsParcelasProduto);
            }
            set
            {
                jsParcelas = JsonConvert.SerializeObject(value);
            }
        }


    }
}