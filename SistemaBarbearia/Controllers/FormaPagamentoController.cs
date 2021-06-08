using SistemaBarbearia.DAOs.FormaPagamentos;
using SistemaBarbearia.DataTables;
using SistemaBarbearia.Models.FormaPagamentos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SistemaBarbearia.Controllers
{
    public class FormaPagamentoController : Controller
    {
        public ActionResult Index()
        {
            var formaPagamentoDAO = new FormaPagamentoDAO();
            ModelState.Clear();
            return View(formaPagamentoDAO.SelecionarFormaPagamento());
        }

        // GET: FormaPagamento/Details/5
        public ActionResult Details(int id)
        {
           var formaPagamentoDAO = new FormaPagamentoDAO();
            return View(formaPagamentoDAO.GetFormaPagamento(id));
        }

        // GET: FormaPagamento/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: FormaPagamento/Create
        [HttpPost]
        public ActionResult Create(FormaPagamento formaPagamento)
        {

            try
            {
                if (ModelState.IsValid)
                {
                    var formaPagamentoDAO = new FormaPagamentoDAO();

                    if (formaPagamentoDAO.InsertFormaPagamento(formaPagamento))
                    {
                        ViewBag.Message = "Forma Pagamento criado com sucesso!";
                    }
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: FormaPagamento/Edit/5
        public ActionResult Edit(int id)
        {
           var formaPagamentoDAO = new FormaPagamentoDAO();
            return View(formaPagamentoDAO.GetFormaPagamento(id));
        }

        // POST: FormaPagamento/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormaPagamento formaPagamento)
        {
            try
            {
                var formaPagamentoDAO = new FormaPagamentoDAO();
                formaPagamentoDAO.UpdateFormaPagamento(formaPagamento);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: FormaPagamento/Delete/5
        public ActionResult Delete(int id)
        {
           var formaPagamentoDAO = new FormaPagamentoDAO();
            return View(formaPagamentoDAO.GetFormaPagamento(id));
        }

        // POST: FormaPagamento/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormaPagamento servico)
        {
            try
            {
                var formaPagamentoDAO = new FormaPagamentoDAO();
                formaPagamentoDAO.DeleteFormaPagamento(id);
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

                var formaPagamentoDAO = new FormaPagamentoDAO();
                var select = formaPagamentoDAO.SelecionarFormaPagamento().Select(u => new { Id = u.Id, dsFormaPagamento = u.dsFormaPagamento });

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
