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
        
        public ActionResult Details(int id)
        {
           var formaPagamentoDAO = new FormaPagamentoDAO();
            return View(formaPagamentoDAO.GetFormaPagamento(id));
        }
       
        public ActionResult Create()
        {
            return View();
        }
    
        [HttpPost]
        public ActionResult Create(FormaPagamento formaPagamento)
        {

            if (string.IsNullOrWhiteSpace(formaPagamento.dsFormaPagamento))
            {
                ModelState.AddModelError("", "Nome do Forma Pagamento Nao pode ser em braco");
            }           
            try
            {
                if (ModelState.IsValid)
                {
                    var formaPagamentoDAO = new FormaPagamentoDAO();

                    formaPagamentoDAO.InsertFormaPagamento(formaPagamento);
                    return RedirectToAction("Index");
                }

                return View();
            }
            catch
            {
                return View();
            }
        }
       
        public ActionResult Edit(int id)
        {
           var formaPagamentoDAO = new FormaPagamentoDAO();
            return View(formaPagamentoDAO.GetFormaPagamento(id));
        }
       
        [HttpPost]
        public ActionResult Edit(int id, FormaPagamento formaPagamento)
        {
            if (string.IsNullOrWhiteSpace(formaPagamento.dsFormaPagamento))
            {
                ModelState.AddModelError("", "Nome do Forma Pagamento Nao pode ser em braco");
            }           
            try
            {
                if (ModelState.IsValid)
                {
                    var formaPagamentoDAO = new FormaPagamentoDAO();

                    formaPagamentoDAO.UpdateFormaPagamento(formaPagamento);

                    return RedirectToAction("Index");

                }
                return View();
            }
            catch
            {
                return View();
            }
        }
       
        public ActionResult Delete(int id)
        {
           var formaPagamentoDAO = new FormaPagamentoDAO();
            return View(formaPagamentoDAO.GetFormaPagamento(id));
        }
       
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
                var select = formaPagamentoDAO.SelecionarFormaPagamento().Select(u => new { Id = u.IdFormaPag, dsFormaPagamento = u.dsFormaPagamento });

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
