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
        // GET: Pais
        public ActionResult Index()
        {
            var paisDAO = new PaisDAO();
            ModelState.Clear();
            return View(paisDAO.SelecionarPais());
        }

        // GET: Pais/Details/5
        public ActionResult Details(int id)
        {
            var paisDAO = new PaisDAO();
            return View(paisDAO.GetPais(id));
        }

        // GET: Pais/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Pais/Create
        [HttpPost]
        public ActionResult Create(Pais pais)
        {
            if (pais.nmPais == null)
            {
                ModelState.AddModelError("", "Nome do Pais Nao pode ser em braco");
            }
            if (pais.dsSigla == null)
            {
                ModelState.AddModelError("", "Sigla do Pais Nao pode ser em braco");
            }
            try
            {
                if (ModelState.IsValid)
                {
                    var paisDAO = new PaisDAO();

                    if (paisDAO.InsertPais(pais))
                    {                      
                        ViewBag.Message = "Pais criado com sucesso!";
                    }
                }
               
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Pais/Edit/5
        public ActionResult Edit(int id)
        {
            var paisDAO = new PaisDAO();
            return View(paisDAO.GetPais(id));
        }

        // POST: Pais/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Pais pais)
        {
            try
            {
                var paisDAO = new PaisDAO();

                paisDAO.UpdatePais(pais);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Pais/Delete/5
        public ActionResult Delete(int id)
        {
            var paisDAO = new PaisDAO();
            return View(paisDAO.GetPais(id));
        }

        // POST: Pais/Delete/5
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
                IQueryable<dynamic> lista = paisDAO.SelecionarPais().Select(u => new { id = u.Id, nmPais = u.nmPais }).AsQueryable();
                return Json(new JsonSelect<object>(lista, page, 10), JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
        }


        public JsonResult JsCreate(Pais pais)
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
            var paisDAO = new PaisDAO();
            var list = paisDAO.SelectPais(id, text);
            var select = list.Select(u => new
            {
                idPais = u.idPais,
                nmPais = u.nmPais,
                dsSigla = u.dsSigla,
                dtCadastro = u.dtCadastro,
                dtUltAlteracao = u.dtUltAlteracao

            }).OrderBy(u => u.nmPais).ToList();
            return select.AsQueryable();
        }

    }
}
