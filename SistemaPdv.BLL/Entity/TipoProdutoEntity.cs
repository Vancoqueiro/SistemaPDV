using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaPdv.Core.Entity
{
    public class TipoProdutoEntity
    {
        public int id_tipo_produto { get; set; }
        public string descricao { get; set; }
        public string observacao { get; set; }
        public DateTime? data_exclusao { get; set; }

        public static int Insert(TipoProdutoEntity entity, NpgsqlConnection connection = null)
        {
            bool closeConnection = connection == null;

            if (connection == null)
                connection = new NpgsqlConnection(UtilConnection.getStrConnection());

            var sql = $@"INSERT INTO TIPO_PRODUTO (
                                        DESCRICAO, 
                                        OBSERVACAO
                                    )VALUES(
                                        @DESCRICAO, 
                                        @OBSERVACAO) RETURNING ID_TIPO_PRODUTO";

            var commando = new NpgsqlCommand(sql, connection);

            commando.Parameters.AddWithValue("@DESCRICAO", entity.descricao);

            if (string.IsNullOrEmpty(entity.observacao))
                commando.Parameters.AddWithValue("@OBSERVACAO", DBNull.Value);
            else
                commando.Parameters.AddWithValue("@OBSERVACAO", entity.observacao);

            if (connection.State != ConnectionState.Open)
                connection.Open();

            entity.id_tipo_produto = Convert.ToInt32(commando.ExecuteScalar());

            if (closeConnection)
                connection?.Close();

            return entity.id_tipo_produto;
        }

        public static List<TipoProdutoEntity> Pesquisar(int? id = null, string descricao = null)
        {
            var connection = new NpgsqlConnection(UtilConnection.getStrConnection());

            try
            {
                var sql = $@"SELECT * 
                                FROM TIPO_PRODUTO
                           WHERE DATA_EXCLUSAO IS NULL ";

                if (id.HasValue)
                    sql += " AND ID_TIPO_PRODUTO = " + id.Value;

                if (!string.IsNullOrEmpty(descricao))
                    sql += $" AND UPPER(DESCRICAO) LIKE '%{descricao.ToUpper()}%'";

                var commando = new NpgsqlCommand(sql, connection);

                connection.Open();
                NpgsqlDataReader reader = commando.ExecuteReader();

                var retorno = new List<TipoProdutoEntity>();

                while (reader.Read())
                {
                    var item = new TipoProdutoEntity();

                    item.id_tipo_produto = Convert.ToInt32(reader["id_tipo_produto"]);
                    item.descricao = reader["descricao"].ToString();
                    item.observacao = reader["observacao"].ToString();

                    if (reader["data_exclusao"] != DBNull.Value)
                        item.data_exclusao = Convert.ToDateTime(reader["data_exclusao"]);

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

        public static int Update(TipoProdutoEntity entity, NpgsqlConnection connection = null)
        {
            bool closeConnection = connection == null;

            if (connection == null)
                connection = new NpgsqlConnection(UtilConnection.getStrConnection());

            var sql = $@"UPDATE TIPO_PRODUTO 
                            SET DESCRICAO = @DESCRICAO,
                            OBSERVACAO = @OBSERVACAO,
                            DATA_EXCLUSAO = @DATA_EXCLUSAO
                            WHERE ID_TIPO_PRODUTO = @ID_TIPO_PRODUTO";

            var commando = new NpgsqlCommand(sql, connection);

            commando.Parameters.AddWithValue("@DESCRICAO", entity.descricao);

            if (string.IsNullOrEmpty(entity.observacao))
                commando.Parameters.AddWithValue("@OBSERVACAO", DBNull.Value);
            else
                commando.Parameters.AddWithValue("@OBSERVACAO", entity.observacao);

            if (entity.data_exclusao.HasValue)
                commando.Parameters.AddWithValue("@DATA_EXCLUSAO", entity.data_exclusao);
            else
                commando.Parameters.AddWithValue("@DATA_EXCLUSAO", DBNull.Value);

            commando.Parameters.AddWithValue("@ID_TIPO_PRODUTO", entity.id_tipo_produto);

            if (connection.State != ConnectionState.Open)
                connection.Open();

            commando.ExecuteScalar();

            if (closeConnection)
                connection?.Close();

            return entity.id_tipo_produto;
        }

        public static void Delete(int id, NpgsqlConnection connection = null)
        {
            bool closeConnection = connection == null;

            if (connection == null)
                connection = new NpgsqlConnection(UtilConnection.getStrConnection());

            var sql = $@"DELETE FROM TIPO_PRODUTO
                            WHERE ID_TIPO_PRODUTO = @ID_TIPO_PRODUTO";

            var commando = new NpgsqlCommand(sql, connection);

            commando.Parameters.AddWithValue("@ID_TIPO_PRODUTO", id);


            if (connection.State != ConnectionState.Open)
                connection.Open();

            commando.ExecuteScalar();

            if (closeConnection)
                connection?.Close();

        }
    }
}

