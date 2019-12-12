using SYS.QUERYS.Lancamentos.Financeiro;
using System.Linq;
using SYS.UTILS;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SYS.FORMS.Lancamentos.Financeiro
{
    public partial class FContas_Busca : SYS.FORMS.FBase_CadastroBusca
    {
        public FContas_Busca()
        {
            InitializeComponent();
        }

        #region Métodos

        public override void Adicionar()
        {
            base.Adicionar();

            using (var adicionar = new FContas_Cadastro() { Modo = Modo.Cadastrar })
            {

                if (adicionar.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    teIdentificador.Text = adicionar.duplicata.ID_DUPLICATA.ToString();
                    Mensagens.Sucesso();
                    Buscar();
                }
            }
        }

        public override void Alterar()
        {
            base.Alterar();

            var selecionado = gvConta.GetSelectedRow();

            if (selecionado == null)
                Mensagens.Selecionar();
            else
            {
                var duplicata = new QDuplicata().Buscar((selecionado.ID as int?).Padrao()).FirstOrDefaultDynamic();

                using (var alterar = new FContas_Cadastro() { duplicata = duplicata, Modo = Modo.Alterar })
                {
                    if (alterar.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        teIdentificador.Text = alterar.duplicata.ID_DUPLICATA.ToString();
                        Mensagens.Sucesso();
                        Buscar();
                    }
                }
            }
        }

        public override void Deletar()
        {
            base.Deletar();

            var selecionado = gvConta.GetSelectedRow();

            if (selecionado == null)
                Mensagens.Selecionar();

            int ID = selecionado.ID;

            var consulta = new QDuplicata();

            var duplicata = consulta.Buscar(ID).FirstOrDefaultDynamic();

            if (Mensagens.Deletar() == System.Windows.Forms.DialogResult.Yes)
            {
                var posicaoTransacao = 0;
                consulta.Deletar(duplicata, ref posicaoTransacao);
                Mensagens.Deletado();
                Buscar();
            }
        }

        public override void Buscar()
        {
            base.Buscar();

            var consulta = (from a in new QDuplicata().Buscar(teIdentificador.Text.ToInt32(true).Padrao())
                            select new
                            {
                                ID = a.ID_DUPLICATA,
                                ID_CLIFOR = a.ID_CLIFOR,
                                NM_CLIFOR = a.TB_REL_CLIFOR.NM
                            });

            teClifor.Text.Validar(true);
            if (teClifor.Text.TemValor())
                consulta = consulta.Where(a => a.NM_CLIFOR.Contains(teClifor.Text));

            gcConta.DataSource = consulta;
            gvConta.BestFitColumns(true);
        }

        public override void Detalhar()
        {
            base.Detalhar();
        }

        #endregion
    }
}
