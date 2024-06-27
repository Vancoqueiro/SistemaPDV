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
    public static class ProdutoLogic
    {
        public static void RegistrarProduto(ProdutoViewModel viewModel)
        {
            var connection = new NpgsqlConnection(UtilConnection.getStrConnection());
            connection.Open();
            var transaction = connection.BeginTransaction();

            try
            {
                var produtoEntity = new ProdutoEntity();

                produtoEntity.nome_fabricante = viewModel.nome_fabricante;
                produtoEntity.numero_codigo_barras = viewModel.numero_codigo_barras;
                produtoEntity.quantidade_estoque = viewModel.quantidade_estoque;
                produtoEntity.descricao = viewModel.descricao;

                ProdutoEntity.Insert(produtoEntity, connection);

                var precoEntity = new PrecoEntity();

                precoEntity.id_produto = produtoEntity.id_produto;
                precoEntity.valor_produto = viewModel.valor_produto;
                precoEntity.data_inicio_vigencia = DateTime.Now;

                PrecoEntity.Incluir(precoEntity, connection);

                transaction.Commit();

            }
            catch (Exception ex)
            {
                transaction.Rollback();

                throw;
            }
        }

        public static List<ProdutoViewModel> ListarProduto(string descricao = null)
        {
            var retorno = new List<ProdutoViewModel>();

            var produtosEntity = ProdutoEntity.Pesquisar(null, descricao);

            foreach (var entity in produtosEntity)
            {
                var model = new ProdutoViewModel();
                model.descricao = entity.descricao;
                model.nome_fabricante = entity.nome_fabricante;
                model.id_produto = entity.id_produto;
                retorno.Add(model);
            }

            return retorno;
        }

        public static ProdutoViewModel PesquisarProduto(int idProduto)
        {
            ProdutoViewModel retorno = null;

            var entity = ProdutoEntity.Pesquisar(idProduto).FirstOrDefault();

            if (entity != null)
            {
                retorno = new ProdutoViewModel();

                retorno.descricao = entity.descricao;
                retorno.nome_fabricante = entity.nome_fabricante;
                retorno.id_produto = entity.id_produto;
                retorno.numero_codigo_barras = entity.numero_codigo_barras;
                retorno.quantidade_estoque = entity.quantidade_estoque;

                var precosEntity = PrecoEntity.Pesquisar(idProduto);
                var precoVigenteEntity = precosEntity.OrderByDescending(x => x.data_inicio_vigencia).FirstOrDefault();

                if (precoVigenteEntity != null)
                    retorno.valor_produto = precoVigenteEntity.valor_produto;
            }

            return retorno;
        }

        public static ProdutoDetailsViewModel PesquisarDetalhesProduto(int idProduto)
        {
            ProdutoDetailsViewModel viewModel = new ProdutoDetailsViewModel();

            viewModel.Produto = PesquisarProduto(idProduto);

            var precosEntity = PrecoEntity.Pesquisar(idProduto);

            foreach (var precoEntity in precosEntity)
            {
                var precoViewModel = new PrecoDetailsViewModel();

                precoViewModel.data_inicio_vigencia = precoEntity.data_inicio_vigencia;
                precoViewModel.valor_produto = precoEntity.valor_produto;

                viewModel.HistoricoPreco.Add(precoViewModel);
            }

            return viewModel;
        }

        public static void Editar(ProdutoViewModel viewModel)
        {
            var connection = new NpgsqlConnection(UtilConnection.getStrConnection());
            connection.Open();
            var transaction = connection.BeginTransaction();

            try
            {
                var produtoEntity = new ProdutoEntity();

                produtoEntity.nome_fabricante = viewModel.nome_fabricante;
                produtoEntity.numero_codigo_barras = viewModel.numero_codigo_barras;
                produtoEntity.quantidade_estoque = viewModel.quantidade_estoque;
                produtoEntity.descricao = viewModel.descricao;
                produtoEntity.id_produto = viewModel.id_produto;

                ProdutoEntity.Update(produtoEntity, connection);

                var precoEntity = new PrecoEntity();

                precoEntity.id_produto = produtoEntity.id_produto;
                precoEntity.valor_produto = viewModel.valor_produto;
                precoEntity.data_inicio_vigencia = DateTime.Now;

                PrecoEntity.Incluir(precoEntity, connection);

                transaction.Commit();

            }
            catch (Exception ex)
            {
                transaction.Rollback();

                throw;
            }
        }

        public static void Excluir(int idProduto)
        {
            var connection = new NpgsqlConnection(UtilConnection.getStrConnection());
            connection.Open();
            var transaction = connection.BeginTransaction();

            try
            {

                var lstItensVenda = ItemVendaEntity.Pesquisar(idProduto);

                if (lstItensVenda.Count > 0)
                {
                    var produtoEntity = ProdutoEntity.Pesquisar(idProduto).FirstOrDefault();
                    produtoEntity.data_exclusao = DateTime.Now;
                    ProdutoEntity.Update(produtoEntity);
                }
                else
                {
                    PrecoEntity.Delete(idProduto, connection);
                    ProdutoEntity.Delete(idProduto, connection);

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