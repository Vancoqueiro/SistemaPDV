using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaPdv.Core.Entity
{
    public class ProdutoEntity
    {
        public int id_produto { get; set; }
        public string nome_fabricante { get; set; }
        public string descricao { get; set; }
        public int quantidade_estoque { get; set; }
        public string numero_codigo_barras { get; set; }
        public DateTime? data_exclusao { get; set; }


        public static int Insert(ProdutoEntity entity, NpgsqlConnection connection = null)
        {
            bool closeConnection = connection == null;

            if (connection == null)
                connection = new NpgsqlConnection(UtilConnection.getStrConnection());

            var sql = $@"INSERT INTO PRODUTO (
                                        NOME_FABRICANTE, 
                                        DESCRICAO, 
                                        QUANTIDADE_ESTOQUE, 
                                        NUMERO_CODIGO_BARRAS
                                    )VALUES(
                                        @NOME_FABRICANTE, 
                                        @DESCRICAO, 
                                        @QUANTIDADE_ESTOQUE, 
                                        @NUMERO_CODIGO_BARRAS) RETURNING ID_PRODUTO";

            var commando = new NpgsqlCommand(sql, connection);

            commando.Parameters.AddWithValue("@NOME_FABRICANTE", entity.nome_fabricante);
            commando.Parameters.AddWithValue("@DESCRICAO", entity.descricao);
            commando.Parameters.AddWithValue("@QUANTIDADE_ESTOQUE", entity.quantidade_estoque);
            commando.Parameters.AddWithValue("@NUMERO_CODIGO_BARRAS", entity.numero_codigo_barras);

            if (connection.State != ConnectionState.Open)
                connection.Open();

            entity.id_produto = Convert.ToInt32(commando.ExecuteScalar());

            if (closeConnection)
                connection?.Close();

            return entity.id_produto;
        }

        public static List<ProdutoEntity> Pesquisar(int? id, string descricao = null)
        {
            var connection = new NpgsqlConnection(UtilConnection.getStrConnection());

            try
            {
                var sql = $@"SELECT * 
                                FROM PRODUTO
                           WHERE DATA_EXCLUSAO IS NULL ";

                if (id.HasValue)
                    sql += " AND ID_PRODUTO = " + id.Value;

                if (!string.IsNullOrEmpty(descricao))
                    sql += $" AND UPPER(DESCRICAO) LIKE '%{descricao.ToUpper()}%'";

                var commando = new NpgsqlCommand(sql, connection);

                connection.Open();
                NpgsqlDataReader reader = commando.ExecuteReader();

                var retorno = new List<ProdutoEntity>();

                while (reader.Read())
                {
                    var item = new ProdutoEntity();

                    item.id_produto = Convert.ToInt32(reader["id_produto"]);
                    item.descricao = reader["descricao"].ToString();
                    item.nome_fabricante = reader["nome_fabricante"].ToString();
                    item.quantidade_estoque = Convert.ToInt32(reader["quantidade_estoque"]);
                    item.numero_codigo_barras = reader["numero_codigo_barras"].ToString();

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

        public static int Update(ProdutoEntity entity, NpgsqlConnection connection = null)
        {
            bool closeConnection = connection == null;

            if (connection == null)
                connection = new NpgsqlConnection(UtilConnection.getStrConnection());

            var sql = $@"UPDATE PRODUTO 
                            SET NOME_FABRICANTE = @NOME_FABRICANTE,
                            DESCRICAO = @DESCRICAO,
                            QUANTIDADE_ESTOQUE = @QUANTIDADE_ESTOQUE,
                            NUMERO_CODIGO_BARRAS = @NUMERO_CODIGO_BARRAS,
                            DATA_EXCLUSAO = @DATA_EXCLUSAO
                            WHERE ID_PRODUTO = @ID_PRODUTO";

            var commando = new NpgsqlCommand(sql, connection);

            commando.Parameters.AddWithValue("@NOME_FABRICANTE", entity.nome_fabricante);
            commando.Parameters.AddWithValue("@DESCRICAO", entity.descricao);
            commando.Parameters.AddWithValue("@QUANTIDADE_ESTOQUE", entity.quantidade_estoque);
            commando.Parameters.AddWithValue("@NUMERO_CODIGO_BARRAS", entity.numero_codigo_barras);
            commando.Parameters.AddWithValue("@ID_PRODUTO", entity.id_produto);
            if (entity.data_exclusao.HasValue)
                commando.Parameters.AddWithValue("@DATA_EXCLUSAO", entity.data_exclusao);
            else
                commando.Parameters.AddWithValue("@DATA_EXCLUSAO", DBNull.Value);

            if (connection.State != ConnectionState.Open)
                connection.Open();

            commando.ExecuteScalar();

            if (closeConnection)
                connection?.Close();

            return entity.id_produto;
        }

        public static void Delete(int id, NpgsqlConnection connection = null)
        {
            bool closeConnection = connection == null;

            if (connection == null)
                connection = new NpgsqlConnection(UtilConnection.getStrConnection());

            var sql = $@"DELETE FROM PRODUTO
                            WHERE ID_PRODUTO = @ID_PRODUTO";

            var commando = new NpgsqlCommand(sql, connection);

            commando.Parameters.AddWithValue("@ID_PRODUTO", id);


            if (connection.State != ConnectionState.Open)
                connection.Open();

            commando.ExecuteScalar();

            if (closeConnection)
                connection?.Close();

        }

    }
}

