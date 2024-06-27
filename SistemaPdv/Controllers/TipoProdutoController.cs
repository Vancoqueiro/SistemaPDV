using SistemaPdv.Core.Logic;
using SistemaPdv.Core.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SistemaPdv.Controllers
{
    public class TipoProdutoController : Controller
    {
        // GET: TipoProduto
        public ActionResult Index()
        {
            var model = new TipoProdutoIndexViewModel();

            model.TipoProduto = TipoProdutoLogic.ListarTipoProduto();

            return View(model);
        }

        [HttpPost]
        public ActionResult Index(TipoProdutoIndexViewModel model)
        {
            model.TipoProduto = TipoProdutoLogic.ListarTipoProduto(descricao: model.descricaoTipoProduto);

            return View(model);

        }

        public ActionResult Create()
        {
            var model = new TipoProdutoViewModel();

            return View(model);
        }

        [HttpPost]
        public ActionResult Create(TipoProdutoViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                TipoProdutoLogic.RegistrarTipoProduto(model);


                return RedirectToAction("Create", "Produto");
            }
            catch (Exception ex)
            {
                ViewBag.msgErro = ex.Message;
                return View(model);
            }

            
        }

        public ActionResult Edit(int id)
        {
            var model = TipoProdutoLogic.Pesquisar(id);

            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(TipoProdutoViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            TipoProdutoLogic.Editar(model);

            return RedirectToAction("Index");
        }

        public ActionResult Delete (int id)
        {
            TipoProdutoLogic.Excluir(id);

            return RedirectToAction("Index");
        }

        public ActionResult Details(int id)
        {
            TipoProdutoViewModel model = TipoProdutoLogic.Pesquisar(id);

            return View(model);
        }
    }
}