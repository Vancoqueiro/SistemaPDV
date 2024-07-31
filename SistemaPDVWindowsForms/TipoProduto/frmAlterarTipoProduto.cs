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
    public partial class frmAlterarTipoProduto : frmBase
    {

        private int _idTipoProduto = 0;
        private TipoProdutoViewModel _clsTipoProdutoModel;

        public frmAlterarTipoProduto(int id)
        {
            _idTipoProduto = id;
            InitializeComponent();
        }


        private void frmCadastroTipoProduto_Load(object sender, EventArgs e)
        {
            try
            {
                _clsTipoProdutoModel = TipoProdutoLogic.Pesquisar(this._idTipoProduto);

                txtDescricao.Text = _clsTipoProdutoModel.descricao;
                txtObservacao.Text = _clsTipoProdutoModel.observacao;

            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Erro no processamento", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAlterar_Click(object sender, EventArgs e)
        {
            try
            {
                _clsTipoProdutoModel.descricao = txtDescricao.Text;
                _clsTipoProdutoModel.observacao = txtObservacao.Text;

                TipoProdutoLogic.Editar(_clsTipoProdutoModel);

                MessageBox.Show(this, "Alterado com sucesso", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);


                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro " + ex.Message);
            }
        }
    }
}
