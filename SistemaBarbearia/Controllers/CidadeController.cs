using SistemaBarbearia.DAOs.Cidades;
using SistemaBarbearia.Models.Cidades;
using SistemaBarbearia.DataTables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SistemaBarbearia.DAOs.Estados;
using SistemaBarbearia.ViewModels.Cidades;

namespace SistemaBarbearia.Controllers
{
    public class CidadeController : Controller
    {
        #region MethodPrivate
        private ActionResult GetView(int id)
        {
            CidadeDAO objCidade = new CidadeDAO();
            EstadoDAO DAOEstado = new EstadoDAO();
            var obj = objCidade.GetCidade(id);
            var result = new CidadeVM
            {
                IdCidade = obj.IdCidade,
                nmCidade = obj.nmCidade,
                DDD = obj.DDD,
              
                dtCadastro = obj.dtCadastro,
                dtUltAlteracao = obj.dtUltAlteracao,
                IdEstado = obj.IdEstado
            };
            var objEstado = DAOEstado.GetEstado(result.IdEstado);
            result.Estado = new ViewModels.Estados.SelectEstadoVM { Id = objEstado.IdEstado, Text = objEstado.nmEstado };
            return View(result);
        }
        #endregion


        public ActionResult Index()
        {
            var cidadeDAO = new CidadeDAO();
            ModelState.Clear();
            return View(cidadeDAO.SelecionarCidade());
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
        public ActionResult Create(Cidade cidade)
        {

            if (string.IsNullOrWhiteSpace(cidade.nmCidade))
            {
                ModelState.AddModelError("", "Nome do Cidade Nao pode ser em braco");
            }
            if (string.IsNullOrWhiteSpace(cidade.DDD))
            {
                ModelState.AddModelError("", "DDD do Cidade Nao pode ser em braco");
            }
            try
            {
                if (ModelState.IsValid)
                {
                    var cidadeDAO = new CidadeDAO();

                    cidadeDAO.InsertCidade(cidade);

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
        public ActionResult Edit(int id, Cidade cidade)
        {
            if (string.IsNullOrWhiteSpace(cidade.nmCidade))
            {
                ModelState.AddModelError("", "Nome do Cidade Nao pode ser em braco");
            }
            if (string.IsNullOrWhiteSpace(cidade.DDD))
            {
                ModelState.AddModelError("", "DDD do Pais Nao pode ser em braco");
            }
            try
            {
                if (ModelState.IsValid)
                {
                    var cidadeDAO = new CidadeDAO();

                    cidadeDAO.UpdateCidade(cidade);

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
        public ActionResult Delete(int id, Cidade cidade)
        {
            try
            {
                var cidadeDAO = new CidadeDAO();
                cidadeDAO.DeleteCidade(id);
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
                var cidadeDAO = new CidadeDAO();
                IQueryable<dynamic> lista = cidadeDAO.SelecionarCidade().Select(u => new { Id = u.IdCidade, Text = u.nmCidade }).AsQueryable();
                return Json(new JsonSelect<object>(lista, page, 10), JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult JsInsert(Cidade cidade)
        {
            var cidadeDAO = new CidadeDAO();
            cidadeDAO.InsertCidade(cidade);
            var result = new
            {
                type = "success",
                field = "",
                message = "Registro adicionado com sucesso!",
                model = cidade
            };
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult JsUpdate(Cidade cidade)
        {
            var cidadeDAO = new CidadeDAO();
            cidadeDAO.UpdateCidade(cidade);

            var result = new
            {
                type = "success",
                field = "",
                message = "Registro alterado com sucesso!",
                model = cidade
            };
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult JsDetails(int? Id, string Text)
        {
            try
            {
                var result = this.Find(Id, Text).FirstOrDefault();
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

        private IQueryable<dynamic> Find(int? id, string text)
        {
            var cidadeDAO = new CidadeDAO();
            var list = cidadeDAO.SelectCidade(id, text);
            var select = list.Select(u => new
            {
                Id = u.Id,
                Text = u.Text,
                DDD = u.DDD,
                dtCadastro = u.dtCadastro,
                dtUltAlteracao = u.dtUltAlteracao

            }).OrderBy(u => u.Text).ToList();
            return select.AsQueryable();
        }
    }
}
