using SYS.QUERYS.Cadastros.Financeiro;
using SYS.UTILS;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Linq;

namespace SYS.FORMS.Cadastros.Financeiro
{
    public partial class FCondicaoPagamento_Busca : SYS.FORMS.FBase_CadastroBusca
    {
        #region Métodos

        public FCondicaoPagamento_Busca()
        {
            InitializeComponent();
        }

        public override void Adicionar()
        {
            base.Adicionar();

            using (var adicionar = new FCondicaoPagamento_Cadastro() { Modo = Modo.Cadastrar })
            {

                if (adicionar.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    teID_CONDICAOPAGAMENTO.Text = adicionar.CondicaoPagamento.ID_CONDICAOPAGAMENTO.ToString();
                    Mensagens.Sucesso();
                    Buscar();
                }
            }
        }

        public override void Alterar()
        {
            base.Alterar();

            var selecionado = gvCondicaoPagamento.GetSelectedRow();

            if (selecionado == null)
                Mensagens.Selecionar();
            else
            {
                var condicaoPagamento = new QCondicaoPagamento().Buscar((selecionado.ID as int?).Padrao()).FirstOrDefault();

                using (var alterar = new FCondicaoPagamento_Cadastro() { CondicaoPagamento = condicaoPagamento, Modo = Modo.Alterar })
                {
                    if (alterar.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        teID_CONDICAOPAGAMENTO.Text = alterar.CondicaoPagamento.ID_CONDICAOPAGAMENTO.ToString();
                        Mensagens.Sucesso();
                        Buscar();
                    }
                }
            }
        }

        public override void Deletar()
        {
            base.Deletar();

            var selecionado = gvCondicaoPagamento.GetSelectedRow();

            if (selecionado == null)
                Mensagens.Selecionar();

            int ID = selecionado.ID;

            var consulta = new QCondicaoPagamento();

            var condicaoPagamento = consulta.Buscar(ID).FirstOrDefault();

            if (Mensagens.Deletar() == System.Windows.Forms.DialogResult.Yes)
            {
                var posicaoTransacao = 0;
                consulta.Deletar(condicaoPagamento, ref posicaoTransacao);
                Mensagens.Deletado();
                Buscar();
            }
        }

        public override void Buscar()
        {
            base.Buscar();

            var consulta = (from a in new QCondicaoPagamento().Buscar(teID_CONDICAOPAGAMENTO.Text.ToInt32(true).Padrao())
                            select new
                            {
                                ID = a.ID_CONDICAOPAGAMENTO,
                                DS = a.DS,
                                QT = a.QT_DIASDESDOBRO
                            });

            teDS.Text.Validar(true);
            if (teDS.Text.TemValor())
                consulta = consulta.Where(a => a.DS.Contains(teDS.Text));

            gcCondicaoPagamento.DataSource = consulta;
            gvCondicaoPagamento.BestFitColumns(true);
        }

        public override void Detalhar()
        {
            base.Detalhar();
        }

        #endregion
    }
}