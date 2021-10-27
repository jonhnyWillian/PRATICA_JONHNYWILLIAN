using SistemaBarbearia.DAOs.Compras;
using SistemaBarbearia.DAOs.CondPagamentos;
using SistemaBarbearia.DAOs.Fornecedores;
using SistemaBarbearia.DAOs.Produtos;
using SistemaBarbearia.Models.Compras;
using SistemaBarbearia.ViewModels.Compras;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SistemaBarbearia.Controllers
{
    public class CompraController : Controller
    {

        #region MethodPrivate
        private ActionResult GetView(int id)
        {
            FornecedorDAO daoFornecedor = new FornecedorDAO();
            CondPagamentoDAO daoCondPag = new CondPagamentoDAO();
            CompraDAO daoCompra = new CompraDAO();
            ProdutoDAO daoProduto = new ProdutoDAO();
            var obj = daoCompra.GetCompra(id);
            var result = new CompraVM
            {
                IdModelPai = obj.IdCompra,
                IdFornecedor = obj.IdFornecedor,
                IdCondPag = obj.IdCondPag,
                IdProduto = obj.IdProduto,
                dtEmissao = obj.dtEmissao,
                dtEntrega = obj.dtEntrega,
                nrModelo = obj.nrModelo,
                nrSerie = obj.nrSerie,
                nrNota = obj.nrNota,

            };
            var objFornecedor = daoFornecedor.GetFornecedor(result.IdFornecedor);
            result.Fornecedor = new ViewModels.Fornecedores.SelectFornecedorVM { IdFornecedor = objFornecedor.IdFornecedor, nmNome = objFornecedor.nmNome };

            var objProduto = daoProduto.GetProduto(result.IdProduto);
            result.Produto = new ViewModels.Produtos.SelectProdutoVM { IdProduto = objProduto.IdProduto, dsProduto = objProduto.dsProduto };

            var objCondPag = daoCondPag.GetCondPagamento(result.IdCondPag);
            result.CondicaoPagamento = new ViewModels.CondPagamentos.SelectCondPagamentoVM { Id = objCondPag.IdCondPag, Text = objCondPag.dsCondPag };


            return View(result);
        }
        #endregion


        public ActionResult Index()
        {
            var compraDAO = new CompraDAO();
            ModelState.Clear();
            return View(compraDAO.SelecionarCompra());
        }
      
        public ActionResult Details(int id)
        {
            return this.GetView(id);
        }
      
        public ActionResult Create()
        {
            return View();
        }
      
        [HttpPost]
        public ActionResult Create(Compra model)
        {
            model.dtEntrega = !string.IsNullOrEmpty(model.dtEntregaAux) ? Convert.ToDateTime(model.dtEntregaAux) : model.dtEntrega;
            model.dtEmissao = !string.IsNullOrEmpty(model.dtEmissaoAux) ? Convert.ToDateTime(model.dtEmissaoAux) : model.dtEmissao;
            model.nrModelo = !string.IsNullOrEmpty(model.nrModeloAux) ? model.nrModeloAux : model.nrModelo;
            model.nrSerie = !string.IsNullOrEmpty(model.nrSerieAux) ? model.nrSerieAux : model.nrSerie;
            model.nrNota = model.nrNotaAux != null ? model.nrNotaAux : model.nrNota;
            model.Fornecedor.IdFornecedor = model.Fornecedor.IdFornecedor != null ? model.Fornecedor.IdFornecedor : model.Fornecedor.IdFornecedor;
            
            model.dtCadastro = DateTime.Now;

            model.flSituacao = "A";


            if (string.IsNullOrWhiteSpace(model.nrModelo))
            {
                ModelState.AddModelError("modelo", "Informe o modelo");
            }
            if (string.IsNullOrWhiteSpace(model.nrSerie))
            {
                ModelState.AddModelError("serie", "Informe a série");
            }
            if (model.nrNota == null || model.nrNota == 0)
            {
                ModelState.AddModelError("nrNota", "Informe o número da nota");
            }
            if (model.Fornecedor.IdFornecedor == null)
            {
                ModelState.AddModelError("Fornecedor.id", "Informe o fornecedor");
            }
            if (model.dtEmissao == null)
            {
                ModelState.AddModelError("dtEmissao", "Informe a data de emissão");
            }
            if (model.dtEntrega == null)
            {
                ModelState.AddModelError("dtEntrega", "Informe a data de entrega");
            }
            if (model.dtEntrega != null && model.dtEmissao != null)
            {
                if (model.dtEntrega < model.dtEmissao)
                {
                    ModelState.AddModelError("dtEntrega", "A data de entrega não pode ser menor que a data de emissão");
                }
            }
           

            try
            {
                var dao = new CompraDAO();
                dao.InsertCompra(model);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    
        public ActionResult Edit(int id)
        {
            return View();
        }
      
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        
        public ActionResult Delete(int id)
        {
            return View();
        }
        
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {              
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public JsonResult JsVerificaNF(string modelo, string serie, int numero, int idFornecedor)
        {
            var dao = new CompraDAO();
            var validNF = dao.validNota(modelo, serie, numero, idFornecedor);
            var type = string.Empty;
            var msg = string.Empty;
            if (validNF)
            {
                type = "success";
                msg = "Nota Fiscal válida!";
            }
            else
            {
                type = "danger";
                msg = "Já existe uma Nota Fiscal registrada com este número e fornecedor, verifique!";
            }
            var result = new
            {
                type = type,
                message = msg,
            };
            return Json(result, JsonRequestBehavior.AllowGet);
        }

    }
}
