using SYS.QUERYS;
using SYS.QUERYS.Cadastros.Financeiro;
using SYS.UTILS;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SYS.FORMS.Cadastros.Financeiro
{
    public partial class FCCusto_Cadastro : SYS.FORMS.FBase_Cadastro
    {

        public TB_FIN_CENTROCUSTO CCusto = null;

        public FCCusto_Cadastro()
        {
            InitializeComponent();

            this.Shown += delegate
            {
                try
                {

                    if (Modo == Modo.Cadastrar)
                        CCusto = new TB_FIN_CENTROCUSTO();
                    else if (Modo == Modo.Alterar)
                    {
                        if (CCusto == null)
                            Excessoes.Alterar();

                        teIdentificador.Text = CCusto.ID_CENTROCUSTO.ToString();
                        teDescricao.Text = CCusto.NM.Validar();
                        
                    }
                }
                catch (Exception excessao)
                {
                    excessao.Validar();
                }
            };
        }

        public override void Gravar()
        {
            try
            {
                Validar();

                CCusto = new TB_FIN_CENTROCUSTO();

                CCusto.ID_CENTROCUSTO = teIdentificador.Text.ToInt32().Padrao();
                CCusto.NM = teDescricao.Text.Validar(true);

                var posicaoTransacao = 0;
                new QCCusto().Gravar(CCusto, ref posicaoTransacao);

                base.Gravar();
            }
            catch (Exception excessao)
            {
                excessao.Validar();
            }
        }
    }
}
