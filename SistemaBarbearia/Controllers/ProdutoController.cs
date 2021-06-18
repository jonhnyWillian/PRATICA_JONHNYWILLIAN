using SistemaBarbearia.DAOs.Produtos;
using SistemaBarbearia.DataTables;
using SistemaBarbearia.Models.Produtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SistemaBarbearia.Controllers
{
    public class ProdutoController : Controller
    {
        
        public ActionResult Index()
        {
            var produtoDAO = new ProdutoDAO();
            ModelState.Clear();
            return View(produtoDAO.SelecionarProduto());
        }

        public ActionResult Details(int id)
        {
            var produtoDAO = new ProdutoDAO();
            return View(produtoDAO.GetProduto(id));
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Produto produto)
        {
            if (produto.dsProduto == null)
            {
                ModelState.AddModelError("", "Nome do Produto Nao pode ser em braco");
            }
            if (string.IsNullOrWhiteSpace(produto.codBarra))
            {
                ModelState.AddModelError("", "Codigo de Barra do Produto Nao pode ser em braco");
            }
            try
            {
                if (ModelState.IsValid)
                {
                    var produtoDAO = new ProdutoDAO();

                    produtoDAO.InsertProduto(produto);
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
            var produtoDAO = new ProdutoDAO();
            return View(produtoDAO.GetProduto(id));
        }

        [HttpPost]
        public ActionResult Edit(int id, Produto produto)
        {
            if (string.IsNullOrWhiteSpace(produto.dsProduto))
            {
                ModelState.AddModelError("", "Nome do Produto Nao pode ser em braco");
            }
            if (string.IsNullOrWhiteSpace(produto.codBarra))
            {
                ModelState.AddModelError("", "Codigo de Barra do Produto Nao pode ser em braco");
            }
            try
            {
                if (ModelState.IsValid)
                {
                    var produtoDAO = new ProdutoDAO();

                    produtoDAO.UpdateProduto(produto);

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
            var produtoDAO = new ProdutoDAO();
            return View(produtoDAO.GetProduto(id));
        }

        [HttpPost]
        public ActionResult Delete(int id, Produto produto)
        {
            try
            {
                var produtoDAO = new ProdutoDAO();
                produtoDAO.DeleteProduto(id);
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
                var produtoDAO = new ProdutoDAO();
                IQueryable<dynamic> lista = produtoDAO.SelecionarProduto().Select(u => new { IdProduto = u.idCategoria, dsProduto = u.dsProduto }).AsQueryable();
                return Json(new JsonSelect<object>(lista, page, 10), JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult JsInsert(Produto produto)
        {
            var produtoDAO = new ProdutoDAO();
            produtoDAO.InsertProduto(produto);
            var result = new
            {
                type = "success",
                field = "",
                message = "Registro adicionado com sucesso!",
                model = produto
            };
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult JsUpdate(Produto produto)
        {
            var produtoDAO = new ProdutoDAO();
            produtoDAO.UpdateProduto(produto);

            var result = new
            {
                type = "success",
                field = "",
                message = "Registro alterado com sucesso!",
                model = produto
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
            var produtoDAO = new ProdutoDAO();
            var list = produtoDAO.SelectCategoria(id, text);
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
