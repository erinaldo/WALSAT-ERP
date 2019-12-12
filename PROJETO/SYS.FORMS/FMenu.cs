using DevExpress.XtraEditors;
using SYS.FORMS.Cadastros.Configuracao;
using SYS.FORMS.Cadastros.Estoque;
using SYS.FORMS.Cadastros.Financeiro;
using SYS.FORMS.Cadastros.Gourmet;
using SYS.FORMS.Cadastros.Relacionamento;
using SYS.FORMS.Comercial.Lancamentos;
using SYS.FORMS.Lancamentos.Comercial;
using SYS.FORMS.Lancamentos.Estoque;
using SYS.FORMS.Lancamentos.Financeiro;
using SYS.FORMS.Lancamentos.Gourmet;
using SYS.FORMS.Relatorios;
using SYS.UTILS;
using System;

namespace SYS.FORMS
{
    public partial class FMenu : SYS.FORMS.FBase
    {
        #region Métodos

        public FMenu()
        {
            InitializeComponent();

            this.Shown += FMenu_Shown;
        }

        private void MDI(XtraForm forma)
        {
            if (forma == null)
                return;

            forma.MdiParent = this;
            forma.Show();
        }


        #endregion

        #region Eventos

        private void FMenu_Shown(object sender, EventArgs e)
        {
            try
            {
                //MDI(new FDashboard());


                //var dadosEmpresa = new ModelDataContext(Parametros.StringConexao).TB_CON_EMPRESAs.FirstOrDefault() ?? new TB_CON_EMPRESA();
                Parametros.ST_Gourmet = true; // dadosEmpresa.ST_GOURMET.Padrao();

                bsiUsuario.Caption = Parametros.NM_Usuario;
                bsiServidor.Caption = "Servidor: " + Parametros.Servidor;
                bsiBanco.Caption = "Banco de dados: " + Parametros.Banco;



                //Configuracao
                bbiCadastroConfiguracao_Usuario.ItemClick += bbiCadastroConfiguracao_Usuario_ItemClick;
                bbiCadastroRelacionamento_Empresa.ItemClick += bbiCadastroRelacionamento_Empresa_ItemClick;

                //Estoque
                bbiLancamentoEstoque_PrecoProduto.ItemClick += bbiCadastroEstoque_Preco_ItemClick;
                bbiCadastroEstoque_Produto.ItemClick += bbiCadastroEstoque_Produto_ItemClick;
                bbiCadastroEstoque_Grupo.ItemClick += bbiCadastroEstoque_Grupo_ItemClick;
                bbiCadastroEstoque_Marca.ItemClick += bbiCadastroEstoque_Marca_ItemClick;
                bbiCadastroEstoque_Departamento.ItemClick += bbiCadastroEstoque_Departamento_ItemClick;


                //Relacionamento
                bbiCadastroRelacionamento_Clifor.ItemClick += bbiCadastroRelacionamento_Clifor_ItemClick;
                bbiCadastroRelacionamento_Endereco.ItemClick += bbiCadastroRelacionamento_Endereco_ItemClick;

                //Gourmet
                bbiLancamentoGourmet_Mesa.ItemClick += bbiLancamentoGourmet_Mesa_ItemClick;
                bbiLancamentoGourmet_Cartao.ItemClick += bbiLancamentoGourmet_Cartao_ItemClick;
                bbiLancamentosGourmet_ConsultaCartoes.ItemClick += bbiLancamentoGourmet_ConsultaCartao_ItemClick;
                
                bbiCadastroGourmet_Ambiente.ItemClick += bbiCadastroGourmet_Ambiente_ItemClick;
                bbiCadastroGourmet_Mesa.ItemClick += bbiCadastroGourmet_Mesa_ItemClick;
                bbiCadastroGourmet_Impressoras.ItemClick += bbiCadastroGourmet_Impressoras_ItemClick;


                //Comercial

                bbiComercial_RelVenda.ItemClick += bbiComercial_RelVenda_ItemClick;

                //Financeiro
                bbiContasPagarReceber.ItemClick += bbiContasPagarReceber_ItemClick;
                bbiCadastroFinanceiro_CCusto.ItemClick += bbiCadastroFinanceiro_CCusto_ItemClick;
                bbiCaixa.ItemClick += bbiCadastroFinanceiro_Caixa_ItemClick;

            }
            catch (Exception excessao)
            {
                excessao.Validar();
            }
        }

        #region Cadastros

        #region Gourmet

        private void bbiCadastroGourmet_Impressoras_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                MDI(new FImpressora_Busca());
            }
            catch (Exception excessao)
            {
                excessao.Validar();
            }
        }

        private void bbiCadastroGourmet_Mesa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                MDI(new FMesa_Busca());
            }
            catch (Exception excessao)
            {
                excessao.Validar();
            }
        }

        private void bbiCadastroGourmet_Ambiente_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                MDI(new FAmbiente_Busca());
            }
            catch (Exception excessao)
            {
                excessao.Validar();
            }
        }
        
        private void bbiLancamentoGourmet_Cartao_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                MDI(new FCartoes());
            }
            catch (Exception excessao)
            {
                excessao.Validar();
            }
        }

        private void bbiLancamentoGourmet_ConsultaCartao_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                new FConsulta_Cartao().ShowDialog();
            }
            catch (Exception excessao)
            {
                excessao.Validar();
            }
        }

        private void bbiLancamentoGourmet_Mesa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                MDI(new FMesas());
            }
            catch (Exception excessao)
            {
                excessao.Validar();
            }
        }


        #endregion

        #region Relacionamento

        private void bbiCadastroRelacionamento_Empresa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                MDI(new FEmpresa_Busca());
            }
            catch (Exception excessao)
            {
                excessao.Validar();
            }
        }

        private void bbiCadastroRelacionamento_Clifor_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                MDI(new FClifor_Busca());
            }
            catch (Exception excessao)
            {
                excessao.Validar();
            }
        }

        private void bbiCadastroRelacionamento_Endereco_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                MDI(new FEndereco_Busca());
            }
            catch (Exception excessao)
            {
                excessao.Validar();
            }
        }

        #endregion

        #region Estoque

        private void bbiCadastroEstoque_Grupo_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                MDI(new FGrupo_Busca());
            }
            catch (Exception excessao)
            {
                excessao.Validar();
            }
        }

        private void bbiCadastroEstoque_Marca_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                MDI(new FMarca_Busca());
            }
            catch (Exception excessao)
            {
                excessao.Validar();
            }
        }

        private void bbiCadastroEstoque_Produto_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                MDI(new FProduto_Busca());
            }
            catch (Exception excessao)
            {
                excessao.Validar();
            }
        }

        private void bbiCadastroEstoque_Preco_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                MDI(new FPreco_Busca());
            }
            catch (Exception excessao)
            {
                excessao.Validar();
            }
        }

        private void bbiCadastroEstoque_Departamento_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                MDI(new FDepartamento_Busca());
            }
            catch (Exception excessao)
            {
                excessao.Validar();
            }
        }


        #endregion

        #region Configuração

        private void bbiCadastroConfiguracao_Usuario_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                MDI(new FUsuario_Busca());
            }
            catch (Exception excessao)
            {
                excessao.Validar();
            }
        }


        #endregion

        #region Financeiro

        private void bbiCadastroFinanceiro_CondicaoPagamento_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                MDI(new FCondicaoPagamento_Busca());
            }
            catch (Exception excessao)
            {
                excessao.Validar();
            }
        }

        private void bbiCadastroFinanceiro_CCusto_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                MDI(new FCCusto_Busca());
            }
            catch (Exception excessao)
            {
                excessao.Validar();
            }
        }


        private void bbiCadastroFinanceiro_Caixa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                MDI(new FCaixa());
            }
            catch (Exception excessao)
            {
                excessao.Validar();
            }
        }


        #endregion

        #endregion

        #region Lançamentos

        #region Comercial

        private void bbiLancamentoComercial_PDV_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                new FPontodeVenda().ShowDialog();
            }
            catch (Exception excessao)
            {
                excessao.Validar();
            }
        }

        #endregion

        private void bbiAbrirCaixa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var frm = new FAberturaCaixa().ShowDialog();
        }

        private void bbiContasPagarReceber_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var frm = new FContas_Busca().ShowDialog();
        }

        #endregion

        #region Relatórios

        private void bbiComercial_RelVenda_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                MDI(new FRelancao_Venda());
            }
            catch (Exception excessao)
            {
                excessao.Validar();
            }
        }

        #endregion

        #endregion
    }
}