using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SistemaPdv.Core.ViewModel
{
    public class ProdutoViewModel
    {
        [Display(Name = "Identificador")]
        public int id_produto { get; set; }

        [Display(Name = "Nome do Fabricante")]
        public string nome_fabricante { get; set; }

        [Required(ErrorMessage = "Informe a descrição do produto")]
        [Display(Name = "Descrição do Produto")]
        public string descricao { get; set; }

        [Required(ErrorMessage = "Informe a quantidade")]
        [Range(1, int.MaxValue, ErrorMessage = "Informe um valor maior que {1}")]
        [Display(Name = "Quantidade em Estoque")]
        public int quantidade_estoque { get; set; }

        [Display(Name = "Código de Barras")]
        public string numero_codigo_barras { get; set; }

        [Display(Name = "Valor do Produto")]
        public decimal valor_produto { get; set; }

        [Display(Name = "Tipo do Produto")]
        public decimal id_tipo_produto { get; set; }
    }
}