using SistemaBarbearia.DAOs.Estados;
using SistemaBarbearia.Models.Estados;
using SistemaBarbearia.Models.Paises;
using SistemaBarbearia.DataTables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SistemaBarbearia.Controllers
{
    public class EstadoController : Controller
    {
        // GET: Estado
        public ActionResult Index()
        {
            var estadoDAO = new EstadoDAO();
            ModelState.Clear();
            return View(estadoDAO.SelecionarEstado());
        }

        // GET: Estado/Details/5
        public ActionResult Details(int id)
        {
            var estadoDAO = new EstadoDAO();
            return View(estadoDAO.GetEstado(id));
        }

        // GET: Estado/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Estado/Create
        [HttpPost]
        public ActionResult Create(Estado estado)
        {
            if (estado.nmEstado == null)
            {
                ModelState.AddModelError("Estado.nmEstado", "Nome do Estado Nao pode ser em braco");
            }
            if (estado.dsUF == null)
            {
                ModelState.AddModelError("Estado.nmEstado", "Sigla do Estado Nao pode ser em braco");
            }
            try
            {
                if (ModelState.IsValid)
                {
                    var paisDAO = new EstadoDAO();

                    if (paisDAO.InsertEstado(estado))
                    {
                        ViewBag.Message = "Estado criado com sucesso!";
                    }
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Estado/Edit/5
        public ActionResult Edit(int id)
        {
            var estadoDAO = new EstadoDAO();
            return View(estadoDAO.GetEstado(id));
        }

        // POST: Estado/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Estado pais)
        {
            try
            {
                var estadoDAO = new EstadoDAO();

                estadoDAO.UpdateEstado(pais);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Estado/Delete/5
        public ActionResult Delete(int id)
        {
            var estadoDAO = new EstadoDAO();
            return View(estadoDAO.GetEstado(id));
        }

        // POST: Estado/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, Estado estado)
        {
            try
            {
                var estadoDAO = new EstadoDAO();
                estadoDAO.DeleteEstado(id);
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

                var estadoDAO = new EstadoDAO();
                var select = estadoDAO.SelecionarEstado().Select(u => new { Id = u.Id, nmEstado = u.nmEstado, dsUF = u.dsUF });

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
