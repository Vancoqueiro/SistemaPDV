using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SistemaPdv.Core.ViewModel
{
    public class ProdutoDetailsViewModel
    {
        public ProdutoViewModel Produto = new ProdutoViewModel();
        public List<PrecoDetailsViewModel> HistoricoPreco = new List<PrecoDetailsViewModel>();
    }

    public class PrecoDetailsViewModel
    {
        [Display(Name = "Data Início Vigência")]
        public DateTime data_inicio_vigencia { get; set; }

        [Display(Name = "Valor do Produto")]
        public decimal valor_produto { get; set; }
    }

}