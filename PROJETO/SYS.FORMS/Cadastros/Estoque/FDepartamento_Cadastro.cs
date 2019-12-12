using SYS.QUERYS;
using SYS.QUERYS.Cadastros.Estoque;
using SYS.UTILS;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SYS.FORMS.Cadastros.Estoque
{
    public partial class FDepartamento_Cadastro : SYS.FORMS.FBase_Cadastro
    {
        public TB_EST_DEPARTAMENTO Departamento = null;

        public FDepartamento_Cadastro()
        {
            InitializeComponent();
            this.Shown += delegate
            {
                try
                {

                    if (Modo == Modo.Cadastrar)
                        Departamento = new TB_EST_DEPARTAMENTO();
                    else if (Modo == Modo.Alterar)
                    {
                        if (Departamento == null)
                            Excessoes.Alterar();

                        teIdentificador.Text = Departamento.ID_DEPARTAMENTO.ToString();
                        teDescricao.Text = Departamento.NM.Validar();

                        
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

                Departamento = new TB_EST_DEPARTAMENTO();

                Departamento.ID_DEPARTAMENTO = teIdentificador.Text.ToInt32().Padrao();
                Departamento.NM = teDescricao.Text.Validar(true);

                var posicaoTransacao = 0;
                new QDepartamento().Gravar(Departamento, ref posicaoTransacao);

                base.Gravar();
            }
            catch (Exception excessao)
            {
                excessao.Validar();
            }
        }
    }
}

