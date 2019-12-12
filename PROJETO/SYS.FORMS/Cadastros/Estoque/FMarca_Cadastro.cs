using SYS.QUERYS;
using SYS.QUERYS.Cadastros.Estoque;
using SYS.UTILS;
using System;

namespace SYS.FORMS.Cadastros.Estoque
{
    public partial class FMarca_Cadastro : SYS.FORMS.FBase_Cadastro
    {
        #region Declarações

        public TB_EST_MARCA marca;

        #endregion

        #region Métodos

        public FMarca_Cadastro()
        {
            InitializeComponent();
        }

        public override void Gravar()
        {
            try
            {
                Validar();

                marca = new TB_EST_MARCA();
                marca.ID_MARCA = teID_MARCA.Text.ToInt32().Padrao();
                marca.NM = teNM_MARCA.Text.Validar(true);

                var posicaoTransacao = 0;
                new QMarca().Gravar(marca, ref posicaoTransacao);

                base.Gravar();
            }
            catch (Exception excessao)
            {
                excessao.Validar();
            }
        }

        #endregion

        #region Eventos
        private void FMarca_Cadastro_Shown(object sender, EventArgs e)
        {
            try
            {
                if (Modo == Modo.Cadastrar)
                    marca = new TB_EST_MARCA();
                else if (Modo == Modo.Alterar)
                {
                    if (marca == null)
                        Excessoes.Alterar();

                    teID_MARCA.Text = marca.ID_MARCA.ToString();
                    teNM_MARCA.Text = marca.NM.Validar();
                }

                teNM_MARCA.Focus();
            }
            catch (Exception excessao)
            {
                excessao.Validar();
            }
        }

        #endregion
    }
}