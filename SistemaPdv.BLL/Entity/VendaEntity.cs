using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaPdv.Core.Entity
{
    public class VendaEntity
    {
        public int id_venda { get; set; }
        public DateTime data_venda { get; set; }
        public int quantidade_total_itens { get; set; }
        public decimal valor_total_produtos { get; set; }
        public decimal valor_desconto { get; set; }
    }
}

     
