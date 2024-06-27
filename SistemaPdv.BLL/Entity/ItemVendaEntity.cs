using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;

namespace SistemaPdv.Core.Entity
{
    public class ItemVendaEntity
    {
        public int id_item_venda { get; set; }
        public int id_produto { get; set; }
        public int id_venda { get; set; }
        public int quantidade { get; set; }
        public decimal valor_unitario { get; set; }
        public decimal valor_total { get; set; }


        public static List<ItemVendaEntity> Pesquisar(int? id_produto = null, int? id_item_venda = null)
        {
            var connection = new NpgsqlConnection(UtilConnection.getStrConnection());

            try
            {
                var sql = $@"SELECT * 
                                 FROM ITEM_VENDA
                           WHERE 1 = 1 ";


                if (id_produto.HasValue)
                    sql += " AND ID_PRODUTO = " + id_produto.Value;

                if (id_item_venda.HasValue)
                    sql += " AND ID_ITEM_VENDA = " + id_item_venda.Value;
                                
                connection.Open();

                var commando = new NpgsqlCommand(sql, connection);

                NpgsqlDataReader reader = commando.ExecuteReader();

                var retorno = new List<ItemVendaEntity>();

                while (reader.Read())
                {
                    var item = new ItemVendaEntity();

                    item.id_item_venda = Convert.ToInt32(reader["id_item_venda"]);
                    item.id_produto = Convert.ToInt32(reader["id_produto"]);
                    item.id_venda = Convert.ToInt32(reader["id_venda"]);
                    item.quantidade = Convert.ToInt32(reader["quantidade"]);
                    item.valor_unitario = Convert.ToDecimal(reader["valor_unitario"]);
                    item.valor_total = Convert.ToDecimal(reader["valor_total"]);

                    retorno.Add(item);
                }
                                
                reader.Close();

                return retorno;
            }
            catch (Exception ex)
            {

                throw;
            }
            finally
            {
                connection?.Close();
            }
        }

    }
}
