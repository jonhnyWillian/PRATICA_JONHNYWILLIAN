using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SistemaBarbearia.Controllers
{
    public class ContaReceberController : Controller
    {
        // GET: ContaReceber
        public ActionResult Index()
        {
            return View();
        }

        // GET: ContaReceber/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ContaReceber/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ContaReceber/Create
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

        // GET: ContaReceber/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ContaReceber/Edit/5
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

        // GET: ContaReceber/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ContaReceber/Delete/5
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
