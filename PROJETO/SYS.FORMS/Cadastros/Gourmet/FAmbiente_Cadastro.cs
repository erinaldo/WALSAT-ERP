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
    public partial class FAmbiente_Cadastro : SYS.FORMS.FBase_Cadastro
    {
        public TB_GOU_AMBIENTE Ambiente = null;

        public FAmbiente_Cadastro()
        {
            InitializeComponent();

            this.Shown += delegate
            {
                try
                {
                    if (Modo == Modo.Cadastrar)
                        Ambiente = new TB_GOU_AMBIENTE();
                    else if (Modo == Modo.Alterar)
                    {
                        if (Ambiente == null)
                            Excessoes.Alterar();

                        teIdentificador.Text = Ambiente.ID_AMBIENTE.ToString();
                        teNMAmbiente.Text = Ambiente.NM.Validar();                        
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

                Ambiente.ID_AMBIENTE = teIdentificador.Text.ToInt32().Padrao();
                Ambiente.NM = teNMAmbiente.Text.Validar(true);

                var posicaoTransacao = 0;
                new QAmbiente().Gravar(Ambiente, ref posicaoTransacao);

                base.Gravar();
            }
            catch (Exception excessao)
            {
                excessao.Validar();
            }
        }
    }
}
