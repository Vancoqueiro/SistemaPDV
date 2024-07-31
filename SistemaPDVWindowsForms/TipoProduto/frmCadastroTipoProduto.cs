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
    public partial class frmCadastroTipoProduto : frmBase
    {
        public frmCadastroTipoProduto()
        {
            InitializeComponent();
        }
                
        private void btnCreate_Click(object sender, EventArgs e)
        {
            try
            {
                var model = new TipoProdutoViewModel();
                model.descricao = txtDescricao.Text;
                model.observacao = txtObservacao.Text;

                TipoProdutoLogic.RegistrarTipoProduto(model);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro " + ex.Message);
            }
        }

        private void txtDescricao_TextChanged(object sender, EventArgs e)
        {

        }

        private void frmCadastroTipoProduto_Load(object sender, EventArgs e)
        {

        }
    }
}
