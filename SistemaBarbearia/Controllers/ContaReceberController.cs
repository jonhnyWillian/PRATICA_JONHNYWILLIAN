using SistemaBarbearia.DAOs.ContasReceber;
using SistemaBarbearia.Models.ContaReceber;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SistemaBarbearia.Controllers
{
    public class ContaReceberController : Controller
    {

        #region MethodPrivate
        private ActionResult GetView(string nrModelo, string nrSerie, int nrNota, int? nrParcela, int? IdCliente)
        {
            try
            {
                var Dao = new ContaReceberDAO();
                var contaReceber = Dao.GetContaReceber(null, nrModelo, nrSerie, nrNota, nrParcela, IdCliente);
                return View(contaReceber);
            }
            catch (Exception)
            {
                return View();
            }
        }
        #endregion
        public ActionResult Index()
        {
            var Dao = new ContaReceberDAO();
            ModelState.Clear();
            return View(Dao.SelecionarContaReceber());
        }

        public ActionResult Details(string nrModelo, string nrSerie, int nrNota, int? nrParcela, int? IdCliente)
        {
            return this.GetView(nrModelo, nrSerie, nrNota, IdCliente, nrParcela);
        }

        public ActionResult Create(string nrModelo, string nrSerie, int nrNota, int? nrParcela, int? IdCliente)
        {
            return this.GetView(nrModelo, nrSerie, nrNota, IdCliente, nrParcela);
        }

        [HttpPost]
        public ActionResult Create(ContaReceber contaReceber, string nrModelo, string nrSerie, int nrNota, int IdCliente, int nrParcela)
        {
            if (contaReceber.ContaBancaria.IdConta == null)
            {
                ModelState.AddModelError("ContaBancaria.IdConta", "Informe a Conta Bancaria");
            }
            try
            {
                var dao = new ContaReceberDAO();
                dao.PagarContaReceber(contaReceber, nrModelo, nrSerie, nrNota, IdCliente, nrParcela);
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
