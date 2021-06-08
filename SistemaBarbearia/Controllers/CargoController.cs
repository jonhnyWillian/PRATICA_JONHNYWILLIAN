using SistemaBarbearia.DAOs.Cargos;
using SistemaBarbearia.Models.Cargos;
using SistemaBarbearia.DataTables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SistemaBarbearia.Controllers
{
    public class CargoController : Controller
    {
        public ActionResult Index()
        {
            var cargoDAO = new CargoDAO();
            ModelState.Clear();
            return View(cargoDAO.SelecionarCargo());
        }

        // GET: Cargo/Details/5
        public ActionResult Details(int id)
        {
            var servicoDAO = new CargoDAO();
            return View(servicoDAO.GetCargo(id));
        }

        // GET: Cargo/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Cargo/Create
        [HttpPost]
        public ActionResult Create(Cargo servico)
        {

            try
            {
                if (ModelState.IsValid)
                {
                    var cargoDAO = new CargoDAO();

                    if (cargoDAO.AdicionarCargo(servico))
                    {
                        ViewBag.Message = "Cargo criado com sucesso!";
                    }
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Cargo/Edit/5
        public ActionResult Edit(int id)
        {
            var servicoDAO = new CargoDAO();
            return View(servicoDAO.GetCargo(id));
        }

        // POST: Cargo/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Cargo servico)
        {
            try
            {
                var cargoDAO = new CargoDAO();

                cargoDAO.UpdateCargo(servico);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Cargo/Delete/5
        public ActionResult Delete(int id)
        {
            var servicoDAO = new CargoDAO();
            return View(servicoDAO.GetCargo(id));
        }

        // POST: Cargo/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, Cargo servico)
        {
            try
            {
                var cargoDAO = new CargoDAO();
                cargoDAO.ExcluirCargo(id);
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

                var cargoDAO = new CargoDAO();
                var select = cargoDAO.SelecionarCargo().Select(u => new { Id = u.Id, dsCargo = u.dsCargo});

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
