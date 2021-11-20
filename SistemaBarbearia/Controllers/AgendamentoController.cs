using SistemaBarbearia.DAOs.Agendamentos;
using SistemaBarbearia.DAOs.Clientes;
using SistemaBarbearia.DAOs.Funcionarios;
using SistemaBarbearia.DAOs.Servicos;
using SistemaBarbearia.DAOs.VendaProdutos;
using SistemaBarbearia.DataTables;
using SistemaBarbearia.Models.Agendamentos;
using SistemaBarbearia.Models.Vendas;
using SistemaBarbearia.ViewModels.Agendamentos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SistemaBarbearia.Controllers
{
    public class AgendamentoController : Controller
    {

        #region MethodPrivate
        private ActionResult GetView(int id)
        {
            AgendamentosDAO objAgenda = new AgendamentosDAO();
            ClienteDAO DAOCliente = new ClienteDAO();
            FuncionarioDAO DAOFuncionario = new FuncionarioDAO();
            ServicoDAO DAOServico = new ServicoDAO();
           
            var obj = objAgenda.GetAgendamento(id);
            var result = new AgendamentoVW
            {
                IdModelPai = obj.IdAgenda,
                //idAgenda = obj.IdAgenda,
                //dtNota = obj.dtAgendamento,
                dtAgendamento = obj.dtAgendamento,
                flhoraAgendamento = obj.flhoraAgendamento,
                flSituação = obj.flSituacao,
                IdCliente = obj.IdCliente,
                IdServico = obj.IdServico,
                IdFuncionario = obj.IdFuncionario,
                dtCadastro = obj.dtCadastro,
                dtUltAlteracao = obj.dtUltAlteracao
            };
            var objFuncionario = DAOFuncionario.DAOGetFuncionario(result.IdFuncionario);
            result.Funcionario = new ViewModels.Funcionarios.SelectFuncionarioVM { IdFuncionario = objFuncionario.IdFuncionario, nmFuncionario = objFuncionario.nmFuncionario };

            var objCliente = DAOCliente.DAOGetCliente(result.IdCliente);
            result.Cliente = new ViewModels.Clientes.SelectClienteVM { IdCliente = objCliente.IdCliente, nmCliente = objCliente.nmCliente };

            var objServico = DAOServico.GetServico(result.IdServico);
            result.Servico = new ViewModels.Servicos.SelectServicoVM { IdServico = objServico.IdServico, dsServico = objServico.dsServico, vlServico = Convert.ToDecimal(objServico.vlServico) };


            return View(result);

        }
        #endregion

        public ActionResult Index()
        {
            var agendaDAO = new AgendamentosDAO();
            ModelState.Clear();
            return View(agendaDAO.SelecionarAgendamento());
        }

        public ActionResult Details(int id)
        {
            return this.GetView(id);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Agenda agenda)
        {
            try
            {
                var agendaDAO = new AgendamentosDAO();

                agenda.flSituacao = "A";

                agendaDAO.InsertAgendamento(agenda);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Edit(int id)
        {
            return this.GetView(id);
        }

        [HttpPost]
        public ActionResult Edit(int id, AgendamentoVW model)
        {
            try
            {
                AgendamentosDAO dao = new AgendamentosDAO();
                var aganda = dao.GetAgendamento(id);

                var bean = model.GetAgenda(aganda);
                bean.dtUltAlteracao = DateTime.Now;

                dao.UpdateAgendamento(bean);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Cancelar(int id)
        {
            return this.GetView(id);
        }

        [HttpPost]
        public ActionResult Cancelar(int id, AgendamentoVW model)
        {
            try
            {
                AgendamentosDAO dao = new AgendamentosDAO();
                var aganda = dao.GetAgendamento(id);

                var bean = model.GetAgenda(aganda);
                bean.dtUltAlteracao = DateTime.Now;

                dao.CancelarAgendamento(bean);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
      
        public ActionResult Delete(int id)
        {
            return this.GetView(id);
        }

        [HttpPost]
        public ActionResult Delete(int id, AgendamentoVW model)
        {
            try
            {
                AgendamentosDAO dao = new AgendamentosDAO();
                dao.DeleteAgendamento(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult FinalizarHorario(int id )
        {            
            return this.GetView(id);
        }

        [HttpPost]
        public ActionResult FinalizarHorario(int id, AgendamentoVW model)
        {
            try
            {
                var vendaDAO = new VendaProdutoDAO();
                vendaDAO.insertVenda(id,model);
                vendaDAO.updateAgenda(id);
                return RedirectToAction("Index");
                
            }
            catch
            {
                return View();
            }
        }

        #region JSON
        public JsonResult JsVerificaHorario(string dtAgendamento, string hora, int idFuncionario)
        {
            var dao = new AgendamentosDAO();
            var valid = dao.validHorario(dtAgendamento, hora, idFuncionario);
            var type = string.Empty;
            var msg = string.Empty;
            if (valid)
            {
                type = "success";
                msg = "Horario Disponivel!";
            }
            else
            {
                type = "danger";
                msg = "Já existe Horario marcado, verifique!";
            }
            var result = new
            {
                type = type,
                message = msg,
            };
            return Json(result, JsonRequestBehavior.AllowGet);
        }

      
        #endregion
    }
}
