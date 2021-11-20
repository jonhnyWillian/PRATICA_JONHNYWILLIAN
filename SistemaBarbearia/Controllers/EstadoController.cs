using SistemaBarbearia.DAOs.Estados;
using SistemaBarbearia.Models.Estados;
using SistemaBarbearia.DataTables;
using System;
using System.Linq;
using System.Web.Mvc;
using SistemaBarbearia.DAOs.Paises;
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
            result.Pais = new ViewModels.Paises.SelectPaisVM { IdPais = objPais.IdPais, Text = objPais.nmPais };
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
            if(estado.pais.IdPais == 0)
            {
                ModelState.AddModelError("", "Campo Pais não pode ser em branco");
            }
            try
            {
                if (ModelState.IsValid)
                {
                    var estadoDAO = new EstadoDAO();

                    estadoDAO.InsertEstado(estado);
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
                    this.AddFlashMessage("Alterado salvo com sucesso!"); ;
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
                this.AddFlashMessage("Registro excluido com sucesso!"); ;
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

        private IQueryable<dynamic> Find(int? IdEstado, string Text)
        {
            var estadoDAO = new EstadoDAO();
            var list = estadoDAO.SelectEstado(IdEstado, Text);
            var select = list.Select(u => new
            {
                IdEstado = u.IdEstado,
                Text = u.Text,
                dsUF = u.dsUF,
                IdPais = u.IdPais,
                nmPais = u.nmPais, 
                dtCadastro = u.dtCadastro,
                dtUltAlteracao = u.dtUltAlteracao

            }).OrderBy(u => u.Text).ToList();
            return select.AsQueryable();
        }

    }
}
