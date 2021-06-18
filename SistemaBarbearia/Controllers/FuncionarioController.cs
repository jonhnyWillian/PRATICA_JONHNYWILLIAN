using SistemaBarbearia.DAOs.Funcionarios;
using SistemaBarbearia.DataTables;
using SistemaBarbearia.Models.Funcionarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SistemaBarbearia.Controllers
{
    public class FuncionarioController : Controller
    {
        // GET: Funcionario
        public ActionResult Index()
        {
            var funcionarioDAO = new FuncionarioDAO();
            ModelState.Clear();
            return View(funcionarioDAO.SelecionarFuncionario());
        }

        // GET: Funcionario/Details/5
        public ActionResult Details(int id)
        {
            var funcionarioDAO = new FuncionarioDAO();
            return View(funcionarioDAO.GetFuncionario(id));
        }

        // GET: Funcionario/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Funcionario/Create
        [HttpPost]
        public ActionResult Create(Funcionario funcionario)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var funcionarioDAO = new FuncionarioDAO();

                    if (funcionarioDAO.InsertFuncionario(funcionario))
                    {
                        ViewBag.Message = "Funcionario criado com sucesso!";
                    }
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Funcionario/Edit/5
        public ActionResult Edit(int id)
        {
            var funcionarioDAO = new FuncionarioDAO();
            return View(funcionarioDAO.GetFuncionario(id));
        }

        // POST: Funcionario/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Funcionario funcionario)
        {

            try
            {
                if (ModelState.IsValid)
                {
                    var funcionarioDAO = new FuncionarioDAO();

                    if (funcionarioDAO.UpdateFuncionario(funcionario))
                    {
                        ViewBag.Message = "Funcionario criado com sucesso!";
                    }
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Funcionario/Delete/5
        public ActionResult Delete(int id)
        {
            var funcionarioDAO = new FuncionarioDAO();
            return View(funcionarioDAO.GetFuncionario(id));
        }

        // POST: Funcionario/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, Funcionario funcionario)
        {
            try
            {
                var funcionarioDAO = new FuncionarioDAO();
                funcionarioDAO.DeleteFuncionario(id);

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

                var funcionarioDAO = new FuncionarioDAO();
                var select = funcionarioDAO.SelecionarFuncionario().Select(u => new { IdFuncionario = u.IdFuncionario, nmFuncionario = u.nmFuncionario, nrTelefone = u.nrTelefone });

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
