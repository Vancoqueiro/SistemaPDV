using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaPdv.Core.ViewModel
{
   public class TipoProdutoIndexViewModel
    {
        public string descricaoTipoProduto { get; set; }

        public List<TipoProdutoViewModel> TipoProduto = new List<TipoProdutoViewModel>();
    }
}
