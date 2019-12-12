using System;
using System.Linq;
using SYS.UTILS;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SYS.QUERYS.Cadastros.Configuracao;

namespace SYS.FORMS
{
    public partial class FAutenticacao : SYS.FORMS.FBase
    {
        public FAutenticacao()
        {
            InitializeComponent();

            teUsuario.Padronizar(false);
            teSenha.Padronizar(false);

            sbOK.Click += delegate
            {
                try
                {
                    if (!teUsuario.Text.Trim().TemValor())
                        throw new SYSException(Mensagens.Necessario("login do usuário!"));

                    if (!teUsuario.Text.Trim().TemValor())
                        throw new SYSException(Mensagens.Necessario("senha do usuário!"));

                    if (Parametros.BackdoorUsuario != teUsuario.Text.Trim().ToUpper())
                    {
                        var result = new QRegraEspecial().BuscarRegraEspecial(teUsuario.Text.Trim(), teSenha.Text.Trim()).ToList();

                        if (result.Count > 0)
                            if (result[0].ST_PERMITECANCELAITEMPEDIDO ?? false)
                            {
                                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                                this.Close();
                            }
                            else
                                throw new Exception("Usuário não tem permissão para cancelar item!");
                    }
                    else
                    {
                        this.DialogResult = System.Windows.Forms.DialogResult.OK;
                        this.Close();
                    }

                }
                catch (Exception execessao)
                {
                    execessao.Validar();
                }
            };

            sbCancelar.Click += delegate
            {
                this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
                this.Close();
            };

        }
    }
}