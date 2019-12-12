using SYS.QUERYS;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SYS.UTILS;
using SYS.QUERYS.Cadastros.Financeiro;

namespace SYS.FORMS.Cadastros.Financeiro
{
    public partial class FCondicaoPagamento_Cadastro : SYS.FORMS.FBase_Cadastro
    {
        #region Declarações

        public TB_FIN_CONDICAOPAGAMENTO CondicaoPagamento = new TB_FIN_CONDICAOPAGAMENTO();

        #endregion

        #region Métodos

        public FCondicaoPagamento_Cadastro()
        {
            InitializeComponent();

            this.Shown += FCondicaoPagamento_Shown;
        }


        public override void Gravar()
        {
            try
            {
                Validar();

                CondicaoPagamento= new TB_FIN_CONDICAOPAGAMENTO();
                CondicaoPagamento.ID_CONDICAOPAGAMENTO= teID_CONDICAOPAGAMENTO.Text.ToInt32().Padrao();
                CondicaoPagamento.DS = teDS.Text.Validar(true);
                CondicaoPagamento.QT_DIASDESDOBRO= seQT_DIASDESDOBRO.Value;

                var posicaoTransacao = 0;

                new QCondicaoPagamento().Gravar(CondicaoPagamento, ref posicaoTransacao);

                base.Gravar();
            }
            catch (Exception excessao)
            {
                excessao.Validar();
            }
        }


        #endregion

        #region Métodos

        private void FCondicaoPagamento_Shown(object sender, EventArgs e)
        {
            try
            {
                if (Modo == Modo.Cadastrar)
                    CondicaoPagamento = new TB_FIN_CONDICAOPAGAMENTO();
                else if (Modo == Modo.Alterar)
                {
                    teID_CONDICAOPAGAMENTO.Text = CondicaoPagamento.ID_CONDICAOPAGAMENTO.ToString();
                    teDS.Text = CondicaoPagamento.DS.Validar();
                    seQT_DIASDESDOBRO.Value = CondicaoPagamento.QT_DIASDESDOBRO.Padrao();
                }

                teDS.Focus();
            }
            catch (Exception excessao)
            {
                excessao.Validar();
            }
        }
        
        #endregion
    }
}