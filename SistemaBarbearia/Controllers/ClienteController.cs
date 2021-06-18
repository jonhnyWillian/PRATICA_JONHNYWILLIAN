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

        public ActionResult Index()
        {
            var clienteDAO = new ClienteDAO();
            ModelState.Clear();
            return View(clienteDAO.SelecionarCliente());
        }

        public ActionResult Details(int id)
        {
            var clienteDAO = new ClienteDAO();
            return View(clienteDAO.GetCliente(id));
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Cliente cliente)
        {
            if (string.IsNullOrWhiteSpace(cliente.nmCliente))
            {
                ModelState.AddModelError("", "Nome do Cliente Nao pode ser em braco");
            }
            if (string.IsNullOrWhiteSpace(cliente.nmApelido))
            {
                ModelState.AddModelError("", "Sigla do Apelido Nao pode ser em braco");
            }
            try
            {
                if (ModelState.IsValid)
                {
                    var clienteDAO = new ClienteDAO();

                    clienteDAO.InsertCliente(cliente);
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
            var clienteDAO = new ClienteDAO();
            return View(clienteDAO.GetCliente(id));
        }

        [HttpPost]
        public ActionResult Edit(int id, Cliente cliente)
        {
            if (string.IsNullOrWhiteSpace(cliente.nmCliente))
            {
                ModelState.AddModelError("", "Nome do Cliente Nao pode ser em braco");
            }
            if (string.IsNullOrWhiteSpace(cliente.nmApelido))
            {
                ModelState.AddModelError("", "Sigla do Apelido Nao pode ser em braco");
            }
            try
            {
                if (ModelState.IsValid)
                {
                    var clienteDAO = new ClienteDAO();

                    clienteDAO.UpdateCliente(cliente);

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
            var clienteDAO = new ClienteDAO();
            return View(clienteDAO.GetCliente(id));
        }
       
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

        //public JsonResult JsQuery([ModelBinder(typeof(DataTablesBinder))] IDataTablesRequest requestModel)
        //{
        //    try
        //    {
        //        var select = this.Find(null, requestModel.Search.Value);

        //        var totalResult = select.Count();

        //        var result = select.OrderBy(requestModel.Columns, requestModel.Start, requestModel.Length).ToList();

        //        return Json(new DataTablesResponse(requestModel.Draw, result, totalResult, result.Count), JsonRequestBehavior.AllowGet);


        //    }
        //    catch (Exception ex)
        //    {
        //        Response.StatusCode = 500;
        //        throw new Exception(ex.Message);
        //    }
        //}

    }
}
