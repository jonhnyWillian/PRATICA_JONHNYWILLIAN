using SistemaBarbearia.DAOs.Paises;
using SistemaBarbearia.Models.Paises;
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
                ModelState.AddModelError("Pais.nmPais", "Nome do Pais Nao pode ser em braco");
            }
            if (pais.dsSigla == null)
            {
                ModelState.AddModelError("Pais.nmPais", "Sigla do Pais Nao pode ser em braco");
            }
            try
            {
                if (ModelState.IsValid)
                {
                    var paisDAO = new PaisDAO();

                    if (paisDAO.AdicionarPais(pais))
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
                paisDAO.ExcluirPais(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
