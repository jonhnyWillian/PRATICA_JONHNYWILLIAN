using SistemaBarbearia.ViewModels.Model;
using System;
using System.ComponentModel.DataAnnotations;

namespace SistemaBarbearia.ViewModels.Agendamentos
{
    public class AgendamentoVW : ModelPaiVM
    {
        [Display(Name = "Dt.Agendamento")]        
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime dtAgendamento { get; set; }

        [Display(Name = "Horário")]       
        [Required(ErrorMessage = "Campo Hora do Agendamento não Pode ser em Branco!")]
        public string hrAgendamento { get; set; }


        public ViewModels.Clientes.SelectClienteVM Cliente { get; set; }
        public ViewModels.Funcionarios.SelectFuncionarioVM Funcionario { get; set; }

        public ViewModels.Servicos.SelectServicoVM Servico { get; set; }

        
    }
}