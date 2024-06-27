using Npgsql;
using NpgsqlTypes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaPdv.Core.Entity
{
    public class ClienteEntity
    {        
        public int Id_Cliente { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public int Ano_Nascimento { get; set; }
        public string Telefone { get; set; }
        public DateTime? DataNascimento { get; set; }

        public int Incluir(NpgsqlConnection connection = null)
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

            commando.Parameters.AddWithValue("@NOME_FABRICANTE", "NOME FABRICANTE");
            commando.Parameters.AddWithValue("@DESCRICAO", "DESCRICAO PROD");
            commando.Parameters.AddWithValue("@QUANTIDADE_ESTOQUE", 150);
            commando.Parameters.AddWithValue("@NUMERO_CODIGO_BARRAS", 1234567);

            connection.Open();

            this.Id_Cliente = Convert.ToInt32(commando.ExecuteScalar());

            if (closeConnection)
                connection?.Close();

            return this.Id_Cliente;
        }

        void Alterar()
        {

        }

        public List<ClienteEntity> Pesquisar()
        {
            var connection = new NpgsqlConnection(UtilConnection.getStrConnection());

            try
            {
                var sql = $@"SELECT * 
                                FROM PRODUTO
                           WHERE 1 = 1 ";

                var commando = new NpgsqlCommand(sql, connection);

                connection.Open();
                NpgsqlDataReader reader = commando.ExecuteReader();

                var retorno = new List<ClienteEntity>();

                while (reader.Read())
                {
                    var item = new ClienteEntity();

                    item.Nome = reader["descricao"].ToString();
                    item.Email = reader["nome_fabricante"].ToString();

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

        void Excluir()
        {

        }
    }
}
