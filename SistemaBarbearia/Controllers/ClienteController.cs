using SistemaBarbearia.DAOs.Clientes;
using SistemaBarbearia.Models.Clientes;
using SistemaBarbearia.DataTables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SistemaBarbearia.Controllers
{
    public class ClienteController : Controller
    {


        // GET: Cliente
        public ActionResult Index()
        {
            var clienteDAO = new ClienteDAO();
            ModelState.Clear();
            return View(clienteDAO.SelecionarCliente());
        }

        // GET: Cliente/Details/5
        public ActionResult Details(int id)
        {
            var clienteDAO = new ClienteDAO();
            return View(clienteDAO.GetCliente(id));
        }

        // GET: Cliente/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Cliente/Create
        [HttpPost]
        public ActionResult Create(Cliente cliente)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var clienteDAO = new ClienteDAO();

                    if (clienteDAO.InsertCliente(cliente))
                    {
                        ViewBag.Message = "Cliente criado com sucesso!";
                    }
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Cliente/Edit/5
        public ActionResult Edit(int id)
        {
            var clienteDAO = new ClienteDAO();
            return View(clienteDAO.GetCliente(id));
        }

        // POST: Cliente/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Cliente cliente)
        {
            try
            {
                var clienteDAO = new ClienteDAO();

                clienteDAO.UpdateCliente(cliente);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Cliente/Delete/5
        public ActionResult Delete(int id)
        {
            var clienteDAO = new ClienteDAO();
            return View(clienteDAO.GetCliente(id));
        }

        // POST: Cliente/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, Cliente cliente)
        {
            try
            {
                var clienteDAO = new ClienteDAO();
                clienteDAO.DeleteCliente(id);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public JsonResult JsQuery([ModelBinder(typeof(DataTablesBinder))] IDataTablesRequest requestModel)
        {
            try
            {

                var clienteDAO = new ClienteDAO();
                var select = clienteDAO.SelecionarCliente().Select(u => new { Id = u.Id, nmCliente = u.nmCliente, nrTelefone = u.nrTelefone });

                IQueryable<dynamic> query = select.AsQueryable();


                var totalResult = query.Count();

                var result = query.OrderBy(requestModel.Columns, requestModel.Start, requestModel.Length).ToList();

                return Json(new DataTablesResponse(requestModel.Draw, result, totalResult, result.Count), JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
        }



    }
}
