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
                IdCondPag = obj.idCondPagamento,

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
            var objCondPag = condPagamentoDAO.GetCondPagamento(result.IdCondPag);
            result.condPagamento = new ViewModels.CondPagamentos.SelectCondPagamentoVM { Id = objCondPag.IdModelPai, Text = objCondPag.dsCondPag };
            return View(result);
        }
        #endregion

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
        public ActionResult Edit(int id, Fornecedor fornecedor)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var fornecedorDAO = new FornecedorDAO();

                    if (fornecedorDAO.UpdateFornecedor(fornecedor))
                    {
                        ViewBag.Message = "Fornecedor criado com sucesso!";
                    }
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
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

        public JsonResult JsQuery([ModelBinder(typeof(DataTablesBinder))] IDataTablesRequest requestModel)
        {
            try
            {

                var fornecedorDAO = new FornecedorDAO();
                var select = fornecedorDAO.SelecionarFornecedor().Select(u => new { Id = u.IdFornecedor, nmNome = u.nmNome, nrTelefone = u.nrTelefone });

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
