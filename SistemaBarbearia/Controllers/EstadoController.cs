using SistemaBarbearia.DAOs.Estados;
using SistemaBarbearia.Models.Estados;
using SistemaBarbearia.Models.Paises;
using SistemaBarbearia.DataTables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SistemaBarbearia.DAOs.Paises;
using SistemaBarbearia.DAOs.Cidades;
using SistemaBarbearia.ViewModels.Paises;
using SistemaBarbearia.ViewModels.Estados;

namespace SistemaBarbearia.Controllers
{
    public class EstadoController : Controller
    {

        #region MethodPrivate
        private ActionResult GetView(int id)
        {
            EstadoDAO objEstado = new EstadoDAO();
            PaisDAO DAOPais = new PaisDAO();
            var obj = objEstado.GetEstado(id);
            var result = new EstadoVM
            {
                IdEstado = obj.IdEstado,
                nmEstado = obj.nmEstado,
                dsUF = obj.dsUF,
                dtCadastro = obj.dtCadastro,
                dtUltAlteracao = obj.dtUltAlteracao,               
                IdPais = obj.IdPais
            };
            var objPais = DAOPais.GetPais(result.IdPais);
            result.Pais = new ViewModels.Paises.SelectPaisVM { Id = objPais.IdPais, text = objPais.nmPais };
            return View(result);
        }
        #endregion

        public ActionResult Index()
        {
            var estadoDAO = new EstadoDAO();
            ModelState.Clear();
            return View(estadoDAO.SelecionarEstado());
        }

        public ActionResult Details(int id)
        {

            return GetView(id);
        }

        public ActionResult Create()
        {
            return View();
        }
     
        [HttpPost]
        public ActionResult Create(Estado estado)
        {
            if (string.IsNullOrWhiteSpace(estado.nmEstado))
            {
                ModelState.AddModelError("", "Nome do Estado não pode ser em branco");
            }
            if (string.IsNullOrWhiteSpace(estado.dsUF))
            {
                ModelState.AddModelError("", "UF do Estado não pode ser em branco");
            }
            if(estado.pais.Id == 0)
            {
                ModelState.AddModelError("", "Campo Pais não pode ser em branco");
            }
            try
            {
                if (ModelState.IsValid)
                {
                    var estadoDAO = new EstadoDAO();

                    estadoDAO.InsertEstado(estado);

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
            return GetView(id);
        }

        [HttpPost]
        public ActionResult Edit(int id, Estado estado)
        {
            if (string.IsNullOrWhiteSpace(estado.nmEstado))
            {
                ModelState.AddModelError("", "Nome do Estado Nao pode ser em braco");
            }
            if (string.IsNullOrWhiteSpace(estado.dsUF))
            {
                ModelState.AddModelError("", "UF do Estado Nao pode ser em braco");
            }
            try
            {
                if (ModelState.IsValid)
                {
                    var estadoDAO = new EstadoDAO();

                    estadoDAO.UpdateEstado(estado);

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
            return GetView(id);
        }

        [HttpPost]
        public ActionResult Delete(int id, Estado estado)
        {
            try
            {
                var estadoDAO = new EstadoDAO();
                estadoDAO.DeleteEstado(id);
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
                var estadoDAO = new EstadoDAO();
                IQueryable<dynamic> lista = estadoDAO.SelecionarEstado().Select(u => new { Id = u.IdEstado, Text = u.nmEstado }).AsQueryable();
                return Json(new JsonSelect<object>(lista, page, 10), JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult JsInsert(Estado estado)
        {
            var estadoDAO = new EstadoDAO();
            estadoDAO.InsertEstado(estado);
            var result = new
            {
                type = "success",
                field = "",
                message = "Registro adicionado com sucesso!",
                model = estado
            };
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult JsUpdate(Estado estado)
        {
            var estadoDAO = new EstadoDAO();
            estadoDAO.UpdateEstado(estado);

            var result = new
            {
                type = "success",
                field = "",
                message = "Registro alterado com sucesso!",
                model = estado
            };
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult JsDetails(int? IdEstado, string nmEstado)
        {
            try
            {
                var result = this.Find(IdEstado, nmEstado).FirstOrDefault();
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

        private IQueryable<dynamic> Find(int? IdEstado, string nmEstado)
        {
            var estadoDAO = new EstadoDAO();
            var list = estadoDAO.SelectEstado(IdEstado, nmEstado);
            var select = list.Select(u => new
            {
                IdEstado = u.IdEstado,
                nmEstado = u.nmEstado,
                dsUF = u.dsUF,
                IdPais = u.IdPais,
                dtCadastro = u.dtCadastro,
                dtUltAlteracao = u.dtUltAlteracao

            }).OrderBy(u => u.nmEstado).ToList();
            return select.AsQueryable();
        }

    }
}
