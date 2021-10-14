using SistemaBarbearia.DAOs.CondPagamentos;
using SistemaBarbearia.DataTables;
using SistemaBarbearia.Models.CondPagamento;
using SistemaBarbearia.ViewModels.CondPagamentos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SistemaBarbearia.Controllers
{
    public class CondPagamentoController : Controller
    {


        #region MethodPrivate
        private ActionResult GetView(int id)
        {
            var condPag = new CondPagamentoDAO();
            
            var obj = condPag.GetCondPagamento(id);
            var result = new CondPagamentoVM
            {
                IdModelPai = obj.IdCondPag,
                dsCondPag = obj.dsCondPag,
                txJuro = obj.txJuro,
                txMulta = obj.txMulta,

                ListCondicao = obj.CondicaoForma,

                dtCadastro = obj.dtCadastro,
                dtUltAlteracao = obj.dtUltAlteracao,
            };          
            return View(result);
        }
        #endregion


        public ActionResult Index()
        {
            var condPag = new CondPagamentoDAO();
            ModelState.Clear();
            return View(condPag.SelecionarCondPagamento());
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
        public ActionResult Create(CondPagamentoVM condPagamento)
        {
            if (string.IsNullOrWhiteSpace(condPagamento.dsCondPag))
            {
                ModelState.AddModelError("", "Nome do CondPagamento Nao pode ser em braco");
            }
            if (!ModelState.IsValid)
            {
                try
                {
                    var bean = condPagamento.GetPagamento(new CondPagamento());
                    var dao = new CondPagamentoDAO();
                    bean.dtCadastro = DateTime.Now;

                    dao.InsertCondPagamento(bean);


                    return RedirectToAction("index");
                }
                catch 
                {
                    return View(condPagamento);
                }
            }
            return View(condPagamento);            
        }

        public ActionResult Edit(int id)
        {

            return this.GetView(id);
        }

        [HttpPost]
        public ActionResult Edit(int id, CondPagamentoVM condPagamento)
        {
            if (string.IsNullOrWhiteSpace(condPagamento.dsCondPag))
            {
                ModelState.AddModelError("", "Nome do CondPagamento Nao pode ser em braco");
            }
            
            try
            {
                if (ModelState.IsValid)
                {
                    var dao = new CondPagamentoDAO();
                    var obj = dao.GetCondPagamento(id);

                    var bean = condPagamento.GetPagamento(obj);
                    
                    bean.dtUltAlteracao = DateTime.Now;
                    dao.UpdateCondPagamento(bean);

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
          
            return this.GetView(id);
        }

        [HttpPost]
        public ActionResult Delete(int id, CondPagamentoVM condPagamento)
        {
            try
            {
                var bean = condPagamento.GetPagamento(new CondPagamento());
                var dao = new CondPagamentoDAO();
                dao.DeleteCondPagamento(condPagamento.IdModelPai);


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
                var condPag = new CondPagamentoDAO();
                IQueryable<dynamic> lista = condPag.SelecionarCondPagamento().Select(u => new { Id = u.IdCondPag, Text = u.dsCondPag }).AsQueryable();
                return Json(new JsonSelect<object>(lista, page, 10), JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult JsInsert(CondPagamento condPagamento)
        {
            var condPag = new CondPagamentoDAO();
            condPag.InsertCondPagamento(condPagamento);
            var result = new
            {
                type = "success",
                field = "",
                message = "Registro adicionado com sucesso!",
                model = condPagamento
            };
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult JsUpdate(CondPagamento condPagamento)
        {
            var condPag = new CondPagamentoDAO();
            condPag.UpdateCondPagamento(condPagamento);

            var result = new
            {
                type = "success",
                field = "",
                message = "Registro alterado com sucesso!",
                model = condPagamento
            };
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult JsDetails(int? id, string text)
        {
            try
            {
                var result = this.Find(id, text).FirstOrDefault();
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
            var condPag = new CondPagamentoDAO();
            var list = condPag.SelectCondPagamento(id, text);
            var select = list.Select(u => new
            {
                Id = u.Id,
                Text = u.Text,              

            }).OrderBy(u => u.Text).ToList();
            return select.AsQueryable();
        }


        public JsonResult JsGetParcelas(int idCondicaoPagamento, decimal vlTotal, DateTime? dtIiniParcela)
        {
            var dao = new CondPagamentoDAO();
            var cond = dao.GetCondPagamento(idCondicaoPagamento);
            var lista = cond.CondicaoForma.OrderBy(k => k.nrParcela);

            var parcelas = new List<CondPagamentoVM.ParcelasVM>();
            var dtinicio = DateTime.Now;
            if(dtIiniParcela != null)
            {
                dtinicio = dtIiniParcela.GetValueOrDefault();
            }
            foreach (var item in lista)
            {
                var itemParcelas = new CondPagamentoVM.ParcelasVM
                {
                    nrParcela = item.nrParcela,
                    dtVencimento = dtinicio.AddDays((double)item.qtdDias),
                    idFormaPag = item.IdFormaPagamento,
                    dsFormaPagamento = item.dsFormaPagamento,
                    vlParcela = decimal.Round(((item.txPercentual / 100) * vlTotal), 2)
                };
                parcelas.Add(itemParcelas);
            }
            var totalParcelas = parcelas.Sum(k => k.vlParcela);
            if(totalParcelas != vlTotal)
            {
                if(totalParcelas < vlTotal)
                {
                    var dif = vlTotal - totalParcelas;
                    var list = parcelas.OrderBy(u => u.nrParcela);
                    list.Last().vlParcela = list.Last().vlParcela + dif;
                    parcelas = list.ToList();
                }
                if (totalParcelas > vlTotal)
                {
                    var dif = totalParcelas - vlTotal;
                    var list = parcelas.OrderBy(u => u.nrParcela);
                    list.Last().vlParcela = list.Last().vlParcela - dif;
                    parcelas = list.ToList();
                }
            }
            var result = new
            {
                type = "success",
                message = "Parcelas geradas com sucesso!",
                parcelas = parcelas
            };
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}
