using SistemaBarbearia.DAOs.Paises;
using SistemaBarbearia.Models.Paises;
using SistemaBarbearia.Helper;
using SistemaBarbearia.DataTables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SistemaBarbearia.Controllers
{
    public class PaisController : Controller
    {
        public ActionResult Index()
        {
            var paisDAO = new PaisDAO();
            ModelState.Clear();
            return View(paisDAO.SelecionarPais());
        }

        public ActionResult Details(int id)
        {
            var paisDAO = new PaisDAO();
            return View(paisDAO.GetPais(id));
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Pais pais)
        {
            if (string.IsNullOrWhiteSpace(pais.nmPais))
            {
                ModelState.AddModelError("pais.nmPais", "Nome do Pais Nao pode ser em braco");
            }
            if (string.IsNullOrWhiteSpace(pais.dsSigla))
            {
                ModelState.AddModelError("pais.dsSigla", "Sigla do Pais Nao pode ser em braco");
            }
            try
            {
                if (ModelState.IsValid)
                {
                    var paisDAO = new PaisDAO();

                    paisDAO.InsertPais(pais);
                    this.AddFlashMessage("Registro salvo com sucesso!"); ;
                    return RedirectToAction("Index");
                }
            }
            catch(Exception ex)
            {
                this.AddFlashMessage(ex.Message, FlashMessage.ERROR);
                return View(pais);
            }
            return View();
        }

        public ActionResult Edit(int id)
        {
            var paisDAO = new PaisDAO();
            return View(paisDAO.GetPais(id));
        }

        [HttpPost]
        public ActionResult Edit(int id, Pais pais)
        {
            if (string.IsNullOrWhiteSpace(pais.nmPais))
            {
                ModelState.AddModelError("", "Nome do Pais Nao pode ser em braco");
            }
            if (string.IsNullOrWhiteSpace(pais.dsSigla))
            {
                ModelState.AddModelError("", "Sigla do Pais Nao pode ser em braco");
            }
            try
            {
                if (ModelState.IsValid)
                {
                    var paisDAO = new PaisDAO();

                    paisDAO.UpdatePais(pais);

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
            var paisDAO = new PaisDAO();
            return View(paisDAO.GetPais(id));
        }

        [HttpPost]
        public ActionResult Delete(int id, Pais pais)
        {
            try
            {
                var paisDAO = new PaisDAO();
                paisDAO.DeletePais(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }


        #region Json
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
                var paisDAO = new PaisDAO();
                IQueryable<dynamic> lista = paisDAO.SelecionarPais().Select(u => new { Id = u.IdPais, Text = u.nmPais }).AsQueryable();
                return Json(new JsonSelect<object>(lista, page, 10), JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult JsInsert(Pais pais)
        {
            var paisDAO = new PaisDAO();
            paisDAO.InsertPais(pais);
            var result = new
            {
                type = "success",
                field = "",
                message = "Registro adicionado com sucesso!",
                model = pais
            };
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult JsUpdate(Pais pais)
        {
            var paisDAO = new PaisDAO();
            paisDAO.UpdatePais(pais);

            var result = new
            {
                type = "success",
                field = "",
                message = "Registro alterado com sucesso!",
                model = pais
            };
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult JsDetails(int? IdPais, string Text)
        {
            try
            {
                var result = this.Find(IdPais, Text).FirstOrDefault();
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

        #endregion

        private IQueryable<dynamic> Find(int? Id, string text)
        {
            var paisDAO = new PaisDAO();
            var list = paisDAO.SelectPais(Id, text);
            var select = list.Select(u => new
            {
                IdPais = u.IdPais,
                Text = u.Text,
                dsSigla = u.dsSigla
             
               
            }).OrderBy(u => u.Text).ToList();
            return select.AsQueryable();
        }

    }
}
