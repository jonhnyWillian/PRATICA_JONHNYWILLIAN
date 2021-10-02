using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SistemaBarbearia.Models.Agendamentos
{
    public class Agenda
    {
        [Key]
        public int IdAgenda { get; set; }

        public DateTime dtAgendamento {get; set;}

        public string hrAgendamento { get; set; }

        public int idServico { get; set; }

        public int idFuncionario { get; set; }

        public int idCliente { get; set; }

        public ViewModels.Funcionarios.SelectFuncionarioVM Funcionario { get; set; }
        public ViewModels.Servicos.SelectServicoVM Servico { get; set; }
        public ViewModels.Clientes.SelectClienteVM Cliente { get; set; }

        
        public DateTime? dtCadastro { get; set; }

        public DateTime? dtUltAlteracao { get; set; }

    }
}