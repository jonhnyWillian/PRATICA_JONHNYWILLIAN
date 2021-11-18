using SistemaBarbearia.DAOs.Categorias;
using SistemaBarbearia.DAOs.Fornecedores;
using SistemaBarbearia.DAOs.Produtos;
using SistemaBarbearia.DataTables;
using SistemaBarbearia.Models.Produtos;
using SistemaBarbearia.ViewModels.Produtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SistemaBarbearia.Controllers
{
    public class ProdutoController : Controller
    {

        #region MethodPrivate
        private ActionResult GetView(int id)
        {
            ProdutoDAO produto = new ProdutoDAO();
            CategoriaDAO categoria = new CategoriaDAO();
            FornecedorDAO fornecedor = new FornecedorDAO();
            var obj = produto.GetProduto(id);
            var result = new ProdutoVW
            {
                IdProduto = obj.IdProduto,
                dsProduto = obj.dsProduto,
                codBarra = obj.codBarra,
                nrQtd = obj.nrQtd,
                nrUnidade = obj.nrUnidade,
                vlCompra = obj.vlCompra,
                vlCusto = obj.vlCusto,
                vlVenda = obj.vlVenda,
                vlUltCompra = obj.vlUltCompra,
                dtCadastro = obj.dtCadastro,
                dtUltAlteracao = obj.dtUltAlteracao,
                IdCategoria = obj.IdCategoria,
                IdFornecedor = obj.IdFornecedor
               
            };
            var objCategoria = categoria.GetCategoria(result.IdCategoria);
            result.categoria = new ViewModels.Categorias.SelectCategoriaVM { Id = objCategoria.IdCategoria, Text = objCategoria.dsCategoria };
            var objFornecedor = fornecedor.GetFornecedor(result.IdFornecedor);
            result.fornecedor = new ViewModels.Fornecedores.SelectFornecedorVM { IdFornecedor = objFornecedor.IdFornecedor, nmNome = objFornecedor.nmNome };
            return View(result);
        }

        private IQueryable<dynamic> Find(int? Id, string Text, int? IdFornecedor)
        {
            var produtoDAO = new ProdutoDAO();
            var list = produtoDAO.SelectProduto(Id, Text, IdFornecedor);
            var select = list.Select(u => new
            {
                IdProduto = u.IdProduto,
                dsProduto = u.dsProduto,
                vlVenda = u.vlVenda

            }).OrderBy(u => u.dsProduto).ToList();
            return select.AsQueryable();
        }

        #endregion

        #region Action
        public ActionResult Index()
        {
            var produtoDAO = new ProdutoDAO();
            ModelState.Clear();
            return View(produtoDAO.SelecionarProduto());
        }

        public ActionResult Details(int id)
        {

            return GetView(id);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Produto produto)
        {
            if (string.IsNullOrWhiteSpace(produto.dsProduto))
            {
                ModelState.AddModelError("", "Nome do Produto Nao pode ser em braco");
            }
            if (string.IsNullOrWhiteSpace(produto.codBarra))
            {
                ModelState.AddModelError("", "Codigo de Barra do Produto Nao pode ser em braco");
            }
            if (string.IsNullOrWhiteSpace(produto.nrUnidade))
            {
                ModelState.AddModelError("", "Unidade do Produto Nao pode ser em braco");
            }

            try
            {
                if (ModelState.IsValid)
                {
                    var produtoDAO = new ProdutoDAO();

                    produtoDAO.InsertProduto(produto);
                    this.AddFlashMessage("Registro salvo com sucesso!");
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
            return GetView(id);
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
                    this.AddFlashMessage("Registro Alterado com sucesso!");
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
            return GetView(id);
        }

        [HttpPost]
        public ActionResult Delete(int id, Produto produto)
        {
            try
            {
                var produtoDAO = new ProdutoDAO();
                produtoDAO.DeleteProduto(id);
                this.AddFlashMessage("Registro removido com sucesso!");
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        #endregion

        #region Json
        public JsonResult JsQuery([ModelBinder(typeof(DataTablesBinder))] IDataTablesRequest requestModel, int? IdFornecedor)
        {
            try
            {
                IdFornecedor = IdFornecedor == 0 ? null : IdFornecedor;
                var select = this.Find(null, requestModel.Search.Value, IdFornecedor);

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
                
                var select = this.Find(null, q, null);
                return Json(new JsonSelect<object>(select, page, 10), JsonRequestBehavior.AllowGet);

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

        public JsonResult JsDetails(int? Id, string Text, int? IdFornecedor)
        {
            try
            {
                var result = this.Find(Id, Text, IdFornecedor).FirstOrDefault();
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

    }
}
