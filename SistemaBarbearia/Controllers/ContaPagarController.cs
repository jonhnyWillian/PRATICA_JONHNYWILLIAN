using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SistemaBarbearia.Controllers
{
    public class ContaPagarController : Controller
    {
        // GET: ContaPagar
        public ActionResult Index()
        {
            return View();
        }

        // GET: ContaPagar/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ContaPagar/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ContaPagar/Create
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

        // GET: ContaPagar/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ContaPagar/Edit/5
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

        // GET: ContaPagar/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ContaPagar/Delete/5
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
