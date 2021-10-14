using SistemaBarbearia.DAOs.Cidades;
using SistemaBarbearia.DAOs.CondPagamentos;
using SistemaBarbearia.DAOs.Fornecedores;
using SistemaBarbearia.DataTables;
using SistemaBarbearia.Models.Fornecedores;
using SistemaBarbearia.ViewModels.Fornecedores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SistemaBarbearia.Controllers
{
    public class FornecedorController : Controller
    {

        #region MethodPrivate
        private ActionResult GetView(int id)
        {
            FornecedorDAO objFornecedor = new FornecedorDAO();
            CidadeDAO DAOCidade = new CidadeDAO();
            CondPagamentoDAO condPagamentoDAO = new CondPagamentoDAO();
            var obj = objFornecedor.GetFornecedor(id);
            var result = new FornecedorVM
            {
                IdModelPai = obj.IdFornecedor,
                nmPessoa = obj.nmNome,
                nrTelefone = obj.nrTelefone,
                nrCelular = obj.nrCelular,
                dsEmail = obj.dsEmail,              
                flTipo = obj.flTipo,                
                nrCEP = obj.nrCEP,
                dsLogradouro = obj.dsLogradouro,
                nrResidencial = obj.nrResidencial,
                dsBairro = obj.dsBairro,
                dsComplemento = obj.dsComplemento,              
                dtCadastro = obj.dtCadastro,
                dtUltAlteracao = obj.dtUltAlteracao,
                idCidade = obj.idCidade,
                idCondPagamento = obj.idCondPagamento,

                Fisica = new FornecedorVM.PessoaFisicaVM
                {
                    nmApelido = obj.dsNome,
                    nrCPF = obj.nrCPFCNPJ,
                    nrRG = obj.nrRGIE,
                    dtNascimento = obj.dtNasc,
                    flSexo = obj.flSexo
                },
                Juridica = new FornecedorVM.PessoaJuridicaVM
                {
                    nmFantasia = obj.dsNome,
                    dsSite = obj.dsSite,
                    nrContato = obj.nrContato,                    
                    nrCNPJ = obj.nrCPFCNPJ,
                    nrIE = obj.nrRGIE,
                    
                }
            };
            var objCidade = DAOCidade.GetCidade(result.idCidade);
            result.Cidade = new ViewModels.Cidades.SelectCidadeVM { Id = objCidade.IdCidade, Text = objCidade.nmCidade };
            var objCondPag = condPagamentoDAO.GetCondPagamento(result.idCondPagamento);
            result.condPagamento = new ViewModels.CondPagamentos.SelectCondPagamentoVM { Id = objCondPag.IdCondPag, Text = objCondPag.dsCondPag };
            return View(result);
        }
        private IQueryable<dynamic> Find(int? id, string text)
        {
            var fornecedorDAO = new FornecedorDAO();
            var list = fornecedorDAO.SelectFornecedor(id, text);
            var select = list.Select(u => new
            {
                IdFornecedor = u.IdFornecedor,
                nmNome = u.nmNome,

            }).OrderBy(u => u.nmNome).ToList();
            return select.AsQueryable();
        }
        #endregion

        #region Action
        public ActionResult Index()
        {
            var fornecedorDAO = new FornecedorDAO();
            ModelState.Clear();
            return View(fornecedorDAO.SelecionarFornecedor());
        }
     
        public ActionResult Details(int id)
        {
            return this.GetView(id);
        }

        public ActionResult Create()
        {
            return View();
        }
     
        [HttpPost]
        public ActionResult Create(FornecedorVM fornecedor)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var bean = fornecedor.GetFornecedor(new Fornecedor());
                    var dao = new FornecedorDAO();
                    bean.dtCadastro = DateTime.Now;
                    //bean.dtUltAlteracao = DateTime.Now;
                   
                    dao.InsertFornecedor(bean);

                    
                    return RedirectToAction("index");
                }
                catch 
                {
                    
                    return View(fornecedor);
                }
            }
            return View(fornecedor);
        }

        public ActionResult Edit(int id)
        {
            return this.GetView(id);
        }

        [HttpPost]
        public ActionResult Edit(int id, FornecedorVM fornecedor)
        {
            if (ModelState.IsValid)
            {

                try
                {
                    
                    FornecedorDAO objFornecedor = new FornecedorDAO();
                    var obj = objFornecedor.GetFornecedor(id);

                    var bean = fornecedor.GetFornecedor(obj);
                    var dao = new FornecedorDAO();
                    bean.dtUltAlteracao = DateTime.Now;
                    dao.UpdateFornecedor(bean);

                  
                    return RedirectToAction("index");
                }
                catch 
                {
                  
                    return View(fornecedor);
                }
            }
            return View(fornecedor);
        }

        public ActionResult Delete(int id)
        {
            return this.GetView(id);
        }

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                var fornecedorDAO = new FornecedorDAO();
                fornecedorDAO.DeleteFornecedor(id);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        #endregion

        #region Json
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
                var fornecedorDAO = new FornecedorDAO();
                IQueryable<dynamic> lista = fornecedorDAO.SelecionarFornecedor().Select(u => new { IdFornecedor = u.IdFornecedor, nmNome = u.nmNome }).AsQueryable();
                return Json(new JsonSelect<object>(lista, page, 10), JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult JsInsert(Fornecedor fornecedor)
        {
            var fornecedorDAO = new FornecedorDAO();

            fornecedorDAO.InsertFornecedor(fornecedor);
            var result = new
            {
                type = "success",
                field = "",
                message = "Registro adicionado com sucesso!",
                model = fornecedor
            };
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult JsUpdate(Fornecedor fornecedor)
        {
            var fornecedorDAO = new FornecedorDAO();
            fornecedorDAO.UpdateFornecedor(fornecedor);

            var result = new
            {
                type = "success",
                field = "",
                message = "Registro alterado com sucesso!",
                model = fornecedor
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
        #endregion
       
    }
}
