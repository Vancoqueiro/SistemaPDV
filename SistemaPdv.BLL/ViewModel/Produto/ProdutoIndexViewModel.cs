using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SistemaPdv.Core.ViewModel
{
    public class ProdutoIndexViewModel
    {
        public string descricaoPesquisa { get; set; }

        public List<ProdutoViewModel> Produtos = new List<ProdutoViewModel>();
    }

}