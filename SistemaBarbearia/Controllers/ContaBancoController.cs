using SistemaBarbearia.DAOs.ContasBancos;
using SistemaBarbearia.DataTables;
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
                    this.AddFlashMessage("Registro salvo com sucesso!");
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
                    this.AddFlashMessage("Registro Alterado com sucesso!");
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
                    this.AddFlashMessage("Registro Removido com sucesso!");
                    return RedirectToAction("Index");
                }

                return View();
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
                var dao = new ContaBancoDAO();
                IQueryable<dynamic> lista = dao.SelecionarContaBanco().Select(u => new { IdConta = u.IdConta, dsConta = u.dsConta }).AsQueryable();
                return Json(new JsonSelect<object>(lista, page, 10), JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult JsInsert(ContaBanco contaBanco)
        {
            var dao = new ContaBancoDAO();
            dao.InsertContaBanco(contaBanco);
            var result = new
            {
                type = "success",
                field = "",
                message = "Registro adicionado com sucesso!",
                model = contaBanco
            };
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult JsUpdate(ContaBanco contaBanco)
        {
            var dao = new ContaBancoDAO();
            dao.UpdateContaBanco(contaBanco);

            var result = new
            {
                type = "success",
                field = "",
                message = "Registro alterado com sucesso!",
                model = contaBanco
            };
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult JsDetails(int? IdConta, string dsConta)
        {
            try
            {
                var result = this.Find(IdConta, dsConta).FirstOrDefault();
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

        private IQueryable<dynamic> Find(int? IdConta, string dsConta)
        {
            var dao = new ContaBancoDAO();
            var list = dao.SelectContaBanco(IdConta, dsConta);
            var select = list.Select(u => new
            {
                IdConta = u.IdConta,
                dsConta = u.dsConta,

            }).OrderBy(u => u.dsConta).ToList();
            return select.AsQueryable();
        }
    }
}
