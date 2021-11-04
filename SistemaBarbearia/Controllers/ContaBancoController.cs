using SistemaBarbearia.DAOs.ContasBancos;
using SistemaBarbearia.Models.ContasBancos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SistemaBarbearia.Controllers
{
    public class ContaBancoController : Controller
    {
        
        public ActionResult Index()
        {
            var dao = new ContaBancoDAO();
            ModelState.Clear();
            return View(dao.SelecionarContaBanco());
        }
        
        public ActionResult Details(int id)
        {
            var dao = new ContaBancoDAO();
            return View(dao.GetContaBanco(id));
        }

        public ActionResult Create()
        {
            return View();
        }

      
        [HttpPost]
        public ActionResult Create(ContaBanco contaBanco)
        {
            if (string.IsNullOrWhiteSpace(contaBanco.dsConta))
            {
                ModelState.AddModelError("", "Nome da Conta Nao pode ser em braco");
            }
            if (string.IsNullOrWhiteSpace(contaBanco.dsClassificacao))
            {
                ModelState.AddModelError("", "Classificação Nao pode ser em braco");
            }
            try
            {
                if (ModelState.IsValid)
                {
                    var dao = new ContaBancoDAO();

                    dao.InsertContaBanco(contaBanco);
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
            var dao = new ContaBancoDAO();
            return View(dao.GetContaBanco(id));
        }

        [HttpPost]
        public ActionResult Edit(int id, ContaBanco contaBanco)
        {
            if (string.IsNullOrWhiteSpace(contaBanco.dsConta))
            {
                ModelState.AddModelError("", "Nome da Conta Nao pode ser em braco");
            }
            if (string.IsNullOrWhiteSpace(contaBanco.dsClassificacao))
            {
                ModelState.AddModelError("", "Classificação Nao pode ser em braco");
            }
            try
            {
                if (ModelState.IsValid)
                {
                    var dao = new ContaBancoDAO();

                    dao.UpdateContaBanco(contaBanco);
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
            var dao = new ContaBancoDAO();
            return View(dao.GetContaBanco(id));
        }

      
        [HttpPost]
        public ActionResult Delete(int id, ContaBanco contaBanco)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    var dao = new ContaBancoDAO();

                    dao.DeleteContaBanco(id);
                    return RedirectToAction("Index");
                }

                return View();
            }
            catch
            {
                return View();
            }
        }
    }
}
