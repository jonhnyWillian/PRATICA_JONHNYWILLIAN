using SistemaBarbearia.DAOs.Fornecedores;
using SistemaBarbearia.DataTables;
using SistemaBarbearia.Models.Fornecedores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SistemaBarbearia.Controllers
{
    public class FornecedorController : Controller
    {
        // GET: Fornecedor
        public ActionResult Index()
        {
            var fornecedorDAO = new FornecedorDAO();
            ModelState.Clear();
            return View(fornecedorDAO.SelecionarFornecedor());
        }

        // GET: Fornecedor/Details/5
        public ActionResult Details(int id)
        {
            var fornecedorDAO = new FornecedorDAO();
            return View(fornecedorDAO.GetFornecedor(id));
        }

        // GET: Fornecedor/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Fornecedor/Create
        [HttpPost]
        public ActionResult Create(Fornecedor fornecedor)
        {
            try
            {
               
                if (ModelState.IsValid)
                {
                    var fornecedorDAO = new FornecedorDAO();

                    if (fornecedorDAO.InsertFornecedor(fornecedor))
                    {
                        ViewBag.Message = "Fornecedor criado com sucesso!";
                    }
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Fornecedor/Edit/5
        public ActionResult Edit(int id)
        {
            var fornecedorDAO = new FornecedorDAO();
            return View(fornecedorDAO.GetFornecedor(id));
        }

        // POST: Fornecedor/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Fornecedor fornecedor)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var fornecedorDAO = new FornecedorDAO();

                    if (fornecedorDAO.UpdateFornecedor(fornecedor))
                    {
                        ViewBag.Message = "Fornecedor criado com sucesso!";
                    }
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Fornecedor/Delete/5
        public ActionResult Delete(int id)
        {
            var fornecedorDAO = new FornecedorDAO();
            return View(fornecedorDAO.GetFornecedor(id));
        }

        // POST: Fornecedor/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                var fornecedorDAO = new FornecedorDAO();
                fornecedorDAO.DeleteFornecedor(id);

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

                var fornecedorDAO = new FornecedorDAO();
                var select = fornecedorDAO.SelecionarFornecedor().Select(u => new { Id = u.Id, nmNome = u.nmNome, nrTelefone = u.nrTelefone });

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
