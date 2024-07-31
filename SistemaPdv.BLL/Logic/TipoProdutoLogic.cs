using Npgsql;
using SistemaPdv.Core.Entity;
using SistemaPdv.Core.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaPdv.Core.Logic
{
    public static class TipoProdutoLogic
    {
        public static void RegistrarTipoProduto(TipoProdutoViewModel viewModel)
        {
            if (string.IsNullOrEmpty(viewModel.descricao))
                throw new ArgumentException("Informe a descrição do Produto");

            var descricoesExistentes = TipoProdutoEntity.Pesquisar(null, viewModel.descricao);

            if (descricoesExistentes.Count > 0)
                throw new ArgumentException("A descrição informada já existe");

            var tipoProdutoEntity = new TipoProdutoEntity();
            tipoProdutoEntity.descricao = viewModel.descricao;
            tipoProdutoEntity.observacao = viewModel.observacao;
            TipoProdutoEntity.Insert(tipoProdutoEntity);
        }

        public static List<TipoProdutoViewModel> ListarTipoProduto(string descricao = null)
        {
            var retorno = new List<TipoProdutoViewModel>();

            var TipoProdutosEntity = TipoProdutoEntity.Pesquisar(null, descricao);

            foreach (var entity in TipoProdutosEntity)
            {
                var model = new TipoProdutoViewModel();
                model.id_Tipo_produto = entity.id_tipo_produto;
                model.descricao = entity.descricao;
                model.observacao = entity.observacao;
                retorno.Add(model);
            }

            return retorno;
        }

        public static void Editar(TipoProdutoViewModel viewModel)
        {
            var connection = new NpgsqlConnection(UtilConnection.getStrConnection());
            connection.Open();
            var transaction = connection.BeginTransaction();

            try
            {
                var TipoprodutoEntity = new TipoProdutoEntity();

                TipoprodutoEntity.id_tipo_produto = viewModel.id_Tipo_produto;
                TipoprodutoEntity.descricao = viewModel.descricao;
                TipoprodutoEntity.observacao = viewModel.observacao;

                TipoProdutoEntity.Update(TipoprodutoEntity, connection);


                transaction.Commit();

            }
            catch (Exception ex)
            {
                transaction.Rollback();

                throw;
            }
        }

        public static TipoProdutoViewModel Pesquisar(int idTipoProduto)
        {
            TipoProdutoViewModel retorno = null;

            var entity = TipoProdutoEntity.Pesquisar(idTipoProduto).FirstOrDefault();

            if (entity != null)
            {
                retorno = new TipoProdutoViewModel();

                retorno.descricao = entity.descricao;
                retorno.id_Tipo_produto = entity.id_tipo_produto;
                retorno.observacao = entity.observacao;
            }

            return retorno;

        }

        public static void Excluir(int idTipoProduto)
        {
            var connection = new NpgsqlConnection(UtilConnection.getStrConnection());
            connection.Open();
            var transaction = connection.BeginTransaction();

            try
            {
                var lstTipoProduto = TipoProdutoEntity.Pesquisar(idTipoProduto);

                if (lstTipoProduto.Count > 0)
                {
                    var TipoprodutoEntity = TipoProdutoEntity.Pesquisar(idTipoProduto).FirstOrDefault();
                    TipoprodutoEntity.data_exclusao = DateTime.Now;
                    TipoProdutoEntity.Update(TipoprodutoEntity);
                }
                else
                {
                    TipoProdutoEntity.Delete(idTipoProduto, connection);

                    transaction.Commit();
                }
            }
            catch (Exception ex)
            {
                transaction.Rollback();

                throw;
            }

        }
    }
}