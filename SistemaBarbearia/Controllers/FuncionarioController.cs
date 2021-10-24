using SistemaBarbearia.DAOs.Cargos;
using SistemaBarbearia.DAOs.Cidades;
using SistemaBarbearia.DAOs.Funcionarios;
using SistemaBarbearia.DataTables;
using SistemaBarbearia.Models.Funcionarios;
using SistemaBarbearia.ViewModels.Funcionarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SistemaBarbearia.Controllers
{
    public class FuncionarioController : Controller
    {
        #region MethodPrivate
        private ActionResult GetView(int id)
        {
            FuncionarioDAO funcionario = new FuncionarioDAO();
            CargoDAO cargo = new CargoDAO();
            CidadeDAO cidade = new CidadeDAO();
            var obj = funcionario.DAOGetFuncionario(id);
            var result = new FuncionarioVM
            {
                IdModelPai = obj.IdFuncionario,
                nmPessoa = obj.nmFuncionario,
              
                nrCEP = obj.nrCEP,
                dsLogradouro = obj.dsLogradouro,
                nrResidencial = obj.nrResidencial,
                dsComplemento = obj.dsComplemento,
                dsBairro = obj.dsBairro,
                idCidade = obj.IdCidade,
                nrTelefone = obj.nrTelefone,
                nrCelular = obj.nrCelular,
                dsEmail = obj.dsEmail,
                idCargo = obj.IdCargo,

                Fisica = new FuncionarioVM.PessoaFisicaVM
                {
                    nmApelido = obj.nmApelido,
                    nrCPF = obj.nrCPF,
                    nrRG = obj.nrRG,
                    dtNasc = obj.dtNasc,
                    flSexo = obj.flSexo,
                    dsLogin = obj.dsLogin,
                    senha = obj.senha,
                    vlSalario = obj.vlSalario,
                    dtAdimissao = obj.dtAdimissao,
                    dtDemissao = obj.dtDemissao,
                },

                dtCadastro = obj.dtCadastro,
                dtUltAlteracao = obj.dtUltAlteracao
            };
            var objCargo = cargo.GetCargo(result.idCargo);
            result.Cargo = new ViewModels.Cargos.SelectCargoVM { Id = objCargo.IdCargo, Text = objCargo.dsCargo };
            var objCidade = cidade.GetCidade(result.idCidade);
            result.Cidade = new ViewModels.Cidades.SelectCidadeVM { Id = objCidade.IdCidade, Text = objCidade.nmCidade };
            return View(result);
        }
        #endregion

        public ActionResult Index()
        {
            var funcionarioDAO = new FuncionarioDAO();
            ModelState.Clear();
            return View(funcionarioDAO.SelecionarFuncionario());
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
        public ActionResult Create(FuncionarioVM model)
        {
            //if (ModelState.IsValid)
            //{
                try
                {
                    var bean = model.GetFuncionario(new Funcionario());
                    var dao = new FuncionarioDAO();

                    bean.dtCadastro = DateTime.Now;
                    bean.dtUltAlteracao = DateTime.Now;

                    dao.InsertFuncionario(bean);


                    return RedirectToAction("index");
                }
                catch
                {
                    return View(model);
                }
            //}
            //return View(model);
        }

        public ActionResult Edit(int id)
        {
            return GetView(id);
        }

        [HttpPost]
        public ActionResult Edit(int id, FuncionarioVM model)
        {

            //if (ModelState.IsValid)
            //{

                try
                {
                    FuncionarioDAO dao = new FuncionarioDAO();
                    var funcionario = dao.DAOGetFuncionario(id);

                    var bean = model.GetFuncionario(funcionario);
                    bean.dtUltAlteracao = DateTime.Now;

                    dao.UpdateFuncionario(bean);
                    return RedirectToAction("index");
                }
                catch
                {

                    return View(model);
                }
            //}
            //return View(model);
        }

        public ActionResult Delete(int id)
        {
            return GetView(id);
        }

        [HttpPost]
        public ActionResult Delete(int id, Funcionario funcionario)
        {
            try
            {
                var funcionarioDAO = new FuncionarioDAO();
                funcionarioDAO.DeleteFuncionario(id);

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
                var funcionarioDAO = new FuncionarioDAO();
                IQueryable<dynamic> lista = funcionarioDAO.SelecionarFuncionario().Select(u => new { IdFuncionario = u.IdFuncionario, nmFuncionario = u.nmFuncionario }).AsQueryable();
                return Json(new JsonSelect<object>(lista, page, 10), JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult JsInsert(Funcionario funcionario)
        {
            var funcionarioDAO = new FuncionarioDAO();

            funcionarioDAO.InsertFuncionario(funcionario);
            var result = new
            {
                type = "success",
                field = "",
                message = "Registro adicionado com sucesso!",
                model = funcionario
            };
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult JsUpdate(Funcionario funcionario)
        {
            var funcionarioDAO = new FuncionarioDAO();
            funcionarioDAO.UpdateFuncionario(funcionario);

            var result = new
            {
                type = "success",
                field = "",
                message = "Registro alterado com sucesso!",
                model = funcionario
            };
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult JsDetails(int? IdFuncionario, string nmFuncionario)
        {
            try
            {
                var result = this.Find(IdFuncionario, nmFuncionario).FirstOrDefault();
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

        private IQueryable<dynamic> Find(int? IdFuncionario, string nmFuncionario)
        {
            var funcionarioDAO = new FuncionarioDAO();
            var list = funcionarioDAO.SelectFuncionario(IdFuncionario, nmFuncionario);
            var select = list.Select(u => new
            {
                IdFuncionario = u.IdFuncionario,
                nmFuncionario = u.nmFuncionario,


            }).OrderBy(u => u.nmFuncionario).ToList();
            return select.AsQueryable();
        }

    }
}
