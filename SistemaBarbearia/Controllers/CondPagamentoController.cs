using SistemaBarbearia.DAOs.CondPagamentos;
using SistemaBarbearia.DataTables;
using SistemaBarbearia.Models.CondPagamento;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SistemaBarbearia.Controllers
{
    public class CondPagamentoController : Controller
    {
        public ActionResult Index()
        {
            var condPag = new CondPagamentoDAO();
            ModelState.Clear();
            return View(condPag.SelecionarCondPagamento());
        }

        public ActionResult Details(int id)
        {
            var condPag = new CondPagamentoDAO();
            return View(condPag.GetCondPagamento(id));
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(CondPagamento condPagamento)
        {
            if (string.IsNullOrWhiteSpace(condPagamento.dsCondPag))
            {
                ModelState.AddModelError("", "Nome do CondPagamento Nao pode ser em braco");
            }       
            try
            {
                if (ModelState.IsValid)
                {
                    var condPag = new CondPagamentoDAO();

                    condPag.InsertCondPagamento(condPagamento);

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
            var condPag = new CondPagamentoDAO();
            return View(condPag.GetCondPagamento(id));
        }

        [HttpPost]
        public ActionResult Edit(int id, CondPagamento condPagamento)
        {
            if (string.IsNullOrWhiteSpace(condPagamento.dsCondPag))
            {
                ModelState.AddModelError("", "Nome do CondPagamento Nao pode ser em braco");
            }
            
            try
            {
                if (ModelState.IsValid)
                {
                    var condPag = new CondPagamentoDAO();

                    condPag.UpdateCondPagamento(condPagamento);

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
            var condPag = new CondPagamentoDAO();
            return View(condPag.GetCondPagamento(id));
        }

        [HttpPost]
        public ActionResult Delete(int id, CondPagamento condPagamento)
        {
            try
            {
                var condPag = new CondPagamentoDAO();
                condPag.DeleteCondPagamento(id);
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
                var condPag = new CondPagamentoDAO();
                IQueryable<dynamic> lista = condPag.SelecionarCondPagamento().Select(u => new { Id = u.IdCondPag, Text = u.dsCondPag }).AsQueryable();
                return Json(new JsonSelect<object>(lista, page, 10), JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult JsInsert(CondPagamento condPagamento)
        {
            var condPag = new CondPagamentoDAO();
            condPag.InsertCondPagamento(condPagamento);
            var result = new
            {
                type = "success",
                field = "",
                message = "Registro adicionado com sucesso!",
                model = condPagamento
            };
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult JsUpdate(CondPagamento condPagamento)
        {
            var condPag = new CondPagamentoDAO();
            condPag.UpdateCondPagamento(condPagamento);

            var result = new
            {
                type = "success",
                field = "",
                message = "Registro alterado com sucesso!",
                model = condPagamento
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
        private IQueryable<dynamic> Find(int? id, string text)
        {
            var condPag = new CondPagamentoDAO();
            var list = condPag.SelectCondPagamento(id, text);
            var select = list.Select(u => new
            {
                Id = u.Id,
                Text = u.Text,              

            }).OrderBy(u => u.Text).ToList();
            return select.AsQueryable();
        }
    }
}
