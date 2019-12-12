using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SYS.UTILS;
using SYS.QUERYS.Cadastros.Fiscal;
using System.Linq;

namespace SYS.FORMS.Cadastros.Fiscal
{
    public partial class FTributo_Busca : SYS.FORMS.FBase_CadastroBusca
    {
        public FTributo_Busca()
        {
            InitializeComponent();

            gcTributo.Padronizar();

            bbiAlterar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
        }

        public override void Adicionar()
        {
            try
            {
                base.Adicionar();

                using (var adicionar = new FTributo_Cadastro() { Modo = Modo.Cadastrar })
                {

                    if (adicionar.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        teIdentificador.Text = adicionar.Tributo.ID_TRIBUTO.ToString();
                        Mensagens.Sucesso();
                        Buscar();
                    }
                }
            }
            catch (Exception excessao)
            {
                excessao.Validar();
            }
        }

        public override void Alterar()
        {
            base.Alterar();

            //var selecionado = gvTributo.GetSelectedRow();

            //if (selecionado == null)
            //    Mensagens.Selecionar();
            //else
            //{
            //    var tributo = new QTributo().Buscar((selecionado.ID as int?).Padrao()).FirstOrDefault();

            //    using (var alterar = new FTributo_Cadastro() { Tributo = tributo, Modo = Modo.Alterar })
            //    {
            //        if (alterar.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            //        {
            //            teIdentificador.Text = alterar.Tributo.ID_TRIBUTO.ToString();
            //            Mensagens.Sucesso();
            //            Buscar();
            //        }
            //    }
            //}
        }

        public override void Deletar()
        {
            try
            {
                base.Deletar();

                var selecionado = gvTributo.GetSelectedRow();

                if (!selecionado.TemValor())
                    Mensagens.Selecionar();

                var consulta = new QTributo();
                var produto = consulta.Buscar(selecionado.ID).FirstOrDefault();

                if (Mensagens.Deletar() == System.Windows.Forms.DialogResult.OK)
                {
                    var posicaoTransacao = 0;
                    consulta.Deletar(produto, ref posicaoTransacao);
                    Mensagens.Deletado();
                    Buscar();
                }
            }
            catch (Exception excessao)
            {
                excessao.Validar();
            }
        }

        public override void Buscar()
        {
            try
            {
                base.Buscar();

                var tributo = new QTributo();

                var consulta = (from a in tributo.Buscar(teIdentificador.Text.ToInt32(true).Padrao())
                                where ceImposto.Checked ? a.TB_FIS_IMPOSTO != null : false
                                && ceTaxa.Checked ? a.TB_FIS_TAXA != null : false
                                && ceContribuicao.Checked ? a.TB_FIS_CONTRIBUICAO != null : false
                                select new
                                {
                                    ID = a.ID_TRIBUTO,
                                    NM = tributo.Nome(a),
                                    IMPOSTO = a.TB_FIS_IMPOSTO != null,
                                    TAXA = a.TB_FIS_TAXA != null,
                                    CONTRIBUICAO = a.TB_FIS_CONTRIBUICAO != null
                                });

                teNomeTributo.Text.Validar(true);
                if (teNomeTributo.Text.TemValor())
                    consulta = consulta.Where(a => a.NM.Contains(teNomeTributo.Text));

                gcTributo.DataSource = consulta;
                gvTributo.BestFitColumns(true);
            }
            catch (Exception excessao)
            {
                excessao.Validar();
            }
        }
    }
}