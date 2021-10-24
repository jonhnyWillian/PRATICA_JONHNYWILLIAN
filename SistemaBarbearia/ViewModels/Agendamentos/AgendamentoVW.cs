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

            agenda.dtAgendamento = this.dtAgendamento;
            agenda.flhoraAgendamento = this.flhoraAgendamento;
            agenda.flSituacao = this.flSituação;
            agenda.IdCliente = this.Cliente.IdCliente;
            agenda.IdServico = this.Servico.IdServico;
            agenda.IdFuncionario = this.Funcionario.IdFuncionario;
            agenda.dtCadastro = this.dtCadastro;
            agenda.dtUltAlteracao = this.dtUltAlteracao;

            return agenda;
        }










        [Display(Name = "Data - Agendamento")]        
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime dtAgendamento { get; set; }

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

        public class ServicoVM
        {
            public int? IdServico { get; set; }
            public string dsServico { get; set; }
            public decimal? vlServico { get; set; }
        }
        public string jsServicos { get; set; }
        public List<ServicoVM> ServicosAgenda
        {
            get
            {
                if (string.IsNullOrEmpty(jsServicos))
                    return new List<ServicoVM>();
                return JsonConvert.DeserializeObject<List<ServicoVM>>(jsServicos);
            }
            set
            {
                jsServicos = JsonConvert.SerializeObject(value);
            }
        }
    }
}