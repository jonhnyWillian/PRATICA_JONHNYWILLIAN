using SistemaBarbearia.DAOs.Clientes;
using SistemaBarbearia.Models.Clientes;
using SistemaBarbearia.DataTables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SistemaBarbearia.ViewModels.Clientes;
using SistemaBarbearia.DAOs.Cidades;
using SistemaBarbearia.DAOs.CondPagamentos;

namespace SistemaBarbearia.Controllers
{
    public class ClienteController : Controller
    {

        #region MethodPrivate
        private ActionResult GetView(int id)
        {
            ClienteDAO objCliente = new ClienteDAO();
            CidadeDAO DAOCidade = new CidadeDAO();
            CondPagamentoDAO condPagamentoDAO = new CondPagamentoDAO();
            var obj = objCliente.DAOGetCliente(id);
            var result = new ClienteVM
            {
                IdModelPai = obj.IdCliente,
                nmPessoa = obj.nmCliente,
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
                idCondPagamento = obj.IdCondPag,
                Fisica = new ClienteVM.PessoaFisicaVM
                {
                    nmApelido = obj.nmApelido,
                    nrCPF = obj.nrCPF,
                    nrRG = obj.nrRG,
                    dataNasc = obj.dataNasc,
                    flSexo = obj.flSexo,
                    
                },
                
            };
            var objCidade = DAOCidade.GetCidade(result.idCidade);
            result.Cidade = new ViewModels.Cidades.SelectCidadeVM { Id = objCidade.IdCidade, Text = objCidade.nmCidade };
            var objCondPag = condPagamentoDAO.GetCondPagamento(result.idCondPagamento);
            result.condPagamento = new ViewModels.CondPagamentos.SelectCondPagamentoVM { Id = objCondPag.IdModelPai, Text = objCondPag.dsCondPag };
            return View(result);
        }
        #endregion

        public ActionResult Index()
        {
            var clienteDAO = new ClienteDAO();
            ModelState.Clear();
            return View(clienteDAO.SelecionarCliente());
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
        public ActionResult Create(ClienteVM model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var bean = model.GetCliente(new Cliente());
                    var dao = new ClienteDAO();
                    bean.dtCadastro = DateTime.Now;
                    bean.dtUltAlteracao = DateTime.Now;

                    dao.InsertCliente(bean);

                  
                    return RedirectToAction("index");
                }
                catch 
                {
                    return View(model);
                }
            }
            return View(model);
        }
      
        public ActionResult Edit(int id)
        {
          
            return this.GetView(id);
        }

        [HttpPost]
        public ActionResult Edit(int id, ClienteVM model)
        {
            if (ModelState.IsValid)
            {

                try
                {
                    ClienteDAO dao = new ClienteDAO();
                    var cliente = dao.DAOGetCliente(id);

                    var bean = model.GetCliente(cliente);
                    bean.dtUltAlteracao = DateTime.Now;

                    dao.UpdateCliente(bean);
                    return RedirectToAction("index");
                }
                catch 
                {
                    
                    return View(model);
                }
            }
            return View(model);
        }
       
        public ActionResult Delete(int id)
        {
            return this.GetView(id);
        }
       
        [HttpPost]
        public ActionResult Delete(int id, ClienteVM cliente)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // TODO: Add update logic here
                    var bean = cliente.GetCliente(new Cliente());
                    var daoCliente = new ClienteDAO();
                    daoCliente.DeleteCliente(id);

                   
                    return RedirectToAction("index");
                }
                catch 
                {
                    
                    return View(cliente);
                }
            }
            return View(cliente);
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
