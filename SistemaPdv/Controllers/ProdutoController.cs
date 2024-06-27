using SistemaPdv.Core.Entity;
using SistemaPdv.Core.Logic;
using SistemaPdv.Core.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SistemaPdv.Controllers
{
    public class ProdutoController : Controller
    {
        public ActionResult Index()
        {
            var model = new ProdutoIndexViewModel();

            model.Produtos = ProdutoLogic.ListarProduto();

            return View(model);
        }

        [HttpPost]
        public ActionResult Index(ProdutoIndexViewModel model)
        {
            model.Produtos = ProdutoLogic.ListarProduto(descricao: model.descricaoPesquisa);

            return View(model);

        }

        public ActionResult Create()
        {
            var model = new ProdutoViewModel();

            var lista = TipoProdutoEntity.Pesquisar();
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(ProdutoViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            ProdutoLogic.RegistrarProduto(model);

            //var TipoProduto = new List<TipoProdutoViewModel>();


            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            var model = ProdutoLogic.PesquisarProduto(id);

            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(ProdutoViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            ProdutoLogic.Editar(model);

            return RedirectToAction("Index");
        }

        public ActionResult Details(int id)
        {
            ProdutoDetailsViewModel model = ProdutoLogic.PesquisarDetalhesProduto(id);

            return View(model);
        }

        public ActionResult Delete(int id)
        {
            ProdutoLogic.Excluir(id);

            return RedirectToAction("Index");

        }
    }
}
