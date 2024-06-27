using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaPdv.Core.Entity
{
    public class PrecoEntity
    {
        public int id_preco { get; set; }
        public int id_produto { get; set; }
        public decimal valor_produto { get; set; }
        public DateTime data_inicio_vigencia { get; set; }


        public static int Incluir(PrecoEntity entity, NpgsqlConnection connection = null)
        {
            bool closeConnection = connection == null;

            if (connection == null)
                connection = new NpgsqlConnection(UtilConnection.getStrConnection());

            var sql = $@"INSERT INTO PRECO (
                                        ID_PRODUTO, 
                                        DATA_INICIO_VIGENCIA, 
                                        VALOR_PRODUTO
                                    )VALUES(
                                        @ID_PRODUTO, 
                                        @DATA_INICIO_VIGENCIA, 
                                        @VALOR_PRODUTO) RETURNING ID_PRECO";

            var commando = new NpgsqlCommand(sql, connection);

            commando.Parameters.AddWithValue("@ID_PRODUTO", entity.id_produto);
            commando.Parameters.AddWithValue("@DATA_INICIO_VIGENCIA", entity.data_inicio_vigencia);
            commando.Parameters.AddWithValue("@VALOR_PRODUTO", entity.valor_produto);

            if (connection.State != ConnectionState.Open)
                connection.Open();

            entity.id_preco = Convert.ToInt32(commando.ExecuteScalar());

            if (closeConnection)
                connection?.Close();

            return entity.id_preco;
        }

        public static List<PrecoEntity> Pesquisar(int? id_produto)
        {
            var connection = new NpgsqlConnection(UtilConnection.getStrConnection());

            try
            {
                var sql = $@"SELECT * 
                                 FROM PRECO
                           WHERE 1 = 1 ";


                if (id_produto.HasValue)
                    sql += " AND ID_PRODUTO = " + id_produto.Value;

                var commando = new NpgsqlCommand(sql, connection);

                connection.Open();
                NpgsqlDataReader reader = commando.ExecuteReader();

                var retorno = new List<PrecoEntity>();

                while (reader.Read())
                {
                    var item = new PrecoEntity
                    {
                        id_produto = Convert.ToInt32(reader["id_produto"]),
                        data_inicio_vigencia = Convert.ToDateTime(reader["data_inicio_vigencia"]),
                        valor_produto = Convert.ToDecimal(reader["valor_produto"])
                    };

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

        public static void Delete(int id_produto, NpgsqlConnection connection = null)
        {
            bool closeConnection = connection == null;

            if (connection == null)
                connection = new NpgsqlConnection(UtilConnection.getStrConnection());

            var sql = $@"DELETE FROM PRECO 
                            WHERE ID_PRODUTO = @ID_PRODUTO";

            var commando = new NpgsqlCommand(sql, connection);

            commando.Parameters.AddWithValue("@ID_PRODUTO", id_produto);
        

            if (connection.State != ConnectionState.Open)
                connection.Open();

            commando.ExecuteScalar();

            if (closeConnection)
                connection?.Close();
    
        }
    }
}
