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
        // GET: Produto
        public ActionResult Index()
        {
            var produtoDAO = new ProdutoDAO();
            ModelState.Clear();
            return View(produtoDAO.SelecionarProduto());
        }

        // GET: Produto/Details/5
        public ActionResult Details(int id)
        {
            var produtoDAO = new ProdutoDAO();
            return View(produtoDAO.GetProduto(id));
        }

        // GET: Produto/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Produto/Create
        [HttpPost]
        public ActionResult Create(Produto produto)
        {
            if (produto.dsProduto == null)
            {
                ModelState.AddModelError("", "Nome do Produto Nao pode ser em braco");
            }
            
            try
            {
                if (ModelState.IsValid)
                {
                    var produtoDAO = new ProdutoDAO();

                    if (produtoDAO.InsertProduto(produto))
                    {
                        ViewBag.Message = "Produto criado com sucesso!";
                    }
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Produto/Edit/5
        public ActionResult Edit(int id)
        {
            var produtoDAO = new ProdutoDAO();
            return View(produtoDAO.GetProduto(id));
        }

        // POST: Produto/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Produto produto)
        {
            try
            {
                var produtoDAO = new ProdutoDAO();

                produtoDAO.UpdateProduto(produto);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Produto/Delete/5
        public ActionResult Delete(int id)
        {
            var produtoDAO = new ProdutoDAO();
            return View(produtoDAO.GetProduto(id));
        }

        // POST: Produto/Delete/5
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

                var produtoDAO = new ProdutoDAO();
                var select = produtoDAO.SelecionarProduto().Select(u => new { id = u.Id, text = u.dsProduto });

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
