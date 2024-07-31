using SistemaPdv.Core.Logic;
using SistemaPdv.Core.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SistemaPDVWindowsForms
{
    public partial class frmListaTipoProduto : frmBase
    {
        public frmListaTipoProduto()
        {
            InitializeComponent();
        }

        private void frmListaTipoProduto_Load(object sender, EventArgs e)
        {
            try
            {
                var lista = TipoProdutoLogic.ListarTipoProduto();
                PupulaGrid(lista);

            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Erro no processamento", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnFiltrar_Click(object sender, EventArgs e)
        {
            try
            {
                var lista = TipoProdutoLogic.ListarTipoProduto(txtDescricao.Text);
                PupulaGrid(lista);

            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Erro no processamento", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void PupulaGrid(List<TipoProdutoViewModel> lista)
        {
            var novaLista = lista.Select(x => new { id = x.id_Tipo_produto, descricao = x.descricao }).ToList();
            dgvLista.DataSource = novaLista;
        }

        private void btnAlterar_Click(object sender, EventArgs e)
        {

        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            try
            {
                var resultado = MessageBox.Show(this, "Tem certeza que deseja excluir?", "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (resultado == DialogResult.No)
                    return;

                if (dgvLista.SelectedRows.Count == 0)
                    return;

                if (!int.TryParse(dgvLista.SelectedRows[0].Cells[0].Value.ToString(), out int id))
                    return;

                TipoProdutoLogic.Excluir(id);

                var lista = TipoProdutoLogic.ListarTipoProduto();
                PupulaGrid(lista);

                MessageBox.Show(this, "Excluído com sucesso", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Erro no processamento", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
