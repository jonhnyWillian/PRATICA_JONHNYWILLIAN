using SistemaBarbearia.DAOs.Servicos;
using SistemaBarbearia.Models.Servicos;
using SistemaBarbearia.DataTables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SistemaBarbearia.Controllers
{
    public class ServicoController : Controller
    {
        // GET: Servico
        public ActionResult Index()
        {
            var servicoDAO = new ServicoDAO();
            ModelState.Clear();
            return View(servicoDAO.SelecionarServico());
        }

        // GET: Servico/Details/5
        public ActionResult Details(int id)
        {
            var servicoDAO = new ServicoDAO();
            return View(servicoDAO.GetServico(id));
        }

        // GET: Servico/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Servico/Create
        [HttpPost]
        public ActionResult Create(Servico servico)
        {
            
            try
            {
                if (ModelState.IsValid)
                {
                    var servicoDAO = new ServicoDAO();

                    if (servicoDAO.InsertServico(servico))
                    {
                        ViewBag.Message = "Servico criado com sucesso!";
                    }
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Servico/Edit/5
        public ActionResult Edit(int id)
        {
            var servicoDAO = new ServicoDAO();
            return View(servicoDAO.GetServico(id));
        }

        // POST: Servico/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Servico servico)
        {
            try
            {
                var servicoDAO = new ServicoDAO();

                servicoDAO.UpdateServico(servico);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Servico/Delete/5
        public ActionResult Delete(int id)
        {
            var servicoDAO = new ServicoDAO();
            return View(servicoDAO.GetServico(id));
        }

        // POST: Servico/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, Servico servico)
        {
            try
            {
                var servicoDAO = new ServicoDAO();
                servicoDAO.DeleteServico(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        public JsonResult JsQuery([ModelBinder(typeof(DataTablesBinder))] IDataTablesRequest requestModel)
        {
            try
            {

                var estadoDAO = new ServicoDAO();
                var select = estadoDAO.SelecionarServico().Select(u => new { Id = u.Id, dsServico = u.dsServico, vlServico = u.vlServico });

                IQueryable<dynamic> query = select.AsQueryable();


                var totalResult = query.Count();

                var result = query.OrderBy(requestModel.Columns, requestModel.Start, requestModel.Length).ToList();

                return Json(new DataTablesResponse(requestModel.Draw, result, totalResult, result.Count), JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
        }
    }
}
