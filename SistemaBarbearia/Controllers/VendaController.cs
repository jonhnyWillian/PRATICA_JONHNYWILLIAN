using SistemaBarbearia.DAOs.VendaProdutos;
using SistemaBarbearia.Models.Vendas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SistemaBarbearia.Controllers
{
    public class VendaController : Controller
    {

        #region MethodPrivate
        private ActionResult GetView(string nrModelo, string nrSerie, int nrNota, int? IdCliente)
        {
            try
            {
                var Dao = new VendaProdutoDAO();
                var venda = Dao.GetVenda(null, nrModelo, nrSerie, nrNota, IdCliente);
                return View(venda);
            }
            catch (Exception)
            {
                return View();
            }
        }



        #endregion


        public ActionResult Index()
        {
            var Dao = new VendaProdutoDAO();
            ModelState.Clear();
            return View(Dao.SelecionarVenda());
        }

        public ActionResult Details(string nrModelo, string nrSerie, int nrNota, int? IdCliente)
        {
            return this.GetView(nrModelo, nrSerie, nrNota, IdCliente);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Venda model)
        {
            if (string.IsNullOrWhiteSpace(model.nrModelo))
            {
                ModelState.AddModelError("modelo", "Informe o modelo");
            }
            if (string.IsNullOrWhiteSpace(model.nrSerie))
            {
                ModelState.AddModelError("serie", "Informe a série");
            }                     
            if (model.dtNota == null)
            {
                ModelState.AddModelError("dtNota", "Informe a data de emissão");
            }
            try
            {
                var dao = new VendaProdutoDAO();
                model.flSituacao = "A";
                dao.InsertVendaProduto(model);
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

        public ActionResult Cancelar(string nrModelo, string nrSerie, int nrNota, int? IdCliente)
        {
            return this.GetView(nrModelo, nrSerie, nrNota, IdCliente);
        }

        [HttpPost]
        public ActionResult Cancelar(Venda model)
        {
            try
            {
                var dao = new VendaProdutoDAO();
                dao.cancelarVenda(model);               
                this.AddFlashMessage("Venda Cancelado com sucesso!");
                dao.cancelarContasReceber(model);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
