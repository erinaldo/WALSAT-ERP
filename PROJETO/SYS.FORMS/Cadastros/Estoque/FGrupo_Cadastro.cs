using DevExpress.XtraLayout.Utils;
using SYS.QUERYS;
using SYS.QUERYS.Cadastros.Estoque;
using SYS.UTILS;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;

namespace SYS.FORMS.Cadastros.Estoque
{
    public partial class FGrupo_Cadastro : SYS.FORMS.FBase_Cadastro
    {
        #region Declarações

        public TB_EST_GRUPO Grupo = null;

        #endregion

        #region Métodos

        public FGrupo_Cadastro()
        {
            InitializeComponent();
        }

        public override void Gravar()
        {
            try
            {
                Validar();

                Grupo = new TB_EST_GRUPO();
                Grupo.ID_GRUPO = teID_GRUPO.Text.ToInt32().Padrao();
                Grupo.ST_COMPLEMENTO = ceST_COMPLEMENTO.Checked;
                Grupo.ST_ALMOXARIFADO = ceST_ALMOXARIFADO.Checked;
                Grupo.ST_FRACAO = ceST_FRACAO.Checked;
                Grupo.ST_SERVICO = ceST_SERVICO.Checked;
                Grupo.NM = teNM_GRUPO.Text.Validar(false);

                if(gvCOM.DataSource != null)
                    Grupo.TB_EST_GRUPO_ADICIONAIs.AddRange(gvCOM.DataSource as BindingList<TB_EST_GRUPO_ADICIONAI>);
                if (gvSEM.DataSource != null)
                    Grupo.TB_EST_GRUPO_ADICIONAIs.AddRange(gvSEM.DataSource as BindingList<TB_EST_GRUPO_ADICIONAI>);

                var posicaoTransacao = 0;
                new QGrupo().Gravar(Grupo, ref posicaoTransacao);

                base.Gravar();
            }
            catch (Exception excessao)
            {
                excessao.Validar();
            }
        }

        #endregion

        #region Eventos

        private void FGrupo_Cadastro_Shown(object sender, EventArgs e)
        {
            try
            {
                //tcgAdicionais.Visibility = Parametros.ST_Gourmet ? LayoutVisibility.Always : LayoutVisibility.Never;

                if (Modo == Modo.Cadastrar)
                    Grupo = new TB_EST_GRUPO();
                else if (Modo == Modo.Alterar)
                {
                    if (Grupo == null)
                        Excessoes.Alterar();

                    teID_GRUPO.Text = Grupo.ID_GRUPO.ToString();
                    ceST_COMPLEMENTO.Checked = Grupo.ST_COMPLEMENTO.Padrao();
                    ceST_ALMOXARIFADO.Checked = Grupo.ST_ALMOXARIFADO.Padrao();
                    ceST_FRACAO.Checked = Grupo.ST_FRACAO.Padrao();
                    ceST_SERVICO.Checked = Grupo.ST_SERVICO.Padrao();
                    teNM_GRUPO.Text = Grupo.NM.Validar();

                    // Aba gourmet
                    if (tcgAdicionais.Visibility == LayoutVisibility.Always)
                    {
                        // Aba COM
                        if (Grupo.TB_EST_GRUPO_ADICIONAIs != null)
                            gcCOM.DataSource = (from a in Grupo.TB_EST_GRUPO_ADICIONAIs
                                                where a.TP == "C"
                                                select a).ToList();

                        // Aba SEM
                        if (Grupo.TB_EST_GRUPO_ADICIONAIs != null)
                            gcSEM.DataSource = (from a in Grupo.TB_EST_GRUPO_ADICIONAIs
                                                where a.TP == "S"
                                                select a).ToList();
                    }
                }
            }
            catch (Exception excessao)
            {
                excessao.Validar();
            }
        }

        private void beCOM_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            try
            {
                if (e.Button.Tag.ToString() == "adicionar")
                {
                    if (beCOM.Text.TemValor())
                    {
                        var existentes = gvCOM.DataSource as BindingList<TB_EST_GRUPO_ADICIONAI>;

                        if (existentes.Any(a => a.DS == beCOM.Text.Validar()))
                            throw new SYSException("O complemento já consta adicionado na lista!");

                        existentes.Add(new TB_EST_GRUPO_ADICIONAI { DS = beCOM.Text.Validar() });

                        gcCOM.DataSource = existentes;
                    }
                }
                else if (e.Button.Tag.ToString() == "remover")
                {
                    var existentes = gvCOM.DataSource as BindingList<TB_EST_GRUPO_ADICIONAI>;

                    var selecionado = gvCOM.GetSelectedRow<TB_EST_GRUPO_ADICIONAI>();

                    if (selecionado != null)
                        existentes.Remove(selecionado);

                    gcCOM.DataSource = existentes;
                }
            }
            catch (Exception excessao)
            {
                excessao.Validar();
            }
        }

        private void beSEM_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            try
            {
                if (e.Button.Tag.ToString() == "adicionar")
                {
                    if (beCOM.Text.TemValor())
                    {
                        var existentes = gvSEM.DataSource as BindingList<TB_EST_GRUPO_ADICIONAI>;

                        if (existentes.Any(a => a.DS == beSEM.Text.Validar()))
                            throw new SYSException("O complemento já consta adicionado na lista!");

                        existentes.Add(new TB_EST_GRUPO_ADICIONAI { DS = beCOM.Text.Validar() });

                        gcSEM.DataSource = existentes;
                    }
                }
                else if (e.Button.Tag.ToString() == "remover")
                {
                    var existentes = gvSEM.DataSource as BindingList<TB_EST_GRUPO_ADICIONAI>;

                    var selecionado = gvSEM.GetSelectedRow<TB_EST_GRUPO_ADICIONAI>();

                    if (selecionado != null)
                        existentes.Remove(selecionado);

                    gcSEM.DataSource = existentes;
                }
            }
            catch (Exception excessao)
            {
                excessao.Validar();
            }
        }

        #endregion
    }
}