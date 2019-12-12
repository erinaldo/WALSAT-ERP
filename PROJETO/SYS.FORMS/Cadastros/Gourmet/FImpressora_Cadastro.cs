using SYS.QUERYS;
using SYS.QUERYS.Cadastros.Gourmet;
using SYS.UTILS;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SYS.FORMS.Cadastros.Gourmet
{
    public partial class FImpressora_Cadastro : SYS.FORMS.FBase_Cadastro
    {
        public TB_GOU_IMPRESSORA impressora = null;

        public FImpressora_Cadastro()
        {
            InitializeComponent();

            this.Shown += delegate
            {
                try
                {
                    if (Modo == Modo.Cadastrar)
                        impressora = new TB_GOU_IMPRESSORA();
                    else if (Modo == Modo.Alterar)
                    {
                        if (impressora == null)
                            Excessoes.Alterar();

                        teIdentificador.Text = impressora.ID_IMPRESSORA.ToString();
                        teNM.Text = impressora.NM.Validar();
                        teCaminho.Text = impressora.NM_CAMINHO.Validar();
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

                impressora = new TB_GOU_IMPRESSORA();

                impressora.ID_IMPRESSORA = teIdentificador.Text.ToInt32().Padrao();
                impressora.NM = teNM.Text.Validar(true);
                impressora.NM_CAMINHO = teCaminho.Text.Trim().Validar();

                var posicaoTransacao = 0;
                new QImpressora().Gravar(impressora, ref posicaoTransacao);

                base.Gravar();
            }
            catch (Exception excessao)
            {
                excessao.Validar();
            }
        }
    }
}
