using SistemaBarbearia.DAOs.Categorias;
using SistemaBarbearia.Models.Categorias;
using SistemaBarbearia.DataTables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SistemaBarbearia.Controllers
{
    public class CategoriaController : Controller
    {
     
        public ActionResult Index()
        {
            var categoriaDAO = new CategoriaDAO();
            ModelState.Clear();
            return View(categoriaDAO.SelecionarCategoria());
        }
      
        public ActionResult Details(int id)
        {
            var categoriaDAO = new CategoriaDAO();
            return View(categoriaDAO.GetCategoria(id));
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Categoria categoria)
        {
            if (string.IsNullOrWhiteSpace(categoria.dsCategoria))
            {
                ModelState.AddModelError("", "Nome do Categoria Nao pode ser em braco");
            }           
            try
            {
                if (ModelState.IsValid)
                {
                    var categoriaDAO = new CategoriaDAO();

                    categoriaDAO.InsertCategoria(categoria);
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
            var categoriaDAO = new CategoriaDAO();

            return View(categoriaDAO.GetCategoria(id));
        }

        [HttpPost]
        public ActionResult Edit(int id, Categoria categoria)
        {
            if (string.IsNullOrWhiteSpace(categoria.dsCategoria))
            {
                ModelState.AddModelError("", "Nome do Categoria Nao pode ser em braco");
            }
            try
            {
                if (ModelState.IsValid)
                {
                    var categoriaDAO = new CategoriaDAO();

                    categoriaDAO.UpdateCategoria(categoria);
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
            var categoriaDAO = new CategoriaDAO();
            return View(categoriaDAO.GetCategoria(id));
        }

        [HttpPost]
        public ActionResult Delete(int id, Categoria categoria)
        {
            try
            {
                var categoriaDAO = new CategoriaDAO();
                categoriaDAO.DeleteCategoria(id);

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

                var categoriaDAO = new CategoriaDAO();
                var select = categoriaDAO.SelecionarCategoria().Select(u => new { IdCategoria = u.IdCategoria, dsCategoria = u.dsCategoria});

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

        public JsonResult JsSelect(string q, int? page, int? pageSize)
        {
            try
            {
                var categoriaDAO = new CategoriaDAO();
                IQueryable<dynamic> lista = categoriaDAO.SelecionarCategoria().Select(u => new { IdCategoria = u.IdCategoria, dsCategoria = u.dsCategoria }).AsQueryable();
                return Json(new JsonSelect<object>(lista, page, 10), JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
        }


        public JsonResult JsInsert(Categoria categoria)
        {
            var categoriaDAO = new CategoriaDAO();
            categoriaDAO.InsertCategoria(categoria);
            var result = new
            {
                type = "success",
                field = "",
                message = "Registro adicionado com sucesso!",
                model = categoria
            };
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult JsUpdate(Categoria categoria)
        {
            var categoriaDAO = new CategoriaDAO();
            categoriaDAO.UpdateCategoria(categoria);

            var result = new
            {
                type = "success",
                field = "",
                message = "Registro alterado com sucesso!",
                model = categoria
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
            var categoriaDAO = new CategoriaDAO();
            var list = categoriaDAO.SelectCategoria(id, text);
            var select = list.Select(u => new
            {
                id = u.id,
                text = u.text,

                dtCadastro = u.dtCadastro,
                dtUltAlteracao = u.dtUltAlteracao

            }).OrderBy(u => u.text).ToList();
            return select.AsQueryable();
        }
    }
}
