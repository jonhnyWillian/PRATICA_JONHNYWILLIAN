using SistemaBarbearia.DAOs.Cidades;
using SistemaBarbearia.Models.Cidades;
using SistemaBarbearia.DataTables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SistemaBarbearia.Controllers
{
    public class CidadeController : Controller
    {
        // GET: Cidade
        public ActionResult Index()
        {
            var cidadeDAO = new CidadeDAO();
            ModelState.Clear();
            return View(cidadeDAO.SelecionarCidade());
        }

        // GET: Cidade/Details/5
        public ActionResult Details(int id)
        {
            var cidadeDAO = new CidadeDAO();
            ModelState.Clear();
            return View(cidadeDAO.GetCidade(id));
        }

        // GET: PaCidadeis/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Cidade/Create
        [HttpPost]
        public ActionResult Create(Cidade cidade)
        {
            
            try
            {
                if (ModelState.IsValid)
                {
                    var cidadeDAO = new CidadeDAO();

                    if (cidadeDAO.InsertCidade(cidade))
                    {

                        ViewBag.Message = "Cidade criado com sucesso!";
                    }
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Cidade/Edit/5
        public ActionResult Edit(int id)
        {
            var cidadeDAO = new CidadeDAO();
            return View(cidadeDAO.GetCidade(id));
        }

        // POST: Cidade/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Cidade cidade)
        {
            try
            {
                var cidadeDAO = new CidadeDAO();

                cidadeDAO.UpdateCidade(cidade);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Cidade/Delete/5
        public ActionResult Delete(int id)
        {
            var cidadeDAO = new CidadeDAO();

            return View(cidadeDAO.GetCidade(id));
        }

        // POST: Cidade/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, Cidade cidade)
        {
            try
            {
                var cidadeDAO = new CidadeDAO();
                cidadeDAO.DeleteCidade(id);
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

                var cidadeDAO = new CidadeDAO();
                var select = cidadeDAO.SelecionarCidade().Select(u => new { Id = u.Id, nmCidade = u.nmCidade, DDD = u.DDD });

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
