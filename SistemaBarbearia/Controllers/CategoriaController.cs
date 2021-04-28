using SistemaBarbearia.DAOs.Categorias;
using SistemaBarbearia.Models.Categorias;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SistemaBarbearia.Controllers
{
    public class CategoriaController : Controller
    {
        // GET: Categoria
        public ActionResult Index()
        {
            var categoriaDAO = new CategoriaDAO();
            ModelState.Clear();
            return View(categoriaDAO.SelecionarCategoria());
        }

        // GET: Categoria/Details/5
        public ActionResult Details(int id)
        {
            var categoriaDAO = new CategoriaDAO();
            return View(categoriaDAO.GetCategoria(id));
        }

        // GET: Categoria/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Categoria/Create
        [HttpPost]
        public ActionResult Create(Categoria categoria)
        {
            if (categoria.dsCategoria == null)
            {
                ModelState.AddModelError("categoria.dsCategoria", "Campo categoria não pode ser em braco");
            }
            try
            {
                if (ModelState.IsValid)                
                {
                    var categoriaDAO = new CategoriaDAO();

                    if (categoriaDAO.AdicionarCategoria(categoria))
                    {
                        ViewBag.Message = "Categoria criado com sucesso!";
                    }

                }
                    return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Categoria/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Categoria/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Categoria categoria)
        {
            try
            {
                var categoriaDAO = new CategoriaDAO();

                categoriaDAO.UpdateCategoria(categoria);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Categoria/Delete/5
        public ActionResult Delete(int id)
        {
            var categoriaDAO = new CategoriaDAO();
            return View(categoriaDAO.GetCategoria(id));
        }

        // POST: Categoria/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                var categoriaDAO = new CategoriaDAO();
                categoriaDAO.ExcluirCategoria(id);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
