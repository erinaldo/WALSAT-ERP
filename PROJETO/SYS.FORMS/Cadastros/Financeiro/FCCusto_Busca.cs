using SYS.QUERYS.Cadastros.Financeiro;
using System.Linq;
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
    public partial class FCCusto_Busca : SYS.FORMS.FBase_CadastroBusca
    {
        public FCCusto_Busca()
        {
            InitializeComponent();
        }

        public override void Adicionar()
        {
            base.Adicionar();

            using (var adicionar = new FCCusto_Cadastro() { Modo = Modo.Cadastrar })
            {

                if (adicionar.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    beIdentificador.Text = adicionar.CCusto.ID_CENTROCUSTO.ToString();
                    Mensagens.Sucesso();
                    Buscar();
                }
            }
        }

        public override void Alterar()
        {
            base.Alterar();

            var selecionado = gvCusto.GetSelectedRow();

            if (selecionado == null)
                Mensagens.Selecionar();
            else
            {
                var ccusto = new QCCusto().Buscar((selecionado.ID as int?).Padrao()).FirstOrDefaultDynamic();

                using (var alterar = new FCCusto_Cadastro() { CCusto = ccusto, Modo = Modo.Alterar })
                {
                    if (alterar.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        beIdentificador.Text = alterar.CCusto.ID_CENTROCUSTO.ToString();
                        Mensagens.Sucesso();
                        Buscar();
                    }
                }
            }
        }

        public override void Deletar()
        {
            base.Deletar();

            var selecionado = gvCusto.GetSelectedRow();

            if (selecionado == null)
                Mensagens.Selecionar();
            else
            {
                int ID = selecionado.ID;

                var consulta = new QCCusto();

                var ccusto = consulta.Buscar(ID).FirstOrDefaultDynamic();

                if (Mensagens.Deletar() == System.Windows.Forms.DialogResult.Yes)
                {
                    var posicaoTransacao = 0;
                    consulta.Deletar(ccusto, ref posicaoTransacao);
                    Mensagens.Deletado();
                    Buscar();
                }
            }
        }

        public override void Buscar()
        {
            base.Buscar();

            var consulta = (from a in new QCCusto().Buscar(beIdentificador.Text.ToInt32(true) ?? 0)
                            select new
                            {
                                ID = a.ID_CENTROCUSTO,
                                NM = a.NM,
                            });

            teNM.Text.Validar(true);
            if (teNM.Text.TemValor())
                consulta = consulta.Where(a => a.NM.Contains(teNM.Text));

            gcCusto.DataSource = consulta;
            gvCusto.BestFitColumns(true);
        }

        public override void Detalhar()
        {
            base.Detalhar();
        }
    }
}
