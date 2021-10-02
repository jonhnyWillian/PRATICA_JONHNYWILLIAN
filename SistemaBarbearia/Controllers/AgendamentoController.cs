using SistemaBarbearia.DAOs.Agendamentos;
using SistemaBarbearia.DataTables;
using SistemaBarbearia.Models.Agendamentos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SistemaBarbearia.Controllers
{
    public class AgendamentoController : Controller
    {
        // GET: Agendamento
        public ActionResult Index()
        {
            return View();
        }

        // GET: Agendamento/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Agendamento/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Agendamento/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Agendamento/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Agendamento/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Agendamento/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Agendamento/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public JsonResult GetEvents([ModelBinder(typeof(DataTablesBinder))] IDataTablesRequest requestModel)
        {
            try
            {
                var select = this.Find(null, requestModel.Search.Value);

                var totalResult = select.Count();

                var result = select.OrderBy(requestModel.Columns, requestModel.Start, requestModel.Length).ToList();

                return Json(new DataTablesResponse(requestModel.Draw, result, totalResult, result.Count), JsonRequestBehavior.AllowGet);


            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                throw new Exception(ex.Message);
            }
        }

       

        public JsonResult DeleteEvent(Agenda agendamento)
        {

            var result = new
            {
                type = "success",
                field = "",
                message = "Registro Removido com sucesso!",
                model = agendamento
            };
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SaveEvent(Agenda agendamento)
        {
            var agendamentosDAO = new AgendamentosDAO();
            agendamentosDAO.InsertAgendamento(agendamento);
            var result = new
            {
                type = "success",
                field = "",
                message = "Registro Removido com sucesso!",
                model = agendamento
            };
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        private IQueryable<dynamic> Find(int? id, string text)
        {
            var agendaDAO = new AgendamentosDAO();
            var list = agendaDAO.SelecionarAgendamento();
            var select = list.Select(u => new
            {
                IdAgendamento = u.IdAgenda,

              
                dtCadastro = u.dtCadastro,
                dtUltAlteracao = u.dtUltAlteracao

            }).ToList();
            return select.AsQueryable();
        }

    }
}
