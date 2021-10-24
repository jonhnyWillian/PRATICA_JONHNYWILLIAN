using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SistemaBarbearia.Models.Agendamentos
{
    public class Agenda
    {
        [Key]
        public int IdAgenda { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime dtAgendamento {get; set;}

        public string flSituacao { get; set; }

        public const string SITUACAO_ATIVA = "A";
        public const string SITUACAO_INATIVA = "R";
        public const string SITUACAO_CANCELADO = "C";

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

        public int IdServico { get; set; }

        public int IdFuncionario { get; set; }

        public int IdCliente { get; set; }
        public string nmCliente { get; set; }
        public ViewModels.Funcionarios.SelectFuncionarioVM Funcionario { get; set; }

        public ViewModels.Servicos.SelectServicoVM Servico { get; set; }
        public ViewModels.Clientes.SelectClienteVM Cliente { get; set; }

        
        public DateTime? dtCadastro { get; set; }

        public DateTime? dtUltAlteracao { get; set; }

    }
}