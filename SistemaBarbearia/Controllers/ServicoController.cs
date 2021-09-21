using SistemaBarbearia.DAOs.Servicos;
using SistemaBarbearia.Models.Servicos;
using SistemaBarbearia.DataTables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;

namespace SistemaBarbearia.Controllers
{
    public class ServicoController : Controller
    {
      
        public ActionResult Index()
        {
            var servicoDAO = new ServicoDAO();
            ModelState.Clear();
            return View(servicoDAO.SelecionarServico());
        }

        public ActionResult Details(int id)
        {
            var servicoDAO = new ServicoDAO();
            return View(servicoDAO.GetServico(id));
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Servico servico)
        {
            if (string.IsNullOrWhiteSpace(servico.dsServico))
            {
                ModelState.AddModelError("", "Nome do serviço Nao pode ser em braco");
            }
            if (servico.vlServico == null)
            {
                ModelState.AddModelError("", "Valor de serviço do Produto não pode ser em braco");     
            }
            try
            {
                if (ModelState.IsValid)
                {
                    var servicoDAO = new ServicoDAO();

                    servicoDAO.InsertServico(servico);
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
            var servicoDAO = new ServicoDAO();
            return View(servicoDAO.GetServico(id));
        }

        [HttpPost]
        public ActionResult Edit(int id, Servico servico)
        {
            if (string.IsNullOrWhiteSpace(servico.dsServico))
            {
                ModelState.AddModelError("", "Nome do serviço Nao pode ser em braco");
            }
            if (servico.vlServico == null)
            {
                ModelState.AddModelError("", "Valor de serviço do Produto não pode ser em braco");
            }
            try
            {
                if (ModelState.IsValid)
                {
                    var servicoDAO = new ServicoDAO();

                    servicoDAO.UpdateServico(servico);
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
            var servicoDAO = new ServicoDAO();
            return View(servicoDAO.GetServico(id));
        }

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
                var servicoDAO = new ServicoDAO();
                IQueryable<dynamic> lista = servicoDAO.SelecionarServico().Select(u => new { IdServico = u.IdServico, dsServico = u.dsServico }).AsQueryable();
                return Json(new JsonSelect<object>(lista, page, 10), JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
        }


        public JsonResult JsInsert(Servico servico)
        {
            var servicoDAO = new ServicoDAO();
            servicoDAO.InsertServico(servico);
            var result = new
            {
                type = "success",
                field = "",
                message = "Registro adicionado com sucesso!",
                model = servico
            };
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult JsUpdate(Servico servico)
        {
            var servicoDAO = new ServicoDAO();
            servicoDAO.UpdateServico(servico);

            var result = new
            {
                type = "success",
                field = "",
                message = "Registro alterado com sucesso!",
                model = servico
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
            var servicoDAO = new ServicoDAO();
            var list = servicoDAO.SelectServico(Id, Text);
            var select = list.Select(u => new
            {
                IdServico = u.IdServico,
                dsServico = u.dsServico,
              

            }).OrderBy(u => u.dsServico).ToList();
            return select.AsQueryable();
        }
    }
}
