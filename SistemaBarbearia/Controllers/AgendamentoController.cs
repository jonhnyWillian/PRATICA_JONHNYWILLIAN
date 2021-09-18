using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SistemaBarbearia.Controllers
{
    public class AgendamentoController : Controller
    {
        // GET: Agendamento
        public ActionResult Index()
        {
            return View();
        }

        // GET: Agendamento/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Agendamento/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Agendamento/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Agendamento/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Agendamento/Edit/5
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

        // GET: Agendamento/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Agendamento/Delete/5
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
