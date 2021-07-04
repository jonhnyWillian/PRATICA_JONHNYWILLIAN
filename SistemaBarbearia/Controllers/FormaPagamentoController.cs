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

        public JsonResult JsSelect(string q, int? page, int? pageSize)
        {
            try
            {
                var formaPagamentoDAO = new FormaPagamentoDAO();
                IQueryable<dynamic> lista = formaPagamentoDAO.SelecionarFormaPagamento().Select(u => new { IdFormaPag = u.IdFormaPag, dsFormaPagamento = u.dsFormaPagamento }).AsQueryable();
                return Json(new JsonSelect<object>(lista, page, 10), JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult JsInsert(FormaPagamento formaPagamento)
        {
            var formaPagamentoDAO = new FormaPagamentoDAO();
            formaPagamentoDAO.InsertFormaPagamento(formaPagamento);
            var result = new
            {
                type = "success",
                field = "",
                message = "Registro adicionado com sucesso!",
                model = formaPagamento
            };
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult JsUpdate(FormaPagamento formaPagamento)
        {
            var formaPagamentoDAO = new FormaPagamentoDAO();
            formaPagamentoDAO.UpdateFormaPagamento(formaPagamento);

            var result = new
            {
                type = "success",
                field = "",
                message = "Registro alterado com sucesso!",
                model = formaPagamento
            };
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult JsDetails(int? id, string text)
        {
            try
            {
                var result = this.Find(id, text).FirstOrDefault();
                if (result != null)
                    return Json(result, JsonRequestBehavior.AllowGet);
                return Json(string.Empty, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                throw new Exception(ex.Message);
            }
        }

        private IQueryable<dynamic> Find(int? Id, string Text)
        {
            var formaPagamentoDAO = new FormaPagamentoDAO();
            var list = formaPagamentoDAO.SelectFormaPagamento(Id, Text);
            var select = list.Select(u => new
            {
                Id = u.Id,
                Text = u.Text,               
                dtCadastro = u.dtCadastro,
                dtUltAlteracao = u.dtUltAlteracao

            }).OrderBy(u => u.Text).ToList();
            return select.AsQueryable();
        }
    }
}
