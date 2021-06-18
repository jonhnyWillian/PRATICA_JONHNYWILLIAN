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
       
        public ActionResult Index()
        {
            var cidadeDAO = new CidadeDAO();
            ModelState.Clear();
            return View(cidadeDAO.SelecionarCidade());
        }

        public ActionResult Details(int id)
        {
            var cidadeDAO = new CidadeDAO();
            ModelState.Clear();
            return View(cidadeDAO.GetCidade(id));
        }
       
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Cidade cidade)
        {

            if (string.IsNullOrWhiteSpace(cidade.nmCidade))
            {
                ModelState.AddModelError("", "Nome do Cidade Nao pode ser em braco");
            }
            if (string.IsNullOrWhiteSpace(cidade.DDD))
            {
                ModelState.AddModelError("", "DDD do Pais Nao pode ser em braco");
            }
            try
            {
                if (ModelState.IsValid)
                {
                    var cidadeDAO = new CidadeDAO();

                    cidadeDAO.InsertCidade(cidade);
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
            var cidadeDAO = new CidadeDAO();
            return View(cidadeDAO.GetCidade(id));
        }

        [HttpPost]
        public ActionResult Edit(int id, Cidade cidade)
        {
            if (string.IsNullOrWhiteSpace(cidade.nmCidade))
            {
                ModelState.AddModelError("", "Nome do Cidade Nao pode ser em braco");
            }
            if (string.IsNullOrWhiteSpace(cidade.DDD))
            {
                ModelState.AddModelError("", "DDD do Pais Nao pode ser em braco");
            }
            try
            {
                if (ModelState.IsValid)
                {
                    var cidadeDAO = new CidadeDAO();

                    cidadeDAO.UpdateCidade(cidade);

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
            var cidadeDAO = new CidadeDAO();

            return View(cidadeDAO.GetCidade(id));
        }

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
                var cidadeDAO = new CidadeDAO();
                IQueryable<dynamic> lista = cidadeDAO.SelecionarCidade().Select(u => new { IdCidade = u.idEstado, nmCidade = u.nmCidade }).AsQueryable();
                return Json(new JsonSelect<object>(lista, page, 10), JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult JsInsert(Cidade cidade)
        {
            var cidadeDAO = new CidadeDAO();
            cidadeDAO.InsertCidade(cidade);
            var result = new
            {
                type = "success",
                field = "",
                message = "Registro adicionado com sucesso!",
                model = cidade
            };
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult JsUpdate(Cidade cidade)
        {
            var cidadeDAO = new CidadeDAO();
            cidadeDAO.UpdateCidade(cidade);

            var result = new
            {
                type = "success",
                field = "",
                message = "Registro alterado com sucesso!",
                model = cidade
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
            var cidadeDAO = new CidadeDAO();
            var list = cidadeDAO.SelectCidade(id, text);
            var select = list.Select(u => new
            {
                IdCidade = u.IdCidade,
                nmCidade = u.nmCidade,
                DDD = u.DDD,
                dtCadastro = u.dtCadastro,
                dtUltAlteracao = u.dtUltAlteracao

            }).OrderBy(u => u.nmCidade).ToList();
            return select.AsQueryable();
        }
    }
}
