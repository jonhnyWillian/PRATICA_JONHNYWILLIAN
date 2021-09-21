using SistemaBarbearia.ViewModels.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SistemaBarbearia.ViewModels.Agendamentos
{
    public class AgendamentoVW : ModelPaiVM
    {
        public ViewModels.Clientes.SelectClienteVM Cliente { get; set; }
        public ViewModels.Funcionarios.SelectFuncionarioVM Funcionario { get; set; }

        public ViewModels.Servicos.SelectServicoVM Servico { get; set; }

        
    }
}