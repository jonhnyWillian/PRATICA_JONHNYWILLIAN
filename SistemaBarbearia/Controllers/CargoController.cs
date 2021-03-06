using SistemaBarbearia.DAOs.Cargos;
using SistemaBarbearia.Models.Cargos;
using SistemaBarbearia.DataTables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SistemaBarbearia.Controllers
{
    public class CargoController : Controller
    {
        public ActionResult Index()
        {
            var cargoDAO = new CargoDAO();
            ModelState.Clear();
            return View(cargoDAO.SelecionarCargo());
        }

        public ActionResult Details(int id)
        {
            var servicoDAO = new CargoDAO();
            return View(servicoDAO.GetCargo(id));
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Cargo cargo)
        {
           
            try
            {
                if (ModelState.IsValid)
                {
                    var cargoDAO = new CargoDAO();

                    cargoDAO.InsertCargo(cargo);

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
            var servicoDAO = new CargoDAO();
            return View(servicoDAO.GetCargo(id));
        }

        [HttpPost]
        public ActionResult Edit(int id, Cargo cargo)
        {
            if (string.IsNullOrWhiteSpace(cargo.dsCargo))
            {
                ModelState.AddModelError("dsCargo", "Informe um nome de cargo válido");
            }
            try
            {
                if (ModelState.IsValid)
                {
                    var cargoDAO = new CargoDAO();

                    cargoDAO.UpdateCargo(cargo);

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
            var servicoDAO = new CargoDAO();
            return View(servicoDAO.GetCargo(id));
        }

        [HttpPost]
        public ActionResult Delete(int id, Cargo servico)
        {
            try
            {
                var cargoDAO = new CargoDAO();
                cargoDAO.DeleteCargo(id);
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
                var select = this.Find(null, requestModel.Search.Value);

                var totalResult = select.Count();

                var result = select.OrderBy(requestModel.Columns, requestModel.Start, requestModel.Length).ToList();

                return Json(new DataTablesResponse(requestModel.Draw, result, totalResult, result.Count), JsonRequestBehavior.AllowGet);


            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                throw new Exception(ex.Message);
            }
        }

        public JsonResult JsSelect(string q, int? page, int? pageSize)
        {
            try
            {
                var cargoDAO = new CargoDAO();
                IQueryable<dynamic> lista = cargoDAO.SelecionarCargo().Select(u => new { id = u.IdCargo, dsCargo = u.dsCargo }).AsQueryable();
                return Json(new JsonSelect<object>(lista, page, 10), JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult JsInsert(Cargo cargo)
        {
            var cargoDAO = new CargoDAO();
            cargoDAO.InsertCargo(cargo);
            var result = new
            {
                type = "success",
                field = "",
                message = "Registro adicionado com sucesso!",
                model = cargo
            };
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult JsUpdate(Cargo cargo)
        {
            var cargoDAO = new CargoDAO();
            cargoDAO.UpdateCargo(cargo);

            var result = new
            {
                type = "success",
                field = "",
                message = "Registro alterado com sucesso!",
                model = cargo
            };
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult JsDetails(int? IdCargo, string dsCargo)
        {
            try
            {
                var result = this.Find(IdCargo, dsCargo).FirstOrDefault();
                if (result != null)
                    return Json(result, JsonRequestBehavior.AllowGet);
                return Json(string.Empty, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                throw new Exception(ex.Message);
            }
        }

        private IQueryable<dynamic> Find(int? Id, string Text)
        {
            var cargoDAO = new CargoDAO();
            var list = cargoDAO.SelectCargo(Id, Text);
            var select = list.Select(u => new
            {
                Id = u.Id,
                Text = u.Text,
                
                dtCadastro = u.dtCadastro,
                dtUltAlteracao = u.dtUltAlteracao

            }).OrderBy(u => u.Text).ToList();
            return select.AsQueryable();
        }
    }
}
