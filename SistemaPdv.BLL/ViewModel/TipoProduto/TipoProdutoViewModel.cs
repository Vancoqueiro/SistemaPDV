using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaPdv.Core.ViewModel
{
    public class TipoProdutoViewModel
    {
        [Display(Name = "Identificador")]
        public int id_Tipo_produto { get; set; }

        [Required(ErrorMessage = "Informe a descrição do Produto")]
        [Display(Name = "Descrição")]
        public string descricao { get; set; }

        [Display(Name = "Observação")]
        public string observacao { get; set; }

    }
}
