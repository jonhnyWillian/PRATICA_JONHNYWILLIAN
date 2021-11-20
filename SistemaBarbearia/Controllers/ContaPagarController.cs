using SistemaBarbearia.DAOs.ContasPagar;
using SistemaBarbearia.Models.ContasPagar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SistemaBarbearia.Controllers
{
    public class ContaPagarController : Controller
    {

        #region MethodPrivate
        private ActionResult GetView(string nrModelo, string nrSerie, int nrNota, int? nrParcela,  int? IdFornecedor)
        {
            try
            {
                var Dao = new ContaPagarDAO();
                var contaPagar = Dao.GetContaPagar(null, nrModelo, nrSerie, nrNota, nrParcela, IdFornecedor);
                return View(contaPagar);
            }
            catch (Exception)
            {
                return View();
            }
        }

        private ActionResult GetViewDetails(string nrModelo, string nrSerie, int nrNota, int? nrParcela, int? IdFornecedor)
        {
            try
            {
                var Dao = new ContaPagarDAO();
                var contaPagar = Dao.GetContaDetais(null, nrModelo, nrSerie, nrNota, nrParcela, IdFornecedor);
                return View(contaPagar);
            }
            catch (Exception)
            {
                return View();
            }
        }

        #endregion

        public ActionResult Index()
        {
            var dao = new ContaPagarDAO();
            ModelState.Clear();
            return View(dao.SelecionarCompra());
        }
    
        public ActionResult Details(string nrModelo, string nrSerie, int nrNota, int? nrParcela, int? IdFornecedor)
        {
            return this.GetViewDetails(nrModelo, nrSerie, nrNota, IdFornecedor, nrParcela);
        }
      
        public ActionResult Create(string nrModelo, string nrSerie, int nrNota, int? nrParcela, int? IdFornecedor)
        {
            return this.GetView(nrModelo, nrSerie, nrNota, IdFornecedor, nrParcela);
        }

        [HttpPost]
        public ActionResult Create(ContaPagar contaPagar , string nrModelo, string nrSerie, int nrNota, int IdFornecedor, int nrParcela)
        {
            if  (contaPagar.ContaBancaria.IdConta == null)
            {
                ModelState.AddModelError("ContaBancaria.IdConta", "Informe a Conta Bancaria");
            }
            try
            {
                var dao = new ContaPagarDAO();
                dao.PagarCompra(contaPagar, nrModelo, nrSerie, nrNota, IdFornecedor, nrParcela);
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
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
